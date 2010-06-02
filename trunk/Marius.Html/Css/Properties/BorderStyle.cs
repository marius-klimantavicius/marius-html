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
    public class BorderStyle: CssProperty
    {
        public static readonly Func<CssExpression, BorderStyle, bool> Parse;

        public static readonly CssIdentifier Dotted = new CssIdentifier("dotted");
        public static readonly CssIdentifier Dashed = new CssIdentifier("dashed");
        public static readonly CssIdentifier Solid = new CssIdentifier("solid");
        public static readonly CssIdentifier Double = new CssIdentifier("double");
        public static readonly CssIdentifier Groove = new CssIdentifier("groove");
        public static readonly CssIdentifier Ridge = new CssIdentifier("rigde");
        public static readonly CssIdentifier Inset = new CssIdentifier("inset");
        public static readonly CssIdentifier Outset = new CssIdentifier("outset");

        public static readonly CssIdentifier[] Keywords = new[] { CssValue.None, CssValue.Hidden, Dotted, Dashed, Solid, Double, Groove, Ridge, Inset, Outset };

        public BorderSideStyle Top { get; private set; }
        public BorderSideStyle Right { get; private set; }
        public BorderSideStyle Bottom { get; private set; }
        public BorderSideStyle Left { get; private set; }

        static BorderStyle()
        {
            Func<CssExpression, BorderStyle, bool> func1 = (e, c) =>
                {
                    if (BorderSideStyle.Parse(e, c.Top))
                    {
                        c.Right = c.Bottom = c.Left = c.Top;
                        return true;
                    }
                    return false;
                };

            Func<CssExpression, BorderStyle, bool> func2 = (e, c) =>
                {
                    if (BorderSideStyle.Parse(e, c.Right))
                    {
                        c.Left = c.Right;
                        return true;
                    }
                    return false;
                };

            Func<CssExpression, BorderStyle, bool> func3 = (e, c) => BorderSideStyle.Parse(e, c.Bottom);
            Func<CssExpression, BorderStyle, bool> func4 = (e, c) => BorderSideStyle.Parse(e, c.Left);

            Func<CssExpression, BorderStyle, bool> inherit = CssPropertyParser.Match<BorderStyle>(CssValue.Inherit, (s, c) => c.Top = c.Right = c.Bottom = c.Left = new BorderSideStyle(s));

            Parse = CssPropertyParser.Any(CssPropertyParser.FourSequence(func1, func2, func3, func4), inherit);
        }

        public BorderStyle()
            : this(new BorderSideStyle(), new BorderSideStyle(), new BorderSideStyle(), new BorderSideStyle())
        {
        }

        public BorderStyle(BorderSideStyle top, BorderSideStyle right, BorderSideStyle bottom, BorderSideStyle left)
        {
            Top = top;
            Right = right;
            Bottom = bottom;
            Left = left;
        }

        public static BorderStyle Create(CssExpression expression, bool full = true)
        {
            BorderStyle result = new BorderStyle();
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
