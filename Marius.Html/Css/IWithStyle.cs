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
using Marius.Html.Css.Values;
namespace Marius.Html.Css
{
    public interface IWithStyle
    {
        CssValue Azimuth { get; set; }
        CssValue BackgroundAttachment { get; set; }
        CssValue BackgroundColor { get; set; }
        CssValue BackgroundImage { get; set; }
        CssValue BackgroundPosition { get; set; }
        CssValue BackgroundRepeat { get; set; }
        CssValue BorderBottomColor { get; set; }
        CssValue BorderBottomStyle { get; set; }
        CssValue BorderBottomWidth { get; set; }
        CssValue BorderCollapse { get; set; }
        CssValue BorderLeftColor { get; set; }
        CssValue BorderLeftStyle { get; set; }
        CssValue BorderLeftWidth { get; set; }
        CssValue BorderRightColor { get; set; }
        CssValue BorderRightStyle { get; set; }
        CssValue BorderRightWidth { get; set; }
        CssValue BorderSpacing { get; set; }
        CssValue BorderTopColor { get; set; }
        CssValue BorderTopStyle { get; set; }
        CssValue BorderTopWidth { get; set; }
        CssValue Bottom { get; set; }
        CssValue CaptionSide { get; set; }
        CssValue Clear { get; set; }
        CssValue Clip { get; set; }
        CssValue Color { get; set; }
        CssValue Content { get; set; }
        CssValue CounterIncrement { get; set; }
        CssValue CounterReset { get; set; }
        CssValue CueAfter { get; set; }
        CssValue CueBefore { get; set; }
        CssValue Cursor { get; set; }
        CssValue Direction { get; set; }
        CssValue Display { get; set; }
        CssValue Elevation { get; set; }
        CssValue EmptyCells { get; set; }
        CssValue Float { get; set; }
        CssValue Font { get; set; }
        CssValue FontFamily { get; set; }
        CssValue FontSize { get; set; }
        CssValue FontStyle { get; set; }
        CssValue FontVariant { get; set; }
        CssValue FontWeight { get; set; }
        CssValue Height { get; set; }
        CssValue Left { get; set; }
        CssValue LetterSpacing { get; set; }
        CssValue LineHeight { get; set; }
        CssValue ListStyleImage { get; set; }
        CssValue ListStylePosition { get; set; }
        CssValue ListStyleType { get; set; }
        CssValue MarginBottom { get; set; }
        CssValue MarginLeft { get; set; }
        CssValue MarginRight { get; set; }
        CssValue MarginTop { get; set; }
        CssValue MaxHeight { get; set; }
        CssValue MaxWidth { get; set; }
        CssValue MinHeight { get; set; }
        CssValue MinWidth { get; set; }
        CssValue Orphans { get; set; }
        CssValue OutlineColor { get; set; }
        CssValue OutlineStyle { get; set; }
        CssValue OutlineWidth { get; set; }
        CssValue Overflow { get; set; }
        CssValue PaddingBottom { get; set; }
        CssValue PaddingLeft { get; set; }
        CssValue PaddingRight { get; set; }
        CssValue PaddingTop { get; set; }
        CssValue PageBreakAfter { get; set; }
        CssValue PageBreakBefore { get; set; }
        CssValue PageBreakInside { get; set; }
        CssValue PauseAfter { get; set; }
        CssValue PauseBefore { get; set; }
        CssValue Pitch { get; set; }
        CssValue PitchRange { get; set; }
        CssValue PlayDuring { get; set; }
        CssValue Position { get; set; }
        CssValue Quotes { get; set; }
        CssValue Richness { get; set; }
        CssValue Right { get; set; }
        CssValue Speak { get; set; }
        CssValue SpeakHeader { get; set; }
        CssValue SpeakNumeral { get; set; }
        CssValue SpeakPunctuation { get; set; }
        CssValue SpeechRate { get; set; }
        CssValue Stress { get; set; }
        CssValue TableLayout { get; set; }
        CssValue TextAlign { get; set; }
        CssValue TextDecoration { get; set; }
        CssValue TextIndent { get; set; }
        CssValue TextTransform { get; set; }
        CssValue Top { get; set; }
        CssValue UnicodeBidi { get; set; }
        CssValue VerticalAlign { get; set; }
        CssValue Visibility { get; set; }
        CssValue VoiceFamily { get; set; }
        CssValue Volume { get; set; }
        CssValue WhiteSpace { get; set; }
        CssValue Widows { get; set; }
        CssValue Width { get; set; }
        CssValue WordSpacing { get; set; }
        CssValue ZIndex { get; set; }
    }
}
