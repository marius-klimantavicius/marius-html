using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marius.Html.Css.Values
{
    /// <summary>
    /// Surrogate class to specify border-color value to be taken from current boxes color
    /// </summary>
    public class CssBoxColor: CssValue
    {
        public static readonly CssBoxColor Instance = new CssBoxColor();

        private CssBoxColor()
        {
        }

        public override CssValueType ValueType
        {
            get { return CssValueType.BoxColor; }
        }

        public override bool Equals(CssValue other)
        {
            CssBoxColor o = other as CssBoxColor;
            if (o == null)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(ValueType);
        }
    }
}
