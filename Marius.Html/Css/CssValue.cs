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

namespace Marius.Html.Css
{
    public abstract class CssValue
    {
        public static readonly CssValue Inherit = CssInheritValue.Instance;

        public abstract CssValueType ValueType { get; }
    }

    public enum CssValueType
    {
        Inherit,
        PrimitiveValue,
        ValueList,
        Custom,
    }

    public class CssPrimitiveValue: CssValue
    {
        public sealed override CssValueType ValueType
        {
            get { return CssValueType.PrimitiveValue; }
        }

        public virtual float FloatValue { get { throw new NotSupportedException(); } set { throw new NotSupportedException(); } }
        public virtual string StringValue { get { throw new NotSupportedException(); } set { throw new NotSupportedException(); } }
        public virtual Counter CounterValue { get { throw new NotSupportedException(); } set { throw new NotSupportedException(); } }
        public virtual Rect RectValue { get { throw new NotSupportedException(); } set { throw new NotSupportedException(); } }
        public virtual RgbColor RgbColorValue { get { throw new NotSupportedException(); } set { throw new NotSupportedException(); } }
    }

    public class CssIdentifier: CssPrimitiveValue
    {
        public string Name { get; set; }

        public override string StringValue
        {
            get { return Name; }
            set { Name = value; }
        }
    }

    public class CssValueList: CssValue
    {
        private CssValue[] _values;

        public sealed override CssValueType ValueType
        {
            get { return CssValueType.ValueList; }
        }

        public int Length { get { return _values.Length; } }
        public CssValue this[int index]
        {
            get
            {
                if (index >= _values.Length)
                    return null;
                return _values[index];
            }
        }
    }

    public class CssInheritValue: CssValue
    {
        private static CssInheritValue _instance;
        public static CssInheritValue Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new CssInheritValue();
                return _instance;
            }
        }

        private CssInheritValue()
        {
        }

        public sealed override CssValueType ValueType
        {
            get { return CssValueType.Inherit; }
        }
    }

    public class RgbColor
    {
        public CssPrimitiveValue Red { get; private set; }
        public CssPrimitiveValue Green { get; private set; }
        public CssPrimitiveValue Blue { get; private set; }
    }

    public class Counter
    {
        public string Identifier { get; private set; }
        public string ListStyle { get; private set; }
        public string Separator { get; private set; }
    }

    public class Rect
    {
        public CssPrimitiveValue Top { get; private set; }
        public CssPrimitiveValue Right { get; private set; }
        public CssPrimitiveValue Bottom { get; private set; }
        public CssPrimitiveValue Left { get; private set; }
    }

    public enum CssPrimitiveValueType
    {
        Unknown,
        Number,
        Percentage,
        Ems,
        Exs,
        Px,
        Cm,
        Mm,
        In,
        Pt,
        Pc,
        Deg,
        Rad,
        Grad,
        Ms,
        S,
        Hz,
        Khz,
        Dimension,
        String,
        Uri,
        Ident,
        Attr,
        Counter,
        Rect,
        Rgbcolor,
    }
}
