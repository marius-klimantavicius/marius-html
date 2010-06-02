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

namespace Marius.Html.Css.Values
{
    public class CssRgbColor: CssColor
    {
        public CssRgbColor(CssFunction rgb)
        {
            CssValue red, green, blue;

            if (rgb.Name.ToUpperInvariant() != "RGB")
                throw new ArgumentException();

            if (!Extract(rgb.Arguments, out red, out green, out blue))
                throw new ArgumentException();

            Red = red;
            Green = green;
            Blue = blue;
        }

        public static bool Extract(CssExpression args, out CssValue red, out CssValue green, out CssValue blue)
        {
            red = null;
            green = null;
            blue = null; 
            
            if (args.Items.Length != 3)
                return false;

            var items = args.Items;
            if (items[0].ValueType == CssValueType.Percentage && items[1].ValueType == CssValueType.Percentage && items[2].ValueType == CssValueType.Percentage)
            {
                red = items[0];
                green = items[1];
                blue = items[2];
                return true;
            }

            if (items[1].ValueType == CssValueType.Number && items[1].ValueType == CssValueType.Number && items[2].ValueType == CssValueType.Number)
            {
                red = items[0];
                green = items[1];
                blue = items[2];
                return true;
            }

            return false;
        }
    }
}
