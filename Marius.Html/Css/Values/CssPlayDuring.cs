﻿#region License
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
using Marius.Html.Internal;

namespace Marius.Html.Css.Values
{
    public class CssPlayDuring: CssValue
    {
        public CssValue Uri { get; private set; }
        public bool Mix { get; private set; }
        public bool Repeat { get; private set; }

        public override CssValueType ValueType
        {
            get { return CssValueType.PlayDuring; }
        }

        public CssPlayDuring(CssValue uri, bool mix, bool repeat)
        {
            Uri = uri;
            Mix = mix;
            Repeat = repeat;
        }

        public override bool Equals(CssValue other)
        {
            CssPlayDuring o = other as CssPlayDuring;
            if (o == null)
                return false;

            return o.Uri.Equals(this.Uri) && o.Mix == this.Mix && o.Repeat == this.Repeat;
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(Uri, Mix, Repeat, ValueType);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(Uri.ToString());

            if (Mix)
                sb.Append(" ").Append(CssKeywords.Mix.ToString());

            if (Repeat)
                sb.Append(" ").Append(CssKeywords.Repeat.ToString());

            return sb.ToString();
        }
    }
}
