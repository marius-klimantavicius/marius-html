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
using System.Runtime.CompilerServices;

namespace Marius.Html.Css
{
    public class CssBox
    {
        private CssPropertyValueDictionary _properties;
        private CssContext _context;

        public CssBox(CssContext context)
        {
            _context = context;
            _properties = new CssPropertyValueDictionary(_context);
        }

        public CssBox Parent { get; set; }
        public CssBox FirstChild { get; set; }
        public CssBox NextSibling { get; set; }

        public Element Element { get; set; }

        public CssPropertyValueDictionary Properties { get; private set; }

        #region Properties
        public virtual CssValue Azimuth
        {
            get { return _properties[CssProperty.Azimuth]; }
            set { _properties[CssProperty.Azimuth] = value; }
        }
        public virtual CssValue BackgroundAttachment
        {
            get { return _properties[CssProperty.BackgroundAttachment]; }
            set { _properties[CssProperty.BackgroundAttachment] = value; }
        }
        public virtual CssValue BackgroundColor
        {
            get { return _properties[CssProperty.BackgroundColor]; }
            set { _properties[CssProperty.BackgroundColor] = value; }
        }
        public virtual CssValue BackgroundImage
        {
            get { return _properties[CssProperty.BackgroundImage]; }
            set { _properties[CssProperty.BackgroundImage] = value; }
        }
        public virtual CssValue BackgroundPosition
        {
            get { return _properties[CssProperty.BackgroundPosition]; }
            set { _properties[CssProperty.BackgroundPosition] = value; }
        }
        public virtual CssValue BackgroundRepeat
        {
            get { return _properties[CssProperty.BackgroundRepeat]; }
            set { _properties[CssProperty.BackgroundRepeat] = value; }
        }
        public virtual CssValue BorderCollapse
        {
            get { return _properties[CssProperty.BorderCollapse]; }
            set { _properties[CssProperty.BorderCollapse] = value; }
        }
        public virtual CssValue BorderSpacing
        {
            get { return _properties[CssProperty.BorderSpacing]; }
            set { _properties[CssProperty.BorderSpacing] = value; }
        }
        public virtual CssValue BorderTopColor
        {
            get { return _properties[CssProperty.BorderTopColor]; }
            set { _properties[CssProperty.BorderTopColor] = value; }
        }
        public virtual CssValue BorderTopWidth
        {
            get { return _properties[CssProperty.BorderTopWidth]; }
            set { _properties[CssProperty.BorderTopWidth] = value; }
        }
        public virtual CssValue BorderTopStyle
        {
            get { return _properties[CssProperty.BorderTopStyle]; }
            set { _properties[CssProperty.BorderTopStyle] = value; }
        }
        public virtual CssValue BorderRightColor
        {
            get { return _properties[CssProperty.BorderRightColor]; }
            set { _properties[CssProperty.BorderRightColor] = value; }
        }
        public virtual CssValue BorderRightWidth
        {
            get { return _properties[CssProperty.BorderRightWidth]; }
            set { _properties[CssProperty.BorderRightWidth] = value; }
        }
        public virtual CssValue BorderRightStyle
        {
            get { return _properties[CssProperty.BorderRightStyle]; }
            set { _properties[CssProperty.BorderRightStyle] = value; }
        }
        public virtual CssValue BorderBottomColor
        {
            get { return _properties[CssProperty.BorderBottomColor]; }
            set { _properties[CssProperty.BorderBottomColor] = value; }
        }
        public virtual CssValue BorderBottomWidth
        {
            get { return _properties[CssProperty.BorderBottomWidth]; }
            set { _properties[CssProperty.BorderBottomWidth] = value; }
        }
        public virtual CssValue BorderBottomStyle
        {
            get { return _properties[CssProperty.BorderBottomStyle]; }
            set { _properties[CssProperty.BorderBottomStyle] = value; }
        }
        public virtual CssValue BorderLeftColor
        {
            get { return _properties[CssProperty.BorderLeftColor]; }
            set { _properties[CssProperty.BorderLeftColor] = value; }
        }
        public virtual CssValue BorderLeftWidth
        {
            get { return _properties[CssProperty.BorderLeftWidth]; }
            set { _properties[CssProperty.BorderLeftWidth] = value; }
        }
        public virtual CssValue BorderLeftStyle
        {
            get { return _properties[CssProperty.BorderLeftStyle]; }
            set { _properties[CssProperty.BorderLeftStyle] = value; }
        }
        public virtual CssValue Bottom
        {
            get { return _properties[CssProperty.Bottom]; }
            set { _properties[CssProperty.Bottom] = value; }
        }
        public virtual CssValue CaptionSide
        {
            get { return _properties[CssProperty.CaptionSide]; }
            set { _properties[CssProperty.CaptionSide] = value; }
        }
        public virtual CssValue Clear
        {
            get { return _properties[CssProperty.Clear]; }
            set { _properties[CssProperty.Clear] = value; }
        }
        public virtual CssValue Clip
        {
            get { return _properties[CssProperty.Clip]; }
            set { _properties[CssProperty.Clip] = value; }
        }
        public virtual CssValue Color
        {
            get { return _properties[CssProperty.Color]; }
            set { _properties[CssProperty.Color] = value; }
        }
        public virtual CssValue Content
        {
            get { return _properties[CssProperty.Content]; }
            set { _properties[CssProperty.Content] = value; }
        }
        public virtual CssValue CounterIncrement
        {
            get { return _properties[CssProperty.CounterIncrement]; }
            set { _properties[CssProperty.CounterIncrement] = value; }
        }
        public virtual CssValue CounterReset
        {
            get { return _properties[CssProperty.CounterReset]; }
            set { _properties[CssProperty.CounterReset] = value; }
        }
        public virtual CssValue CueBefore
        {
            get { return _properties[CssProperty.CueBefore]; }
            set { _properties[CssProperty.CueBefore] = value; }
        }
        public virtual CssValue CueAfter
        {
            get { return _properties[CssProperty.CueAfter]; }
            set { _properties[CssProperty.CueAfter] = value; }
        }
        public virtual CssValue Cursor
        {
            get { return _properties[CssProperty.Cursor]; }
            set { _properties[CssProperty.Cursor] = value; }
        }
        public virtual CssValue Direction
        {
            get { return _properties[CssProperty.Direction]; }
            set { _properties[CssProperty.Direction] = value; }
        }
        public virtual CssValue Display
        {
            get { return _properties[CssProperty.Display]; }
            set { _properties[CssProperty.Display] = value; }
        }
        public virtual CssValue Elevation
        {
            get { return _properties[CssProperty.Elevation]; }
            set { _properties[CssProperty.Elevation] = value; }
        }
        public virtual CssValue EmptyCells
        {
            get { return _properties[CssProperty.EmptyCells]; }
            set { _properties[CssProperty.EmptyCells] = value; }
        }
        public virtual CssValue Float
        {
            get { return _properties[CssProperty.Float]; }
            set { _properties[CssProperty.Float] = value; }
        }
        public virtual CssValue FontFamily
        {
            get { return _properties[CssProperty.FontFamily]; }
            set { _properties[CssProperty.FontFamily] = value; }
        }
        public virtual CssValue FontSize
        {
            get { return _properties[CssProperty.FontSize]; }
            set { _properties[CssProperty.FontSize] = value; }
        }
        public virtual CssValue FontStyle
        {
            get { return _properties[CssProperty.FontStyle]; }
            set { _properties[CssProperty.FontStyle] = value; }
        }
        public virtual CssValue FontVariant
        {
            get { return _properties[CssProperty.FontVariant]; }
            set { _properties[CssProperty.FontVariant] = value; }
        }
        public virtual CssValue FontWeight
        {
            get { return _properties[CssProperty.FontWeight]; }
            set { _properties[CssProperty.FontWeight] = value; }
        }
        public virtual CssValue Font
        {
            get { return _properties[CssProperty.Font]; }
            set { _properties[CssProperty.Font] = value; }
        }
        public virtual CssValue Height
        {
            get { return _properties[CssProperty.Height]; }
            set { _properties[CssProperty.Height] = value; }
        }
        public virtual CssValue Left
        {
            get { return _properties[CssProperty.Left]; }
            set { _properties[CssProperty.Left] = value; }
        }
        public virtual CssValue LetterSpacing
        {
            get { return _properties[CssProperty.LetterSpacing]; }
            set { _properties[CssProperty.LetterSpacing] = value; }
        }
        public virtual CssValue LineHeight
        {
            get { return _properties[CssProperty.LineHeight]; }
            set { _properties[CssProperty.LineHeight] = value; }
        }
        public virtual CssValue ListStyleImage
        {
            get { return _properties[CssProperty.ListStyleImage]; }
            set { _properties[CssProperty.ListStyleImage] = value; }
        }
        public virtual CssValue ListStylePosition
        {
            get { return _properties[CssProperty.ListStylePosition]; }
            set { _properties[CssProperty.ListStylePosition] = value; }
        }
        public virtual CssValue ListStyleType
        {
            get { return _properties[CssProperty.ListStyleType]; }
            set { _properties[CssProperty.ListStyleType] = value; }
        }
        public virtual CssValue MarginRight
        {
            get { return _properties[CssProperty.MarginRight]; }
            set { _properties[CssProperty.MarginRight] = value; }
        }
        public virtual CssValue MarginLeft
        {
            get { return _properties[CssProperty.MarginLeft]; }
            set { _properties[CssProperty.MarginLeft] = value; }
        }
        public virtual CssValue MarginTop
        {
            get { return _properties[CssProperty.MarginTop]; }
            set { _properties[CssProperty.MarginTop] = value; }
        }
        public virtual CssValue MarginBottom
        {
            get { return _properties[CssProperty.MarginBottom]; }
            set { _properties[CssProperty.MarginBottom] = value; }
        }
        public virtual CssValue MaxHeight
        {
            get { return _properties[CssProperty.MaxHeight]; }
            set { _properties[CssProperty.MaxHeight] = value; }
        }
        public virtual CssValue MaxWidth
        {
            get { return _properties[CssProperty.MaxWidth]; }
            set { _properties[CssProperty.MaxWidth] = value; }
        }
        public virtual CssValue MinHeight
        {
            get { return _properties[CssProperty.MinHeight]; }
            set { _properties[CssProperty.MinHeight] = value; }
        }
        public virtual CssValue MinWidth
        {
            get { return _properties[CssProperty.MinWidth]; }
            set { _properties[CssProperty.MinWidth] = value; }
        }
        public virtual CssValue Orphans
        {
            get { return _properties[CssProperty.Orphans]; }
            set { _properties[CssProperty.Orphans] = value; }
        }
        public virtual CssValue OutlineColor
        {
            get { return _properties[CssProperty.OutlineColor]; }
            set { _properties[CssProperty.OutlineColor] = value; }
        }
        public virtual CssValue OutlineStyle
        {
            get { return _properties[CssProperty.OutlineStyle]; }
            set { _properties[CssProperty.OutlineStyle] = value; }
        }
        public virtual CssValue OutlineWidth
        {
            get { return _properties[CssProperty.OutlineWidth]; }
            set { _properties[CssProperty.OutlineWidth] = value; }
        }
        public virtual CssValue Overflow
        {
            get { return _properties[CssProperty.Overflow]; }
            set { _properties[CssProperty.Overflow] = value; }
        }
        public virtual CssValue PaddingTop
        {
            get { return _properties[CssProperty.PaddingTop]; }
            set { _properties[CssProperty.PaddingTop] = value; }
        }
        public virtual CssValue PaddingRight
        {
            get { return _properties[CssProperty.PaddingRight]; }
            set { _properties[CssProperty.PaddingRight] = value; }
        }
        public virtual CssValue PaddingBottom
        {
            get { return _properties[CssProperty.PaddingBottom]; }
            set { _properties[CssProperty.PaddingBottom] = value; }
        }
        public virtual CssValue PaddingLeft
        {
            get { return _properties[CssProperty.PaddingLeft]; }
            set { _properties[CssProperty.PaddingLeft] = value; }
        }
        public virtual CssValue PageBreakAfter
        {
            get { return _properties[CssProperty.PageBreakAfter]; }
            set { _properties[CssProperty.PageBreakAfter] = value; }
        }
        public virtual CssValue PageBreakBefore
        {
            get { return _properties[CssProperty.PageBreakBefore]; }
            set { _properties[CssProperty.PageBreakBefore] = value; }
        }
        public virtual CssValue PageBreakInside
        {
            get { return _properties[CssProperty.PageBreakInside]; }
            set { _properties[CssProperty.PageBreakInside] = value; }
        }
        public virtual CssValue PauseAfter
        {
            get { return _properties[CssProperty.PauseAfter]; }
            set { _properties[CssProperty.PauseAfter] = value; }
        }
        public virtual CssValue PauseBefore
        {
            get { return _properties[CssProperty.PauseBefore]; }
            set { _properties[CssProperty.PauseBefore] = value; }
        }
        public virtual CssValue PitchRange
        {
            get { return _properties[CssProperty.PitchRange]; }
            set { _properties[CssProperty.PitchRange] = value; }
        }
        public virtual CssValue Pitch
        {
            get { return _properties[CssProperty.Pitch]; }
            set { _properties[CssProperty.Pitch] = value; }
        }
        public virtual CssValue PlayDuring
        {
            get { return _properties[CssProperty.PlayDuring]; }
            set { _properties[CssProperty.PlayDuring] = value; }
        }
        public virtual CssValue Position
        {
            get { return _properties[CssProperty.Position]; }
            set { _properties[CssProperty.Position] = value; }
        }
        public virtual CssValue Quotes
        {
            get { return _properties[CssProperty.Quotes]; }
            set { _properties[CssProperty.Quotes] = value; }
        }
        public virtual CssValue Richness
        {
            get { return _properties[CssProperty.Richness]; }
            set { _properties[CssProperty.Richness] = value; }
        }
        public virtual CssValue Right
        {
            get { return _properties[CssProperty.Right]; }
            set { _properties[CssProperty.Right] = value; }
        }
        public virtual CssValue SpeakHeader
        {
            get { return _properties[CssProperty.SpeakHeader]; }
            set { _properties[CssProperty.SpeakHeader] = value; }
        }
        public virtual CssValue SpeakNumeral
        {
            get { return _properties[CssProperty.SpeakNumeral]; }
            set { _properties[CssProperty.SpeakNumeral] = value; }
        }
        public virtual CssValue SpeakPunctuation
        {
            get { return _properties[CssProperty.SpeakPunctuation]; }
            set { _properties[CssProperty.SpeakPunctuation] = value; }
        }
        public virtual CssValue Speak
        {
            get { return _properties[CssProperty.Speak]; }
            set { _properties[CssProperty.Speak] = value; }
        }
        public virtual CssValue SpeechRate
        {
            get { return _properties[CssProperty.SpeechRate]; }
            set { _properties[CssProperty.SpeechRate] = value; }
        }
        public virtual CssValue Stress
        {
            get { return _properties[CssProperty.Stress]; }
            set { _properties[CssProperty.Stress] = value; }
        }
        public virtual CssValue TableLayout
        {
            get { return _properties[CssProperty.TableLayout]; }
            set { _properties[CssProperty.TableLayout] = value; }
        }
        public virtual CssValue TextAlign
        {
            get { return _properties[CssProperty.TextAlign]; }
            set { _properties[CssProperty.TextAlign] = value; }
        }
        public virtual CssValue TextDecoration
        {
            get { return _properties[CssProperty.TextDecoration]; }
            set { _properties[CssProperty.TextDecoration] = value; }
        }
        public virtual CssValue TextIndent
        {
            get { return _properties[CssProperty.TextIndent]; }
            set { _properties[CssProperty.TextIndent] = value; }
        }
        public virtual CssValue TextTransform
        {
            get { return _properties[CssProperty.TextTransform]; }
            set { _properties[CssProperty.TextTransform] = value; }
        }
        public virtual CssValue Top
        {
            get { return _properties[CssProperty.Top]; }
            set { _properties[CssProperty.Top] = value; }
        }
        public virtual CssValue UnicodeBidi
        {
            get { return _properties[CssProperty.UnicodeBidi]; }
            set { _properties[CssProperty.UnicodeBidi] = value; }
        }
        public virtual CssValue VerticalAlign
        {
            get { return _properties[CssProperty.VerticalAlign]; }
            set { _properties[CssProperty.VerticalAlign] = value; }
        }
        public virtual CssValue Visibility
        {
            get { return _properties[CssProperty.Visibility]; }
            set { _properties[CssProperty.Visibility] = value; }
        }
        public virtual CssValue VoiceFamily
        {
            get { return _properties[CssProperty.VoiceFamily]; }
            set { _properties[CssProperty.VoiceFamily] = value; }
        }
        public virtual CssValue Volume
        {
            get { return _properties[CssProperty.Volume]; }
            set { _properties[CssProperty.Volume] = value; }
        }
        public virtual CssValue WhiteSpace
        {
            get { return _properties[CssProperty.WhiteSpace]; }
            set { _properties[CssProperty.WhiteSpace] = value; }
        }
        public virtual CssValue Widows
        {
            get { return _properties[CssProperty.Widows]; }
            set { _properties[CssProperty.Widows] = value; }
        }
        public virtual CssValue Width
        {
            get { return _properties[CssProperty.Width]; }
            set { _properties[CssProperty.Width] = value; }
        }
        public virtual CssValue WordSpacing
        {
            get { return _properties[CssProperty.WordSpacing]; }
            set { _properties[CssProperty.WordSpacing] = value; }
        }
        public virtual CssValue ZIndex
        {
            get { return _properties[CssProperty.ZIndex]; }
            set { _properties[CssProperty.ZIndex] = value; }
        }
        #endregion
    }
}
