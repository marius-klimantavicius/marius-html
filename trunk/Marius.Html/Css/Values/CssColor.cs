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
using System.Text.RegularExpressions;

namespace Marius.Html.Css.Values
{
    public abstract class CssColor: CssValue
    {
        public CssValue Red { get; protected set; }
        public CssValue Green { get; protected set; }
        public CssValue Blue { get; protected set; }

        public CssColor()
        {
        }

        public sealed override CssValueType ValueType
        {
            get { return CssValueType.Color; }
        }

        public override bool Equals(CssValue other)
        {
            CssColor o = other as CssColor;
            if (o == null)
                return false;
            return o.Red.Equals(this.Red) && o.Green.Equals(this.Green) && o.Blue.Equals(this.Blue);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(Red, Green, Blue);
        }
    }

    public class CssColors
    {
        public static readonly CssIdentifier Aqua = new CssIdentifier("aqua");
        public static readonly CssIdentifier Black = new CssIdentifier("black");
        public static readonly CssIdentifier Blue = new CssIdentifier("blue");
        public static readonly CssIdentifier Fuchsia = new CssIdentifier("fuchsia");
        public static readonly CssIdentifier Gray = new CssIdentifier("gray");
        public static readonly CssIdentifier Green = new CssIdentifier("green");
        public static readonly CssIdentifier Lime = new CssIdentifier("lime");
        public static readonly CssIdentifier Maroon = new CssIdentifier("maroon");
        public static readonly CssIdentifier Navy = new CssIdentifier("navy");
        public static readonly CssIdentifier Olive = new CssIdentifier("olive");
        public static readonly CssIdentifier Orange = new CssIdentifier("orange");
        public static readonly CssIdentifier Purple = new CssIdentifier("purple");
        public static readonly CssIdentifier Red = new CssIdentifier("red");
        public static readonly CssIdentifier Silver = new CssIdentifier("silver");
        public static readonly CssIdentifier Teal = new CssIdentifier("teal");
        public static readonly CssIdentifier White = new CssIdentifier("white");
        public static readonly CssIdentifier Yellow = new CssIdentifier("yellow");

        public static readonly CssIdentifier[] Colors = new CssIdentifier[]
        {
            Aqua, Black, Blue, Fuchsia, Gray, Green, Lime, Maroon, Navy, Olive, Orange, Purple, Red, Silver, Teal, White, Yellow 
        };
    }
}
