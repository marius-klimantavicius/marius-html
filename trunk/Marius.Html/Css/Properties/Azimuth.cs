﻿#region License
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
using System.Text;
using System.Linq;
using Marius.Html.Css.Dom;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Properties
{
    public class Azimuth: CssPropertyStrategy
    {
        public override bool IsInherited
        {
            get { return true; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.Center; }
        }

        public override bool Apply(CssContext context, CssBox box, CssExpression expression, bool full)
        {
            CssValue value = Parse(context, expression);
            if (value == null || (full && !expression.Current.IsNull()))
                return false;

            box.Azimuth = value;

            return true;
        }

        public virtual CssValue Parse(CssContext context, CssExpression expression)
        {
            if (Match(expression, CssKeywords.Inherit))
                    return CssKeywords.Inherit;

            CssValue value = null;
            if (MatchAny(expression, new[] { CssKeywords.Leftwards, CssKeywords.Rightwards }, ref value))
                    return value;

            bool hasBehind = false, hasPosition = false;
            for (int i = 0; i < 2; i++)
            {
                if (Match(expression, CssKeywords.Behind))
                {
                    if (hasBehind)
                        return null;
                    hasBehind = true;
                }
                else if (MatchAny(expression, new[] { CssKeywords.LeftSide, CssKeywords.FarLeft, CssKeywords.Left, CssKeywords.CenterLeft, CssKeywords.Center, CssKeywords.CenterRight, CssKeywords.Right, CssKeywords.FarRight, CssKeywords.RightSide }, ref value))
                {
                    if (hasPosition)
                        return null;
                    hasPosition = true;
                }
            }

            if (hasPosition || hasBehind)
            {
                if (!hasPosition)
                    value = CssKeywords.Center;
                return new CssAzimuth(value, hasBehind);
            }

            return null;
        }
    }
}
