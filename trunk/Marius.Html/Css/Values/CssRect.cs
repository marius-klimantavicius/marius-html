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

namespace Marius.Html.Css.Values
{
    public class CssRect: CssValue
    {
        public CssValue Top { get; private set; }
        public CssValue Right { get; private set; }
        public CssValue Bottom { get; private set; }
        public CssValue Left { get; private set; }

        public override CssValueType ValueType
        {
            get { return CssValueType.Rect; }
        }

        public CssRect(CssValue top, CssValue right, CssValue bottom, CssValue left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public override bool Equals(CssValue other)
        {
            CssRect o = other as CssRect;
            if (o == null)
                return false;

            return o.Top.Equals(this.Top) && o.Right.Equals(this.Right) && o.Bottom.Equals(this.Bottom) && o.Left.Equals(this.Left);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(Top, Right, Bottom, Left, ValueType);
        }

        public override string ToString()
        {
            return string.Format("rect({0}, {1}, {2}, {3})", Top, Right, Bottom, Left);
        }
    }
}
