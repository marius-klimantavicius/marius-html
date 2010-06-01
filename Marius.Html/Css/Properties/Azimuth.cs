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
using System.Text;
using System.Linq;
using Marius.Html.Css.Dom;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Properties
{
    public class Azimuth: CssProperty
    {
        public static readonly Func<CssExpression, Azimuth, bool> Parse;

        public static readonly CssIdentifier[] Keywords = new string[] { "left-side", "far-left", "left", "center-left", "center", "center-right", "right", "far-right", "right-side" }.Select(s => new CssIdentifier(s)).ToArray();
        public static readonly CssIdentifier Center = Keywords[4];

        public static readonly CssIdentifier Leftwards = new CssIdentifier("leftwards");
        public static readonly CssIdentifier Rightwards = new CssIdentifier("rightwards");
        public static readonly CssIdentifier Behind = new CssIdentifier("behind");

        static Azimuth()
        {
            // <angle> | [[ left-side | far-left | left | center-left | center | center-right | right | far-right | right-side ] || behind ] | leftwards | rightwards | inherit

            var side = CssPropertyParser.Any<Azimuth>(Keywords, (s, c) => c.Location = s);
            var behind = CssPropertyParser.Match<Azimuth>(Behind, (s, c) => c.IsBehind = true);
            var sideBehind = CssPropertyParser.Pipe<Azimuth>(side, behind);
            var angle = CssPropertyParser.Angle<Azimuth>((s, c) => c.Location = s);
            var other = CssPropertyParser.Any<Azimuth>(new[] { Leftwards, Rightwards, CssValue.Inherit }, (s, c) => c.Location = s);
            var rule = CssPropertyParser.Any<Azimuth>(angle, sideBehind, other);

            Parse = rule;
        }

        public CssValue Location { get; private set; }
        public bool IsBehind { get; private set; }

        public Azimuth()
            : this(Center, false)
        {
        }

        public Azimuth(CssValue location, bool isBehind)
        {
            Location = location;
            IsBehind = isBehind;
        }

        public static Azimuth Create(CssExpression value, bool full = true)
        {
            Azimuth result = new Azimuth();
            if (Parse(value, result))
            {
                if (full && value.Current != null)
                    return null;

                if (result.Location == null)
                    result.Location = Center;

                return result;
            }
            return null;
        }
    }
}
