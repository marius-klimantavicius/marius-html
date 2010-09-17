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
using Marius.Html.Css.Values;
using Marius.Html.Css.Box;

namespace Marius.Html.Css.Properties
{
    public partial class FontSize: CssSimplePropertyHandler
    {
        public override bool IsInherited
        {
            get { return true; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.Medium; }
        }

        public FontSize(CssContext context)
            : base(context)
        {
        }

        public override CssValue Parse(CssExpression expression)
        {
            CssValue result = null;

            if (MatchAny(expression, new[] { CssKeywords.XXSmall, CssKeywords.XSmall, CssKeywords.Small, CssKeywords.Medium, CssKeywords.Large, CssKeywords.XLarge, CssKeywords.XXLarge }, ref result))
                return result;

            if (MatchAny(expression, new[] { CssKeywords.Larger, CssKeywords.Smaller }, ref result))
                return result;

            if (MatchLength(expression, ref result))
                return result;

            if (MatchPercentage(expression, ref result))
                return result;

            return MatchInherit(expression);
        }

        protected override CssValue PostCompute(CssBox box, CssValue computed)
        {
            var result = computed;
            if (computed.ValueGroup == CssValueGroup.Identifier)
            {
                if (CssKeywords.Larger.Equals(computed) || CssKeywords.Smaller.Equals(computed))
                {
                    var value = Inherited(box);
                    if (CssKeywords.Larger.Equals(computed))
                        result = _context.LargerFontSize(value);

                    if (CssKeywords.Smaller.Equals(computed))
                        result = _context.SmallerFontSize(value);
                }
                else if (CssKeywords.XXSmall.Equals(computed))
                    result = _context.TranslateFontSize(CssFontSize.XXSmall);
                else if (CssKeywords.XSmall.Equals(computed))
                    result = _context.TranslateFontSize(CssFontSize.XSmall);
                else if (CssKeywords.Small.Equals(computed))
                    result = _context.TranslateFontSize(CssFontSize.Small);
                else if (CssKeywords.Medium.Equals(computed))
                    result = _context.TranslateFontSize(CssFontSize.Medium);
                else if (CssKeywords.Large.Equals(computed))
                    result = _context.TranslateFontSize(CssFontSize.Large);
                else if (CssKeywords.XLarge.Equals(computed))
                    result = _context.TranslateFontSize(CssFontSize.XLarge);
                else if (CssKeywords.XXLarge.Equals(computed))
                    result = _context.TranslateFontSize(CssFontSize.XXLarge);
            }
            else if (computed.ValueType == CssValueType.Em)
            {
                var size = Inherited(box);
                if (size.ValueGroup != CssValueGroup.Length)
                    throw new CssInvalidStateException();

                CssLength baseSize = (CssLength)size;
                CssLength computedSize = (CssLength)computed;

                if (baseSize.Units == CssUnits.Em || baseSize.Units == CssUnits.Ex)
                    throw new CssInvalidStateException();

                result = new CssLength(baseSize.Value * computedSize.Value, baseSize.Units);
            }
            else if (computed.ValueType == CssValueType.Ex)
            {
                var size = _context.FontSize.Inherited(box);
                var family = _context.FontFamily.Inherited(box);
                var variant = _context.FontVariant.Inherited(box);
                var weight = _context.FontWeight.Inherited(box);
                var style = _context.FontStyle.Inherited(box);

                var baseSize = _context.FontXHeight(size, family, variant, weight, style);
                var computedSize = (CssLength)computed;

                result = new CssLength(baseSize.Value * computedSize.Value, baseSize.Units); 
            }

            if (result.ValueGroup != CssValueGroup.Length)
                throw new CssInvalidStateException();   // we cannot simply return null, as computed value must always be present, in this case throw an error

            if (result.ValueType == CssValueType.Em || result.ValueType == CssValueType.Ex)
                throw new CssInvalidStateException();

            return result;
        }
    }
}
