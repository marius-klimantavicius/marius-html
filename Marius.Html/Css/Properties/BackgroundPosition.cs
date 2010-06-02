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
    public class BackgroundPosition: CssProperty
    {
        public static readonly Func<CssExpression, BackgroundPosition, bool> Parse;

        public static readonly CssIdentifier Left = new CssIdentifier("left");
        public static readonly CssIdentifier Right = new CssIdentifier("right");
        public static readonly CssIdentifier Center = new CssIdentifier("center");
        public static readonly CssIdentifier Top = new CssIdentifier("top");
        public static readonly CssIdentifier Bottom = new CssIdentifier("bottom");

        public static readonly CssPercentage Zero = new CssPercentage(0);
        public static readonly CssPercentage Fifty = new CssPercentage(50);
        public static readonly CssPercentage Hundred = new CssPercentage(100);

        public CssValue Horizontal { get; private set; }
        public CssValue Vertical { get; private set; }

        static BackgroundPosition()
        {
            /* 	
             * [ 
             *      [ <percentage>  | <length>  | left | center | right ] [ <percentage>  | <length>  | top | center | bottom ]? 
             * ] | 
             * [ 
             *      [ left | center | right ] || [ top | center | bottom ] 
             * ] | 
             *      inherit
             */

            var percFirst = CssPropertyParser.Any(
                CssPropertyParser.Percentage<BackgroundPosition>((s, c) => c.Horizontal = s),
                CssPropertyParser.Length<BackgroundPosition>((s, c) => c.Horizontal = s),
                CssPropertyParser.Match<BackgroundPosition>(Left, (s, c) => c.Horizontal = s),
                CssPropertyParser.Match<BackgroundPosition>(Right, (s, c) => c.Horizontal = s));

            var percSecond = CssPropertyParser.Any(
                CssPropertyParser.Percentage<BackgroundPosition>((s, c) => c.Vertical = s),
                CssPropertyParser.Length<BackgroundPosition>((s, c) => c.Vertical = s),
                CssPropertyParser.Match<BackgroundPosition>(Top, (s, c) => c.Vertical = s),
                CssPropertyParser.Match<BackgroundPosition>(Center, (s, c) => c.Vertical = s),
                CssPropertyParser.Match<BackgroundPosition>(Bottom, (s, c) => c.Vertical = s));

            var horiz = CssPropertyParser.Any<BackgroundPosition>(new[] { Left, Center, Right }, (s, c) => c.Horizontal = s);
            var vert = CssPropertyParser.Any<BackgroundPosition>(new[] { Top, Center, Bottom }, (s, c) => c.Vertical = s);

            Func<CssExpression, BackgroundPosition, bool> center = (expression, context) =>
                {
                    if (CssPropertyParser.Match(expression, Center, context, (s, c) => { }))
                    {
                        if (percSecond(expression, context))
                        {
                            context.Horizontal = Center;
                            return true;
                        }
                        if (horiz(expression, context))
                        {
                            context.Vertical = Center;
                            return true;
                        }
                        if (vert(expression, context))
                        {
                            context.Horizontal = Center;
                            return true;
                        }

                        context.Horizontal = Center;
                        context.Vertical = Center;
                        return true;
                    }
                    return false;
                };

            Parse = (expression, context) =>
                {
                    if (percFirst(expression, context))
                    {
                        if (!percSecond(expression, context))
                            context.Vertical = Center;
                        return true;
                    }

                    if (center(expression, context))
                        return true;

                    if (vert(expression, context))
                    {
                        if (!horiz(expression, context))
                            context.Horizontal = Center;
                        return true;
                    }

                    if (CssPropertyParser.Match(expression, CssValue.Inherit, context, (s, c) => { c.Horizontal = s; c.Vertical = s; }))
                        return true;

                    return false;
                };
        }

        public BackgroundPosition()
            : this(Zero, Zero)
        {
        }

        public BackgroundPosition(CssValue horizontal, CssValue vertical)
        {
            Horizontal = horizontal;
            Vertical = vertical;
        }

        public static BackgroundPosition Create(CssExpression expression, bool full = true)
        {
            BackgroundPosition result = new BackgroundPosition();
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
