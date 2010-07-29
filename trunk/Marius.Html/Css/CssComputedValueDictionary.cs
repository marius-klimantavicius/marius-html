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
using Marius.Html.Css.Box;
using System.Diagnostics;

namespace Marius.Html.Css
{
    public class CssComputedValueDictionary
    {
        private CssBox _box;

        public CssComputedValueDictionary(CssBox box)
        {
            _box = box;
        }

        public virtual CssValue this[string property]
        {
            get
            {
                CssSimplePropertyHandler handler = _box.Context.Properties[property] as CssSimplePropertyHandler;
                if (handler == null)
                    return null;

                CssValue computed = handler.Compute(_box);
                Debug.Assert(computed != null);
                return computed;
            }
        }

        #region Properties
        public virtual CssValue Azimuth
        {
            get { return this[CssProperty.Azimuth]; }
        }
        public virtual CssValue BackgroundAttachment
        {
            get { return this[CssProperty.BackgroundAttachment]; }
        }
        public virtual CssValue BackgroundColor
        {
            get { return this[CssProperty.BackgroundColor]; }
        }
        public virtual CssValue BackgroundImage
        {
            get { return this[CssProperty.BackgroundImage]; }
        }
        public virtual CssValue BackgroundPosition
        {
            get { return this[CssProperty.BackgroundPosition]; }
        }
        public virtual CssValue BackgroundRepeat
        {
            get { return this[CssProperty.BackgroundRepeat]; }
        }
        public virtual CssValue BorderCollapse
        {
            get { return this[CssProperty.BorderCollapse]; }
        }
        public virtual CssValue BorderSpacing
        {
            get { return this[CssProperty.BorderSpacing]; }
        }
        public virtual CssValue BorderTopColor
        {
            get { return this[CssProperty.BorderTopColor]; }
        }
        public virtual CssValue BorderTopWidth
        {
            get { return this[CssProperty.BorderTopWidth]; }
        }
        public virtual CssValue BorderTopStyle
        {
            get { return this[CssProperty.BorderTopStyle]; }
        }
        public virtual CssValue BorderRightColor
        {
            get { return this[CssProperty.BorderRightColor]; }
        }
        public virtual CssValue BorderRightWidth
        {
            get { return this[CssProperty.BorderRightWidth]; }
        }
        public virtual CssValue BorderRightStyle
        {
            get { return this[CssProperty.BorderRightStyle]; }
        }
        public virtual CssValue BorderBottomColor
        {
            get { return this[CssProperty.BorderBottomColor]; }
        }
        public virtual CssValue BorderBottomWidth
        {
            get { return this[CssProperty.BorderBottomWidth]; }
        }
        public virtual CssValue BorderBottomStyle
        {
            get { return this[CssProperty.BorderBottomStyle]; }
        }
        public virtual CssValue BorderLeftColor
        {
            get { return this[CssProperty.BorderLeftColor]; }
        }
        public virtual CssValue BorderLeftWidth
        {
            get { return this[CssProperty.BorderLeftWidth]; }
        }
        public virtual CssValue BorderLeftStyle
        {
            get { return this[CssProperty.BorderLeftStyle]; }
        }
        public virtual CssValue Bottom
        {
            get { return this[CssProperty.Bottom]; }
        }
        public virtual CssValue CaptionSide
        {
            get { return this[CssProperty.CaptionSide]; }
        }
        public virtual CssValue Clear
        {
            get { return this[CssProperty.Clear]; }
        }
        public virtual CssValue Clip
        {
            get { return this[CssProperty.Clip]; }
        }
        public virtual CssValue Color
        {
            get { return this[CssProperty.Color]; }
        }
        public virtual CssValue Content
        {
            get { return this[CssProperty.Content]; }
        }
        public virtual CssValue CounterIncrement
        {
            get { return this[CssProperty.CounterIncrement]; }
        }
        public virtual CssValue CounterReset
        {
            get { return this[CssProperty.CounterReset]; }
        }
        public virtual CssValue CueBefore
        {
            get { return this[CssProperty.CueBefore]; }
        }
        public virtual CssValue CueAfter
        {
            get { return this[CssProperty.CueAfter]; }
        }
        public virtual CssValue Cursor
        {
            get { return this[CssProperty.Cursor]; }
        }
        public virtual CssValue Direction
        {
            get { return this[CssProperty.Direction]; }
        }
        public virtual CssValue Display
        {
            get { return this[CssProperty.Display]; }
        }
        public virtual CssValue Elevation
        {
            get { return this[CssProperty.Elevation]; }
        }
        public virtual CssValue EmptyCells
        {
            get { return this[CssProperty.EmptyCells]; }
        }
        public virtual CssValue Float
        {
            get { return this[CssProperty.Float]; }
        }
        public virtual CssValue FontFamily
        {
            get { return this[CssProperty.FontFamily]; }
        }
        public virtual CssValue FontSize
        {
            get { return this[CssProperty.FontSize]; }
        }
        public virtual CssValue FontStyle
        {
            get { return this[CssProperty.FontStyle]; }
        }
        public virtual CssValue FontVariant
        {
            get { return this[CssProperty.FontVariant]; }
        }
        public virtual CssValue FontWeight
        {
            get { return this[CssProperty.FontWeight]; }
        }
        public virtual CssValue Font
        {
            get { return this[CssProperty.Font]; }
        }
        public virtual CssValue Height
        {
            get { return this[CssProperty.Height]; }
        }
        public virtual CssValue Left
        {
            get { return this[CssProperty.Left]; }
        }
        public virtual CssValue LetterSpacing
        {
            get { return this[CssProperty.LetterSpacing]; }
        }
        public virtual CssValue LineHeight
        {
            get { return this[CssProperty.LineHeight]; }
        }
        public virtual CssValue ListStyleImage
        {
            get { return this[CssProperty.ListStyleImage]; }
        }
        public virtual CssValue ListStylePosition
        {
            get { return this[CssProperty.ListStylePosition]; }
        }
        public virtual CssValue ListStyleType
        {
            get { return this[CssProperty.ListStyleType]; }
        }
        public virtual CssValue MarginRight
        {
            get { return this[CssProperty.MarginRight]; }
        }
        public virtual CssValue MarginLeft
        {
            get { return this[CssProperty.MarginLeft]; }
        }
        public virtual CssValue MarginTop
        {
            get { return this[CssProperty.MarginTop]; }
        }
        public virtual CssValue MarginBottom
        {
            get { return this[CssProperty.MarginBottom]; }
        }
        public virtual CssValue MaxHeight
        {
            get { return this[CssProperty.MaxHeight]; }
        }
        public virtual CssValue MaxWidth
        {
            get { return this[CssProperty.MaxWidth]; }
        }
        public virtual CssValue MinHeight
        {
            get { return this[CssProperty.MinHeight]; }
        }
        public virtual CssValue MinWidth
        {
            get { return this[CssProperty.MinWidth]; }
        }
        public virtual CssValue Orphans
        {
            get { return this[CssProperty.Orphans]; }
        }
        public virtual CssValue OutlineColor
        {
            get { return this[CssProperty.OutlineColor]; }
        }
        public virtual CssValue OutlineStyle
        {
            get { return this[CssProperty.OutlineStyle]; }
        }
        public virtual CssValue OutlineWidth
        {
            get { return this[CssProperty.OutlineWidth]; }
        }
        public virtual CssValue Overflow
        {
            get { return this[CssProperty.Overflow]; }
        }
        public virtual CssValue PaddingTop
        {
            get { return this[CssProperty.PaddingTop]; }
        }
        public virtual CssValue PaddingRight
        {
            get { return this[CssProperty.PaddingRight]; }
        }
        public virtual CssValue PaddingBottom
        {
            get { return this[CssProperty.PaddingBottom]; }
        }
        public virtual CssValue PaddingLeft
        {
            get { return this[CssProperty.PaddingLeft]; }
        }
        public virtual CssValue PageBreakAfter
        {
            get { return this[CssProperty.PageBreakAfter]; }
        }
        public virtual CssValue PageBreakBefore
        {
            get { return this[CssProperty.PageBreakBefore]; }
        }
        public virtual CssValue PageBreakInside
        {
            get { return this[CssProperty.PageBreakInside]; }
        }
        public virtual CssValue PauseAfter
        {
            get { return this[CssProperty.PauseAfter]; }
        }
        public virtual CssValue PauseBefore
        {
            get { return this[CssProperty.PauseBefore]; }
        }
        public virtual CssValue PitchRange
        {
            get { return this[CssProperty.PitchRange]; }
        }
        public virtual CssValue Pitch
        {
            get { return this[CssProperty.Pitch]; }
        }
        public virtual CssValue PlayDuring
        {
            get { return this[CssProperty.PlayDuring]; }
        }
        public virtual CssValue Position
        {
            get { return this[CssProperty.Position]; }
        }
        public virtual CssValue Quotes
        {
            get { return this[CssProperty.Quotes]; }
        }
        public virtual CssValue Richness
        {
            get { return this[CssProperty.Richness]; }
        }
        public virtual CssValue Right
        {
            get { return this[CssProperty.Right]; }
        }
        public virtual CssValue SpeakHeader
        {
            get { return this[CssProperty.SpeakHeader]; }
        }
        public virtual CssValue SpeakNumeral
        {
            get { return this[CssProperty.SpeakNumeral]; }
        }
        public virtual CssValue SpeakPunctuation
        {
            get { return this[CssProperty.SpeakPunctuation]; }
        }
        public virtual CssValue Speak
        {
            get { return this[CssProperty.Speak]; }
        }
        public virtual CssValue SpeechRate
        {
            get { return this[CssProperty.SpeechRate]; }
        }
        public virtual CssValue Stress
        {
            get { return this[CssProperty.Stress]; }
        }
        public virtual CssValue TableLayout
        {
            get { return this[CssProperty.TableLayout]; }
        }
        public virtual CssValue TextAlign
        {
            get { return this[CssProperty.TextAlign]; }
        }
        public virtual CssValue TextDecoration
        {
            get { return this[CssProperty.TextDecoration]; }
        }
        public virtual CssValue TextIndent
        {
            get { return this[CssProperty.TextIndent]; }
        }
        public virtual CssValue TextTransform
        {
            get { return this[CssProperty.TextTransform]; }
        }
        public virtual CssValue Top
        {
            get { return this[CssProperty.Top]; }
        }
        public virtual CssValue UnicodeBidi
        {
            get { return this[CssProperty.UnicodeBidi]; }
        }
        public virtual CssValue VerticalAlign
        {
            get { return this[CssProperty.VerticalAlign]; }
        }
        public virtual CssValue Visibility
        {
            get { return this[CssProperty.Visibility]; }
        }
        public virtual CssValue VoiceFamily
        {
            get { return this[CssProperty.VoiceFamily]; }
        }
        public virtual CssValue Volume
        {
            get { return this[CssProperty.Volume]; }
        }
        public virtual CssValue WhiteSpace
        {
            get { return this[CssProperty.WhiteSpace]; }
        }
        public virtual CssValue Widows
        {
            get { return this[CssProperty.Widows]; }
        }
        public virtual CssValue Width
        {
            get { return this[CssProperty.Width]; }
        }
        public virtual CssValue WordSpacing
        {
            get { return this[CssProperty.WordSpacing]; }
        }
        public virtual CssValue ZIndex
        {
            get { return this[CssProperty.ZIndex]; }
        }
        #endregion
    }
}
