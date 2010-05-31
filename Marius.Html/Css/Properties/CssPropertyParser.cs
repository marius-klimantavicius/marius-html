#region License
/*
Distributed under the terms of an MIT-style license:

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
    public static class CssPropertyParser
    {
        public static Func<CssExpression, T, bool> Any<T>(CssIdentifier[] keywords, Action<CssValue, T> onMatch)
        {
            return (expression, context) =>
            {
                for (int i = 0; i < keywords.Length; i++)
                {
                    if (Match(expression, keywords[i], context, onMatch))
                        return true;
                }
                return false;
            };
        }

        public static Func<CssExpression, T, bool> Any<T>(params Func<CssExpression, T, bool>[] matches)
        {
            return (expression, context) =>
            {
                for (int i = 0; i < matches.Length; i++)
                {
                    if (matches[i](expression, context))
                        return true;
                }
                return false;
            };
        }

        public static Func<CssExpression, T, bool> Pipe<T>(params Func<CssExpression, T, bool>[] matches)
        {
            return (expression, context) =>
            {
                int[] counter = new int[matches.Length];

                bool has = true;
                for (int i = 0; i < matches.Length && has; i++)
                {
                    has = false;

                    for (int j = 0; j < matches.Length; j++)
                    {
                        // MAYBE: if (counter[j] == 0 && matches[j](expression)) // to ensure that it is called only once
                        if (matches[j](expression, context))
                        {
                            counter[j]++;
                            has = true;
                        }
                    }
                }

                has = false;
                for (int i = 0; i < counter.Length; i++)
                {
                    if (counter[i] > 1)
                        return false;
                    else if (counter[i] == 1)
                        has = true;
                }

                return has;
            };
        }

        public static Func<CssExpression, T, bool> Match<T>(CssIdentifier key, Action<CssValue, T> onMatch)
        {
            return (expression, context) =>
            {
                return Match(expression, key, context, onMatch);
            };
        }

        public static Func<CssExpression, T, bool> Maybe<T>(Func<CssExpression, T, bool> match)
        {
            return (expression, context) =>
                {
                    match(expression, context);
                    return true;
                };
        }

        public static Func<CssExpression, T, bool> Sequence<T>(params Func<CssExpression, T, bool>[] matches)
        {
            return (expression, context) =>
                {
                    for (int i = 0; i < matches.Length; i++)
                    {
                        if (!matches[i](expression, context))
                            return false;
                    }
                    return true;
                };
        }

        public static bool Match<T>(CssExpression expression, CssIdentifier value, T context, Action<CssValue, T> onMatch)
        {
            var item = expression.Current;
            if (item == null)
                return false;

            if (item.ValueGroup != CssValueGroup.Identifier)
                return false;

            if (item.Equals(value))
            {
                expression.MoveNext();
                onMatch(item, context);
                return true;
            }
            return false;
        }

        public static bool Match<T>(CssExpression expression, Func<CssValue, bool> predicate, T context, Action<CssValue, T> onMatch)
        {
            var item = expression.Current;
            if (item == null)
                return false;

            if (predicate(item))
            {
                expression.MoveNext();
                onMatch(item, context);
                return true;
            }

            return false;
        }

        public static Func<CssExpression, T, bool> Angle<T>(Action<CssValue, T> onMatch)
        {
            return (expression, context) =>
            {
                return Match(expression, s => s.ValueGroup == CssValueGroup.Angle, context, onMatch);
            };
        }

        public static Func<CssExpression, T, bool> Color<T>(Action<CssValue, T> onMatch)
        {
            // aqua, black, blue, fuchsia, gray, green, lime, maroon, navy, olive, orange, purple, red, silver, teal, white, and yellow
            return (expression, context) =>
                {
                    var item = expression.Current;

                    if (item == null)
                        return false;

                    if (item.ValueGroup == CssValueGroup.Identifier)
                    {
                        var rule = Any(CssColors.Colors, onMatch);
                        if (rule(expression, context))
                            return true;
                        return false;
                    }

                    return Match(expression, s => s.ValueGroup == CssValueGroup.Color, context, onMatch);
                };
        }

        public static Func<CssExpression, T, bool> Uri<T>(Action<CssValue, T> onMatch)
        {
            return (expression, context) =>
                {
                    return Match(expression, s => s.ValueGroup == CssValueGroup.Uri, context, onMatch);
                };
        }

        public static Func<CssExpression, T, bool> Percentage<T>(Action<CssValue, T> onMatch)
        {
            return (expression, context) =>
                {
                    return Match(expression, s => s.ValueGroup == CssValueGroup.Percentage, context, onMatch);
                };
        }

        public static Func<CssExpression, T, bool> Length<T>(Action<CssValue, T> onMatch)
        {
            return (expression, context) =>
                {
                    return Match(expression, s => s.ValueGroup == CssValueGroup.Length, context, onMatch);
                };
        }
    }
}
