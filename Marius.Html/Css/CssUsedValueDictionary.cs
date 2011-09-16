using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marius.Html.Css.Values;
using Marius.Html.Css.Layout;
using Marius.Html.Css.Box;

namespace Marius.Html.Css
{
    /// <summary>
    /// Some values are computed during layout (all dimension properties)
    /// </summary>
    public class CssUsedValueDictionary
    {
        private CssBox _box;

        public CssUsedValueDictionary(CssBox box)
        {
            _box = box;
        }

        public CssDeviceUnit BackgroundPositionVertical { get; private set; }
        public CssDeviceUnit BackgroundPositionHorizontal { get; set; }

        public CssDeviceUnit BorderSpacingVertical { get; set; }
        public CssDeviceUnit BorderSpacingHorizontal { get; set; }

        public CssDeviceUnit BorderTopWidth { get; set; }
        public CssDeviceUnit BorderLeftWidth { get; set; }
        public CssDeviceUnit BorderBottomWidth { get; set; }
        public CssDeviceUnit BorderRightWidth { get; set; }

        public CssDeviceUnit Top { get; set; }
        public CssDeviceUnit Left { get; set; }
        public CssDeviceUnit Bottom { get; set; }
        public CssDeviceUnit Right { get; set; }

        public CssDeviceUnit FontSize { get; set; }

        public CssDeviceUnit Width { get; set; }
        public CssDeviceUnit MaxWidth { get; set; }
        public CssDeviceUnit MinWidth { get; set; }

        public CssDeviceUnit Height { get; set; }
        public CssDeviceUnit MaxHeight { get; set; }
        public CssDeviceUnit MinHeight { get; set; }

        public CssDeviceUnit LineHeight { get; set; }

        public CssDeviceUnit MarginTop { get; set; }
        public CssDeviceUnit MarginLeft { get; set; }
        public CssDeviceUnit MarginBottom { get; set; }
        public CssDeviceUnit MarginRight { get; set; }

        public CssDeviceUnit OutlineWidth { get; set; }

        public CssDeviceUnit PaddingTop { get; set; }
        public CssDeviceUnit PaddingLeft { get; set; }
        public CssDeviceUnit PaddingBottom { get; set; }
        public CssDeviceUnit PaddingRight { get; set; }

        public CssDeviceUnit TextIndent { get; set; }

        public CssDeviceUnit OffsetX { get; set; }
        public CssDeviceUnit OffsetY { get; set; }

        public void Update(CssLayoutContext Context)
        {

        }
    }
}
