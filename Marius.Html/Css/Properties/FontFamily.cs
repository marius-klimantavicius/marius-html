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
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Properties
{
    public class FontFamily: CssSimplePropertyHandler
    {
        public override bool IsInherited
        {
            get { return true; }
        }

        public override CssValue Initial
        {
            get { return CssNull.Value; }
        }

        public override void Apply(CssBox box, CssValue value)
        {
            box.FontFamily = value;
        }

        public override CssValue Parse(CssContext context, CssExpression expression)
        {
            CssValue result = null;
            List<CssValue> values = new List<CssValue>();

            if (MatchFamily(context, expression, ref result))
            {
                while (expression.Current.ValueType == CssValueType.Comma)
                {
                    if (!MatchFamily(context, expression, ref result))
                        return null;

                    values.Add(result);
                }

                return new CssFontFamily(values.ToArray());
            }

            return MatchInherit(expression);
        }

        protected virtual bool MatchFamily(CssContext context, CssExpression expression, ref CssValue result)
        {
            if (Match(expression, CssKeywords.Initial))
                return false;

            if (Match(expression, CssKeywords.Default))
                return false;

            if (MatchAny(expression, new[] { CssKeywords.Serif, CssKeywords.SansSerif, CssKeywords.Cursive, CssKeywords.Fantasy, CssKeywords.Monospace }, ref result))
                return true;

            if (MatchString(expression, ref result))
                return true;

            List<CssValue> names = new List<CssValue>();
            CssValue value = null;

            while (MatchIdentifier(expression, ref value))
                names.Add(value);

            if (names.Count == 0)
                return false;

            result = new CssValueList(names.ToArray());
            return true;
        }
    }
}
