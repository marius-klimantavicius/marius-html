using System;
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

            CalculateBlockWidth(box);

            // basically either this box contains all inlines or blocks
            // if all boxes

            CalculateBlockTopMargin(box);

            if (IsBlocContext(box))
                LayoutChildBlocks(box);
            else
                LayoutChildInlines(box);

            // top and left are used only in absolute layout
        }

        private void CalculateBlockTopMargin(CssBox box)
        {
            // block margins collapse
            // I am going to implement this via used.MarginTop/used.MarginBottom
            // ie. used value might be different from specified in case of margin collapse
            // idea: set top margins in such a way, so sum of collapsible margins was equal to the collapsed margin

            // get formatting context
            dynamic context = GetBlockFormattingContext(box);
            var positiveMargin = context.PositiveMargin;
            var negativeMargin = context.NegativeMargin;

            var computed = box.Computed;
            var used = box.Used;

            var marginTop = 0.0f;

            if (!CssUtils.IsAuto(computed.MarginTop))
                marginTop = CalculateDimension(used.Width, computed.MarginTop);

            if (marginTop >= 0)
            {
                if (positiveMargin > marginTop)
                {
                    marginTop = 0;
                }
                else
                {
                    var old = positiveMargin;
                    positiveMargin = marginTop;
                    marginTop = marginTop - old;
                }
            }
            else
            {
                if (negativeMargin < marginTop)
                {
                    marginTop = 0;
                }
                else
                {
                    var old = negativeMargin;
                    negativeMargin = marginTop;
                    marginTop = marginTop - old;
                }
            }

            used.MarginTop = marginTop;

            context.PositiveMargin = positiveMargin;
            context.NegativeMargin = negativeMargin;
        }

        private object GetBlockFormattingContext(CssBox box)
        {
            throw new NotImplementedException();
        }

        private void LayoutChildInlines(CssBox box)
        {
            throw new NotImplementedException();
        }

        private bool IsBlocContext(CssBox box)
        {
            throw new NotImplementedException();
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

        private float BorderBoxHeight(CssBox box)
        {
            return box.Used.BorderTopWidth + box.Used.PaddingTop + box.Used.Height + box.Used.PaddingBottom + box.Used.BorderBottomWidth;
        }

        private void CalculateBlockWidth(CssBox box)
        {
            var computed = box.Computed;
            var used = box.Used;

            // parent must have its width
            var parentWidth = 0.0f;

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

            var width = 0.0f;
            var marginLeft = 0.0f;
            var marginRight = 0.0f;

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

        private float Clamp(float value, float min, float max)
        {
            if (value < min)
                return min;
            if (value > max)
                return max;
            return value;
        }

        private float CalculateBorderWidth(float baseWidth, CssValue style, CssValue width)
        {
            if (CssKeywords.None.Equals(style))
                return 0;

            return CalculateDimension(baseWidth, width);
        }

        public float CalculateDimension(float baseValue, CssValue value)
        {
            if (value.ValueType == CssValueType.Percentage)
                return baseValue * (((CssPercentage)value).Value / 100.0f);
            else
                return Context.ToDeviceUnits(value);
        }
    }
}
