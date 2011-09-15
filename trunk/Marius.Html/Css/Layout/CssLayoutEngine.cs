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

        private void LayoutBlock(CssBox box)
        {
            var used = box.Used;
            var computed = box.Computed;

            used.Update(Context);

            var top = Context.ToDeviceUnits(box.Computed.Top);
            var left = Context.ToDeviceUnits(box.Computed.Left);

            var parentWidth = CssDeviceUnit.Zero;

            //if (box.Parent == null)
            //    parentWidth = Context.Width;
            //else
            //    parentWidth = box.Parent.Actual.Width;

            var paddingLeft = Context.ToDeviceUnits(computed.PaddingLeft);
            var paddingRight = Context.ToDeviceUnits(computed.PaddingRight);

            var borderLeft = CssDeviceUnit.Zero;
            if (!computed.BorderLeftStyle.Equals(CssKeywords.None))
                borderLeft = Context.ToDeviceUnits(computed.BorderLeftWidth);

            var borderRight = CssDeviceUnit.Zero;
            if (!computed.BorderRightStyle.Equals(CssKeywords.None))
                borderRight = Context.ToDeviceUnits(computed.BorderRightWidth);

            var marginLeft = CssDeviceUnit.Zero;
            var marginRight = CssDeviceUnit.Zero;

            if (!CssUtils.IsAuto(computed.MarginLeft))
                marginLeft = Context.ToDeviceUnits(computed.MarginLeft);

            if (!CssUtils.IsAuto(computed.MarginRight))
                marginRight = Context.ToDeviceUnits(computed.MarginRight);

            var outerWidth = borderLeft + paddingLeft + paddingRight + borderRight;

            var width = CssDeviceUnit.Zero;

            if (CssUtils.IsAuto(computed.Width))
            {
                // margin-left and margin-right auto becomes 0
                if (CssUtils.IsAuto(computed.MarginLeft))
                    marginLeft = CssDeviceUnit.Zero;

                if (CssUtils.IsAuto(computed.MarginRight))
                    marginRight = CssDeviceUnit.Zero;

                var fullWidth = outerWidth + marginLeft + marginRight;
                if (fullWidth > parentWidth)
                {
                    // overconstrained
                    width = CssDeviceUnit.Zero;

                    if (computed.Direction.Equals(CssKeywords.Ltr))
                    {
                        marginLeft = CssDeviceUnit.Zero;
                        marginRight = parentWidth - outerWidth;
                    }
                    else
                    {
                    }
                }
                else
                {
                }
            }
            else
            {
            }
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
                parentWidth = Context.Height;

            used.PaddingTop = CalculateDimension(parentWidth, computed.PaddingTop);
            used.PaddingLeft = CalculateDimension(parentWidth, computed.PaddingLeft);
            used.PaddingBottom = CalculateDimension(parentWidth, computed.PaddingBottom);
            used.PaddingRight = CalculateDimension(parentWidth, computed.PaddingRight);

            used.BorderTopWidth = CalculateBorderWidth(parentWidth, computed.BorderTopStyle, computed.BorderTopWidth);
            used.BorderLeftWidth = CalculateBorderWidth(parentWidth, computed.BorderLeftStyle, computed.BorderLeftWidth);
            used.BorderBottomWidth = CalculateBorderWidth(parentWidth, computed.BorderBottomStyle, computed.BorderBottomWidth);
            used.BorderRightWidth = CalculateBorderWidth(parentWidth, computed.BorderRightStyle, computed.BorderRightWidth);

            var marginLeft = CssDeviceUnit.Zero;
            var marginRight = CssDeviceUnit.Zero;

            var marginLeftAuto = CssUtils.IsAuto(computed.MarginLeft);
            var marginRightAuto = CssUtils.IsAuto(computed.MarginRight);
            var widthAuto = CssUtils.IsAuto(computed.Width);

            if (!marginLeftAuto)
                marginLeft = CalculateDimension(parentWidth, computed.MarginLeft);

            if (!marginRightAuto)
                marginRight = CalculateDimension(parentWidth, computed.MarginRight);

            var outerWidth = marginLeft + used.BorderLeftWidth + used.PaddingLeft + used.PaddingRight + used.BorderRightWidth + marginRight;

            if (widthAuto || (parentWidth < outerWidth && !widthAuto))
            {
                marginLeftAuto = false;
                marginRightAuto = false;
            }

            /*
             * At this point, we cannot have all three autos
             */
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
