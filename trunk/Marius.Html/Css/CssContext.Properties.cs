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
    public partial class CssContext
    {
        protected virtual void InitProperties()
        {
            #region Initialization of default property handlers
            Properties.Add(new Azimuth(this));
            Properties.Add(new Background(this));
            Properties.Add(new BackgroundAttachment(this));
            Properties.Add(new BackgroundColor(this));
            Properties.Add(new BackgroundImage(this));
            Properties.Add(new BackgroundPosition(this));
            Properties.Add(new BackgroundRepeat(this));
            Properties.Add(new Border(this));
            Properties.Add(new BorderCollapse(this));
            Properties.Add(new BorderColor(this));
            Properties.Add(new BorderTopColor(this));
            Properties.Add(new BorderRightColor(this));
            Properties.Add(new BorderBottomColor(this));
            Properties.Add(new BorderLeftColor(this));
            Properties.Add(new BorderSpacing(this));
            Properties.Add(new BorderStyle(this));
            Properties.Add(new BorderTopStyle(this));
            Properties.Add(new BorderRightStyle(this));
            Properties.Add(new BorderBottomStyle(this));
            Properties.Add(new BorderLeftStyle(this));
            Properties.Add(new BorderWidth(this));
            Properties.Add(new BorderTopWidth(this));
            Properties.Add(new BorderRightWidth(this));
            Properties.Add(new BorderBottomWidth(this));
            Properties.Add(new BorderLeftWidth(this));
            Properties.Add(new Bottom(this));
            Properties.Add(new CaptionSide(this));
            Properties.Add(new Clear(this));
            Properties.Add(new Clip(this));
            Properties.Add(new Color(this));
            Properties.Add(new Content(this));
            Properties.Add(new CounterReset(this));
            Properties.Add(new CounterIncrement(this));
            Properties.Add(new Cue(this));
            Properties.Add(new CueAfter(this));
            Properties.Add(new CueBefore(this));
            Properties.Add(new Cursor(this));
            Properties.Add(new Direction(this));
            Properties.Add(new Display(this));
            Properties.Add(new Elevation(this));
            Properties.Add(new EmptyCells(this));
            Properties.Add(new Float(this));
            Properties.Add(new Font(this));
            Properties.Add(new FontFamily(this));
            Properties.Add(new FontSize(this));
            Properties.Add(new FontStyle(this));
            Properties.Add(new FontVariant(this));
            Properties.Add(new FontWeight(this));
            Properties.Add(new Height(this));
            Properties.Add(new Left(this));
            Properties.Add(new LetterSpacing(this));
            Properties.Add(new LineHeight(this));
            Properties.Add(new ListStyle(this));
            Properties.Add(new ListStyleImage(this));
            Properties.Add(new ListStylePosition(this));
            Properties.Add(new ListStyleType(this));
            Properties.Add(new Margin(this));
            Properties.Add(new MarginTop(this));
            Properties.Add(new MarginRight(this));
            Properties.Add(new MarginBottom(this));
            Properties.Add(new MarginLeft(this));
            Properties.Add(new MaxHeight(this));
            Properties.Add(new MaxWidth(this));
            Properties.Add(new MinHeight(this));
            Properties.Add(new MinWidth(this));
            Properties.Add(new Orphans(this));
            Properties.Add(new Outline(this));
            Properties.Add(new OutlineColor(this));
            Properties.Add(new OutlineStyle(this));
            Properties.Add(new OutlineWidth(this));
            Properties.Add(new Overflow(this));
            Properties.Add(new Padding(this));
            Properties.Add(new PaddingTop(this));
            Properties.Add(new PaddingRight(this));
            Properties.Add(new PaddingBottom(this));
            Properties.Add(new PaddingLeft(this));
            Properties.Add(new PageBreakAfter(this));
            Properties.Add(new PageBreakBefore(this));
            Properties.Add(new PageBreakInside(this));
            Properties.Add(new Pause(this));
            Properties.Add(new PauseAfter(this));
            Properties.Add(new PauseBefore(this));
            Properties.Add(new Pitch(this));
            Properties.Add(new PitchRange(this));
            Properties.Add(new PlayDuring(this));
            Properties.Add(new Position(this));
            Properties.Add(new Quotes(this));
            Properties.Add(new Richness(this));
            Properties.Add(new Right(this));
            Properties.Add(new Speak(this));
            Properties.Add(new SpeakHeader(this));
            Properties.Add(new SpeakNumeral(this));
            Properties.Add(new SpeakPunctuation(this));
            Properties.Add(new SpeechRate(this));
            Properties.Add(new Stress(this));
            Properties.Add(new TableLayout(this));
            Properties.Add(new TextAlign(this));
            Properties.Add(new TextDecoration(this));
            Properties.Add(new TextIndent(this));
            Properties.Add(new TextTransform(this));
            Properties.Add(new Top(this));
            Properties.Add(new UnicodeBidi(this));
            Properties.Add(new VerticalAlign(this));
            Properties.Add(new Visibility(this));
            Properties.Add(new VoiceFamily(this));
            Properties.Add(new Volume(this));
            Properties.Add(new WhiteSpace(this));
            Properties.Add(new Widows(this));
            Properties.Add(new Width(this));
            Properties.Add(new WordSpacing(this));
            Properties.Add(new ZIndex(this));
            #endregion
        }

        #region Properties
        public virtual Azimuth Azimuth { get { return (Azimuth)Properties[CssProperty.Azimuth]; } }
        public virtual Background Background { get { return (Background)Properties[CssProperty.Background]; } }
        public virtual BackgroundAttachment BackgroundAttachment { get { return (BackgroundAttachment)Properties[CssProperty.BackgroundAttachment]; } }
        public virtual BackgroundColor BackgroundColor { get { return (BackgroundColor)Properties[CssProperty.BackgroundColor]; } }
        public virtual BackgroundImage BackgroundImage { get { return (BackgroundImage)Properties[CssProperty.BackgroundImage]; } }
        public virtual BackgroundPosition BackgroundPosition { get { return (BackgroundPosition)Properties[CssProperty.BackgroundPosition]; } }
        public virtual BackgroundRepeat BackgroundRepeat { get { return (BackgroundRepeat)Properties[CssProperty.BackgroundRepeat]; } }
        public virtual Border Border { get { return (Border)Properties[CssProperty.Border]; } }
        public virtual BorderCollapse BorderCollapse { get { return (BorderCollapse)Properties[CssProperty.BorderCollapse]; } }
        public virtual BorderColor BorderColor { get { return (BorderColor)Properties[CssProperty.BorderColor]; } }
        public virtual BorderTopColor BorderTopColor { get { return (BorderTopColor)Properties[CssProperty.BorderTopColor]; } }
        public virtual BorderRightColor BorderRightColor { get { return (BorderRightColor)Properties[CssProperty.BorderRightColor]; } }
        public virtual BorderBottomColor BorderBottomColor { get { return (BorderBottomColor)Properties[CssProperty.BorderBottomColor]; } }
        public virtual BorderLeftColor BorderLeftColor { get { return (BorderLeftColor)Properties[CssProperty.BorderLeftColor]; } }
        public virtual BorderSpacing BorderSpacing { get { return (BorderSpacing)Properties[CssProperty.BorderSpacing]; } }
        public virtual BorderStyle BorderStyle { get { return (BorderStyle)Properties[CssProperty.BorderStyle]; } }
        public virtual BorderTopStyle BorderTopStyle { get { return (BorderTopStyle)Properties[CssProperty.BorderTopStyle]; } }
        public virtual BorderRightStyle BorderRightStyle { get { return (BorderRightStyle)Properties[CssProperty.BorderRightStyle]; } }
        public virtual BorderBottomStyle BorderBottomStyle { get { return (BorderBottomStyle)Properties[CssProperty.BorderBottomStyle]; } }
        public virtual BorderLeftStyle BorderLeftStyle { get { return (BorderLeftStyle)Properties[CssProperty.BorderLeftStyle]; } }
        public virtual BorderWidth BorderWidth { get { return (BorderWidth)Properties[CssProperty.BorderWidth]; } }
        public virtual BorderTopWidth BorderTopWidth { get { return (BorderTopWidth)Properties[CssProperty.BorderTopWidth]; } }
        public virtual BorderRightWidth BorderRightWidth { get { return (BorderRightWidth)Properties[CssProperty.BorderRightWidth]; } }
        public virtual BorderBottomWidth BorderBottomWidth { get { return (BorderBottomWidth)Properties[CssProperty.BorderBottomWidth]; } }
        public virtual BorderLeftWidth BorderLeftWidth { get { return (BorderLeftWidth)Properties[CssProperty.BorderLeftWidth]; } }
        public virtual Bottom Bottom { get { return (Bottom)Properties[CssProperty.Bottom]; } }
        public virtual CaptionSide CaptionSide { get { return (CaptionSide)Properties[CssProperty.CaptionSide]; } }
        public virtual Clear Clear { get { return (Clear)Properties[CssProperty.Clear]; } }
        public virtual Clip Clip { get { return (Clip)Properties[CssProperty.Clip]; } }
        public virtual Color Color { get { return (Color)Properties[CssProperty.Color]; } }
        public virtual Content Content { get { return (Content)Properties[CssProperty.Content]; } }
        public virtual CounterReset CounterReset { get { return (CounterReset)Properties[CssProperty.CounterReset]; } }
        public virtual CounterIncrement CounterIncrement { get { return (CounterIncrement)Properties[CssProperty.CounterIncrement]; } }
        public virtual Cue Cue { get { return (Cue)Properties[CssProperty.Cue]; } }
        public virtual CueAfter CueAfter { get { return (CueAfter)Properties[CssProperty.CueAfter]; } }
        public virtual CueBefore CueBefore { get { return (CueBefore)Properties[CssProperty.CueBefore]; } }
        public virtual Cursor Cursor { get { return (Cursor)Properties[CssProperty.Cursor]; } }
        public virtual Direction Direction { get { return (Direction)Properties[CssProperty.Direction]; } }
        public virtual Display Display { get { return (Display)Properties[CssProperty.Display]; } }
        public virtual Elevation Elevation { get { return (Elevation)Properties[CssProperty.Elevation]; } }
        public virtual EmptyCells EmptyCells { get { return (EmptyCells)Properties[CssProperty.EmptyCells]; } }
        public virtual Float Float { get { return (Float)Properties[CssProperty.Float]; } }
        public virtual Font Font { get { return (Font)Properties[CssProperty.Font]; } }
        public virtual FontFamily FontFamily { get { return (FontFamily)Properties[CssProperty.FontFamily]; } }
        public virtual FontSize FontSize { get { return (FontSize)Properties[CssProperty.FontSize]; } }
        public virtual FontStyle FontStyle { get { return (FontStyle)Properties[CssProperty.FontStyle]; } }
        public virtual FontVariant FontVariant { get { return (FontVariant)Properties[CssProperty.FontVariant]; } }
        public virtual FontWeight FontWeight { get { return (FontWeight)Properties[CssProperty.FontWeight]; } }
        public virtual Height Height { get { return (Height)Properties[CssProperty.Height]; } }
        public virtual Left Left { get { return (Left)Properties[CssProperty.Left]; } }
        public virtual LetterSpacing LetterSpacing { get { return (LetterSpacing)Properties[CssProperty.LetterSpacing]; } }
        public virtual LineHeight LineHeight { get { return (LineHeight)Properties[CssProperty.LineHeight]; } }
        public virtual ListStyle ListStyle { get { return (ListStyle)Properties[CssProperty.ListStyle]; } }
        public virtual ListStyleImage ListStyleImage { get { return (ListStyleImage)Properties[CssProperty.ListStyleImage]; } }
        public virtual ListStylePosition ListStylePosition { get { return (ListStylePosition)Properties[CssProperty.ListStylePosition]; } }
        public virtual ListStyleType ListStyleType { get { return (ListStyleType)Properties[CssProperty.ListStyleType]; } }
        public virtual Margin Margin { get { return (Margin)Properties[CssProperty.Margin]; } }
        public virtual MarginTop MarginTop { get { return (MarginTop)Properties[CssProperty.MarginTop]; } }
        public virtual MarginRight MarginRight { get { return (MarginRight)Properties[CssProperty.MarginRight]; } }
        public virtual MarginBottom MarginBottom { get { return (MarginBottom)Properties[CssProperty.MarginBottom]; } }
        public virtual MarginLeft MarginLeft { get { return (MarginLeft)Properties[CssProperty.MarginLeft]; } }
        public virtual MaxHeight MaxHeight { get { return (MaxHeight)Properties[CssProperty.MaxHeight]; } }
        public virtual MaxWidth MaxWidth { get { return (MaxWidth)Properties[CssProperty.MaxWidth]; } }
        public virtual MinHeight MinHeight { get { return (MinHeight)Properties[CssProperty.MinHeight]; } }
        public virtual MinWidth MinWidth { get { return (MinWidth)Properties[CssProperty.MinWidth]; } }
        public virtual Orphans Orphans { get { return (Orphans)Properties[CssProperty.Orphans]; } }
        public virtual Outline Outline { get { return (Outline)Properties[CssProperty.Outline]; } }
        public virtual OutlineColor OutlineColor { get { return (OutlineColor)Properties[CssProperty.OutlineColor]; } }
        public virtual OutlineStyle OutlineStyle { get { return (OutlineStyle)Properties[CssProperty.OutlineStyle]; } }
        public virtual OutlineWidth OutlineWidth { get { return (OutlineWidth)Properties[CssProperty.OutlineWidth]; } }
        public virtual Overflow Overflow { get { return (Overflow)Properties[CssProperty.Overflow]; } }
        public virtual Padding Padding { get { return (Padding)Properties[CssProperty.Padding]; } }
        public virtual PaddingTop PaddingTop { get { return (PaddingTop)Properties[CssProperty.PaddingTop]; } }
        public virtual PaddingRight PaddingRight { get { return (PaddingRight)Properties[CssProperty.PaddingRight]; } }
        public virtual PaddingBottom PaddingBottom { get { return (PaddingBottom)Properties[CssProperty.PaddingBottom]; } }
        public virtual PaddingLeft PaddingLeft { get { return (PaddingLeft)Properties[CssProperty.PaddingLeft]; } }
        public virtual PageBreakAfter PageBreakAfter { get { return (PageBreakAfter)Properties[CssProperty.PageBreakAfter]; } }
        public virtual PageBreakBefore PageBreakBefore { get { return (PageBreakBefore)Properties[CssProperty.PageBreakBefore]; } }
        public virtual PageBreakInside PageBreakInside { get { return (PageBreakInside)Properties[CssProperty.PageBreakInside]; } }
        public virtual Pause Pause { get { return (Pause)Properties[CssProperty.Pause]; } }
        public virtual PauseAfter PauseAfter { get { return (PauseAfter)Properties[CssProperty.PauseAfter]; } }
        public virtual PauseBefore PauseBefore { get { return (PauseBefore)Properties[CssProperty.PauseBefore]; } }
        public virtual Pitch Pitch { get { return (Pitch)Properties[CssProperty.Pitch]; } }
        public virtual PitchRange PitchRange { get { return (PitchRange)Properties[CssProperty.PitchRange]; } }
        public virtual PlayDuring PlayDuring { get { return (PlayDuring)Properties[CssProperty.PlayDuring]; } }
        public virtual Position Position { get { return (Position)Properties[CssProperty.Position]; } }
        public virtual Quotes Quotes { get { return (Quotes)Properties[CssProperty.Quotes]; } }
        public virtual Richness Richness { get { return (Richness)Properties[CssProperty.Richness]; } }
        public virtual Right Right { get { return (Right)Properties[CssProperty.Right]; } }
        public virtual Speak Speak { get { return (Speak)Properties[CssProperty.Speak]; } }
        public virtual SpeakHeader SpeakHeader { get { return (SpeakHeader)Properties[CssProperty.SpeakHeader]; } }
        public virtual SpeakNumeral SpeakNumeral { get { return (SpeakNumeral)Properties[CssProperty.SpeakNumeral]; } }
        public virtual SpeakPunctuation SpeakPunctuation { get { return (SpeakPunctuation)Properties[CssProperty.SpeakPunctuation]; } }
        public virtual SpeechRate SpeechRate { get { return (SpeechRate)Properties[CssProperty.SpeechRate]; } }
        public virtual Stress Stress { get { return (Stress)Properties[CssProperty.Stress]; } }
        public virtual TableLayout TableLayout { get { return (TableLayout)Properties[CssProperty.TableLayout]; } }
        public virtual TextAlign TextAlign { get { return (TextAlign)Properties[CssProperty.TextAlign]; } }
        public virtual TextDecoration TextDecoration { get { return (TextDecoration)Properties[CssProperty.TextDecoration]; } }
        public virtual TextIndent TextIndent { get { return (TextIndent)Properties[CssProperty.TextIndent]; } }
        public virtual TextTransform TextTransform { get { return (TextTransform)Properties[CssProperty.TextTransform]; } }
        public virtual Top Top { get { return (Top)Properties[CssProperty.Top]; } }
        public virtual UnicodeBidi UnicodeBidi { get { return (UnicodeBidi)Properties[CssProperty.UnicodeBidi]; } }
        public virtual VerticalAlign VerticalAlign { get { return (VerticalAlign)Properties[CssProperty.VerticalAlign]; } }
        public virtual Visibility Visibility { get { return (Visibility)Properties[CssProperty.Visibility]; } }
        public virtual VoiceFamily VoiceFamily { get { return (VoiceFamily)Properties[CssProperty.VoiceFamily]; } }
        public virtual Volume Volume { get { return (Volume)Properties[CssProperty.Volume]; } }
        public virtual WhiteSpace WhiteSpace { get { return (WhiteSpace)Properties[CssProperty.WhiteSpace]; } }
        public virtual Widows Widows { get { return (Widows)Properties[CssProperty.Widows]; } }
        public virtual Width Width { get { return (Width)Properties[CssProperty.Width]; } }
        public virtual WordSpacing WordSpacing { get { return (WordSpacing)Properties[CssProperty.WordSpacing]; } }
        public virtual ZIndex ZIndex { get { return (ZIndex)Properties[CssProperty.ZIndex]; } }
        #endregion
    }
}
