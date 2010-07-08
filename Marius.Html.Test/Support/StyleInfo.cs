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
using Marius.Html.Css;
using Marius.Html.Css.Values;

namespace Marius.Html.Tests.Support
{
    public class StyleInfo: IWithStyle
    {
        public CssValue Azimuth { get; set; }
        public CssValue BackgroundAttachment { get; set; }
        public CssValue BackgroundColor { get; set; }
        public CssValue BackgroundImage { get; set; }
        public CssValue BackgroundPosition { get; set; }
        public CssValue BackgroundRepeat { get; set; }
        public CssValue BorderBottomColor { get; set; }
        public CssValue BorderBottomStyle { get; set; }
        public CssValue BorderBottomWidth { get; set; }
        public CssValue BorderCollapse { get; set; }
        public CssValue BorderLeftColor { get; set; }
        public CssValue BorderLeftStyle { get; set; }
        public CssValue BorderLeftWidth { get; set; }
        public CssValue BorderRightColor { get; set; }
        public CssValue BorderRightStyle { get; set; }
        public CssValue BorderRightWidth { get; set; }
        public CssValue BorderSpacing { get; set; }
        public CssValue BorderTopColor { get; set; }
        public CssValue BorderTopStyle { get; set; }
        public CssValue BorderTopWidth { get; set; }
        public CssValue Bottom { get; set; }
        public CssValue CaptionSide { get; set; }
        public CssValue Clear { get; set; }
        public CssValue Clip { get; set; }
        public CssValue Color { get; set; }
        public CssValue Content { get; set; }
        public CssValue CounterIncrement { get; set; }
        public CssValue CounterReset { get; set; }
        public CssValue CueAfter { get; set; }
        public CssValue CueBefore { get; set; }
        public CssValue Cursor { get; set; }
        public CssValue Direction { get; set; }
        public CssValue Display { get; set; }
        public CssValue Elevation { get; set; }
        public CssValue EmptyCells { get; set; }
        public CssValue Float { get; set; }
        public CssValue Font { get; set; }
        public CssValue FontFamily { get; set; }
        public CssValue FontSize { get; set; }
        public CssValue FontStyle { get; set; }
        public CssValue FontVariant { get; set; }
        public CssValue FontWeight { get; set; }
        public CssValue Height { get; set; }
        public CssValue Left { get; set; }
        public CssValue LetterSpacing { get; set; }
        public CssValue LineHeight { get; set; }
        public CssValue ListStyleImage { get; set; }
        public CssValue ListStylePosition { get; set; }
        public CssValue ListStyleType { get; set; }
        public CssValue MarginBottom { get; set; }
        public CssValue MarginLeft { get; set; }
        public CssValue MarginRight { get; set; }
        public CssValue MarginTop { get; set; }
        public CssValue MaxHeight { get; set; }
        public CssValue MaxWidth { get; set; }
        public CssValue MinHeight { get; set; }
        public CssValue MinWidth { get; set; }
        public CssValue Orphans { get; set; }
        public CssValue OutlineColor { get; set; }
        public CssValue OutlineStyle { get; set; }
        public CssValue OutlineWidth { get; set; }
        public CssValue Overflow { get; set; }
        public CssValue PaddingBottom { get; set; }
        public CssValue PaddingLeft { get; set; }
        public CssValue PaddingRight { get; set; }
        public CssValue PaddingTop { get; set; }
        public CssValue PageBreakAfter { get; set; }
        public CssValue PageBreakBefore { get; set; }
        public CssValue PageBreakInside { get; set; }
        public CssValue PauseAfter { get; set; }
        public CssValue PauseBefore { get; set; }
        public CssValue Pitch { get; set; }
        public CssValue PitchRange { get; set; }
        public CssValue PlayDuring { get; set; }
        public CssValue Position { get; set; }
        public CssValue Quotes { get; set; }
        public CssValue Richness { get; set; }
        public CssValue Right { get; set; }
        public CssValue Speak { get; set; }
        public CssValue SpeakHeader { get; set; }
        public CssValue SpeakNumeral { get; set; }
        public CssValue SpeakPunctuation { get; set; }
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


        public bool HasStyle
        {
            get
            {
                return
                    Azimuth != null ||
                    BackgroundAttachment != null ||
                    BackgroundColor != null ||
                    BackgroundImage != null ||
                    BackgroundPosition != null ||
                    BackgroundRepeat != null ||
                    BorderBottomColor != null ||
                    BorderBottomStyle != null ||
                    BorderBottomWidth != null ||
                    BorderCollapse != null ||
                    BorderLeftColor != null ||
                    BorderLeftStyle != null ||
                    BorderLeftWidth != null ||
                    BorderRightColor != null ||
                    BorderRightStyle != null ||
                    BorderRightWidth != null ||
                    BorderSpacing != null ||
                    BorderTopColor != null ||
                    BorderTopStyle != null ||
                    BorderTopWidth != null ||
                    Bottom != null ||
                    CaptionSide != null ||
                    Clear != null ||
                    Clip != null ||
                    Color != null ||
                    Content != null ||
                    CounterIncrement != null ||
                    CounterReset != null ||
                    CueAfter != null ||
                    CueBefore != null ||
                    Cursor != null ||
                    Direction != null ||
                    Display != null ||
                    Elevation != null ||
                    EmptyCells != null ||
                    Float != null ||
                    Font != null ||
                    FontFamily != null ||
                    FontSize != null ||
                    FontStyle != null ||
                    FontVariant != null ||
                    FontWeight != null ||
                    Height != null ||
                    Left != null ||
                    LetterSpacing != null ||
                    LineHeight != null ||
                    ListStyleImage != null ||
                    ListStylePosition != null ||
                    ListStyleType != null ||
                    MarginBottom != null ||
                    MarginLeft != null ||
                    MarginRight != null ||
                    MarginTop != null ||
                    MaxHeight != null ||
                    MaxWidth != null ||
                    MinHeight != null ||
                    MinWidth != null ||
                    Orphans != null ||
                    OutlineColor != null ||
                    OutlineStyle != null ||
                    OutlineWidth != null ||
                    Overflow != null ||
                    PaddingBottom != null ||
                    PaddingLeft != null ||
                    PaddingRight != null ||
                    PaddingTop != null ||
                    PageBreakAfter != null ||
                    PageBreakBefore != null ||
                    PageBreakInside != null ||
                    PauseAfter != null ||
                    PauseBefore != null ||
                    Pitch != null ||
                    PitchRange != null ||
                    PlayDuring != null ||
                    Position != null ||
                    Quotes != null ||
                    Richness != null ||
                    Right != null ||
                    Speak != null ||
                    SpeakHeader != null ||
                    SpeakNumeral != null ||
                    SpeakPunctuation != null ||
                    SpeechRate != null ||
                    Stress != null ||
                    TableLayout != null ||
                    TextAlign != null ||
                    TextDecoration != null ||
                    TextIndent != null ||
                    TextTransform != null ||
                    Top != null ||
                    UnicodeBidi != null ||
                    VerticalAlign != null ||
                    Visibility != null ||
                    VoiceFamily != null ||
                    Volume != null ||
                    WhiteSpace != null ||
                    Widows != null ||
                    Width != null ||
                    WordSpacing != null ||
                    ZIndex != null;
            }
        }

        public void CopyTo(CssPropertyValueDictionary target)
        {
            if (Azimuth != null)
                target[CssProperty.Azimuth] = Azimuth;
            if (BackgroundAttachment != null)
                target[CssProperty.BackgroundAttachment] = BackgroundAttachment;
            if (BackgroundColor != null)
                target[CssProperty.BackgroundColor] = BackgroundColor;
            if (BackgroundImage != null)
                target[CssProperty.BackgroundImage] = BackgroundImage;
            if (BackgroundPosition != null)
                target[CssProperty.BackgroundPosition] = BackgroundPosition;
            if (BackgroundRepeat != null)
                target[CssProperty.BackgroundRepeat] = BackgroundRepeat;
            if (BorderBottomColor != null)
                target[CssProperty.BorderBottomColor] = BorderBottomColor;
            if (BorderBottomStyle != null)
                target[CssProperty.BorderBottomStyle] = BorderBottomStyle;
            if (BorderBottomWidth != null)
                target[CssProperty.BorderBottomWidth] = BorderBottomWidth;
            if (BorderCollapse != null)
                target[CssProperty.BorderCollapse] = BorderCollapse;
            if (BorderLeftColor != null)
                target[CssProperty.BorderLeftColor] = BorderLeftColor;
            if (BorderLeftStyle != null)
                target[CssProperty.BorderLeftStyle] = BorderLeftStyle;
            if (BorderLeftWidth != null)
                target[CssProperty.BorderLeftWidth] = BorderLeftWidth;
            if (BorderRightColor != null)
                target[CssProperty.BorderRightColor] = BorderRightColor;
            if (BorderRightStyle != null)
                target[CssProperty.BorderRightStyle] = BorderRightStyle;
            if (BorderRightWidth != null)
                target[CssProperty.BorderRightWidth] = BorderRightWidth;
            if (BorderSpacing != null)
                target[CssProperty.BorderSpacing] = BorderSpacing;
            if (BorderTopColor != null)
                target[CssProperty.BorderTopColor] = BorderTopColor;
            if (BorderTopStyle != null)
                target[CssProperty.BorderTopStyle] = BorderTopStyle;
            if (BorderTopWidth != null)
                target[CssProperty.BorderTopWidth] = BorderTopWidth;
            if (Bottom != null)
                target[CssProperty.Bottom] = Bottom;
            if (CaptionSide != null)
                target[CssProperty.CaptionSide] = CaptionSide;
            if (Clear != null)
                target[CssProperty.Clear] = Clear;
            if (Clip != null)
                target[CssProperty.Clip] = Clip;
            if (Color != null)
                target[CssProperty.Color] = Color;
            if (Content != null)
                target[CssProperty.Content] = Content;
            if (CounterIncrement != null)
                target[CssProperty.CounterIncrement] = CounterIncrement;
            if (CounterReset != null)
                target[CssProperty.CounterReset] = CounterReset;
            if (CueAfter != null)
                target[CssProperty.CueAfter] = CueAfter;
            if (CueBefore != null)
                target[CssProperty.CueBefore] = CueBefore;
            if (Cursor != null)
                target[CssProperty.Cursor] = Cursor;
            if (Direction != null)
                target[CssProperty.Direction] = Direction;
            if (Display != null)
                target[CssProperty.Display] = Display;
            if (Elevation != null)
                target[CssProperty.Elevation] = Elevation;
            if (EmptyCells != null)
                target[CssProperty.EmptyCells] = EmptyCells;
            if (Float != null)
                target[CssProperty.Float] = Float;
            if (Font != null)
                target[CssProperty.Font] = Font;
            if (FontFamily != null)
                target[CssProperty.FontFamily] = FontFamily;
            if (FontSize != null)
                target[CssProperty.FontSize] = FontSize;
            if (FontStyle != null)
                target[CssProperty.FontStyle] = FontStyle;
            if (FontVariant != null)
                target[CssProperty.FontVariant] = FontVariant;
            if (FontWeight != null)
                target[CssProperty.FontWeight] = FontWeight;
            if (Height != null)
                target[CssProperty.Height] = Height;
            if (Left != null)
                target[CssProperty.Left] = Left;
            if (LetterSpacing != null)
                target[CssProperty.LetterSpacing] = LetterSpacing;
            if (LineHeight != null)
                target[CssProperty.LineHeight] = LineHeight;
            if (ListStyleImage != null)
                target[CssProperty.ListStyleImage] = ListStyleImage;
            if (ListStylePosition != null)
                target[CssProperty.ListStylePosition] = ListStylePosition;
            if (ListStyleType != null)
                target[CssProperty.ListStyleType] = ListStyleType;
            if (MarginBottom != null)
                target[CssProperty.MarginBottom] = MarginBottom;
            if (MarginLeft != null)
                target[CssProperty.MarginLeft] = MarginLeft;
            if (MarginRight != null)
                target[CssProperty.MarginRight] = MarginRight;
            if (MarginTop != null)
                target[CssProperty.MarginTop] = MarginTop;
            if (MaxHeight != null)
                target[CssProperty.MaxHeight] = MaxHeight;
            if (MaxWidth != null)
                target[CssProperty.MaxWidth] = MaxWidth;
            if (MinHeight != null)
                target[CssProperty.MinHeight] = MinHeight;
            if (MinWidth != null)
                target[CssProperty.MinWidth] = MinWidth;
            if (Orphans != null)
                target[CssProperty.Orphans] = Orphans;
            if (OutlineColor != null)
                target[CssProperty.OutlineColor] = OutlineColor;
            if (OutlineStyle != null)
                target[CssProperty.OutlineStyle] = OutlineStyle;
            if (OutlineWidth != null)
                target[CssProperty.OutlineWidth] = OutlineWidth;
            if (Overflow != null)
                target[CssProperty.Overflow] = Overflow;
            if (PaddingBottom != null)
                target[CssProperty.PaddingBottom] = PaddingBottom;
            if (PaddingLeft != null)
                target[CssProperty.PaddingLeft] = PaddingLeft;
            if (PaddingRight != null)
                target[CssProperty.PaddingRight] = PaddingRight;
            if (PaddingTop != null)
                target[CssProperty.PaddingTop] = PaddingTop;
            if (PageBreakAfter != null)
                target[CssProperty.PageBreakAfter] = PageBreakAfter;
            if (PageBreakBefore != null)
                target[CssProperty.PageBreakBefore] = PageBreakBefore;
            if (PageBreakInside != null)
                target[CssProperty.PageBreakInside] = PageBreakInside;
            if (PauseAfter != null)
                target[CssProperty.PauseAfter] = PauseAfter;
            if (PauseBefore != null)
                target[CssProperty.PauseBefore] = PauseBefore;
            if (Pitch != null)
                target[CssProperty.Pitch] = Pitch;
            if (PitchRange != null)
                target[CssProperty.PitchRange] = PitchRange;
            if (PlayDuring != null)
                target[CssProperty.PlayDuring] = PlayDuring;
            if (Position != null)
                target[CssProperty.Position] = Position;
            if (Quotes != null)
                target[CssProperty.Quotes] = Quotes;
            if (Richness != null)
                target[CssProperty.Richness] = Richness;
            if (Right != null)
                target[CssProperty.Right] = Right;
            if (Speak != null)
                target[CssProperty.Speak] = Speak;
            if (SpeakHeader != null)
                target[CssProperty.SpeakHeader] = SpeakHeader;
            if (SpeakNumeral != null)
                target[CssProperty.SpeakNumeral] = SpeakNumeral;
            if (SpeakPunctuation != null)
                target[CssProperty.SpeakPunctuation] = SpeakPunctuation;
            if (SpeechRate != null)
                target[CssProperty.SpeechRate] = SpeechRate;
            if (Stress != null)
                target[CssProperty.Stress] = Stress;
            if (TableLayout != null)
                target[CssProperty.TableLayout] = TableLayout;
            if (TextAlign != null)
                target[CssProperty.TextAlign] = TextAlign;
            if (TextDecoration != null)
                target[CssProperty.TextDecoration] = TextDecoration;
            if (TextIndent != null)
                target[CssProperty.TextIndent] = TextIndent;
            if (TextTransform != null)
                target[CssProperty.TextTransform] = TextTransform;
            if (Top != null)
                target[CssProperty.Top] = Top;
            if (UnicodeBidi != null)
                target[CssProperty.UnicodeBidi] = UnicodeBidi;
            if (VerticalAlign != null)
                target[CssProperty.VerticalAlign] = VerticalAlign;
            if (Visibility != null)
                target[CssProperty.Visibility] = Visibility;
            if (VoiceFamily != null)
                target[CssProperty.VoiceFamily] = VoiceFamily;
            if (Volume != null)
                target[CssProperty.Volume] = Volume;
            if (WhiteSpace != null)
                target[CssProperty.WhiteSpace] = WhiteSpace;
            if (Widows != null)
                target[CssProperty.Widows] = Widows;
            if (Width != null)
                target[CssProperty.Width] = Width;
            if (WordSpacing != null)
                target[CssProperty.WordSpacing] = WordSpacing;
            if (ZIndex != null)
                target[CssProperty.ZIndex] = ZIndex;
        }
    }
}
