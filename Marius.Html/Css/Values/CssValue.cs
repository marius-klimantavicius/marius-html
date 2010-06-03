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
using Marius.Html.Css.Dom;

namespace Marius.Html.Css.Values
{
    public abstract class CssValue: IEquatable<CssValue>
    {
        public static readonly CssIdentifier Inherit = new CssIdentifier("inherit");
        public static readonly CssIdentifier Auto = new CssIdentifier("auto");
        public static readonly CssIdentifier None = new CssIdentifier("none");
        public static readonly CssIdentifier Hidden = new CssIdentifier("hidden");
        public static readonly CssIdentifier Transparent = new CssIdentifier("transparent");

        public static readonly CssNumber Zero = new CssNumber(0);

        public abstract CssValueType ValueType { get; }
        public CssValueGroup ValueGroup
        {
            get
            {
                switch (ValueType)
                {
                    case CssValueType.Unknown:
                        return CssValueGroup.Unknown;
                    case CssValueType.Number:
                        return CssValueGroup.Number;
                    case CssValueType.Percentage:
                        return CssValueGroup.Percentage;
                    case CssValueType.Em:
                    case CssValueType.Ex:
                    case CssValueType.Px:
                    case CssValueType.Cm:
                    case CssValueType.Mm:
                    case CssValueType.In:
                    case CssValueType.Pt:
                    case CssValueType.Pc:
                        return CssValueGroup.Length;
                    case CssValueType.Deg:
                    case CssValueType.Rad:
                    case CssValueType.Grad:
                        return CssValueGroup.Angle;
                    case CssValueType.Ms:
                    case CssValueType.S:
                        return CssValueGroup.Time;
                    case CssValueType.Hz:
                    case CssValueType.KHz:
                        return CssValueGroup.Frequency;
                    case CssValueType.Dimension:
                        return CssValueGroup.Dimension;
                    case CssValueType.String:
                        return CssValueGroup.String;
                    case CssValueType.Uri:
                        return CssValueGroup.Uri;
                    case CssValueType.Identifier:
                        return CssValueGroup.Identifier;
                    case CssValueType.Color:
                        return CssValueGroup.Color;
                    case CssValueType.Rect:
                    case CssValueType.Function:
                        return CssValueGroup.Function;
                    case CssValueType.SignedDimension:
                        return CssValueGroup.SignedDimension;
                    case CssValueType.Slash:
                    case CssValueType.Comma:
                        return CssValueGroup.Operator;
                    case CssValueType.BoxColor:
                        return CssValueGroup.BoxColor;
                    case CssValueType.ValueList:
                        return CssValueGroup.ValueList;
                    case CssValueType.Null:
                        return CssValueGroup.Null;
                }
                throw new NotSupportedException();
            }
        }

        public abstract bool Equals(CssValue other);
        public abstract override int GetHashCode();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
                return false;

            return Equals((CssValue)obj);
        }

        public virtual bool IsNull()
        {
            return false;
        }
    }
}
