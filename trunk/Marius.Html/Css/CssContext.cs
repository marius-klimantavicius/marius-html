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
using System.Net;
using System.IO;
using Marius.Html.Css.Cascade;

namespace Marius.Html.Css
{
    public class CssContext
    {
        public const string All = "all";
        public const string Screen = "screen";

        public CssContext()
        {
            FunctionFactory = new FunctionFactory();
            PseudoConditionFactory = new PseudoConditionFactory();

            #region Initialization of default property handlers
            Azimuth = new Azimuth(this);
            Background = new Background(this);
            BackgroundAttachment = new BackgroundAttachment(this);
            BackgroundColor = new BackgroundColor(this);
            BackgroundImage = new BackgroundImage(this);
            BackgroundPosition = new BackgroundPosition(this);
            BackgroundRepeat = new BackgroundRepeat(this);
            Border = new Border(this);
            BorderCollapse = new BorderCollapse(this);
            BorderColor = new BorderColor(this);
            BorderTopColor = new BorderTopColor(this);
            BorderRightColor = new BorderRightColor(this);
            BorderBottomColor = new BorderBottomColor(this);
            BorderLeftColor = new BorderLeftColor(this);
            BorderSpacing = new BorderSpacing(this);
            BorderStyle = new BorderStyle(this);
            BorderTopStyle = new BorderTopStyle(this);
            BorderRightStyle = new BorderRightStyle(this);
            BorderBottomStyle = new BorderBottomStyle(this);
            BorderLeftStyle = new BorderLeftStyle(this);
            BorderWidth = new BorderWidth(this);
            BorderTopWidth = new BorderTopWidth(this);
            BorderRightWidth = new BorderRightWidth(this);
            BorderBottomWidth = new BorderBottomWidth(this);
            BorderLeftWidth = new BorderLeftWidth(this);
            Bottom = new Bottom(this);
            CaptionSide = new CaptionSide(this);
            Clear = new Clear(this);
            Clip = new Clip(this);
            Color = new Color(this);
            Content = new Content(this);
            CounterReset = new CounterReset(this);
            CounterIncrement = new CounterIncrement(this);
            Cue = new Cue(this);
            CueAfter = new CueAfter(this);
            CueBefore = new CueBefore(this);
            Cursor = new Cursor(this);
            Direction = new Direction(this);
            Display = new Display(this);
            Elevation = new Elevation(this);
            EmptyCells = new EmptyCells(this);
            Float = new Float(this);
            Font = new Font(this);
            FontFamily = new FontFamily(this);
            FontSize = new FontSize(this);
            FontStyle = new FontStyle(this);
            FontVariant = new FontVariant(this);
            FontWeight = new FontWeight(this);
            Height = new Height(this);
            Left = new Left(this);
            LetterSpacing = new LetterSpacing(this);
            LineHeight = new LineHeight(this);
            ListStyle = new ListStyle(this);
            ListStyleImage = new ListStyleImage(this);
            ListStylePosition = new ListStylePosition(this);
            ListStyleType = new ListStyleType(this);
            Margin = new Margin(this);
            MarginTop = new MarginTop(this);
            MarginRight = new MarginRight(this);
            MarginBottom = new MarginBottom(this);
            MarginLeft = new MarginLeft(this);
            MaxHeight = new MaxHeight(this);
            MaxWidth = new MaxWidth(this);
            MinHeight = new MinHeight(this);
            MinWidth = new MinWidth(this);
            Orphans = new Orphans(this);
            Outline = new Outline(this);
            OutlineColor = new OutlineColor(this);
            OutlineStyle = new OutlineStyle(this);
            OutlineWidth = new OutlineWidth(this);
            Overflow = new Overflow(this);
            Padding = new Padding(this);
            PaddingTop = new PaddingTop(this);
            PaddingRight = new PaddingRight(this);
            PaddingBottom = new PaddingBottom(this);
            PaddingLeft = new PaddingLeft(this);
            PageBreakAfter = new PageBreakAfter(this);
            PageBreakBefore = new PageBreakBefore(this);
            PageBreakInside = new PageBreakInside(this);
            Pause = new Pause(this);
            PauseAfter = new PauseAfter(this);
            PauseBefore = new PauseBefore(this);
            Pitch = new Pitch(this);
            PitchRange = new PitchRange(this);
            PlayDuring = new PlayDuring(this);
            Position = new Position(this);
            Quotes = new Quotes(this);
            Richness = new Richness(this);
            Right = new Right(this);
            Speak = new Speak(this);
            SpeakHeader = new SpeakHeader(this);
            SpeakNumeral = new SpeakNumeral(this);
            SpeakPunctuation = new SpeakPunctuation(this);
            SpeechRate = new SpeechRate(this);
            Stress = new Stress(this);
            TableLayout = new TableLayout(this);
            TextAlign = new TextAlign(this);
            TextDecoration = new TextDecoration(this);
            TextIndent = new TextIndent(this);
            TextTransform = new TextTransform(this);
            Top = new Top(this);
            UnicodeBidi = new UnicodeBidi(this);
            VerticalAlign = new VerticalAlign(this);
            Visibility = new Visibility(this);
            VoiceFamily = new VoiceFamily(this);
            Volume = new Volume(this);
            WhiteSpace = new WhiteSpace(this);
            Widows = new Widows(this);
            Width = new Width(this);
            WordSpacing = new WordSpacing(this);
            ZIndex = new ZIndex(this);
            #endregion
        }

        public virtual FunctionFactory FunctionFactory { get; set; }
        public virtual PseudoConditionFactory PseudoConditionFactory { get; set; }
        public virtual int MaxImportDetpth { get { return 20; } }
        public virtual IComparer<CssPreparedStyle> StyleComparer { get { return DefaultStyleComparer.Instance; } }

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

        public virtual bool IsMediaSupported(string[] media)
        {
            if (media == null || media.Length == 0)
                return true;

            for (int i = 0; i < media.Length; i++)
            {
                if (All.Equals(media[i], StringComparison.InvariantCultureIgnoreCase)
                    || Screen.Equals(media[i], StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public CssStylesheet ImportStylesheet(string uri, CssStylesheetSource stylesheetSource)
        {
            try
            {
                string source = null;

                var request = WebRequest.Create(uri);
                var response = request.GetResponse();

                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    source = reader.ReadToEnd();
                }

                return CssStylesheet.Parse(this, source, stylesheetSource);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual CssPreparedStylesheet PrepareStylesheets(params CssStylesheet[] stylesheets)
        {
            StyleCacheManager manager = new StyleCacheManager(this, stylesheets);
            return manager.Prepare();
        }
    }
}
