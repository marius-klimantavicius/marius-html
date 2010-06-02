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
    public class BorderWidth: CssProperty
    {
        public static readonly ParseFunc<BorderWidth> Parse;

        public static readonly CssIdentifier Thin = new CssIdentifier("thin");
        public static readonly CssIdentifier Medium = new CssIdentifier("medium");
        public static readonly CssIdentifier Thick = new CssIdentifier("thick");
        public static readonly CssIdentifier[] Keywords = new[] { Thin, Medium, Thick };

        public BorderSideWidth Top { get; private set; }
        public BorderSideWidth Right { get; private set; }
        public BorderSideWidth Bottom { get; private set; }
        public BorderSideWidth Left { get; private set; }

        static BorderWidth()
        {
            ParseFunc<BorderWidth> func1 = (e, c) =>
                {
                    if (BorderSideWidth.Parse(e, c.Top))
                    {
                        c.Right = c.Bottom = c.Left = c.Top;
                        return true;
                    }
                    return false;
                };

            ParseFunc<BorderWidth> func2 = (e, c) =>
                {
                    if (BorderSideWidth.Parse(e, c.Right))
                    {
                        c.Left = c.Right;
                    }
                    return false;
                };

            ParseFunc<BorderWidth> func3 = (e, c) => BorderSideWidth.Parse(e, c.Bottom);
            ParseFunc<BorderWidth> func4 = (e, c) => BorderSideWidth.Parse(e, c.Left);

            ParseFunc<BorderWidth> inherit = CssPropertyParser.Match<BorderWidth>(CssValue.Inherit, (s, c) => c.Top = c.Right = c.Bottom = c.Left = new BorderSideWidth(s));

            Parse = CssPropertyParser.Any(inherit, CssPropertyParser.FourSequence(func1, func2, func3, func4));
        }

        public BorderWidth()
            : this(new BorderSideWidth(), new BorderSideWidth(), new BorderSideWidth(), new BorderSideWidth())
        {
        }

        public BorderWidth(CssValue all)
            : this(new BorderSideWidth(all), new BorderSideWidth(all), new BorderSideWidth(all), new BorderSideWidth(all))
        {

        }

        public BorderWidth(BorderSideWidth top, BorderSideWidth right, BorderSideWidth bottom, BorderSideWidth left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public static BorderWidth Create(CssExpression expression, bool full = true)
        {
            BorderWidth result = new BorderWidth();
            if (Parse(expression, result))
            {
                if (full && expression.Current != null)
                    return null;

                return result;
            }
            return null;
        }
    }
}
