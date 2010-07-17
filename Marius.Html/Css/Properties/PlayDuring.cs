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
    public partial class PlayDuring: CssSimplePropertyHandler
    {
        public override bool IsInherited
        {
            get { return false; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.Auto; }
        }

        public PlayDuring(CssContext context)
            : base(context)
        {
        }

        public override CssValue Parse(CssExpression expression)
        {
            CssValue result = null;
            if (MatchUri(expression, ref result))
            {
                bool mix = false, repeat = false;
                bool has = true;

                for (int i = 0; i < 2 && has; i++)
                {
                    has = false;

                    if (Match(expression, CssKeywords.Mix))
                    {
                        if (mix)
                            return null;

                        mix = true;
                        has = true;
                    }

                    if (Match(expression, CssKeywords.Repeat))
                    {
                        if (repeat)
                            return null;

                        repeat = true;
                        has = true;
                    }
                }

                return new CssPlayDuring(result, mix, repeat);
            }

            if (Match(expression, CssKeywords.Auto))
                return CssKeywords.Auto;

            if (Match(expression, CssKeywords.None))
                return CssKeywords.None;

            return MatchInherit(expression);
        }
    }
}
