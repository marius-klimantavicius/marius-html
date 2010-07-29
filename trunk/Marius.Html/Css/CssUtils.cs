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
using Marius.Html.Css.Box;

namespace Marius.Html.Css
{
    public static class CssUtils
    {
        public static bool IsAuto(CssValue value)
        {
            return CssKeywords.Auto.Equals(value);
        }

        public static bool IsBlock(CssBox box)
        {
            var display = box.Computed.Display;
            return CssKeywords.Block.Equals(display)
                || CssKeywords.ListItem.Equals(display)
                || CssKeywords.Table.Equals(display);
        }

        public static bool IsInline(CssBox box)
        {
            var display = box.Computed.Display;
            return CssKeywords.Inline.Equals(display) || CssKeywords.InlineBlock.Equals(display) || CssKeywords.InlineTable.Equals(display); // run-ins should have been already fixed
        }

        public static bool IsAbsolutelyPositioned(CssBox box)
        {
            var position = box.Computed.Position;
            return CssKeywords.Fixed.Equals(position) || CssKeywords.Absolute.Equals(position);
        }

        public static bool IsFloat(CssBox box)
        {
            var cssFloat = box.Computed.Float;
            return !CssKeywords.None.Equals(cssFloat);
        }

        public static bool IsFloatRoot(CssBox box)
        {
            if (CssUtils.IsFloat(box))
                return true;

            if (CssUtils.IsAbsolutelyPositioned(box))
                return true;

            var display = box.Computed.Display;
            if (CssKeywords.InlineBlock.Equals(display) || CssKeywords.TableCell.Equals(display) || CssKeywords.TableCaption.Equals(display))
                return true;

            var overflow = box.Computed.Overflow;
            return !CssKeywords.Visible.Equals(overflow);
        }

        public static bool IsInlineBlock(CssBox box)
        {
            var display = box.Computed.Display;

            return CssKeywords.InlineBlock.Equals(box);
        }
    }
}
