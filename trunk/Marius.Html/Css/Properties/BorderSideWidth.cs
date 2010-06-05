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
    public class BorderSideWidth: BorderSideStrategy
    {
        public CssBorderSide Side { get; private set; }

        public override bool IsInherited
        {
            get { return false; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.Medium; }
        }

        public BorderSideWidth(CssBorderSide side)
        {
            Side = side;
        }

        public override bool Apply(CssContext context, CssBox box, CssExpression expression, bool full)
        {
            CssValue value = Parse(context, expression);
            if (value == null || !Valid(expression, full))
                return false;

            switch (Side)
            {
                case CssBorderSide.Top:
                    box.BorderTopWidth = value;
                    break;
                case CssBorderSide.Right:
                    box.BorderRightWidth = value;
                    break;
                case CssBorderSide.Bottom:
                    box.BorderBottomWidth = value;
                    break;
                case CssBorderSide.Left:
                    box.BorderLeftWidth = value;
                    break;
                default:
                    throw new NotSupportedException();
            }

            return true;
        }

        public override CssValue Parse(CssContext context, CssExpression expression)
        {
            CssValue result = null;
            if (MatchAny(expression, new[] { CssKeywords.Thin, CssKeywords.Thick, CssKeywords.Medium }, ref result))
                return result;

            if (MatchLength(expression, ref result))
                return result;

            return MatchInherit(expression);
        }
    }
}
