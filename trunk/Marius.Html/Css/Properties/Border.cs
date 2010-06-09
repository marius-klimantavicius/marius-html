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
    public class Border: CssPropertyHandler
    {
        public override bool Apply(CssContext context, CssBox box, CssExpression expression)
        {
            CssValue[][] values = Parse(context, expression);
            if (values == null || !Valid(expression))
                return false;

            if (values[0] == null)
                context.BorderWidth.Apply(box, context.BorderTopWidth.Initial, context.BorderRightWidth.Initial, context.BorderBottomWidth.Initial, context.BorderLeftWidth.Initial);
            else
                context.BorderWidth.ApplyValues(box, values[0]);

            if (values[1] == null)
                context.BorderStyle.Apply(box, context.BorderTopStyle.Initial, context.BorderRightStyle.Initial, context.BorderBottomStyle.Initial, context.BorderLeftStyle.Initial);
            else
                context.BorderStyle.ApplyValues(box, values[1]);

            if (values[2] == null)
                context.BorderColor.Apply(box, context.BorderTopColor.Initial, context.BorderRightColor.Initial, context.BorderBottomColor.Initial, context.BorderLeftColor.Initial);
            else
                context.BorderColor.ApplyValues(box, values[2]);

            return true;
        }

        public override bool Validate(CssContext context, CssExpression expression)
        {
            CssValue[][] values = Parse(context, expression);
            return (values != null && Valid(expression));
        }

        protected virtual CssValue[][] Parse(CssContext context, CssExpression expression)
        {
            if (MatchInherit(expression) != null)
                return new[]{
                    new[]{ CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit},
                    new[]{ CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit},
                    new[]{ CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit}
                };

            CssValue[] values;
            CssValue[] width = null, style = null, color = null;

            bool has = true;
            for (int i = 0; i < 3 && has; i++)
            {
                has = false;

                values = context.BorderWidth.Parse(context, expression);
                if (values != null)
                {
                    if (width != null)
                        return null;

                    width = values;
                    has = true;
                }

                values = context.BorderStyle.Parse(context, expression);
                if (values != null)
                {
                    if (style != null)
                        return null;

                    style = values;
                    has = true;
                }

                values = context.BorderColor.Parse(context, expression);
                if (values != null)
                {
                    if (color != null)
                        return null;

                    color = values;
                    has = true;
                }
            }

            if (width == null && style == null && color == null)
                return null;

            return new[] { width, style, color };
        }
    }
}
