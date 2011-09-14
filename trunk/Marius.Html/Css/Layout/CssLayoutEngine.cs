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
            var top = Context.ToDeviceUnits(box.Computed.Top);
            var left = Context.ToDeviceUnits(box.Computed.Left);

            var parentWidth = CssDeviceUnit.Zero;

            //if (box.Parent == null)
            //    parentWidth = Context.Width;
            //else
            //    parentWidth = box.Parent.Actual.Width;

            var computed = box.Computed;

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
    }
}
