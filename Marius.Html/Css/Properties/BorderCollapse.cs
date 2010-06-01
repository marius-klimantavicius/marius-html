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
    public class BorderCollapse: CssProperty
    {
        public static readonly Func<CssExpression, BorderCollapse, bool> Parse;

        public static readonly CssIdentifier Collapse = new CssIdentifier("collapse");
        public static readonly CssIdentifier Separate = new CssIdentifier("separate");

        public CssValue Value { get; private set; }

        static BorderCollapse()
        {
            Parse = CssPropertyParser.Any<BorderCollapse>(new[] { Collapse, Separate, CssValue.Inherit }, (s, c) => c.Value = s);
        }

        public BorderCollapse()
            : this(Separate)
        {
        }

        public BorderCollapse(CssValue value)
        {
            Value = value;
        }

        public static BorderCollapse Create(CssExpression expression, bool full = true)
        {
            BorderCollapse result = new BorderCollapse();
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
