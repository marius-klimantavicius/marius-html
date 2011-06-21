using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marius.Html.Css.Values
{
    /// <summary>
    /// Represents a device unit (all css length values are converted to device units).
    /// It is used to render as well (can be pixels, sub-pixels and so on...)
    /// </summary>
    public sealed class CssDeviceUnit
    {
        public double Value { get; private set; }

        public CssDeviceUnit(double value)
        {
            Value = value;
        }
    }
}
