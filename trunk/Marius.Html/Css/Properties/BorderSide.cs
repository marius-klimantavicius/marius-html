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
    public class BorderSide: CssProperty
    {
        public static readonly ParseFunc<BorderSide> Parse;

        public BorderSideColor Color { get; private set; }
        public BorderSideStyle Style { get; private set; }
        public BorderSideWidth Width { get; private set; }

        static BorderSide()
        {
            // [ <border-width>  || <border-style>  || 'border-top-color'?? should be border-*-color  ] | inherit
            ParseFunc<BorderSide> width = (e, c) => BorderSideWidth.Parse(e, c.Width);
            ParseFunc<BorderSide> style = (e, c) => BorderSideStyle.Parse(e, c.Style);
            ParseFunc<BorderSide> color = (e, c) => BorderSideColor.Parse(e, c.Color);

            ParseFunc<BorderSide> inherit = CssPropertyParser.Match<BorderSide>(CssValue.Inherit, (s, c) => { c.Width = new BorderSideWidth(s); c.Style = new BorderSideStyle(s); c.Color = new BorderSideColor(s); });

            Parse = CssPropertyParser.Any(inherit, CssPropertyParser.Pipe(width, style, color));
        }

        public BorderSide()
            : this(new BorderSideColor(), new BorderSideStyle(), new BorderSideWidth())
        {
        }

        public BorderSide(BorderSideColor color, BorderSideStyle style, BorderSideWidth width)
        {
            Color = color;
            Style = style;
            Width = width;
        }

        public static BorderSide Create(CssExpression expression, bool full = true)
        {
            BorderSide result = new BorderSide();
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
