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
    public class BorderSideColor: CssProperty
    {
        public static ParseFunc<BorderSideColor> Parse;

        public CssValue Color { get; private set; }

        static BorderSideColor()
        {
            var color = CssPropertyParser.ColorWithTransparent<BorderSideColor>((s, c) => c.Color = s);
            Parse = CssPropertyParser.Any(color, CssPropertyParser.Match<BorderSideColor>(CssKeywords.Inherit, (s, c) => c.Color = s));
        }

        public BorderSideColor()
            : this(CssBoxColor.Instance)
        {
        }

        public BorderSideColor(CssValue color)
        {
            Color = color;
        }

        public static BorderSideColor Create(CssExpression expression, bool full = true)
        {
            BorderSideColor result = new BorderSideColor();
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
