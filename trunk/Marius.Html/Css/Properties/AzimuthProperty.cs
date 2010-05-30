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
using System.Text;
using System.Linq;
using Marius.Html.Css.Dom;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Attributes
{
    public class AzimuthProperty: CssProperty
    {
        public static readonly CssIdentifier[] Keywords = new string[] { "left-side", "far-left", "left", "center-left", "center", "center-right", "right", "far-right", "right-side" }.Select(s => new CssIdentifier(s)).ToArray();
        public static readonly CssIdentifier Center = Keywords[4];

        public static readonly CssIdentifier Leftwards = new CssIdentifier("leftwards");
        public static readonly CssIdentifier Rightwards = new CssIdentifier("rightwards");
        public static readonly CssIdentifier Behind = new CssIdentifier("behind");

        public CssValue Location { get; private set; }
        public bool IsBehind { get; private set; }

        public AzimuthProperty(CssValue location, bool isBehind)
        {
            Location = location;
            IsBehind = isBehind;
        }

        public static AzimuthProperty Create(CssExpression value)
        {
            return null;
        }
    }

    public partial class CssPropertyParser
    {
        public bool Azimuth(CssExpression expression, out CssValue location, out bool isBehind)
        {
            location = null;
            isBehind = false;

            // 	<angle> | [[ left-side | far-left | left | center-left | center | center-right | right | far-right | right-side ] || behind ] | leftwards | rightwards | inherit

            var side = MatchAny(AzimuthProperty.Keywords);
            var sideBehind = MatchPipe(side, Match(AzimuthProperty.Behind));
            var rule = MatchAny(Angle, side, MatchAny(AzimuthProperty.Leftwards, AzimuthProperty.Rightwards), Match(CssValue.Inherit));

            if (!rule(expression))
                return false;

            return false;
        }

        private bool Angle(CssExpression expression)
        {
            var item = expression.Current;

            if (item == null)
                return false;

            if (item.ValueGroup == CssValueGroup.Angle)
            {
                expression.MoveNext();
                return true;
            }

            return false;
        }

        private Func<CssExpression, bool> MatchPipe(params Func<CssExpression, bool>[] items)
        {
            return expression =>
                {
                    int[] counter = new int[items.Length];
                    for (int i = 0; i < items.Length; i++)
                    {
                        for (int k = 0; k < items.Length; k++)
                        {
                            if (items[k](expression))
                                counter[k]++;
                        }
                    }

                    bool hasValid = false;
                    for (int i = 0; i < counter.Length; i++)
                    {
                        if (counter[i] > 1)
                            return false;
                        if (counter[i] == 1)
                            hasValid = true;
                    }

                    return hasValid;
                };
        }

        private Func<CssExpression, bool> Match(CssIdentifier item)
        {
            return expression =>
                {
                    var current = expression.Current;
                    if (current == null)
                        return false;

                    if (current.ValueType != CssValueType.Identifier)
                        return false;

                    if (item.Equals(current))
                    {
                        expression.MoveNext();
                        return true;
                    }
                    return false;
                };
        }

        private Func<CssExpression, bool> MatchAny(params CssIdentifier[] items)
        {
            return MatchAny(items.Select(s => Match(s)).ToArray());
        }

        private Func<CssExpression, bool> MatchAny(params Func<CssExpression, bool>[] items)
        {
            return expression =>
                {
                    for (int i = 0; i < items.Length; i++)
                    {
                        if (items[i](expression))
                            return true;
                    }
                    return false;
                };
        }
    }
}
