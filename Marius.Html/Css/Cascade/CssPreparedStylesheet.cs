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
using Marius.Html.Dom;
using Marius.Html.Css.Parser;

namespace Marius.Html.Css.Cascade
{
    public enum CssStyleTarget
    {
        None,
        Style,
        FirstLine,
        FirstLetter,
        Before,
        After,
    }

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

        public virtual void Apply(INode box)
        {
            // TODO: add inline style and maybe node attribute support
            for (int i = 0; i < _styles.Length; i++)
                Apply(_styles[i], box);
        }

        protected virtual void Apply(CssPreparedStyle style, INode box)
        {
            CssStyleTarget target = Applies(style.Selector, box);
            switch (target)
            {
                case CssStyleTarget.None:
                    break;
                case CssStyleTarget.Style:
                    ApplyDeclarations(style.Declarations, box.Style);
                    break;
                case CssStyleTarget.FirstLine:
                    ApplyDeclarations(style.Declarations, box.FirstLineStyle);
                    break;
                case CssStyleTarget.FirstLetter:
                    ApplyDeclarations(style.Declarations, box.FirstLetterStyle);
                    break;
                case CssStyleTarget.Before:
                    ApplyDeclarations(style.Declarations, box.BeforeStyle);
                    break;
                case CssStyleTarget.After:
                    ApplyDeclarations(style.Declarations, box.AfterStyle);
                    break;
                default:
                    throw new CssInvalidStateException();
            }
        }

        protected virtual void ApplyDeclarations(IList<CssDeclaration> list, IWithStyle box)
        {
            for (int i = 0; i < list.Count; i++)
            {
                var decl = list[i];
                decl.Value.Reset();
                _context.Properties[decl.Property].Apply(box, decl.Value);
            }
        }

        public virtual IList<CssDeclaration> GetAplicableDeclarations(INode box)
        {
            // TODO: make selector responsible for selecting
            List<CssDeclaration> result = new List<CssDeclaration>();
            for (int i = 0; i < _styles.Length; i++)
            {
                if (Applies(_styles[i].Selector, box) != CssStyleTarget.None)
                    result.AddRange(_styles[i].Declarations);
            }

            return result;
        }

        protected virtual CssStyleTarget Applies(CssSelector selector, INode box)
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
                    return box.NodeType == NodeType.Element || box.NodeType == NodeType.Image ? CssStyleTarget.Style : CssStyleTarget.None;
                case CssSelectorType.Conditional:
                    return AppliesConditional((CssConditionalSelector)selector, box);
                case CssSelectorType.Element:
                    return AppliesElement((CssElementSelector)selector, box);
                case CssSelectorType.InlineStyle:
                    return AppliesInlineStyle((CssInlineElementSelector)selector, box);
            }
            throw new CssInvalidStateException();
        }

        private CssStyleTarget AppliesInlineStyle(CssInlineElementSelector selector, INode box)
        {
            if (box == selector.Element)
                return CssStyleTarget.Style;

            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget AppliesConditional(CssConditionalSelector selector, INode box)
        {
            var target = Applies(selector.SimpleSelector, box);
            if (target == CssStyleTarget.None)
                return CssStyleTarget.None;

            var prev = target;
            var conditions = selector.Conditions;
            for (int i = 0; i < conditions.Length; i++)
            {
                target = MatchesCondition(conditions[i], box);

                if (target == CssStyleTarget.None)
                    return CssStyleTarget.None;

                if (prev != CssStyleTarget.Style && target != CssStyleTarget.Style)
                    return CssStyleTarget.None;

                if (target != CssStyleTarget.Style)
                    prev = target;
            }

            return prev;
        }

        protected virtual CssStyleTarget MatchesCondition(CssCondition condition, INode box)
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

        protected virtual CssStyleTarget MatchesPseudoClass(CssPseudoClassCondition condition, INode box)
        {
            // TODO: currently pseudo elements and classes are not implemented
            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget MatchesPseudoElement(CssPseudoElementCondition condition, INode box)
        {
            if (box.NodeType != NodeType.Element)
                return CssStyleTarget.None;

            if (_context.PseudoConditionFactory.IsFirstLine(condition))
                return CssStyleTarget.FirstLine;

            if (_context.PseudoConditionFactory.IsFirstLetter(condition))
                return CssStyleTarget.FirstLetter;

            if (_context.PseudoConditionFactory.IsAfter(condition))
                return CssStyleTarget.After;

            if (_context.PseudoConditionFactory.IsBefore(condition))
                return CssStyleTarget.Before;

            // TODO: unknown pseudo element - need to think of a way to provide custom pseudo handlers (that would select how and when to apply style)
            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget MatchesClass(CssClassCondition condition, INode box)
        {
            if (condition.Value == null)
                throw new CssInvalidStateException();   // this must not happen

            if (box.NodeType != NodeType.Element)
                return CssStyleTarget.None;

            var element = (IElementNode)box;

            if (StringComparer.InvariantCultureIgnoreCase.Equals(element.Attributes.Class, condition.Value))
                return CssStyleTarget.Style;

            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget MatchesId(CssIdCondition condition, INode box)
        {
            if (condition.Value == null)
                throw new CssInvalidStateException();   // this must not happen

            if (box.NodeType != NodeType.Element)
                return CssStyleTarget.None;

            var element = (IElementNode)box;

            if (StringComparer.InvariantCultureIgnoreCase.Equals(element.Id, condition.Value))
                return CssStyleTarget.Style;

            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget MatchesIncludesAttribute(CssIncludesAttributeCondition condition, INode box)
        {
            if (condition.Value == null)
                throw new CssInvalidStateException();   // this must not happen

            if (box.NodeType != NodeType.Element)
                return CssStyleTarget.None;

            var element = (IElementNode)box;
            if (element.Attributes.Contains(condition.Attribute))
            {
                var values = element.Attributes[condition.Attribute].Split(); // TODO: specify split chars, as currently it is separated by whitespace, which might be a violation of spec
                for (int i = 0; i < values.Length; i++)
                {
                    if (StringComparer.InvariantCultureIgnoreCase.Equals(values[i], condition.Value))
                        return CssStyleTarget.Style;
                }
            }

            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget MatchesBeginHypenAttribute(CssBeginHyphenAttributeCondition condition, INode box)
        {
            if (condition.Value == null)
                throw new CssInvalidStateException();   // this must not happen

            if (box.NodeType != NodeType.Element)
                return CssStyleTarget.None;

            var element = (IElementNode)box;
            if (element.Attributes.Contains(condition.Attribute))
            {
                var value = element.Attributes[condition.Attribute];
                if (value.Contains('-'))
                    value = value.Substring(0, value.IndexOf('-'));

                if (StringComparer.InvariantCultureIgnoreCase.Equals(value, condition.Value))
                    return CssStyleTarget.Style;
            }

            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget MatchesAttribute(CssAttributeCondition condition, INode box)
        {
            if (box.NodeType != NodeType.Element)
                return CssStyleTarget.None;

            var element = (IElementNode)box;
            if (element.Attributes.Contains(condition.Attribute))
            {
                if (condition.Value == null)
                    return CssStyleTarget.Style;
                if (StringComparer.InvariantCultureIgnoreCase.Equals(element.Attributes[condition.Attribute], condition.Value))
                    return CssStyleTarget.Style;
            }

            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget AppliesDescendant(CssDescendantSelector selector, INode box)
        {
            var target = Applies(selector.Selector, box);
            if (target == CssStyleTarget.None)
                return CssStyleTarget.None;

            var parent = box.Parent;
            while (parent != null)
            {
                var prev = Applies(selector.AncestorSelector, parent);
                if (prev == CssStyleTarget.Style)
                    return target;
                else if (prev != CssStyleTarget.None) // the pseudo-element selector MUST be the last simple selector
                    return CssStyleTarget.None;
                parent = parent.Parent;
            }
            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget AppliesSibling(CssSiblingSelector selector, INode box)
        {
            var parent = box.Parent;
            if (parent == null)
                return CssStyleTarget.None;

            var target = Applies(selector.Selector, box);
            if (target == CssStyleTarget.None)
                return CssStyleTarget.None;

            var sibling = box.PreviousSibling;
            if (sibling == null)
                return CssStyleTarget.None;

            var prev = Applies(selector.SiblingSelector, sibling);
            if (prev == CssStyleTarget.Style)
                return target;

            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget AppliesChild(CssChildSelector selector, INode box)
        {
            var target = Applies(selector.Selector, box);
            if (target == CssStyleTarget.None)
                return CssStyleTarget.None;

            var parent = box.Parent;
            if (parent == null)
                return CssStyleTarget.None;

            var prev = Applies(selector.AncestorSelector, parent);
            if (prev == CssStyleTarget.Style)
                return target;

            return CssStyleTarget.None;
        }

        protected virtual CssStyleTarget AppliesElement(CssElementSelector selector, INode box)
        {
            if (box.NodeType != NodeType.Element)
                return CssStyleTarget.None;

            var element = (IElementNode)box;

            if (StringComparer.InvariantCultureIgnoreCase.Equals(element.TagName, selector.Name))
                return CssStyleTarget.Style;

            return CssStyleTarget.None;
        }
    }
}
