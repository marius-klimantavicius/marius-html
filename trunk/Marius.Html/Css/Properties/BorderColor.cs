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
    public class BorderColor: CssProperty
    {
        public static ParseFunc<BorderColor> Parse;

        public BorderSideColor Top { get; private set; }
        public BorderSideColor Right { get; private set; }
        public BorderSideColor Bottom { get; private set; }
        public BorderSideColor Left { get; private set; }

        static BorderColor()
        {
            ParseFunc<BorderColor> func1 = (e, c) =>
                {
                    if (BorderSideColor.Parse(e, c.Top))
                    {
                        c.Right = c.Bottom = c.Left = c.Top;
                        return true;
                    }
                    return false;
                };

            ParseFunc<BorderColor> func2 = (e, c) =>
                {
                    if (BorderSideColor.Parse(e, c.Right))
                    {
                        c.Left = c.Right;
                        return true;
                    }
                    return false;
                };

            ParseFunc<BorderColor> func3 = (e, c) => BorderSideColor.Parse(e, c.Bottom);
            ParseFunc<BorderColor> func4 = (e, c) => BorderSideColor.Parse(e, c.Left);

            // not sure what to do in border-color: inherit inherit; - should this rule be disarded?
            // FF and others discard the rule
            var inherit = CssPropertyParser.Match<BorderColor>(CssKeywords.Inherit, (s, c) => c.Top = c.Right = c.Bottom = c.Left = new BorderSideColor(s));

            Parse = CssPropertyParser.Any(inherit, CssPropertyParser.FourSequence<BorderColor>(func1, func2, func3, func4));
        }

        public BorderColor()
            : this(new BorderSideColor(), new BorderSideColor(), new BorderSideColor(), new BorderSideColor())
        {
        }

        public BorderColor(CssValue all)
            : this(new BorderSideColor(all), new BorderSideColor(all), new BorderSideColor(all), new BorderSideColor(all))
        {
        }

        public BorderColor(BorderSideColor top, BorderSideColor right, BorderSideColor bottom, BorderSideColor left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public static BorderColor Create(CssExpression expression, bool full = true)
        {
            BorderColor result = new BorderColor();
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
