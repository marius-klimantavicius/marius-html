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
using Marius.Html.Css.Values;

namespace Marius.Html.Css
{
    public class CssBox
    {
        public CssProperty Azimuth { get; set; }
        public CssProperty BackgroundAttachment { get; set; }
        public CssProperty BackgroundColor { get; set; }
        public CssProperty BackgroundImage { get; set; }
        public CssProperty BackgroundPosition { get; set; }
        public CssProperty BackgroundRepeat { get; set; }
        public CssProperty BorderCollapse { get; set; }
        public CssProperty BorderTopColor { get; set; }
        public CssProperty BorderTopWidth { get; set; }
        public CssProperty BorderTopStyle { get; set; }
        public CssProperty BorderRightColor { get; set; }
        public CssProperty BorderRightWidth { get; set; }
        public CssProperty BorderRightStyle { get; set; }
        public CssProperty BorderBottomColor { get; set; }
        public CssProperty BorderBottomWidth { get; set; }
        public CssProperty BorderBottomStyle { get; set; }
        public CssProperty BorderLeftColor { get; set; }
        public CssProperty BorderLeftWidth { get; set; }
        public CssProperty BorderLeftStyle { get; set; }
        public CssProperty Bottom { get; set; }
        public CssProperty CaptionSide { get; set; }
        public CssProperty Clear { get; set; }
        public CssProperty Clip { get; set; }
        public CssProperty Color { get; set; }
        public CssProperty Content { get; set; }
        public CssProperty CounterIncrement { get; set; }
        public CssProperty CounterReset { get; set; }
        public CssProperty CueBefore { get; set; }
        public CssProperty CueAfter { get; set; }
        public CssProperty Direction { get; set; }
        public CssProperty Display { get; set; }
        public CssProperty Elevation { get; set; }
        public CssProperty EmptyCells { get; set; }
        public CssProperty Float { get; set; }
        public CssProperty FontSize { get; set; }
        public CssProperty FontStyle { get; set; }
        public CssProperty FontVariant { get; set; }
        public CssProperty FontWeight { get; set; }
        public CssProperty Height { get; set; }
        public CssProperty Left { get; set; }
        public CssProperty LineHeight { get; set; }
        public CssProperty ListStyleImage { get; set; }
        public CssProperty ListStylePosition { get; set; }
        public CssProperty ListStyleType { get; set; }
        public CssProperty MarginRight { get; set; }
        public CssProperty MarginLeft { get; set; }
        public CssProperty MarginTop { get; set; }
        public CssProperty MarginBottom { get; set; }
        public CssProperty MaxHeight { get; set; }
        public CssProperty MaxWidth { get; set; }
        public CssProperty MinHeight { get; set; }
        public CssProperty MinWidth { get; set; }
        public CssProperty Orphans { get; set; }
        public CssProperty OutlineColor { get; set; }
        public CssProperty OutlineStyle { get; set; }
        public CssProperty OutlineWidth { get; set; }
        public CssProperty Overflow { get; set; }
        public CssProperty PaddingTop { get; set; }
        public CssProperty PaddingRight { get; set; }
        public CssProperty PaddingBottom { get; set; }
        public CssProperty PaddingLeft { get; set; }
        public CssProperty PageBreakAfter { get; set; }
        public CssProperty PageBreakBefore { get; set; }
        public CssProperty PageBreakInside { get; set; }
        public CssProperty PauseAfter { get; set; }
        public CssProperty PauseBefore { get; set; }
        public CssProperty PitchRange { get; set; }
        public CssProperty Pitch { get; set; }
        public CssProperty PlayDuring { get; set; }
        public CssProperty Position { get; set; }
        public CssProperty Quotes { get; set; }
        public CssProperty Richness { get; set; }
        public CssProperty Right { get; set; }
        public CssProperty SpeakHeader { get; set; }
        public CssProperty SpeakNumeral { get; set; }
        public CssProperty SpeakPunctuation { get; set; }
        public CssProperty Speak { get; set; }
        public CssProperty SpeechRate { get; set; }
        public CssProperty Stress { get; set; }
        public CssProperty TableLayout { get; set; }
        public CssProperty TextAlign { get; set; }
        public CssProperty TextDecoration { get; set; }
        public CssProperty TextIndent { get; set; }
        public CssProperty TextTransform { get; set; }
        public CssProperty UnicodeBidi { get; set; }
        public CssProperty VerticalAlign { get; set; }
        public CssProperty Visibility { get; set; }
        public CssProperty VoiceFamily { get; set; }
        public CssProperty Volume { get; set; }
        public CssProperty WhiteSpace { get; set; }
        public CssProperty Widows { get; set; }
        public CssProperty Width { get; set; }
        public CssProperty WordSpacing { get; set; }
        public CssProperty ZIndex { get; set; }
    }
}
