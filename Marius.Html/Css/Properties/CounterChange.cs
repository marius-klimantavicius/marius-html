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
using Marius.Html.Css.Values;
using System.Collections.Generic;

namespace Marius.Html.Css.Properties
{
    public class CounterChange: CssProperty
    {
        public static readonly ParseFunc<CounterChange> Parse;

        public CssValue Value { get; private set; }

        static CounterChange()
        {
            ParseFunc<CounterChange> ids = (expression, context) =>
                {
                    List<CssValue> items = new List<CssValue>();
                    bool has = false;

                    while (CssPropertyParser.Match<List<CssValue>>(expression, s => s.ValueType == CssValueType.Identifier, items, (s, c) => c.Add(s)))
                    {
                        has = true;
                        CssPropertyParser.Match<List<CssValue>>(expression, s => s.ValueType == CssValueType.Number, items, (s, c) => c.Add(s));
                    }

                    if (has)
                        context.Value = new CssValueList(items.ToArray());

                    return has;
                };

            Parse = CssPropertyParser.Any(ids, CssPropertyParser.Match<CounterChange>(CssValue.None, (s, c) => c.Value = s), CssPropertyParser.Match<CounterChange>(CssValue.Inherit, (s, c) => c.Value = s));
        }

        public CounterChange()
            : this(CssValue.None)
        {
        }

        public CounterChange(CssValue value)
        {
            Value = value;
        }

        public static CounterChange Create(CssExpression expression, bool full = true)
        {
            CounterChange result = new CounterChange();
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
