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
using Marius.Html.Css.Parser;
using Marius.Html.Css.Dom;

namespace Marius.Html.Css
{
    public class CssContext
    {
        public CssContext()
        {
            FunctionFactory = new FunctionFactory();
            PseudoConditionFactory = new PseudoConditionFactory();

            #region Initialization of default property handlers
            Azimuth = new Azimuth();
            Background = new Background();
            BackgroundAttachment = new BackgroundAttachment();
            BackgroundColor = new BackgroundColor();
            BackgroundImage = new BackgroundImage();
            BackgroundPosition = new BackgroundPosition();
            BackgroundRepeat = new BackgroundRepeat();
            Border = new Border();
            BorderCollapse = new BorderCollapse();
            BorderColor = new BorderColor();
            BorderTopColor = new BorderTopColor();
            BorderRightColor = new BorderRightColor();
            BorderBottomColor = new BorderBottomColor();
            BorderLeftColor = new BorderLeftColor();
            BorderSpacing = new BorderSpacing();
            BorderStyle = new BorderStyle();
            BorderTopStyle = new BorderTopStyle();
            BorderRightStyle = new BorderRightStyle();
            BorderBottomStyle = new BorderBottomStyle();
            BorderLeftStyle = new BorderLeftStyle();
            BorderWidth = new BorderWidth();
            BorderTopWidth = new BorderTopWidth();
            BorderRightWidth = new BorderRightWidth();
            BorderBottomWidth = new BorderBottomWidth();
            BorderLeftWidth = new BorderLeftWidth();
            Bottom = new Bottom();
            CaptionSide = new CaptionSide();
            Clear = new Clear();
            Clip = new Clip();
            Color = new Color();
            Content = new Content();
            CounterReset = new CounterReset();
            CounterIncrement = new CounterIncrement();
            Cue = new Cue();
            CueAfter = new CueAfter();
            CueBefore = new CueBefore();
            Cursor = new Cursor();
            Direction = new Direction();
            Display = new Display();
            Elevation = new Elevation();
            EmptyCells = new EmptyCells();
            Float = new Float();
            Font = new Font();
            FontFamily = new FontFamily();
            FontSize = new FontSize();
            FontStyle = new FontStyle();
            FontVariant = new FontVariant();
            FontWeight = new FontWeight();
            Height = new Height();
            Left = new Left();
            LetterSpacing = new LetterSpacing();
            LineHeight = new LineHeight();
            ListStyle = new ListStyle();
            ListStyleImage = new ListStyleImage();
            ListStylePosition = new ListStylePosition();
            ListStyleType = new ListStyleType();
            Margin = new Margin();
            MarginTop = new MarginTop();
            MarginRight = new MarginRight();
            MarginBottom = new MarginBottom();
            MarginLeft = new MarginLeft();
            MaxHeight = new MaxHeight();
            MaxWidth = new MaxWidth();
            MinHeight = new MinHeight();
            MinWidth = new MinWidth();
            Orphans = new Orphans();
            Outline = new Outline();
            OutlineColor = new OutlineColor();
            OutlineStyle = new OutlineStyle();
            OutlineWidth = new OutlineWidth();
            Overflow = new Overflow();
            Padding = new Padding();
            PaddingTop = new PaddingTop();
            PaddingRight = new PaddingRight();
            PaddingBottom = new PaddingBottom();
            PaddingLeft = new PaddingLeft();
            PageBreakAfter = new PageBreakAfter();
            PageBreakBefore = new PageBreakBefore();
            PageBreakInside = new PageBreakInside();
            Pause = new Pause();
            PauseAfter = new PauseAfter();
            PauseBefore = new PauseBefore();
            Pitch = new Pitch();
            PitchRange = new PitchRange();
            PlayDuring = new PlayDuring();
            Position = new Position();
            Quotes = new Quotes();
            Richness = new Richness();
            Right = new Right();
            Speak = new Speak();
            SpeakHeader = new SpeakHeader();
            SpeakNumeral = new SpeakNumeral();
            SpeakPunctuation = new SpeakPunctuation();
            SpeechRate = new SpeechRate();
            Stress = new Stress();
            TableLayout = new TableLayout();
            TextAlign = new TextAlign();
            TextDecoration = new TextDecoration();
            TextIndent = new TextIndent();
            TextTransform = new TextTransform();
            Top = new Top();
            UnicodeBidi = new UnicodeBidi();
            VerticalAlign = new VerticalAlign();
            Visibility = new Visibility();
            VoiceFamily = new VoiceFamily();
            Volume = new Volume();
            WhiteSpace = new WhiteSpace();
            Widows = new Widows();
            Width = new Width();
            WordSpacing = new WordSpacing();
            ZIndex = new ZIndex();
            #endregion
        }

        public virtual FunctionFactory FunctionFactory { get; set; }
        public virtual PseudoConditionFactory PseudoConditionFactory { get; set; }

        #region Property handlers
        public virtual Azimuth Azimuth { get; private set; }
        public virtual Background Background { get; private set; }
        public virtual BackgroundAttachment BackgroundAttachment { get; private set; }
        public virtual BackgroundColor BackgroundColor { get; private set; }
        public virtual BackgroundImage BackgroundImage { get; private set; }
        public virtual BackgroundPosition BackgroundPosition { get; private set; }
        public virtual BackgroundRepeat BackgroundRepeat { get; private set; }
        public virtual Border Border { get; private set; }
        public virtual BorderCollapse BorderCollapse { get; private set; }
        public virtual BorderColor BorderColor { get; private set; }
        public virtual BorderTopColor BorderTopColor { get; private set; }
        public virtual BorderRightColor BorderRightColor { get; private set; }
        public virtual BorderBottomColor BorderBottomColor { get; private set; }
        public virtual BorderLeftColor BorderLeftColor { get; private set; }
        public virtual BorderSpacing BorderSpacing { get; private set; }
        public virtual BorderStyle BorderStyle { get; private set; }
        public virtual BorderTopStyle BorderTopStyle { get; private set; }
        public virtual BorderRightStyle BorderRightStyle { get; private set; }
        public virtual BorderBottomStyle BorderBottomStyle { get; private set; }
        public virtual BorderLeftStyle BorderLeftStyle { get; private set; }
        public virtual BorderWidth BorderWidth { get; private set; }
        public virtual BorderTopWidth BorderTopWidth { get; private set; }
        public virtual BorderRightWidth BorderRightWidth { get; private set; }
        public virtual BorderBottomWidth BorderBottomWidth { get; private set; }
        public virtual BorderLeftWidth BorderLeftWidth { get; private set; }
        public virtual Bottom Bottom { get; private set; }
        public virtual CaptionSide CaptionSide { get; private set; }
        public virtual Clear Clear { get; private set; }
        public virtual Clip Clip { get; private set; }
        public virtual Color Color { get; private set; }
        public virtual Content Content { get; private set; }
        public virtual CounterReset CounterReset { get; private set; }
        public virtual CounterIncrement CounterIncrement { get; private set; }
        public virtual Cue Cue { get; private set; }
        public virtual CueAfter CueAfter { get; private set; }
        public virtual CueBefore CueBefore { get; private set; }
        public virtual Cursor Cursor { get; private set; }
        public virtual Direction Direction { get; private set; }
        public virtual Display Display { get; private set; }
        public virtual Elevation Elevation { get; private set; }
        public virtual EmptyCells EmptyCells { get; private set; }
        public virtual Float Float { get; private set; }
        public virtual Font Font { get; private set; }
        public virtual FontFamily FontFamily { get; private set; }
        public virtual FontSize FontSize { get; private set; }
        public virtual FontStyle FontStyle { get; private set; }
        public virtual FontVariant FontVariant { get; private set; }
        public virtual FontWeight FontWeight { get; private set; }
        public virtual Height Height { get; private set; }
        public virtual Left Left { get; private set; }
        public virtual LetterSpacing LetterSpacing { get; private set; }
        public virtual LineHeight LineHeight { get; private set; }
        public virtual ListStyle ListStyle { get; private set; }
        public virtual ListStyleImage ListStyleImage { get; private set; }
        public virtual ListStylePosition ListStylePosition { get; private set; }
        public virtual ListStyleType ListStyleType { get; private set; }
        public virtual Margin Margin { get; private set; }
        public virtual MarginTop MarginTop { get; private set; }
        public virtual MarginRight MarginRight { get; private set; }
        public virtual MarginBottom MarginBottom { get; private set; }
        public virtual MarginLeft MarginLeft { get; private set; }
        public virtual MaxHeight MaxHeight { get; private set; }
        public virtual MaxWidth MaxWidth { get; private set; }
        public virtual MinHeight MinHeight { get; private set; }
        public virtual MinWidth MinWidth { get; private set; }
        public virtual Orphans Orphans { get; private set; }
        public virtual Outline Outline { get; private set; }
        public virtual OutlineColor OutlineColor { get; private set; }
        public virtual OutlineStyle OutlineStyle { get; private set; }
        public virtual OutlineWidth OutlineWidth { get; private set; }
        public virtual Overflow Overflow { get; private set; }
        public virtual Padding Padding { get; private set; }
        public virtual PaddingTop PaddingTop { get; private set; }
        public virtual PaddingRight PaddingRight { get; private set; }
        public virtual PaddingBottom PaddingBottom { get; private set; }
        public virtual PaddingLeft PaddingLeft { get; private set; }
        public virtual PageBreakAfter PageBreakAfter { get; private set; }
        public virtual PageBreakBefore PageBreakBefore { get; private set; }
        public virtual PageBreakInside PageBreakInside { get; private set; }
        public virtual Pause Pause { get; private set; }
        public virtual PauseAfter PauseAfter { get; private set; }
        public virtual PauseBefore PauseBefore { get; private set; }
        public virtual Pitch Pitch { get; private set; }
        public virtual PitchRange PitchRange { get; private set; }
        public virtual PlayDuring PlayDuring { get; private set; }
        public virtual Position Position { get; private set; }
        public virtual Quotes Quotes { get; private set; }
        public virtual Richness Richness { get; private set; }
        public virtual Right Right { get; private set; }
        public virtual Speak Speak { get; private set; }
        public virtual SpeakHeader SpeakHeader { get; private set; }
        public virtual SpeakNumeral SpeakNumeral { get; private set; }
        public virtual SpeakPunctuation SpeakPunctuation { get; private set; }
        public virtual SpeechRate SpeechRate { get; private set; }
        public virtual Stress Stress { get; private set; }
        public virtual TableLayout TableLayout { get; private set; }
        public virtual TextAlign TextAlign { get; private set; }
        public virtual TextDecoration TextDecoration { get; private set; }
        public virtual TextIndent TextIndent { get; private set; }
        public virtual TextTransform TextTransform { get; private set; }
        public virtual Top Top { get; private set; }
        public virtual UnicodeBidi UnicodeBidi { get; private set; }
        public virtual VerticalAlign VerticalAlign { get; private set; }
        public virtual Visibility Visibility { get; private set; }
        public virtual VoiceFamily VoiceFamily { get; private set; }
        public virtual Volume Volume { get; private set; }
        public virtual WhiteSpace WhiteSpace { get; private set; }
        public virtual Widows Widows { get; private set; }
        public virtual Width Width { get; private set; }
        public virtual WordSpacing WordSpacing { get; private set; }
        public virtual ZIndex ZIndex { get; private set; }
        #endregion
    }
}
