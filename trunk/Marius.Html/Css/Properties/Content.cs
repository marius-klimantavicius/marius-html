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
using System.Linq.Expressions;

namespace Marius.Html.Css.Properties
{
    public class Content: CssProperty
    {
        public static readonly ParseFunc<Content> Parse;

        public static readonly CssIdentifier Normal = new CssIdentifier("normal");
        public static readonly CssIdentifier OpenQuote = new CssIdentifier("open-quote");
        public static readonly CssIdentifier CloseQuote = new CssIdentifier("close-quote");
        public static readonly CssIdentifier NoOpenQuote = new CssIdentifier("no-open-quote");
        public static readonly CssIdentifier NoCloseQuote = new CssIdentifier("no-close-quote");

        public CssValue Value { get; private set; }

        static Content()
        {
            // 	normal | none | [ <string>  | <uri> | <counter>  | attr(<identifier>) | open-quote | close-quote | no-open-quote | no-close-quote ]+ | inherit
            ParseFunc<Content> simple = CssPropertyParser.Any<Content>(new[] { Normal, CssKeywords.None, CssKeywords.Inherit }, (s, c) => c.Value = s);
            ParseFunc<List<CssValue>> complexItem = CssPropertyParser.Any<List<CssValue>>(
                CssPropertyParser.String<List<CssValue>>((s, c) => c.Add(s)),
                CssPropertyParser.Uri<List<CssValue>>((s, c) => c.Add(s)),
                CssPropertyParser.Counter<List<CssValue>>((s, c) => c.Add(s)),
                CssPropertyParser.Attr<List<CssValue>>((s, c) => c.Add(s)),
                CssPropertyParser.Any<List<CssValue>>(new[] { OpenQuote, CloseQuote, NoOpenQuote, NoCloseQuote }, (s, c) => c.Add(s)));

            ParseFunc<Content> complex = (expression, context) =>
                {
                    List<CssValue> result = new List<CssValue>();
                    bool has = false;

                    while (complexItem(expression, result))
                        has = true;

                    if (has)
                        context.Value = new CssValueList(result.ToArray());

                    return has;
                };

            Parse = CssPropertyParser.Any(
                CssPropertyParser.Match<Content>(Normal, (s, c) => c.Value = s),
                CssPropertyParser.Match<Content>(CssKeywords.None, (s, c) => c.Value = s),
                complex,
                CssPropertyParser.Match<Content>(CssKeywords.Inherit, (s, c) => c.Value = s));
        }

        public Content()
            : this(Normal)
        {
        }

        public Content(CssValue value)
        {
            Value = value;
        }

        public static Content Create(CssExpression expression, bool full = true)
        {
            Content result = new Content();
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
