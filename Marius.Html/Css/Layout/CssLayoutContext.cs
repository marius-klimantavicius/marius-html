using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Layout
{
    public abstract class CssLayoutContext
    {
        public abstract CssDeviceUnit Width { get; }
        public abstract CssDeviceUnit Height { get; }

        public abstract CssDeviceUnit ToDeviceUnits(CssValue value);
    }
}
