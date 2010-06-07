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
    public class CounterIncrement: CssPropertyHandler
    {
        public override bool IsInherited
        {
            get { return false; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.None; }
        }

        public override bool Apply(CssContext context, CssBox box, CssExpression expression, bool full)
        {
            CssValue value = Parse(context, expression);
            if (value == null || !Valid(expression, full))
                return false;

            box.CounterIncrement = value;

            return true;
        }

        public virtual CssValue Parse(CssContext context, CssExpression expression)
        {
            // [ <identifier>  <integer>? ]+ | none | inherit
            if (Match(expression, CssKeywords.None))
                return CssKeywords.None;

            // 'initial' reserved for future use and must not be used as counter name
            if (Match(expression, CssKeywords.Initial))
                return null;

            CssValue result = null;
            List<CssValue> values = new List<CssValue>();

            while (MatchChangeItem(context, expression, ref result))
                values.Add(result);

            if (values.Count > 0)
                return new CssValueList(values.ToArray());

            return MatchInherit(expression);
        }

        private bool MatchChangeItem(CssContext context, CssExpression expression, ref CssValue result)
        {
            CssValue name = null, value = null;

            if (!MatchIdentifier(expression, ref name))
                return false;

            MatchNumber(expression, ref value);

            if (value == null)
                value = CssNumber.One;

            return true;
        }
    }
}
