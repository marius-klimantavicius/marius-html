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
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Properties
{
    public partial class Outline: CssPropertyHandler
    {
        public Outline(CssContext context)
            : base(context)
        {
        }

        public override bool Apply(CssBox box, CssExpression expression)
        {
            CssValue[] values = Parse(expression);
            if (values == null || !Valid(expression))
                return false;

            box.OutlineColor = values[0] ?? _context.OutlineColor.Initial;
            box.OutlineStyle = values[1] ?? _context.OutlineStyle.Initial;
            box.OutlineWidth = values[2] ?? _context.OutlineWidth.Initial;

            return true;
        }

        public override bool Validate(CssExpression expression)
        {
            CssValue[] values = Parse(expression);
            return (values != null && Valid(expression));
        }

        protected virtual CssValue[] Parse(CssExpression expression)
        {
            if (MatchInherit(expression) != null)
                return new[] { CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit };

            CssValue value = null;
            CssValue color = null, style = null, width = null;
            bool has = true;

            for (int i = 0; i < 3 && has; i++)
            {
                has = false;

                value = _context.OutlineColor.Parse(expression);
                if (value != null)
                {
                    if (color != null)
                        return null;

                    has = true;
                    color = value;
                }

                value = _context.OutlineStyle.Parse(expression);
                if (value != null)
                {
                    if (style != null)
                        return null;

                    has = true;
                    style = value;
                }

                value = _context.OutlineWidth.Parse(expression);
                if (value != null)
                {
                    if (width != null)
                        return null;

                    has = true;
                    width = value;
                }
            }

            if (color == null && style == null && width == null)
                return null;

            return new[] { color, style, width };
        }
    }
}
