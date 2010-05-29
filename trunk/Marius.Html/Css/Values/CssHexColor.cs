using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Marius.Html.Css.Values
{
    public class CssHexColor: CssColor
    {
        private static readonly Regex ColorHex = new Regex("([0-9a-f]{6})|([0-9a-f]{3})", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);

        private readonly string _value;

        public CssHexColor(string hex)
        {
            if (!ColorHex.IsMatch(hex))
                throw new ArgumentException();

            _value = hex;

            int color = Convert.ToInt32(hex, 16);

            if (hex.Length == 3)
            {
                int b = color & 0xF;
                int g = (color & 0xF0) >> 4;
                int r = (color & 0xF00) >> 8;

                Red = new CssNumber(r | (r << 4));
                Green = new CssNumber(g | (g << 4));
                Blue = new CssNumber(b | (b << 4));
            }
            else if (hex.Length == 6)
            {
                int b = color & 0xFF;
                int g = (color & 0xFF00) >> 8;
                int r = (color & 0xFF0000) >> 16;

                Red = new CssNumber(r);
                Green = new CssNumber(g);
                Blue = new CssNumber(b);
            }
            else
                throw new ArgumentException(); // should not reach
        }

        public override string ToString()
        {
            return string.Format("#{0}", _value);
        }
    }
}
