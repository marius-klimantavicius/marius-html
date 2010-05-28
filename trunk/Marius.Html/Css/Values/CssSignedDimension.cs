#region License
/*
Distributed under the terms of an MIT-style license:

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
    public class CssSignedDimension: CssPrimitiveValue
    {
        public CssPrimitiveValue Dimension { get; private set; }
        public CssSignOperator Sign { get; private set; }

        public sealed override CssPrimitiveValueType PrimitiveValueType
        {
            get { return CssPrimitiveValueType.SignedDimension; }
        }

        public CssSignedDimension(CssPrimitiveValue dimension, CssSignOperator sign)
        {
            switch (dimension.PrimitiveValueType)
            {
                case CssPrimitiveValueType.Ems:
                case CssPrimitiveValueType.Exs:
                case CssPrimitiveValueType.Px:
                case CssPrimitiveValueType.Cm:
                case CssPrimitiveValueType.Mm:
                case CssPrimitiveValueType.In:
                case CssPrimitiveValueType.Pt:
                case CssPrimitiveValueType.Pc:
                case CssPrimitiveValueType.Deg:
                case CssPrimitiveValueType.Rad:
                case CssPrimitiveValueType.Grad:
                case CssPrimitiveValueType.Ms:
                case CssPrimitiveValueType.S:
                case CssPrimitiveValueType.Hz:
                case CssPrimitiveValueType.KHz:
                case CssPrimitiveValueType.Dimension:
                    break;
                default:
                    throw new ArgumentException();
            }
            Dimension = dimension;
            Sign = sign;
        }

        public override bool Equals(Dom.CssValue other)
        {
            CssSignedDimension o = other as CssSignedDimension;
            if (o == null)
                return false;
            return o.Sign == this.Sign && o.Dimension.Equals(this.Dimension);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(Sign, Dimension, PrimitiveValueType);
        }
    }
}
