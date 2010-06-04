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

namespace Marius.Html.Css.Properties
{
    public class Clear: CssProperty
    {
        public static readonly ParseFunc<Clear> Parse;

        public static readonly CssIdentifier Left = new CssIdentifier("left");
        public static readonly CssIdentifier Right = new CssIdentifier("right");
        public static readonly CssIdentifier Both = new CssIdentifier("both");

        public CssValue Value { get; private set; }

        static Clear()
        {
            // 	none | left | right | both | inherit
            Parse = CssPropertyParser.Any<Clear>(new[] { Left, Right, Both, CssKeywords.None, CssKeywords.Inherit }, (s, c) => c.Value = s);
        }

        public Clear()
            : this(CssKeywords.None)
        {
        }

        public Clear(CssValue value)
        {
            Value = value;
        }

        public static Clear Create(CssExpression expression, bool full = true)
        {
            Clear result = new Clear();
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
