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
using System.Linq.Expressions;

namespace Marius.Html.Css.Properties
{
    public delegate bool ParseFunc<T>(CssExpression expression, T context);
    public delegate void ParseAction<T>(CssValue value, T context);
    public delegate void ShapeParseAction<T>(Tuple<CssValue, CssValue, CssValue, CssValue> shape, T context);

    public static class CssPropertyParser
    {
        public static ParseFunc<T> Any<T>(CssIdentifier[] keywords, ParseAction<T> onMatch)
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

        public static ParseFunc<T> Any<T>(params ParseFunc<T>[] matches)
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

        public static ParseFunc<T> Pipe<T>(params ParseFunc<T>[] matches)
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

        public static ParseFunc<T> Match<T>(CssIdentifier key, ParseAction<T> onMatch)
        {
            return (expression, context) =>
            {
                return Match(expression, key, context, onMatch);
            };
        }

        public static ParseFunc<T> Maybe<T>(ParseFunc<T> match)
        {
            return (expression, context) =>
                {
                    match(expression, context);
                    return true;
                };
        }

        public static ParseFunc<T> Sequence<T>(params ParseFunc<T>[] matches)
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

        public static ParseFunc<T> TwoSequence<T>(ParseFunc<T> func1, ParseFunc<T> func2)
        {
            return (expression, context) =>
                {
                    if (!func1(expression, context))
                        return false;

                    func2(expression, context);
                    return true;
                };
        }

        public static ParseFunc<T> FourSequence<T>(ParseFunc<T> func1, ParseFunc<T> func2, ParseFunc<T> func3, ParseFunc<T> func4)
        {
            return (expression, context) =>
                {
                    if (!func1(expression, context))
                        return false;

                    if (!func2(expression, context))
                        return true;

                    if (!func3(expression, context))
                        return true;

                    func4(expression, context);
                    return true;
                };
        }

        public static bool Match<T>(CssExpression expression, CssIdentifier value, T context, ParseAction<T> onMatch)
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

        public static bool Match<T>(CssExpression expression, Func<CssValue, bool> predicate, T context, ParseAction<T> onMatch)
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

        public static bool MatchLength<T>(CssExpression expression, T context, ParseAction<T> onMatch)
        {
            return Match(expression, s => s.ValueGroup == CssValueGroup.Length || (s.ValueType == CssValueType.Number && ((CssNumber)s).Value == 0), context, onMatch);
        }

        public static ParseFunc<T> Angle<T>(ParseAction<T> onMatch)
        {
            return (expression, context) =>
            {
                return Match(expression, s => s.ValueGroup == CssValueGroup.Angle, context, onMatch);
            };
        }

        public static ParseFunc<T> Color<T>(ParseAction<T> onMatch)
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

        public static ParseFunc<T> ColorWithTransparent<T>(ParseAction<T> onMatch)
        {
            return Any(Color(onMatch), Match<T>(CssValue.Transparent, onMatch));
        }

        public static ParseFunc<T> Uri<T>(ParseAction<T> onMatch)
        {
            return (expression, context) =>
                {
                    return Match(expression, s => s.ValueGroup == CssValueGroup.Uri, context, onMatch);
                };
        }

        public static ParseFunc<T> Percentage<T>(ParseAction<T> onMatch)
        {
            return (expression, context) =>
                {
                    return Match(expression, s => s.ValueGroup == CssValueGroup.Percentage, context, onMatch);
                };
        }

        public static ParseFunc<T> Length<T>(ParseAction<T> onMatch)
        {
            return (expression, context) =>
                {
                    return MatchLength<T>(expression, context, onMatch);
                };
        }

        public static ParseFunc<T> Shape<T>(ShapeParseAction<T> onMatch)
        {
            return (expression, context) =>
                {
                    if (expression.Current.ValueType != CssValueType.Function)
                        return false;

                    CssFunction fun = (CssFunction)expression.Current;
                    if (!fun.Name.Equals("rect", StringComparison.InvariantCultureIgnoreCase))
                        return false;

                    if (fun.Arguments.Items.Length != 4)
                        return false;

                    CssValue top = null, right = null, bottom = null, left = null;
                    if (!MatchLength<object>(expression, null, (s, c) => top = s) && !Match<object>(expression, CssValue.Auto, null, (s, c) => top = s))
                        return false;

                    if (!MatchLength<object>(expression, null, (s, c) => right = s) && !Match<object>(expression, CssValue.Auto, null, (s, c) => right = s))
                        return false;
                    
                    if (!MatchLength<object>(expression, null, (s, c) => bottom = s) && !Match<object>(expression, CssValue.Auto, null, (s, c) => bottom = s))
                        return false;
                    
                    if (!MatchLength<object>(expression, null, (s, c) => left = s) && !Match<object>(expression, CssValue.Auto, null, (s, c) => left = s))
                        return false;

                    onMatch(new Tuple<CssValue, CssValue, CssValue, CssValue>(top, right, bottom, left), context);
                    return true;
                };
        }

        public static ParseFunc<T> String<T>(ParseAction<T> onMatch)
        {
            return (expression, context) =>
                {
                    return Match<T>(expression, s => s.ValueType == CssValueType.String, context, onMatch);
                };
        }

        public static ParseFunc<T> Counter<T>(ParseAction<T> onMatch)
        {
            return (expression, context) =>
                {
                    // TODO: implement
                    // for the moment, accept any function
                    return Match(expression, s => s.ValueType == CssValueType.Function, context, onMatch);
                };
        }

        public static ParseFunc<T> Attr<T>(ParseAction<T> onMatch)
        {
            return (expression, context) =>
                {
                    // TODO: implement
                    // for the moment, accept any function
                    return Match(expression, s => s.ValueType == CssValueType.Function, context, onMatch);
                };
        }

        public static ParseFunc<T> Identifier<T>(ParseAction<T> onMatch)
        {
            return (expression, context) =>
                {
                    return Match<T>(expression, s => s.ValueType == CssValueType.Identifier, context, onMatch);
                };
        }

        public static ParseFunc<T> Number<T>(ParseAction<T> onMatch)
        {
            return (expression, context) =>
                {
                    return Match<T>(expression, s => s.ValueType == CssValueType.Number, context, onMatch);
                };
        }
    }
}
