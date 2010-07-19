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
using Marius.Html.Css.Layout;

namespace Marius.Html.Css.Box
{
    // the actual values given during layout
    public partial class CssBox
    {
        // position
        public double Top { get; private set; }
        public double Right { get; private set; }
        public double Bottom { get; private set; }
        public double Left { get; private set; }

        // content
        public double Width { get; private set; }
        public double Heigth { get; private set; }

        // padding
        public double PaddingTop { get; private set; }
        public double PaddingRight { get; private set; }
        public double PaddingBottom { get; private set; }
        public double PaddingLeft { get; private set; }

        // margin
        public double MarginTop { get; private set; }
        public double MarginRight { get; private set; }
        public double MarginBottom { get; private set; }
        public double MarginLeft { get; private set; }

        // border
        public CssBoxBorder BorderTop { get; private set; }
        public CssBoxBorder BorderRight { get; private set; }
        public CssBoxBorder BorderBottom { get; private set; }
        public CssBoxBorder BorderLeft { get; private set; }
    }
}
