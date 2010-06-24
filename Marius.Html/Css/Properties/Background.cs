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
    public partial class Background: CssPropertyHandler
    {
        public Background(CssContext context)
            : base(context)
        {
        }

        public override bool Apply(IWithStyle box, CssExpression expression)
        {
            CssValue[] values = Parse(expression);
            if (values == null || !Valid(expression))
                return false;

            box.BackgroundAttachment = values[0] ?? _context.BackgroundAttachment.Initial;
            box.BackgroundColor = values[1] ?? _context.BackgroundColor.Initial;
            box.BackgroundImage = values[2] ?? _context.BackgroundImage.Initial;
            box.BackgroundPosition = values[3] ?? _context.BackgroundPosition.Initial;
            box.BackgroundRepeat = values[4] ?? _context.BackgroundRepeat.Initial;

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
                return new[] { CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit };

            bool has = true;
            CssValue value;
            CssValue attachment = null, color = null, image = null, position = null, repeat = null;
            for (int i = 0; i < 5 && has; i++)
            {
                has = false;

                value = _context.BackgroundAttachment.Parse(expression);
                if (value != null)
                {
                    if (attachment != null)
                        return null;

                    has = true;
                    attachment = value;
                }

                value = _context.BackgroundColor.Parse(expression);
                if (value != null)
                {
                    if (color != null)
                        return null;

                    has = true;
                    color = value;
                }

                value = _context.BackgroundImage.Parse(expression);
                if (value != null)
                {
                    if (image != null)
                        return null;

                    has = true;
                    image = value;
                }

                value = _context.BackgroundPosition.Parse(expression);
                if (value != null)
                {
                    if (position != null)
                        return null;

                    has = true;
                    position = value;
                }

                value = _context.BackgroundRepeat.Parse(expression);
                if (value != null)
                {
                    if (repeat != null)
                        return null;

                    has = true;
                    repeat = value;
                }
            }

            if (attachment == null && color == null && image == null && position == null && repeat == null)
                return null;

            return new[] { attachment, color, image, position, repeat };
        }
    }
}
