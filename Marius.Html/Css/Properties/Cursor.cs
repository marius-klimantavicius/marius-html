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
    public class Cursor: CssSimplePropertyHandler
    {
        public override bool IsInherited
        {
            get { return true; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.Auto; }
        }

        public override void Apply(CssBox box, CssValue value)
        {
            box.Cursor = value;
        }

        public override CssValue Parse(CssContext context, CssExpression expression)
        {
            CssValue result = null;
            List<CssValue> values = new List<CssValue>();

            while (MatchCursorItem(context, expression, ref result))
                values.Add(result);

            if (MatchAny(expression, new[] { CssKeywords.Crosshair, CssKeywords.Default, CssKeywords.Pointer, CssKeywords.Move, CssKeywords.EResize, CssKeywords.NEResize, CssKeywords.NWResize, CssKeywords.NResize, CssKeywords.SEResize, CssKeywords.SWResize, CssKeywords.SResize, CssKeywords.WResize, CssKeywords.Text, CssKeywords.Wait, CssKeywords.Help, CssKeywords.Progress }, ref result))
            {
                if (values.Count == 0)
                    return result;

                values.Add(result);
                return new CssValueList(values.ToArray());
            }

            if (values.Count > 0)
                return null;

            return MatchInherit(expression);
        }

        protected virtual bool MatchCursorItem(CssContext context, CssExpression expression, ref CssValue result)
        {
            CssValue value = null;
            if (MatchUri(expression, ref value))
            {
                if (expression.Current.ValueType == CssValueType.Comma)
                {
                    expression.MoveNext();
                    result = value;
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}
