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
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Properties
{
    public partial class BackgroundPosition: CssSimplePropertyHandler
    {
        public override bool IsInherited
        {
            get { return false; }
        }

        public override CssValue Initial
        {
            get { return new CssBackgroundPosition(CssPercentage.Zero, CssPercentage.Zero); }
        }

        public BackgroundPosition(CssContext context)
            : base(context)
        {
        }

        public override CssValue Parse(CssExpression expression)
        {
            /* 	
             * [ 
             *      [ <percentage>  | <length>  | left | center | right ] [ <percentage>  | <length>  | top | center | bottom ]? 
             * ] | 
             * [ 
             *      [ left | center | right ] || [ top | center | bottom ] 
             * ] | 
             *      inherit
             */
            CssValue result = null;

            if (MatchInherit(expression) != null)
                return CssKeywords.Inherit;

            CssValue v = CssKeywords.Center, h = CssKeywords.Center;
            if (expression.Current.ValueGroup == CssValueGroup.Percentage || expression.Current.ValueGroup == CssValueGroup.Length)
            {
                h = expression.Current;
                expression.MoveNext();

                VerticalPosition(expression, ref v);
                return new CssBackgroundPosition(v, h);
            }
            else if (MatchAny(expression, new[] { CssKeywords.Left, CssKeywords.Right }, ref result))
            {
                h = result;
                VerticalPosition(expression, ref v);

                return new CssBackgroundPosition(v, h);
            }
            else if (MatchAny(expression, new[] { CssKeywords.Top, CssKeywords.Bottom }, ref result))
            {
                v = result;
                MatchAny(expression, new[] { CssKeywords.Left, CssKeywords.Center, CssKeywords.Right }, ref h);

                return new CssBackgroundPosition(v, h);
            }
            else if (Match(expression, CssKeywords.Center))
            {
                if (VerticalPosition(expression, ref v))
                    h = result;
                else if (MatchAny(expression, new[] { CssKeywords.Left, CssKeywords.Center, CssKeywords.Right }, ref h))
                    v = result;
                else
                {
                    Match(expression, CssKeywords.Center); // can be center, but it does not matter, we ignore value, but we must eat this token
                    h = v = CssKeywords.Center;
                }
                return new CssBackgroundPosition(v, h);
            }

            return null;
        }

        private bool VerticalPosition(CssExpression expression, ref CssValue v)
        {
            if (expression.Current.ValueGroup == CssValueGroup.Percentage || expression.Current.ValueGroup == CssValueGroup.Length)
            {
                v = expression.Current;
                expression.MoveNext();
                return true;
            }
            else
            {
                return MatchAny(expression, new[] { CssKeywords.Top, CssKeywords.Center, CssKeywords.Bottom }, ref v); // ignore value - optional
            }
        }

        public override CssValue Compute(Box.CssBox box)
        {
            var specified = GetValue(box.Properties);

            if (specified == null)
                return base.Compute(box);

            if (specified.ValueType == CssValueType.BackgroundPosition)
            {
                CssBackgroundPosition value = (CssBackgroundPosition)specified;

                CssValue v, h;
                CssValue vresult, hresult;

                vresult = v = value.Vertical;
                hresult = h = value.Horizontal;

                if (v.ValueGroup == CssValueGroup.Identifier)
                {
                    if (CssKeywords.Top.Equals(v))
                        vresult = CssPercentage.Zero;
                    else if (CssKeywords.Center.Equals(v))
                        vresult = CssPercentage.Fifty;
                    else if (CssKeywords.Bottom.Equals(v))
                        vresult = CssPercentage.Hundred;
                    else
                        throw new CssInvalidStateException();
                }

                if (h.ValueGroup == CssValueGroup.Identifier)
                {
                    if (CssKeywords.Left.Equals(h))
                        hresult = CssPercentage.Zero;
                    else if (CssKeywords.Center.Equals(h))
                        hresult = CssPercentage.Fifty;
                    else if (CssKeywords.Right.Equals(h))
                        hresult = CssPercentage.Hundred;
                    else
                        throw new CssInvalidStateException();
                }

                return new CssBackgroundPosition(v, h);
            }

            return base.Compute(box);
        }
    }
}
