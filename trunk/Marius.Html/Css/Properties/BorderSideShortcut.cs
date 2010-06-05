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
    public abstract class BorderSideShortcut: CssPropertyHandler
    {
        public override bool IsInherited
        {
            get { return false; }
        }

        public override CssValue Initial
        {
            get { throw new NotSupportedException(); }
        }

        public abstract void Apply(CssBox box, CssValue top, CssValue right, CssValue bottom, CssValue left);

        public override bool Apply(CssContext context, CssBox box, CssExpression expression, bool full)
        {
            CssValue[] values = Parse(context, expression);
            if (values == null || !Valid(expression, full))
                return false;

            return ApplyValues(box, values);
        }

        public virtual CssValue[] Parse(CssContext context, CssExpression expression)
        {
            CssValue value;
            CssValue top, right, bottom, left;
            if (MatchInherit(expression) != null)
                return new[] { CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit, CssKeywords.Inherit };

            BorderSideStrategy topHandler, rightHandler, bottomHandler, leftHandler;
            RetrieveHandlers(context, out topHandler, out rightHandler, out bottomHandler, out leftHandler);

            value = topHandler.Parse(context, expression);
            if (value == null)
                return null;

            top = right = bottom = left = value;

            value = rightHandler.Parse(context, expression);
            if (value == null)
                return new[] { top, right, bottom, left };

            left = value;

            value = bottomHandler.Parse(context, expression);
            if (value == null)
                return new[] { top, right, bottom, left };

            bottom = value;

            value = leftHandler.Parse(context, expression);
            if (value == null)
                return new[] { top, right, bottom, left };

            left = value;
            return new[] { top, right, bottom, left };
        }

        protected abstract void RetrieveHandlers(CssContext context, out BorderSideStrategy top, out BorderSideStrategy right, out BorderSideStrategy bottom, out BorderSideStrategy left);

        public virtual bool ApplyValues(CssBox box, CssValue[] values)
        {
            Apply(box, values[0], values[1], values[2], values[3]);
            return true;
        }
    }
}
