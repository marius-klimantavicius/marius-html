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

namespace Marius.Html.Css
{
    public abstract class CssPropertyHandler
    {
        protected CssContext _context;

        protected CssPropertyHandler(CssContext context)
        {
            _context = context;
        }

        public abstract bool Apply(CssBox box, CssExpression expression);
        public abstract bool Validate(CssExpression expression);

        public static bool Valid(CssExpression expression)
        {
            return expression.Current.IsNull();
        }

        public static bool Match(CssExpression expression, CssIdentifier keyword)
        {
            if (expression.Current.ValueType == CssValueType.Identifier)
            {
                if (expression.Current.Equals(keyword))
                {
                    expression.MoveNext();
                    return true;
                }
            }

            return false;
        }

        public static CssValue MatchInherit(CssExpression expression)
        {
            if (Match(expression, CssKeywords.Inherit))
                return CssKeywords.Inherit;
            return null;
        }

        public static bool Match(CssExpression expression, CssIdentifier keyword, ref CssValue result)
        {
            if (expression.Current.ValueType == CssValueType.Identifier)
            {
                if (expression.Current.Equals(keyword))
                {
                    result = keyword;
                    expression.MoveNext();
                    return true;
                }
            }

            return false;
        }

        public static bool MatchAny(CssExpression expression, CssIdentifier[] keywords, ref CssValue result)
        {
            if (expression.Current.ValueType != CssValueType.Identifier)
                return false;

            for (int i = 0; i < keywords.Length; i++)
            {
                if (Match(expression, keywords[i], ref result))
                    return true;
            }

            return false;
        }

        public static bool MatchAngle(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueGroup == CssValueGroup.Angle)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }
            return false;
        }

        public static bool MatchColor(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueGroup == CssValueGroup.Identifier)
            {
                return MatchAny(expression, new[] { CssKeywords.Aqua, CssKeywords.Black, CssKeywords.Blue, CssKeywords.Fuchsia, CssKeywords.Gray, CssKeywords.Green, CssKeywords.Lime, CssKeywords.Maroon, CssKeywords.Navy, CssKeywords.Olive, CssKeywords.Orange, CssKeywords.Purple, CssKeywords.Red, CssKeywords.Silver, CssKeywords.Teal, CssKeywords.White, CssKeywords.Yellow }, ref result);
            }
            else if (expression.Current.ValueGroup == CssValueGroup.Color)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }

            return false;
        }

        public static bool MatchUri(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueGroup == CssValueGroup.Uri)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }

            return false;
        }

        public static bool MatchLength(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueGroup == CssValueGroup.Length)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }
            else if (expression.Current.ValueType == CssValueType.Number)
            {
                CssNumber value = (CssNumber)expression.Current;
                if (value.Value != 0)
                    return false;

                result = value;
                expression.MoveNext();
                return true;
            }

            return false;
        }

        public static bool MatchShape(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueType == CssValueType.Rect)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }

            return false;
        }

        public static bool MatchString(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueType == CssValueType.String)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }

            return false;
        }

        public static bool MatchCounter(CssExpression expression, ref CssValue result)
        {
            // TODO: implement
            return false;
        }

        public static bool MatchAttr(CssExpression expression, ref CssValue result)
        {
            // TODO: implement
            return false;
        }

        public static bool MatchIdentifier(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueType == CssValueType.Identifier)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }

            return false;
        }

        public static bool MatchNumber(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueType == CssValueType.Number)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }

            return false;
        }

        public static bool MatchPercentage(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueType == CssValueType.Percentage)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }
            return false;
        }

        public static bool MatchTime(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueGroup == CssValueGroup.Time)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }
            else if (expression.Current.ValueType == CssValueType.Number)
            {
                CssNumber value = (CssNumber)expression.Current;
                if (value.Value != 0)
                    return false;

                result = value;
                expression.MoveNext();
                return true;
            }

            return false;
        }

        public static bool MatchFrequency(CssExpression expression, ref CssValue result)
        {
            if (expression.Current.ValueGroup == CssValueGroup.Frequency)
            {
                result = expression.Current;
                expression.MoveNext();
                return true;
            }
            else if (expression.Current.ValueType == CssValueType.Number)
            {
                CssNumber value = (CssNumber)expression.Current;
                if (value.Value != 0)
                    return false;

                result = value;
                expression.MoveNext();
                return true;
            }

            return false;
        }
    }
}
