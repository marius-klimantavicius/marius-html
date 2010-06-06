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
    public class Pause: CssPropertyHandler
    {
        public override bool IsInherited
        {
            get { return false; }
        }

        public override CssValue Initial
        {
            get { throw new NotSupportedException(); }
        }

        public override bool Apply(CssContext context, CssBox box, CssExpression expression, bool full)
        {
            if (MatchInherit(expression) != null)
            {
                if (!Valid(expression, full))
                    return false;

                box.PauseBefore = CssKeywords.Inherit;
                box.PauseAfter = CssKeywords.Inherit;

                return true;
            }

            CssValue value = null;
            CssValue before, after;

            value = context.PauseBefore.Parse(context, expression);
            if (value == null)
                return false;

            before = after = value;

            value = context.PauseAfter.Parse(context, expression);
            if (value != null)
                after = value;

            if (!Valid(expression, full))
                return false;

            box.PauseBefore = before;
            box.PauseAfter = after;
            return true;
        }
    }
}
