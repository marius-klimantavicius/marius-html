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
    public class CssSignedDimension: CssValue
    {
        public CssValue Dimension { get; private set; }
        public CssSignOperator Sign { get; private set; }

        public sealed override CssValueType ValueType
        {
            get { return CssValueType.SignedDimension; }
        }

        public CssSignedDimension(CssValue dimension, CssSignOperator sign)
        {
            switch (dimension.ValueType)
            {
                case CssValueType.Em:
                case CssValueType.Ex:
                case CssValueType.Px:
                case CssValueType.Cm:
                case CssValueType.Mm:
                case CssValueType.In:
                case CssValueType.Pt:
                case CssValueType.Pc:
                case CssValueType.Deg:
                case CssValueType.Rad:
                case CssValueType.Grad:
                case CssValueType.Ms:
                case CssValueType.S:
                case CssValueType.Hz:
                case CssValueType.KHz:
                case CssValueType.Dimension:
                    break;
                default:
                    throw new ArgumentException();
            }
            Dimension = dimension;
            Sign = sign;
        }

        public override bool Equals(CssValue other)
        {
            CssSignedDimension o = other as CssSignedDimension;
            if (o == null)
                return false;
            return o.Sign == this.Sign && o.Dimension.Equals(this.Dimension);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(Sign, Dimension, ValueType);
        }
    }
}
