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

namespace Marius.Html.Css
{
    public static class CssProperty
    {
        public const string Azimuth = "azimuth";
        public const string Background = "background";
        public const string BackgroundAttachment = "background-attachment";
        public const string BackgroundColor = "background-color";
        public const string BackgroundImage = "background-image";
        public const string BackgroundPosition = "background-position";
        public const string BackgroundRepeat = "background-repeat";
        public const string Border = "border";
        public const string BorderCollapse = "border-collapse";
        public const string BorderColor = "border-color";
        public const string BorderTopColor = "border-top-color";
        public const string BorderRightColor = "border-right-color";
        public const string BorderBottomColor = "border-bottom-color";
        public const string BorderLeftColor = "border-left-color";
        public const string BorderSpacing = "border-spacing";
        public const string BorderStyle = "border-style";
        public const string BorderTopStyle = "border-top-style";
        public const string BorderRightStyle = "border-right-style";
        public const string BorderBottomStyle = "border-bottom-style";
        public const string BorderLeftStyle = "border-left-style";
        public const string BorderWidth = "border-width";
        public const string BorderTopWidth = "border-top-width";
        public const string BorderRightWidth = "border-right-width";
        public const string BorderBottomWidth = "border-bottom-width";
        public const string BorderLeftWidth = "border-left-width";
        public const string Bottom = "bottom";
        public const string CaptionSide = "caption-side";
        public const string Clear = "clear";
        public const string Clip = "clip";
        public const string Color = "color";
        public const string Content = "content";
        public const string CounterReset = "counter-reset";
        public const string CounterIncrement = "counter-increment";
        public const string Cue = "cue";
        public const string CueAfter = "cue-after";
        public const string CueBefore = "cue-before";
        public const string Cursor = "cursor";
        public const string Direction = "direction";
        public const string Display = "display";
        public const string Elevation = "elevation";
        public const string EmptyCells = "empty-cells";
        public const string Float = "float";
        public const string Font = "font";
        public const string FontFamily = "font-family";
        public const string FontSize = "font-size";
        public const string FontStyle = "font-style";
        public const string FontVariant = "font-variant";
        public const string FontWeight = "font-weight";
        public const string Height = "height";
        public const string Left = "left";
        public const string LetterSpacing = "letter-spacing";
        public const string LineHeight = "line-height";
        public const string ListStyle = "list-style";
        public const string ListStyleImage = "list-style-image";
        public const string ListStylePosition = "list-style-position";
        public const string ListStyleType = "list-style-type";
        public const string Margin = "margin";
        public const string MarginTop = "margin-top";
        public const string MarginRight = "margin-right";
        public const string MarginBottom = "margin-bottom";
        public const string MarginLeft = "margin-left";
        public const string MaxHeight = "max-height";
        public const string MaxWidth = "max-width";
        public const string MinHeight = "min-height";
        public const string MinWidth = "min-width";
        public const string Orphans = "orphans";
        public const string Outline = "outline";
        public const string OutlineColor = "outline-color";
        public const string OutlineStyle = "outline-style";
        public const string OutlineWidth = "outline-width";
        public const string Overflow = "overflow";
        public const string Padding = "padding";
        public const string PaddingTop = "padding-top";
        public const string PaddingRight = "padding-right";
        public const string PaddingBottom = "padding-bottom";
        public const string PaddingLeft = "padding-left";
        public const string PageBreakAfter = "page-break-after";
        public const string PageBreakBefore = "page-break-before";
        public const string PageBreakInside = "page-break-inside";
        public const string Pause = "pause";
        public const string PauseAfter = "pause-after";
        public const string PauseBefore = "pause-before";
        public const string Pitch = "pitch";
        public const string PitchRange = "pitch-range";
        public const string PlayDuring = "play-during";
        public const string Position = "position";
        public const string Quotes = "quotes";
        public const string Richness = "richness";
        public const string Right = "right";
        public const string Speak = "speak";
        public const string SpeakHeader = "speak-header";
        public const string SpeakNumeral = "speak-numeral";
        public const string SpeakPunctuation = "speak-punctuation";
        public const string SpeechRate = "speech-rate";
        public const string Stress = "stress";
        public const string TableLayout = "table-layout";
        public const string TextAlign = "text-align";
        public const string TextDecoration = "text-decoration";
        public const string TextIndent = "text-indent";
        public const string TextTransform = "text-transform";
        public const string Top = "top";
        public const string UnicodeBidi = "unicode-bidi";
        public const string VerticalAlign = "vertical-align";
        public const string Visibility = "visibility";
        public const string VoiceFamily = "voice-family";
        public const string Volume = "volume";
        public const string WhiteSpace = "white-space";
        public const string Widows = "widows";
        public const string Width = "width";
        public const string WordSpacing = "word-spacing";
        public const string ZIndex = "z-index";
    }
}
