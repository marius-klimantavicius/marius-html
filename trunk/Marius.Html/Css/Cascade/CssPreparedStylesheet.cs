#region License
/*
Distributed under the terms of a MIT-style license:

The MIT License

Copyright (c) 2010 Marius Klimantavičius

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using Marius.Html.Internal;
using Marius.Html.Css.Dom;
using System.Net;
using System.IO;
using Marius.Html.Css.Selectors;

namespace Marius.Html.Css.Cascade
{
    public class CssPreparedStylesheet
    {
        private CssContext _context;
        private CssPreparedStyle[] _styles;

        public CssPreparedStylesheet(CssContext context, IEnumerable<CssPreparedStyle> styles)
        {
            _context = context;
            _styles = styles.ToArray();

            Array.Sort<CssPreparedStyle>(_styles, _context.StyleComparer);
        }

        public virtual IList<CssDeclaration> GetAplicableDeclarations(CssBox box)
        {
            List<CssDeclaration> result = new List<CssDeclaration>();
            for (int i = 0; i < _styles.Length; i++)
            {
                if (Applies(_styles[i].Selector, box))
                    result.AddRange(_styles[i].Declarations);
            }

            return result;
        }

        protected virtual bool Applies(CssSelector selector, CssBox box)
        {
            switch (selector.SelectorType)
            {
                case CssSelectorType.Child:
                    return AppliesChild((CssChildSelector)selector, box);
                case CssSelectorType.Descendant:
                    return AppliesDescendant((CssDescendantSelector)selector, box);
                case CssSelectorType.Sibling:
                    return AppliesSibling((CssSiblingSelector)selector, box);
                case CssSelectorType.Universal:
                    return true;
                case CssSelectorType.Conditional:
                    return AppliesConditional((CssConditionalSelector)selector, box);
                case CssSelectorType.Element:
                    return AppliesElement((CssElementSelector)selector, box);
                case CssSelectorType.InlineStyle:
                    return AppliesInlineStyle((CssInlineStyleSelector)selector, box);
            }
            throw new CssInvalidStateException();
        }

        private bool AppliesInlineStyle(CssInlineStyleSelector selector, CssBox box)
        {
            if (box.Element == selector.Element)
                return true;

            return false;
        }

        protected virtual bool AppliesConditional(CssConditionalSelector selector, CssBox box)
        {
            if (!Applies(selector.SimpleSelector, box))
                return false;

            var conditions = selector.Conditions;
            for (int i = 0; i < conditions.Length; i++)
            {
                if (!MatchesCondition(conditions[i], box))
                    return false;
            }

            return true;
        }

        protected virtual bool MatchesCondition(CssCondition condition, CssBox box)
        {
            switch (condition.ConditionType)
            {
                case CssConditionType.Attribute:
                    return MatchesAttribute((CssAttributeCondition)condition, box);
                case CssConditionType.BeginHyphenAttribute:
                    return MatchesBeginHypenAttribute((CssBeginHyphenAttributeCondition)condition, box);
                case CssConditionType.IncludesAttribute:
                    return MatchesIncludesAttribute((CssIncludesAttributeCondition)condition, box);
                case CssConditionType.Id:
                    return MatchesId((CssIdCondition)condition, box);
                case CssConditionType.Class:
                    return MatchesClass((CssClassCondition)condition, box);
                case CssConditionType.PseudoElement:
                    return MatchesPseudoElement((CssPseudoElementCondition)condition, box);
                case CssConditionType.PseudoClass:
                    return MatchesPseudoClass((CssPseudoClassCondition)condition, box);
            }

            throw new CssInvalidStateException();
        }

        protected virtual bool MatchesPseudoClass(CssPseudoClassCondition condition, CssBox box)
        {
            // currently pseudo elements and classes are not implemented
            return false;
        }

        protected virtual bool MatchesPseudoElement(CssPseudoElementCondition condition, CssBox box)
        {
            // currently pseudo elements and classes are not implemented
            return false;
        }

        protected virtual bool MatchesClass(CssClassCondition condition, CssBox box)
        {
            if (box.Element == null)
                return false;

            var element = box.Element;

            if (condition.Value == null)
                return false; // should not happen, maybe throw an exception?

            return StringComparer.InvariantCultureIgnoreCase.Equals(element.Class, condition.Value);
        }

        protected virtual bool MatchesId(CssIdCondition condition, CssBox box)
        {
            if (box.Element == null)
                return false;

            var element = box.Element;

            if (condition.Value == null)
                return false; // should not happen, maybe throw an exception?

            return StringComparer.InvariantCultureIgnoreCase.Equals(element.Id, condition.Value);
        }

        protected virtual bool MatchesIncludesAttribute(CssIncludesAttributeCondition condition, CssBox box)
        {
            if (box.Element == null)
                return false;

            var element = box.Element;
            if (element.Attributes.ContainsAttribute(condition.Attribute))
            {
                if (condition.Value == null)
                    return false; // should not happen, maybe throw an exception?

                var values = element.Attributes[condition.Attribute].Split(); // TODO: specify split chars, as currently it is separated by whitespace, which might be a violation of spec
                for (int i = 0; i < values.Length; i++)
                {
                    if (StringComparer.InvariantCultureIgnoreCase.Equals(values[i], condition.Value))
                        return true;
                }

                return false;
            }

            return false;
        }

        protected virtual bool MatchesBeginHypenAttribute(CssBeginHyphenAttributeCondition condition, CssBox box)
        {
            if (box.Element == null)
                return false;

            var element = box.Element;
            if (element.Attributes.ContainsAttribute(condition.Attribute))
            {
                if (condition.Value == null)
                    return false; // should not happen, maybe throw an exception?

                var value = element.Attributes[condition.Attribute];
                if (value.Contains('-'))
                    value = value.Substring(0, value.IndexOf('-'));

                return StringComparer.InvariantCultureIgnoreCase.Equals(value, condition.Value);
            }

            return false;
        }

        protected virtual bool MatchesAttribute(CssAttributeCondition condition, CssBox box)
        {
            if (box.Element == null)
                return false;

            var element = box.Element;
            if (element.Attributes.ContainsAttribute(condition.Attribute))
            {
                if (condition.Value == null)
                    return true;
                return StringComparer.InvariantCultureIgnoreCase.Equals(element.Attributes[condition.Attribute], condition.Value);
            }

            return false;
        }

        protected virtual bool AppliesDescendant(CssDescendantSelector selector, CssBox box)
        {
            if (!Applies(selector.Selector, box))
                return false;

            var parent = box.Parent;
            while (parent != null)
            {
                if (Applies(selector.AncestorSelector, parent))
                    return true;
                parent = parent.Parent;
            }
            return false;
        }

        protected virtual bool AppliesSibling(CssSiblingSelector selector, CssBox box)
        {
            var parent = box.Parent;
            if (parent == null)
                return false;

            if (!Applies(selector.Selector, box))
                return false;

            var child = parent.FirstChild;
            while (child.NextSibling != null)
            {
                if (child.NextSibling == box)
                    return Applies(selector.SiblingSelector, child);

                child = child.NextSibling;
            }

            throw new CssInvalidStateException();
        }

        protected virtual bool AppliesChild(CssChildSelector selector, CssBox box)
        {
            if (!Applies(selector.Selector, box))
                return false;

            var parent = box.Parent;
            if (parent == null)
                return false;

            return Applies(selector.AncestorSelector, parent);
        }

        protected virtual bool AppliesElement(CssElementSelector selector, CssBox box)
        {
            if (box.Element == null)
                return false;

            var element = box.Element;

            return StringComparer.InvariantCultureIgnoreCase.Equals(element.Name, selector.Name);
        }
    }
}
