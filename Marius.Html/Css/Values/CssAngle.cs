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
using Marius.Html.Internal;

namespace Marius.Html.Css.Values
{
    public class CssAngle: CssValue
    {
        public CssUnits Units { get; private set; }
        public double Value { get; private set; }

        public sealed override CssValueType ValueType
        {
            get
            {
                switch (Units)
                {
                    case CssUnits.Deg:
                        return CssValueType.Deg;
                    case CssUnits.Rad:
                        return CssValueType.Rad;
                    case CssUnits.Grad:
                        return CssValueType.Grad;
                }
                throw new CssInvalidStateException();
            }
        }

        public CssAngle(double value, CssUnits units)
        {
            Value = value;
            Units = units;
        }

        public override bool Equals(CssValue other)
        {
            CssAngle o = other as CssAngle;
            if (o == null)
                return false;

            return o.Units == this.Units && o.Value == this.Value;
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(Units, Value);
        }
    }
}
