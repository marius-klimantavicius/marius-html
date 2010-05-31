#region License
/*
Distributed under the terms of an MIT-style license:

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
    public class BackgoundPosition: CssProperty
    {
        private static readonly Func<CssExpression, BackgoundPosition, bool> Parse;

        private static readonly CssIdentifier Left = new CssIdentifier("left");
        private static readonly CssIdentifier Right = new CssIdentifier("right");
        private static readonly CssIdentifier Center = new CssIdentifier("center");
        private static readonly CssIdentifier Top = new CssIdentifier("top");
        private static readonly CssIdentifier Bottom = new CssIdentifier("bottom");

        public CssValue Horizontal { get; private set; }
        public CssValue Vertical { get; private set; }

        static BackgoundPosition()
        {
            /* 	
             * [ 
             *      [ <percentage>  | <length>  | left | center | right ] [ <percentage>  | <length>  | top | center | bottom ]? 
             * ] | 
             * [ 
             *      [ left | center | right ] || [ top | center | bottom ] 
             * ] | 
             *      inherit
             */
            /*
            var hop = CssPropertyParser.Any<BackgoundPosition>(
                CssPropertyParser.Percentage<BackgoundPosition>((s, c) => c.Horizontal = s),
                CssPropertyParser.Length<BackgoundPosition>((s, c) => c.Horizontal = s));

            var hoh = CssPropertyParser.Any<BackgoundPosition>(new[] { Left, Center, Right }, (s, c) => c.Horizontal = s);

            var vep = CssPropertyParser.Any<BackgoundPosition>(
                CssPropertyParser.Percentage<BackgoundPosition>((s, c) => c.Vertical = s),
                CssPropertyParser.Length<BackgoundPosition>((s, c) => c.Vertical = s));

            var vev = CssPropertyParser.Any<BackgoundPosition>(new[] { Top, Center, Bottom }, (s, c) => c.Vertical = s);
            */
        }
    }
}
