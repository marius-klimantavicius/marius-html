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
using Marius.Html.Css.Box;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Layout
{
    public class CssBlockLayoutManager: CssLayoutManager
    {
        public CssBlockLayoutManager(CssLayoutContext layoutContext)
            : base(layoutContext)
        {
        }

        public override void Layout(CssBox box)
        {
            CalculateWidthAndMargins(box);

            throw new NotImplementedException();
        }

        private void CalculateWidthAndMargins(CssBox box)
        {
            //if (box.Parent.Width == null)
            //    throw new CssInvalidStateException();

            //bool autoWidth = CssUtils.IsAuto(box.Computed.Width);

            //if (!CssUtils.IsAuto(box.Computed.MarginLeft))
            //    box.MarginLeft = LayoutContext.ToLength(box.Computed.MarginLeft, box.Parent.Width);

            //if (!CssUtils.IsAuto(box.Computed.MarginRight))
            //    box.MarginRight = LayoutContext.ToLength(box.Computed.MarginRight, box.Parent.Width);

            //box.PaddingLeft = LayoutContext.ToLength(box.Computed.PaddingLeft, box.Parent.Width);
            //box.PaddingRight = LayoutContext.ToLength(box.Computed.PaddingRight, box.Parent.Width);

            //CssBoxLength borderLeft = LayoutContext.ToLength(box.Computed.BorderLeftWidth);
            //CssBoxLength borderRight = LayoutContext.ToLength(box.Computed.BorderRightWidth);

            //CssBoxLength sum;

            //if (!autoWidth)
            //{
            //    box.Width = LayoutContext.ToLength(box.Computed.Width, box.Parent.Width);

            //    sum = borderLeft + box.PaddingLeft + box.Width + box.PaddingRight + borderRight;

            //    if (box.MarginLeft != null)
            //        sum = sum + box.MarginLeft;
            //    if (box.MarginRight != null)
            //        sum = sum + box.MarginRight;
            //}
            //else
            //{
            //    if (box.MarginLeft == null)
            //        box.MarginLeft = CssBoxLength.Zero;
            //    if (box.MarginRight == null)
            //        box.MarginRight = CssBoxLength.Zero;

            //    sum = borderLeft + box.PaddingLeft + box.PaddingRight + borderRight + box.MarginLeft + box.MarginRight;
            //    box.Width = box.Parent.Width - sum;

            //    if (box.Width.IsNegative)
            //    {
            //    }
            //}
        }

        public override bool ContributesWidth(CssBox box)
        {
            return true;
        }

        public override CssBoxLength ShrinkToFit(CssBox box, CssBoxLength availableSpace)
        {
            var width = box.Computed.Width;
            if (!CssUtils.IsAuto(width))
            {
                // there is nothing to shrink
                // calculate other properties taking into account available space
            }

            throw new NotImplementedException();
        }
    }
}
