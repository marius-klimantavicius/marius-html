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
    public class Font: CssPropertyHandler
    {
        public override bool IsInherited
        {
            get { return true; }
        }

        public override CssValue Initial
        {
            get { throw new NotSupportedException(); }
        }

        public override bool Apply(CssContext context, CssBox box, CssExpression expression, bool full)
        {
            CssValue value = null;
            if (MatchAny(expression, new[] { CssKeywords.Caption, CssKeywords.Icon, CssKeywords.Menu, CssKeywords.MessageBox, CssKeywords.SmallCaption, CssKeywords.StatusBar }, ref value))
            {
                if (!Valid(expression, full))
                    return false;

                box.FontFamily = null;
                box.FontSize = null;
                box.FontStyle = null;
                box.FontVariant = null;
                box.FontWeight = null;

                box.Font = value;
                return true;
            }

            if (MatchInherit(expression) != null)
            {
                if (!Valid(expression, full))
                    return false;

                box.FontFamily = CssKeywords.Inherit;
                box.FontSize = CssKeywords.Inherit;
                box.FontStyle = CssKeywords.Inherit;
                box.FontVariant = CssKeywords.Inherit;
                box.FontWeight = CssKeywords.Inherit;

                box.Font = null;
                return true;
            }

            // 	[ [ 'font-style'  || 'font-variant'  || 'font-weight'  ]? 'font-size' [ / 'line-height'  ]? 'font-family'  ] | caption | icon | menu | message-box | small-caption | status-bar | inherit
            CssValue style = null, variant = null, weight = null, size = null, family = null, height = null;

            bool has = true;
            for (int i = 0; i < 3 && has; i++)
            {
                has = false;

                value = context.FontStyle.Parse(context, expression);
                if (value != null)
                {
                    if (style != null)
                        return false;

                    has = true;
                    style = value;
                }

                value = context.FontVariant.Parse(context, expression);
                if (value != null)
                {
                    if (variant != null)
                        return false;

                    has = true;
                    variant = value;
                }

                value = context.FontWeight.Parse(context, expression);
                if (value != null)
                {
                    if (weight != null)
                        return false;

                    has = true;
                    weight = value;
                }
            }

            size = context.FontSize.Parse(context, expression);
            if (size == null)
                return false;

            if (expression.Current.ValueType == CssValueType.Slash)
            {
                expression.MoveNext();

                height = context.LineHeight.Parse(context, expression);
                if (height == null)
                    return false;
            }

            family = context.FontFamily.Parse(context, expression);
            if (family == null)
                return false;

            box.Font = null;

            box.FontStyle = style ?? context.FontStyle.Initial;
            box.FontVariant = variant ?? context.FontVariant.Initial;
            box.FontWeight = weight ?? context.FontWeight.Initial;
            box.FontSize = size;
            box.LineHeight = height ?? context.LineHeight.Initial;
            box.FontFamily = family ?? context.FontFamily.Initial;

            return true;
        }
    }
}
