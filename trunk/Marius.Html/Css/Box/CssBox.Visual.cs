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
using Marius.Html.Css.Layout;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Box
{
    // the used values given during layout
    public partial class CssBox
    {
        private CssBox _coordinateParent;

        // position (in parent coordinate system)
        public virtual CssBox CoordinateParent
        {
            get { return _coordinateParent ?? Parent; }
            set { _coordinateParent = value; }
        }
                             
        // location (in px?) 
        public virtual float Top { get; set; }
        public virtual float Right { get; set; }
        public virtual float Bottom { get; set; }
        public virtual float Left { get; set; }
                             
        // content           
        public virtual float Width { get; set; }
        public virtual float Heigth { get; set; }
                             
        // padding           
        public virtual float PaddingTop { get; set; }
        public virtual float PaddingRight { get; set; }
        public virtual float PaddingBottom { get; set; }
        public virtual float PaddingLeft { get; set; }
                             
        // margin            
        public virtual float MarginTop { get; set; }
        public virtual float MarginRight { get; set; }
        public virtual float MarginBottom { get; set; }
        public virtual float MarginLeft { get; set; }

        // border
        public virtual CssBoxBorder BorderTop { get; set; }
        public virtual CssBoxBorder BorderRight { get; set; }
        public virtual CssBoxBorder BorderBottom { get; set; }
        public virtual CssBoxBorder BorderLeft { get; set; }
    }
}