﻿#region License
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
using Marius.Html.Css.Box;

namespace Marius.Html.Css.Properties
{
    public partial class Font: CssPropertyHandler
    {
        public Font(CssContext context)
            : base(context)
        {
        }

        public override bool Apply(IWithStyle box, CssExpression expression)
        {
            CssValue[] values = Parse(expression);
            if (values == null || !Valid(expression))
                return false;

            if (values[6] == null)
            {
                box.FontStyle = values[0] ?? _context.FontStyle.Initial;
                box.FontVariant = values[1] ?? _context.FontVariant.Initial;
                box.FontWeight = values[2] ?? _context.FontWeight.Initial;
                box.FontSize = values[3];
                box.LineHeight = values[4] ?? _context.LineHeight.Initial;
                box.FontFamily = values[5] ?? _context.FontFamily.Initial;
                box.Font = null;
            }
            else
            {
                box.FontStyle = null;
                box.FontVariant = null;
                box.FontWeight = null;
                box.FontSize = null;
                box.LineHeight = null;
                box.FontFamily = null;
                box.Font = values[6];
            }
            return true;
        }

        public override bool Validate(CssExpression expression)
        {
            CssValue[] values = Parse(expression);
            return (values != null && Valid(expression));
        }

        protected virtual CssValue[] Parse(CssExpression expression)
        {
            CssValue value = null;
            if (MatchAny(expression, new[] { CssKeywords.Caption, CssKeywords.Icon, CssKeywords.Menu, CssKeywords.MessageBox, CssKeywords.SmallCaption, CssKeywords.StatusBar }, ref value))
                return new[] { null, null, null, null, null, null, value };

            if (MatchInherit(expression) != null)
                return new[] { CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit, null };

            // 	[ [ 'font-style'  || 'font-variant'  || 'font-weight'  ]? 'font-size' [ / 'line-height'  ]? 'font-family'  ] | caption | icon | menu | message-box | small-caption | status-bar | inherit
            CssValue style = null, variant = null, weight = null, size = null, family = null, height = null;

            bool has = true;
            for (int i = 0; i < 3 && has; i++)
            {
                has = false;

                value = _context.FontStyle.Parse(expression);
                if (value != null)
                {
                    if (style != null)
                        return null;

                    has = true;
                    style = value;
                }

                value = _context.FontVariant.Parse(expression);
                if (value != null)
                {
                    if (variant != null)
                        return null;

                    has = true;
                    variant = value;
                }

                value = _context.FontWeight.Parse(expression);
                if (value != null)
                {
                    if (weight != null)
                        return null;

                    has = true;
                    weight = value;
                }
            }

            size = _context.FontSize.Parse(expression);
            if (size == null)
                return null;

            if (expression.Current.ValueType == CssValueType.Slash)
            {
                expression.MoveNext();

                height = _context.LineHeight.Parse(expression);
                if (height == null)
                    return null;
            }

            family = _context.FontFamily.Parse(expression);
            if (family == null)
                return null;

            return new[] { style, variant, weight, size, height, family, null };
        }
    }
}
