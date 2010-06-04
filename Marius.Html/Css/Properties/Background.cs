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
    public class Background: CssPropertyStrategy
    {
        public override bool IsInherited
        {
            get { return false; }
        }

        public override CssValue Initial
        {
            get { throw new NotSupportedException(); } // should I thrown or should I null
        }

        public override bool Apply(CssContext context, CssBox box, CssExpression expression, bool full)
        {
            CssValue inherit = MatchInherit(expression);
            if (inherit != null)
            {
                if (full && !expression.Current.IsNull())
                    return false;

                box.BackgroundAttachment = CssKeywords.Inherit;
                box.BackgroundColor = CssKeywords.Inherit;
                box.BackgroundImage = CssKeywords.Inherit;
                box.BackgroundPosition= CssKeywords.Inherit;
                box.BackgroundRepeat = CssKeywords.Inherit;
                return true;
            }

            bool has = true;
            CssValue value;
            CssValue attachment = null, color = null, image = null, position = null, repeat = null;
            for (int i = 0; i < 5 && has; i++)
            {
                has = false;

                value = context.BackgroundAttachment.Parse(context, expression);
                if (value != null)
                {
                    if (attachment != null)
                        return false;

                    has = true;
                    attachment = value;
                }

                value = context.BackgroundColor.Parse(context, expression);
                if (value != null)
                {
                    if (color != null)
                        return false;

                    has = true;
                    color = value;
                }

                value = context.BackgroundImage.Parse(context, expression);
                if (value != null)
                {
                    if (image != null)
                        return false;

                    has = true;
                    image = value;
                }

                value = context.BackgroundPosition.Parse(context, expression);
                if (value != null)
                {
                    if (position != null)
                        return false;

                    has = true;
                    position = value;
                }

                value = context.BackgroundRepeat.Parse(context, expression);
                if (value != null)
                {
                    if (repeat != null)
                        return false;

                    has = true;
                    repeat = value;
                }
            }

            if (attachment == null && color == null && image == null && position == null && repeat == null)
                return false;

            if (full && !expression.Current.IsNull())
                return false;

            box.BackgroundAttachment = attachment ?? context.BackgroundAttachment.Initial;
            box.BackgroundColor = color ?? context.BackgroundColor.Initial;
            box.BackgroundImage = image ?? context.BackgroundImage.Initial;
            box.BackgroundPosition = position ?? context.BackgroundPosition.Initial;
            box.BackgroundRepeat = repeat ?? context.BackgroundRepeat.Initial;

            return true;
        }
    }
}
