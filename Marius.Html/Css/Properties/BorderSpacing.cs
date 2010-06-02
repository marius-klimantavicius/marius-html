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
    public class BorderSpacing: CssProperty
    {
        public static readonly Func<CssExpression, BorderSpacing, bool> Parse;

        public CssValue Horizontal { get; private set; }
        public CssValue Vertical { get; private set; }

        static BorderSpacing()
        {
            var length = CssPropertyParser.Sequence(
                CssPropertyParser.Length<BorderSpacing>((s, c) => c.Horizontal = c.Vertical = s),
                CssPropertyParser.Maybe(CssPropertyParser.Length<BorderSpacing>((s, c) => c.Vertical = s))
                );

            Parse = CssPropertyParser.Any(length, CssPropertyParser.Match<BorderSpacing>(CssValue.Inherit, (s, c) => c.Horizontal = c.Vertical = s));
        }

        public BorderSpacing()
            : this(CssValue.Zero, CssValue.Zero)
        {
        }

        public BorderSpacing(CssValue horizontal, CssValue vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }

        public static BorderSpacing Create(CssExpression expression, bool full = true)
        {
            BorderSpacing result = new BorderSpacing();
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
