#region License
/*
Distributed under the terms of a MIT-style license:

The MIT License

Copyright (c) 2010 Marius Klimantavičius

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Marius.Html.Css.Values
{
    public class CssTextDecoration: CssValue
    {
        public bool Underline { get; private set; }
        public bool Overline { get; private set; }
        public bool LineThrough { get; private set; }
        public bool Blink { get; private set; }

        public override CssValueType ValueType
        {
            get { return CssValueType.TextDecoration; }
        }

        public CssTextDecoration(bool underline, bool overline, bool lineThrough, bool blink)
        {
            Underline = underline;
            Overline = overline;
            LineThrough = lineThrough;
            Blink = blink;
        }

        public override bool Equals(CssValue other)
        {
            CssTextDecoration o = other as CssTextDecoration;
            if (o == null)
                return false;

            return o.Underline == this.Underline && o.Overline == this.Overline && o.LineThrough == this.LineThrough && o.Blink == this.Blink;
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(Underline, Overline, LineThrough, Blink, ValueType);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            if (Underline)
                sb.Append(CssKeywords.Underline.ToString());

            if (Overline)
                sb.Append(" ").Append(CssKeywords.Overline.ToString());

            if (LineThrough)
                sb.Append(" ").Append(CssKeywords.LineThrough.ToString());

            if (Blink)
                sb.Append(" ").Append(CssKeywords.Blink.ToString());

            return sb.ToString();
        }
    }
}
