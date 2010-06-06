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
using Marius.Html.Css.Properties;

namespace Marius.Html.Css
{
    public class CssContext
    {
        public BackgroundAttachment BackgroundAttachment { get; set; }
        public BackgroundColor BackgroundColor { get; set; }
        public BackgroundImage BackgroundImage { get; set; }
        public BackgroundPosition BackgroundPosition { get; set; }
        public BackgroundRepeat BackgroundRepeat { get; set; }

        public Border Border { get; set; }

        public BorderColor BorderColor { get; set; }
        public BorderSideColor BorderTopColor { get; set; }
        public BorderSideColor BorderRightColor { get; set; }
        public BorderSideColor BorderBottomColor { get; set; }
        public BorderSideColor BorderLeftColor { get; set; }

        public BorderStyle BorderStyle { get; set; }
        public BorderSideStyle BorderTopStyle { get; set; }
        public BorderSideStyle BorderRightStyle { get; set; }
        public BorderSideStyle BorderBottomStyle { get; set; }
        public BorderSideStyle BorderLeftStyle { get; set; }

        public BorderWidth BorderWidth { get; set; }
        public BorderSideWidth BorderTopWidth { get; set; }
        public BorderSideWidth BorderRightWidth { get; set; }
        public BorderSideWidth BorderBottomWidth { get; set; }
        public BorderSideWidth BorderLeftWidth { get; set; }

        public CueValue CueBefore { get; set; }
        public CueValue CueAfter { get; set; }

        public FontStyle FontStyle { get; set; }
        public FontVariant FontVariant { get; set; }
        public FontSize FontSize { get; set; }
        public FontFamily FontFamily { get; set; }
        public FontWeight FontWeight { get; set; }

        public LineHeight LineHeight { get; set; }

        public ListStyleImage ListStyleImage { get; set; }
        public ListStylePosition ListStylePosition { get; set; }
        public ListStyleType ListStyleType { get; set; }

        public SideMargin MarginTop { get; set; }
        public SideMargin MarginRight { get; set; }
        public SideMargin MarginBottom { get; set; }
        public SideMargin MarginLeft { get; set; }

        public OutlineColor OutlineColor { get; set; }
        public OutlineStyle OutlineStyle { get; set; }
        public OutlineWidth OutlineWidth { get; set; }

        public SidePadding PaddingTop { get; set; }
        public SidePadding PaddingRight { get; set; }
        public SidePadding PaddingBottom { get; set; }
        public SidePadding PaddingLeft { get; set; }

        public PauseBefore PauseBefore { get; set; }
        public PauseAfter PauseAfter { get; set; }
    }
}
