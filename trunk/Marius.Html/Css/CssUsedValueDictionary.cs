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

        public float BackgroundPositionVertical { get; private set; }
        public float BackgroundPositionHorizontal { get; set; }

        public float BorderSpacingVertical { get; set; }
        public float BorderSpacingHorizontal { get; set; }

        public float BorderTopWidth { get; set; }
        public float BorderLeftWidth { get; set; }
        public float BorderBottomWidth { get; set; }
        public float BorderRightWidth { get; set; }

        public float Top { get; set; }
        public float Left { get; set; }
        public float Bottom { get; set; }
        public float Right { get; set; }

        public float FontSize { get; set; }

        public float Width { get; set; }
        public float MaxWidth { get; set; }
        public float MinWidth { get; set; }

        public float Height { get; set; }
        public float MaxHeight { get; set; }
        public float MinHeight { get; set; }

        public float LineHeight { get; set; }

        public float MarginTop { get; set; }
        public float MarginLeft { get; set; }
        public float MarginBottom { get; set; }
        public float MarginRight { get; set; }

        public float OutlineWidth { get; set; }

        public float PaddingTop { get; set; }
        public float PaddingLeft { get; set; }
        public float PaddingBottom { get; set; }
        public float PaddingRight { get; set; }

        public float TextIndent { get; set; }

        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
    }
}
