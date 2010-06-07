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
using Marius.Html.Css.Dom;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Parser
{
    public class FunctionFactory
    {
        public virtual CssValue Function(string name, CssExpression args)
        {
            CssValue result = null;
            switch (name.ToUpperInvariant())
            {
                case "RGB":
                    if (ParseRgb(args, ref result))
                        return result;
                    break;
                case "RECT":
                    if (ParseRect(args, ref result))
                        return result;
                    break;
                    // TODO: implement attr(id) and counter/counters functions
            }

            return new CssFunction(name, args);
        }

        protected virtual bool ParseRgb(CssExpression args, ref CssValue result)
        {
            args.Reset();

            CssValue red = null, green = null, blue = null;

            if (CssPropertyParser.MatchPercentage(args, ref red))
            {
                if (!CssPropertyParser.MatchPercentage(args, ref green))
                    return false;

                if (!CssPropertyParser.MatchPercentage(args, ref blue))
                    return false;

                if (!CssPropertyParser.Valid(args))
                    return false;

                result = new CssRgbColor(red, green, blue);
                return true;
            }
            else if (CssPropertyParser.MatchNumber(args, ref red))
            {
                if (!CssPropertyParser.MatchNumber(args, ref green))
                    return false;

                if (!CssPropertyParser.MatchNumber(args, ref blue))
                    return false;

                if (!CssPropertyParser.Valid(args))
                    return false;

                result = new CssRgbColor(red, green, blue);
                return true;
            }

            return false;
        }

        protected virtual bool ParseRect(CssExpression args, ref CssValue result)
        {
            args.Reset();

            CssValue top = null, right = null, bottom = null, left = null;
            if (!CssPropertyParser.MatchLength(args, ref top) && !CssPropertyParser.Match(args, CssKeywords.Auto, ref top))
                return false;

            if (!CssPropertyParser.MatchLength(args, ref right) && !CssPropertyParser.Match(args, CssKeywords.Auto, ref right))
                return false;

            if (!CssPropertyParser.MatchLength(args, ref bottom) && !CssPropertyParser.Match(args, CssKeywords.Auto, ref bottom))
                return false;

            if (!CssPropertyParser.MatchLength(args, ref left) && !CssPropertyParser.Match(args, CssKeywords.Auto, ref left))
                return false;

            if (!CssPropertyParser.Valid(args))
                return false;

            result = new CssRect(top, right, bottom, left);
            return true;
        }
    }
}
