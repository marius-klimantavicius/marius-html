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
        public static readonly CssDeviceUnit Zero = new CssDeviceUnit(0.0f);

        public float Value { get; private set; }

        public CssDeviceUnit(float value)
        {
            Value = value;
        }

        public static CssDeviceUnit operator +(CssDeviceUnit left, CssDeviceUnit right)
        {
            return new CssDeviceUnit(left.Value + right.Value);
        }

        public static CssDeviceUnit operator -(CssDeviceUnit left, CssDeviceUnit right)
        {
            return new CssDeviceUnit(left.Value - right.Value);
        }

        public static bool operator >(CssDeviceUnit left, CssDeviceUnit right)
        {
            return left.Value > right.Value;
        }

        public static bool operator <(CssDeviceUnit left, CssDeviceUnit right)
        {
            return left.Value < right.Value;
        }

        public static CssDeviceUnit operator *(CssDeviceUnit left, float right)
        {
            return new CssDeviceUnit(left.Value * right);
        }

        public static CssDeviceUnit operator /(CssDeviceUnit left, float right)
        {
            return new CssDeviceUnit(left.Value / right);
        }
    }
}
