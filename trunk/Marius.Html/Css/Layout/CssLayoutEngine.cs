﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marius.Html.Css.Box;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Layout
{
    public class CssLayoutEngine
    {
        private CssLayoutContext Context { get; set; }

        public void Layout(CssInitialBox initial)
        {
        }

        private void Layout(CssBox box)
        {
        }

        private void LayoutBlock(CssBox box)
        {
            var used = box.Used;
            var computed = box.Computed;

            used.Update(Context);

            CalculateBlockWidth(box);

            // basically either this box contains all inlines or blocks
            // if all boxes

            // top and left are used only in absolute layout
        }

        private void LayoutChildBlocks(CssBox box)
        {
            // all children are blocks (not line boxes)
            var used = box.Used;
            var computed = box.Computed;

            var clientX = used.OffsetX + used.MarginLeft + used.BorderLeftWidth + used.PaddingLeft;
            var clientY = used.OffsetY + used.MarginTop + used.BorderTopWidth + used.PaddingTop;
            for (CssBox current = box.FirstChild; current != null; current = current.NextSibling)
            {
                if (computed.Position.Equals(CssKeywords.Static) || computed.Position.Equals(CssKeywords.Relative))
                {
                    current.Used.OffsetX = clientX;
                    current.Used.OffsetY = clientY;

                    Layout(current);

                    // current should have calculated its width (ignored) and height
                    // in this mode, blocks are laid out top to bottom
                    var borderHeight = BorderBoxHeight(box);
                     
                    // the fucking margin collapse
                    /*
                     * There are basically two cases: 
                     *      margin-top collapses with bottom of previous sibling or margin-top of ancestor
                     *      margin-bottom collapses with margin-bottom of container
                     *  
                     * Because margins only specifies the offset from top/left... where to start rendering
                     * We can contain a context (block formatting context) specific variable having value of current margin (top)
                     * 
                     * Idea:
                     * 
                     * var contextMargin = ...;
                     * var marginTop = element.margin-top
                     * 
                     * var newmargin;
                     * if ((marginTop > 0) == (contextMargin > 0))
                     * {
                     *  newmargin = absolute-max(marginTop, contextMargin);
                     *  
                     *  // will it work with negative margins?
                     *  if(marginTop > contextMargin)
                     *  {
                     *      // child has bigger margin
                     *      element.top -= (marginTop - contextMargin);
                     *  }
                     *  else
                     *  {
                     *      element.top += (contextMargin - marginTop);
                     *  }
                     * }
                     * else
                     * {
                     *  newmargin = marginTop + contextMargin;
                     * }
                     * 
                     */
                }
            }
        }

        private CssDeviceUnit BorderBoxHeight(CssBox box)
        {
            return box.Used.BorderTopWidth + box.Used.PaddingTop + box.Used.Height + box.Used.PaddingBottom + box.Used.BorderBottomWidth;
        }

        private void CalculateBlockWidth(CssBox box)
        {
            var computed = box.Computed;
            var used = box.Used;

            used.Update(Context);

            // parent must have its width
            var parentWidth = CssDeviceUnit.Zero;

            if (box.Parent == null)
                parentWidth = Context.Width;
            else
                parentWidth = box.Parent.Used.Height;

            used.PaddingTop = CalculateDimension(parentWidth, computed.PaddingTop);
            used.PaddingLeft = CalculateDimension(parentWidth, computed.PaddingLeft);
            used.PaddingBottom = CalculateDimension(parentWidth, computed.PaddingBottom);
            used.PaddingRight = CalculateDimension(parentWidth, computed.PaddingRight);

            used.BorderTopWidth = CalculateBorderWidth(parentWidth, computed.BorderTopStyle, computed.BorderTopWidth);
            used.BorderLeftWidth = CalculateBorderWidth(parentWidth, computed.BorderLeftStyle, computed.BorderLeftWidth);
            used.BorderBottomWidth = CalculateBorderWidth(parentWidth, computed.BorderBottomStyle, computed.BorderBottomWidth);
            used.BorderRightWidth = CalculateBorderWidth(parentWidth, computed.BorderRightStyle, computed.BorderRightWidth);

            var width = CssDeviceUnit.Zero;
            var marginLeft = CssDeviceUnit.Zero;
            var marginRight = CssDeviceUnit.Zero;

            var widthAuto = CssUtils.IsAuto(computed.Width);
            var marginLeftAuto = CssUtils.IsAuto(computed.MarginLeft);
            var marginRightAuto = CssUtils.IsAuto(computed.MarginRight);

            if (!widthAuto)
                width = CalculateDimension(parentWidth, computed.Width);

            if (!marginLeftAuto)
                marginLeft = CalculateDimension(parentWidth, computed.MarginLeft);

            if (!marginRightAuto)
                marginRight = CalculateDimension(parentWidth, computed.MarginRight);

            var outerWidth = marginLeft + used.BorderLeftWidth + used.PaddingLeft + width + used.PaddingRight + used.BorderRightWidth + marginRight;

            // if parentWidth < outerWidth the following if will execute always
            // lets say widthAuto = p and (parentWidth < outerWidth) = true
            // then (p || (true && !p)) => (p || !p) => true 
            if (widthAuto || (parentWidth < outerWidth && !widthAuto))
            {
                marginLeftAuto = false;
                marginRightAuto = false;
            }

            if (marginLeftAuto && marginRightAuto)
                marginLeft = marginRight = (parentWidth - outerWidth) / 2;
            else if (marginLeftAuto)
                marginLeft = parentWidth - outerWidth;
            else if (marginRightAuto)
                marginRight = parentWidth - outerWidth;
            else if (widthAuto)
                width = parentWidth - outerWidth;
            else  // overconstrained
            {
                outerWidth = used.BorderLeftWidth + used.PaddingLeft + width + used.PaddingRight + used.BorderRightWidth;

                if (CssKeywords.Ltr.Equals(computed.Direction))
                    marginRight = parentWidth - outerWidth - marginLeft; // ignore margin-right
                else
                    marginLeft = parentWidth - outerWidth - marginRight; // ignore margin-left
            }
            used.Width = width;
            used.MarginLeft = marginLeft;
            used.MarginRight = marginRight;
        }

        private CssDeviceUnit CalculateBorderWidth(CssDeviceUnit baseWidth, CssValue style, CssValue width)
        {
            if (CssKeywords.None.Equals(style))
                return CssDeviceUnit.Zero;

            return CalculateDimension(baseWidth, width);
        }

        public CssDeviceUnit CalculateDimension(CssDeviceUnit baseValue, CssValue value)
        {
            if (value.ValueType == CssValueType.Percentage)
                return baseValue * (((CssPercentage)value).Value / 100.0f);
            else
                return Context.ToDeviceUnits(value);
        }
    }
}
