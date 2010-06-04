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
    public class Border: CssProperty
    {
        public static readonly ParseFunc<Border> Parse;

        public BorderWidth Width { get; private set; }
        public BorderStyle Style { get; private set; }
        public BorderColor Color { get; private set; }

        static Border()
        {
            ParseFunc<Border> width = (e, c) => BorderWidth.Parse(e, c.Width);
            ParseFunc<Border> style = (e, c) => BorderStyle.Parse(e, c.Style);
            ParseFunc<Border> color = (e, c) => BorderColor.Parse(e, c.Color);

            ParseFunc<Border> inherit = CssPropertyParser.Match<Border>(CssKeywords.Inherit, (s, c) => { c.Width = new BorderWidth(s); c.Style = new BorderStyle(s); c.Color = new BorderColor(s); });

            Parse = CssPropertyParser.Any(inherit, CssPropertyParser.Pipe(width, style, color));
        }

        public Border()
            : this(new BorderWidth(), new BorderStyle(), new BorderColor())
        {
        }

        public Border(BorderWidth width, BorderStyle style, BorderColor color)
        {
            Width = width;
            Style = style;
            Color = color;
        }

        public static Border Create(CssExpression expression, bool full = true)
        {
            Border result = new Border();
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
