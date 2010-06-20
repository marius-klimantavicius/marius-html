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

            if (CssPropertyHandler.MatchPercentage(args, ref red))
            {
                bool presult = CssPropertyHandler.MatchComma(args)
                    && CssPropertyHandler.MatchPercentage(args, ref green)
                    && CssPropertyHandler.MatchComma(args)
                    && CssPropertyHandler.MatchPercentage(args, ref blue);

                if (!presult)
                    return false;

                if (!CssPropertyHandler.Valid(args))
                    return false;

                result = new CssRgbColor(red, green, blue);
                return true;
            }
            else if (CssPropertyHandler.MatchNumber(args, ref red))
            {
                bool presult = CssPropertyHandler.MatchComma(args)
                    && CssPropertyHandler.MatchNumber(args, ref green)
                    && CssPropertyHandler.MatchComma(args)
                    && CssPropertyHandler.MatchNumber(args, ref blue);

                if (!presult)
                    return false;

                if (!CssPropertyHandler.Valid(args))
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
            if (!CssPropertyHandler.MatchLength(args, ref top) && !CssPropertyHandler.Match(args, CssKeywords.Auto, ref top))
                return false;

            if (!CssPropertyHandler.MatchComma(args))
                return false;

            if (!CssPropertyHandler.MatchLength(args, ref right) && !CssPropertyHandler.Match(args, CssKeywords.Auto, ref right))
                return false;

            if (!CssPropertyHandler.MatchComma(args))
                return false;

            if (!CssPropertyHandler.MatchLength(args, ref bottom) && !CssPropertyHandler.Match(args, CssKeywords.Auto, ref bottom))
                return false;

            if (!CssPropertyHandler.MatchComma(args))
                return false;

            if (!CssPropertyHandler.MatchLength(args, ref left) && !CssPropertyHandler.Match(args, CssKeywords.Auto, ref left))
                return false;

            if (!CssPropertyHandler.Valid(args))
                return false;

            result = new CssRect(top, right, bottom, left);
            return true;
        }
    }
}
