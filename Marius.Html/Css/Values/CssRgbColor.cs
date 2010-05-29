using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Marius.Html.Css.Dom;

namespace Marius.Html.Css.Values
{
    public class CssRgbColor: CssColor
    {
        public CssRgbColor(CssFunction rgb)
        {
            CssValue red, green, blue;

            if (!Extract(rgb, out red, out green, out blue))
                throw new ArgumentException();

            Red = red;
            Green = green;
            Blue = blue;
        }

        public static bool Extract(CssFunction rgb, out CssValue red, out CssValue green, out CssValue blue)
        {
            red = null;
            green = null;
            blue = null;

            if (!rgb.Name.Equals("rgb", StringComparison.InvariantCultureIgnoreCase))
                return false;

            if (rgb.Arguments.Items.Length != 3)
                return false;

            CssPrimitiveValueType last = CssPrimitiveValueType.Unknown;

            CssValue[] color = new CssValue[3];
            for (int i = 0; i < color.Length; i++)
            {
                var item = rgb.Arguments.Items[i];
                color[i] = item.Value;
                if (i == 0 && item.Operator != CssOperator.Space)
                    return false;
                else if (item.Operator != CssOperator.Comma)
                    return false;

                if (item.Value.ValueType != CssValueType.Primitive)
                    return false;
                CssPrimitiveValue pval = (CssPrimitiveValue)item.Value;

                if (i == 0)
                {
                    last = pval.PrimitiveValueType;
                    if (last != CssPrimitiveValueType.Percentage && last != CssPrimitiveValueType.Number)
                        return false;
                }
                else
                {
                    if (pval.PrimitiveValueType != last)
                        return false;
                }
            }

            red = color[0];
            green = color[1];
            blue = color[2];

            return true;
        }
    }
}
