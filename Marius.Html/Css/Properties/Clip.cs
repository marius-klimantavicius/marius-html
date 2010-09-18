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
using Marius.Html.Css.Values;
using Marius.Html.Css.Box;

namespace Marius.Html.Css.Properties
{
    public partial class Clip: CssSimplePropertyHandler
    {
        public override bool IsInherited
        {
            get { return false; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.Auto; }
        }

        public Clip(CssContext context)
            : base(context)
        {
        }

        public override CssValue Parse(CssExpression expression)
        {
            CssValue result = null;
            if (MatchShape(expression, ref result))
                return result;

            if (Match(expression, CssKeywords.Auto))
                return CssKeywords.Auto;

            return MatchInherit(expression);
        }

        protected override CssValue PostCompute(CssBox box, CssValue computed)
        {
            if (computed.ValueType == CssValueType.Rect)
            {
                CssRect rect = (CssRect)computed;
                CssValue top = null, left = null, bottom = null, right = null;

                if (rect.Top.ValueType == CssValueType.Em || rect.Top.ValueType == CssValueType.Ex)
                    top = RelativeToAbsoluteLength(box, rect.Top);

                if (rect.Left.ValueType == CssValueType.Em || rect.Left.ValueType == CssValueType.Ex)
                    left = RelativeToAbsoluteLength(box, rect.Left);

                if (rect.Bottom.ValueType == CssValueType.Em || rect.Bottom.ValueType == CssValueType.Ex)
                    bottom = RelativeToAbsoluteLength(box, rect.Bottom);

                if (rect.Right.ValueType == CssValueType.Em || rect.Right.ValueType == CssValueType.Ex)
                    right = RelativeToAbsoluteLength(box, rect.Right);

                if (top == null && left == null && bottom == null && right == null)
                    return computed;

                return new CssRect(top, left, bottom, right);
            }

            return computed;
        }
    }
}
