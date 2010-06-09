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
using System.Text;
using Marius.Html.Css.Selectors;

namespace Marius.Html.Css
{
    public class CssSelectorMatcher
    {
        public virtual bool IsMatch(CssSelector selector, CssBox box)
        {
            switch (selector.SelectorType)
            {
                case CssSelectorType.Child:
                    return IsChildMatch((CssChildSelector)selector, box);
                case CssSelectorType.Descendant:
                    return IsDescendantMatch((CssDescendantSelector)selector, box);
                case CssSelectorType.Sibling:
                    return IsSiblingMatch((CssSiblingSelector)selector, box);
                case CssSelectorType.Universal:
                    return IsUniversalMatch((CssUniversalSelector)selector, box);
                case CssSelectorType.Conditional:
                    return IsConditionalMatch((CssConditionalSelector)selector, box);
                case CssSelectorType.Element:
                    return IsElementMatch((CssElementSelector)selector, box);
                case CssSelectorType.InlineStyle:
                    return IsStyleMatch((CssInlineStyleSelector)selector, box);
            }
            return false;
        }

        private bool IsStyleMatch(CssInlineStyleSelector selector, CssBox box)
        {
            return selector.Element.Equals(box.Element);
        }

        protected virtual bool IsElementMatch(CssElementSelector selector, CssBox box)
        {
            if (box.Element == null)
                return false;

            return selector.Name.Equals(box.Element.Name, StringComparison.InvariantCultureIgnoreCase);
        }

        protected virtual bool IsConditionalMatch(CssConditionalSelector selector, CssBox box)
        {
            if (!IsMatch(selector.SimpleSelector, box))
                return false;

            return IsConditionSatisfied(selector.Condition, box);
        }

        protected virtual bool IsConditionSatisfied(CssCondition condition, CssBox box)
        {
            switch (condition.ConditionType)
            {
                case CssConditionType.Attribute:
                    return IsAttributeConditionSatisfied((CssAttributeCondition)condition, box);
                case CssConditionType.BeginHyphenAttribute:
                    return IsBeginHyphenConditionSatisfied((CssBeginHyphenAttributeCondition)condition, box);
                case CssConditionType.IncludesAttribute:
                    return IsIncludesConditionSatisfied((CssIncludesAttributeCondition)condition, box);
                case CssConditionType.Id:
                    break;
                case CssConditionType.Class:
                    break;
                case CssConditionType.And:
                    break;
                case CssConditionType.PseudoElement:
                    break;
                case CssConditionType.PseudoClass:
                    break;
                default:
                    break;
            }

            return false;
        }

        private bool IsIncludesConditionSatisfied(CssIncludesAttributeCondition condition, CssBox box)
        {
            if (box.Element == null)
                return false;

            string value = AttributeValue(condition, box);

            string[] items = value.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < items.Length; i++)
            {
                if (items[i].Equals(condition.Value, StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }

            return false;
        }

        private bool IsBeginHyphenConditionSatisfied(CssBeginHyphenAttributeCondition condition, CssBox box)
        {
            if (box.Element == null)
                return false;

            string value = AttributeValue(condition, box);

            if (value.Contains('-'))
                value = value.Substring(0, value.IndexOf('-'));

            return value.Equals(condition.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        protected virtual bool IsAttributeConditionSatisfied(CssAttributeCondition condition, CssBox box)
        {
            if (box.Element == null)
                return false;

            string attributeValue = AttributeValue(condition, box);

            if (attributeValue == null)
                return false;

            if (!condition.IsSpecified)
                return true;

            return attributeValue.Equals(condition.Value, StringComparison.InvariantCultureIgnoreCase);
        }

        private static string AttributeValue(CssAttributeCondition condition, CssBox box)
        {
            string attributeValue = null;
            switch (condition.Attribute.ToUpperInvariant())
            {
                case "ID":
                    attributeValue = box.Element.Id;
                    break;
                case "CLASS":
                    attributeValue = box.Element.Class;
                    break;
                default:
                    if (box.Element.Attributes.ContainsKey(attributeValue))
                        attributeValue = box.Element.Attributes[condition.Attribute];
                    break;
            }

            return attributeValue;
        }

        protected virtual bool IsUniversalMatch(CssUniversalSelector selector, CssBox box)
        {
            if (box.Element == null)
                return false;

            return true;
        }

        protected virtual bool IsSiblingMatch(CssSiblingSelector selector, CssBox box)
        {
            if (!IsMatch(selector.Selector, box))
                return false;

            var parent = box.Parent;
            if (parent == null)
                return false;

            var child = parent.FirstChild;
            if (child == null)
                throw new CssInvalidStateException("child has parent, but parent does not have children");

            while (child != null)
            {
                if (object.ReferenceEquals(child.NextSibling, box))
                    return IsMatch(selector.SiblingSelector, child);
            }

            return false;
        }

        protected virtual bool IsDescendantMatch(CssDescendantSelector selector, CssBox box)
        {
            if (!IsMatch(selector.Selector, box))
                return false;

            var parent = box.Parent;
            while (parent != null)
            {
                if (IsMatch(selector.AncestorSelector, box))
                    return true;

                parent = parent.Parent;
            }

            return false;
        }

        protected virtual bool IsChildMatch(CssChildSelector selector, CssBox box)
        {
            if (!IsMatch(selector.Selector, box))
                return false;

            var parent = box.Parent;
            if (parent == null)
                return false;

            return IsMatch(selector.AncestorSelector, box);
        }
    }
}
