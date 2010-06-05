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
        public CssValue Azimuth { get; set; }
        public CssValue BackgroundAttachment { get; set; }
        public CssValue BackgroundColor { get; set; }
        public CssValue BackgroundImage { get; set; }
        public CssValue BackgroundPosition { get; set; }
        public CssValue BackgroundRepeat { get; set; }
        public CssValue BorderCollapse { get; set; }
        public CssValue BorderSpacing { get; set; }
        public CssValue BorderTopColor { get; set; }
        public CssValue BorderTopWidth { get; set; }
        public CssValue BorderTopStyle { get; set; }
        public CssValue BorderRightColor { get; set; }
        public CssValue BorderRightWidth { get; set; }
        public CssValue BorderRightStyle { get; set; }
        public CssValue BorderBottomColor { get; set; }
        public CssValue BorderBottomWidth { get; set; }
        public CssValue BorderBottomStyle { get; set; }
        public CssValue BorderLeftColor { get; set; }
        public CssValue BorderLeftWidth { get; set; }
        public CssValue BorderLeftStyle { get; set; }
        public CssValue Bottom { get; set; }
        public CssValue CaptionSide { get; set; }
        public CssValue Clear { get; set; }
        public CssValue Clip { get; set; }
        public CssValue Color { get; set; }
        public CssValue Content { get; set; }
        public CssValue CounterIncrement { get; set; }
        public CssValue CounterReset { get; set; }
        public CssValue CueBefore { get; set; }
        public CssValue CueAfter { get; set; }
        public CssValue Direction { get; set; }
        public CssValue Display { get; set; }
        public CssValue Elevation { get; set; }
        public CssValue EmptyCells { get; set; }
        public CssValue Float { get; set; }
        public CssValue FontSize { get; set; }
        public CssValue FontStyle { get; set; }
        public CssValue FontVariant { get; set; }
        public CssValue FontWeight { get; set; }
        public CssValue Height { get; set; }
        public CssValue Left { get; set; }
        public CssValue LineHeight { get; set; }
        public CssValue ListStyleImage { get; set; }
        public CssValue ListStylePosition { get; set; }
        public CssValue ListStyleType { get; set; }
        public CssValue MarginRight { get; set; }
        public CssValue MarginLeft { get; set; }
        public CssValue MarginTop { get; set; }
        public CssValue MarginBottom { get; set; }
        public CssValue MaxHeight { get; set; }
        public CssValue MaxWidth { get; set; }
        public CssValue MinHeight { get; set; }
        public CssValue MinWidth { get; set; }
        public CssValue Orphans { get; set; }
        public CssValue OutlineColor { get; set; }
        public CssValue OutlineStyle { get; set; }
        public CssValue OutlineWidth { get; set; }
        public CssValue Overflow { get; set; }
        public CssValue PaddingTop { get; set; }
        public CssValue PaddingRight { get; set; }
        public CssValue PaddingBottom { get; set; }
        public CssValue PaddingLeft { get; set; }
        public CssValue PageBreakAfter { get; set; }
        public CssValue PageBreakBefore { get; set; }
        public CssValue PageBreakInside { get; set; }
        public CssValue PauseAfter { get; set; }
        public CssValue PauseBefore { get; set; }
        public CssValue PitchRange { get; set; }
        public CssValue Pitch { get; set; }
        public CssValue PlayDuring { get; set; }
        public CssValue Position { get; set; }
        public CssValue Quotes { get; set; }
        public CssValue Richness { get; set; }
        public CssValue Right { get; set; }
        public CssValue SpeakHeader { get; set; }
        public CssValue SpeakNumeral { get; set; }
        public CssValue SpeakPunctuation { get; set; }
        public CssValue Speak { get; set; }
        public CssValue SpeechRate { get; set; }
        public CssValue Stress { get; set; }
        public CssValue TableLayout { get; set; }
        public CssValue TextAlign { get; set; }
        public CssValue TextDecoration { get; set; }
        public CssValue TextIndent { get; set; }
        public CssValue TextTransform { get; set; }
        public CssValue Top { get; set; }
        public CssValue UnicodeBidi { get; set; }
        public CssValue VerticalAlign { get; set; }
        public CssValue Visibility { get; set; }
        public CssValue VoiceFamily { get; set; }
        public CssValue Volume { get; set; }
        public CssValue WhiteSpace { get; set; }
        public CssValue Widows { get; set; }
        public CssValue Width { get; set; }
        public CssValue WordSpacing { get; set; }
        public CssValue ZIndex { get; set; }
    }
}
