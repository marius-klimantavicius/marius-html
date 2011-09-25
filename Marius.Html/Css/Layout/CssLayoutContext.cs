using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Layout
{
    public abstract class CssLayoutContext
    {
        public abstract float Width { get; }
        public abstract float Height { get; }

        public abstract float ToDeviceUnits(CssValue value);
    }
}
