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
    public class Content: CssPropertyHandler
    {
        public override bool IsInherited
        {
            get { return false; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.Normal; }
        }

        public override bool Apply(CssContext context, CssBox box, CssExpression expression)
        {
            CssValue value = Parse(context, expression);
            if (value == null || !Valid(expression))
                return false;

            box.Content = value;
            return true;
        }

        public override CssValue Parse(CssContext context, CssExpression expression)
        {
            // 	normal | none
            //         | [ <string>  | <uri> | <counter>  | attr(<identifier>) | open-quote | close-quote  | no-open-quote | no-close-quote ]+ 
            //         | inherit

            if (Match(expression, CssKeywords.Normal))
                return CssKeywords.Normal;

            if (Match(expression, CssKeywords.None))
                return CssKeywords.None;

            CssValue result = null;
            List<CssValue> content = new List<CssValue>();

            while (MatchContentElement(context, expression, ref result))
                content.Add(result);

            if (content.Count > 0)
                return new CssValueList(content.ToArray());

            return MatchInherit(expression);
        }

        protected virtual bool MatchContentElement(CssContext context, CssExpression expression, ref CssValue result)
        {
            if (MatchString(expression, ref result))
                return true;

            if (MatchUri(expression, ref result))
                return true;

            if (MatchAny(expression, new[] { CssKeywords.OpenQuote, CssKeywords.CloseQuote, CssKeywords.NoOpenQuote, CssKeywords.NoCloseQuote }, ref result))
                return true;

            if (MatchCounter(expression, ref result))
                return true;

            if (MatchAttr(expression, ref result))
                return true;

            return false;
        }
    }
}
