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
    public abstract class SideBorderWidth: CssSimplePropertyHandler
    {
        public override bool IsInherited
        {
            get { return false; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.Medium; }
        }

        public SideBorderWidth(CssContext context)
            : base(context)
        {
        }

        public override CssValue Parse(CssExpression expression)
        {
            CssValue result = null;
            if (MatchAny(expression, new[] { CssKeywords.Thin, CssKeywords.Thick, CssKeywords.Medium }, ref result))
                return result;

            if (MatchLength(expression, ref result))
                return result;

            return MatchInherit(expression);
        }

        public CssValue ParseValue(CssExpression expression)
        {
            return Parse(expression);
        }
    }

    public partial class BorderTopWidth: SideBorderWidth
    {
        public BorderTopWidth(CssContext context)
            : base(context)
        {
        }
    }

    public partial class BorderRightWidth: SideBorderWidth
    {
        public BorderRightWidth(CssContext context)
            : base(context)
        {
        }
    }

    public partial class BorderBottomWidth: SideBorderWidth
    {
        public BorderBottomWidth(CssContext context)
            : base(context)
        {
        }
    }

    public partial class BorderLeftWidth: SideBorderWidth
    {
        public BorderLeftWidth(CssContext context)
            : base(context)
        {
        }
    }

}
