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
using System.Text;
using Marius.Html.Css.Values;
using Marius.Html.Css.Box;

namespace Marius.Html.Css
{
    public class CssPropertyValueDictionary: IEnumerable<Tuple<string, CssValue>>
    {
        private CssBox _box;
        private Dictionary<string, CssValue> _values;

        public CssPropertyValueDictionary(CssBox box)
        {
            _box = box;
            _values = new Dictionary<string, CssValue>(StringComparer.InvariantCultureIgnoreCase);
        }

        public CssValue this[string property]
        {
            get
            {
                CssValue result = null;
                if (!_values.TryGetValue(property, out result))
                    return null;
                return result;
            }
            set
            {
                if (_values.ContainsKey(property))
                    _values[property] = value;
                else
                    _values.Add(property, value);
            }
        }

        public bool HasStyle
        {
            get { return _values.Count > 0; }
        }

        public IEnumerator<Tuple<string, CssValue>> GetEnumerator()
        {
            foreach (var key in _values.Keys)
            {
                yield return new Tuple<string, CssValue>(key, _values[key]);
            }
            yield break;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Tuple<string, CssValue>>)this).GetEnumerator();
        }

        #region Properties
        public virtual CssValue Azimuth
        {
            get { return this[CssProperty.Azimuth]; }
            set { this[CssProperty.Azimuth] = value; }
        }
        public virtual CssValue BackgroundAttachment
        {
            get { return this[CssProperty.BackgroundAttachment]; }
            set { this[CssProperty.BackgroundAttachment] = value; }
        }
        public virtual CssValue BackgroundColor
        {
            get { return this[CssProperty.BackgroundColor]; }
            set { this[CssProperty.BackgroundColor] = value; }
        }
        public virtual CssValue BackgroundImage
        {
            get { return this[CssProperty.BackgroundImage]; }
            set { this[CssProperty.BackgroundImage] = value; }
        }
        public virtual CssValue BackgroundPosition
        {
            get { return this[CssProperty.BackgroundPosition]; }
            set { this[CssProperty.BackgroundPosition] = value; }
        }
        public virtual CssValue BackgroundRepeat
        {
            get { return this[CssProperty.BackgroundRepeat]; }
            set { this[CssProperty.BackgroundRepeat] = value; }
        }
        public virtual CssValue BorderCollapse
        {
            get { return this[CssProperty.BorderCollapse]; }
            set { this[CssProperty.BorderCollapse] = value; }
        }
        public virtual CssValue BorderSpacing
        {
            get { return this[CssProperty.BorderSpacing]; }
            set { this[CssProperty.BorderSpacing] = value; }
        }
        public virtual CssValue BorderTopColor
        {
            get { return this[CssProperty.BorderTopColor]; }
            set { this[CssProperty.BorderTopColor] = value; }
        }
        public virtual CssValue BorderTopWidth
        {
            get { return this[CssProperty.BorderTopWidth]; }
            set { this[CssProperty.BorderTopWidth] = value; }
        }
        public virtual CssValue BorderTopStyle
        {
            get { return this[CssProperty.BorderTopStyle]; }
            set { this[CssProperty.BorderTopStyle] = value; }
        }
        public virtual CssValue BorderRightColor
        {
            get { return this[CssProperty.BorderRightColor]; }
            set { this[CssProperty.BorderRightColor] = value; }
        }
        public virtual CssValue BorderRightWidth
        {
            get { return this[CssProperty.BorderRightWidth]; }
            set { this[CssProperty.BorderRightWidth] = value; }
        }
        public virtual CssValue BorderRightStyle
        {
            get { return this[CssProperty.BorderRightStyle]; }
            set { this[CssProperty.BorderRightStyle] = value; }
        }
        public virtual CssValue BorderBottomColor
        {
            get { return this[CssProperty.BorderBottomColor]; }
            set { this[CssProperty.BorderBottomColor] = value; }
        }
        public virtual CssValue BorderBottomWidth
        {
            get { return this[CssProperty.BorderBottomWidth]; }
            set { this[CssProperty.BorderBottomWidth] = value; }
        }
        public virtual CssValue BorderBottomStyle
        {
            get { return this[CssProperty.BorderBottomStyle]; }
            set { this[CssProperty.BorderBottomStyle] = value; }
        }
        public virtual CssValue BorderLeftColor
        {
            get { return this[CssProperty.BorderLeftColor]; }
            set { this[CssProperty.BorderLeftColor] = value; }
        }
        public virtual CssValue BorderLeftWidth
        {
            get { return this[CssProperty.BorderLeftWidth]; }
            set { this[CssProperty.BorderLeftWidth] = value; }
        }
        public virtual CssValue BorderLeftStyle
        {
            get { return this[CssProperty.BorderLeftStyle]; }
            set { this[CssProperty.BorderLeftStyle] = value; }
        }
        public virtual CssValue Bottom
        {
            get { return this[CssProperty.Bottom]; }
            set { this[CssProperty.Bottom] = value; }
        }
        public virtual CssValue CaptionSide
        {
            get { return this[CssProperty.CaptionSide]; }
            set { this[CssProperty.CaptionSide] = value; }
        }
        public virtual CssValue Clear
        {
            get { return this[CssProperty.Clear]; }
            set { this[CssProperty.Clear] = value; }
        }
        public virtual CssValue Clip
        {
            get { return this[CssProperty.Clip]; }
            set { this[CssProperty.Clip] = value; }
        }
        public virtual CssValue Color
        {
            get { return this[CssProperty.Color]; }
            set { this[CssProperty.Color] = value; }
        }
        public virtual CssValue Content
        {
            get { return this[CssProperty.Content]; }
            set { this[CssProperty.Content] = value; }
        }
        public virtual CssValue CounterIncrement
        {
            get { return this[CssProperty.CounterIncrement]; }
            set { this[CssProperty.CounterIncrement] = value; }
        }
        public virtual CssValue CounterReset
        {
            get { return this[CssProperty.CounterReset]; }
            set { this[CssProperty.CounterReset] = value; }
        }
        public virtual CssValue CueBefore
        {
            get { return this[CssProperty.CueBefore]; }
            set { this[CssProperty.CueBefore] = value; }
        }
        public virtual CssValue CueAfter
        {
            get { return this[CssProperty.CueAfter]; }
            set { this[CssProperty.CueAfter] = value; }
        }
        public virtual CssValue Cursor
        {
            get { return this[CssProperty.Cursor]; }
            set { this[CssProperty.Cursor] = value; }
        }
        public virtual CssValue Direction
        {
            get { return this[CssProperty.Direction]; }
            set { this[CssProperty.Direction] = value; }
        }
        public virtual CssValue Display
        {
            get { return this[CssProperty.Display]; }
            set { this[CssProperty.Display] = value; }
        }
        public virtual CssValue Elevation
        {
            get { return this[CssProperty.Elevation]; }
            set { this[CssProperty.Elevation] = value; }
        }
        public virtual CssValue EmptyCells
        {
            get { return this[CssProperty.EmptyCells]; }
            set { this[CssProperty.EmptyCells] = value; }
        }
        public virtual CssValue Float
        {
            get { return this[CssProperty.Float]; }
            set { this[CssProperty.Float] = value; }
        }
        public virtual CssValue FontFamily
        {
            get { return this[CssProperty.FontFamily]; }
            set { this[CssProperty.FontFamily] = value; }
        }
        public virtual CssValue FontSize
        {
            get { return this[CssProperty.FontSize]; }
            set { this[CssProperty.FontSize] = value; }
        }
        public virtual CssValue FontStyle
        {
            get { return this[CssProperty.FontStyle]; }
            set { this[CssProperty.FontStyle] = value; }
        }
        public virtual CssValue FontVariant
        {
            get { return this[CssProperty.FontVariant]; }
            set { this[CssProperty.FontVariant] = value; }
        }
        public virtual CssValue FontWeight
        {
            get { return this[CssProperty.FontWeight]; }
            set { this[CssProperty.FontWeight] = value; }
        }
        public virtual CssValue Font
        {
            get { return this[CssProperty.Font]; }
            set { this[CssProperty.Font] = value; }
        }
        public virtual CssValue Height
        {
            get { return this[CssProperty.Height]; }
            set { this[CssProperty.Height] = value; }
        }
        public virtual CssValue Left
        {
            get { return this[CssProperty.Left]; }
            set { this[CssProperty.Left] = value; }
        }
        public virtual CssValue LetterSpacing
        {
            get { return this[CssProperty.LetterSpacing]; }
            set { this[CssProperty.LetterSpacing] = value; }
        }
        public virtual CssValue LineHeight
        {
            get { return this[CssProperty.LineHeight]; }
            set { this[CssProperty.LineHeight] = value; }
        }
        public virtual CssValue ListStyleImage
        {
            get { return this[CssProperty.ListStyleImage]; }
            set { this[CssProperty.ListStyleImage] = value; }
        }
        public virtual CssValue ListStylePosition
        {
            get { return this[CssProperty.ListStylePosition]; }
            set { this[CssProperty.ListStylePosition] = value; }
        }
        public virtual CssValue ListStyleType
        {
            get { return this[CssProperty.ListStyleType]; }
            set { this[CssProperty.ListStyleType] = value; }
        }
        public virtual CssValue MarginRight
        {
            get { return this[CssProperty.MarginRight]; }
            set { this[CssProperty.MarginRight] = value; }
        }
        public virtual CssValue MarginLeft
        {
            get { return this[CssProperty.MarginLeft]; }
            set { this[CssProperty.MarginLeft] = value; }
        }
        public virtual CssValue MarginTop
        {
            get { return this[CssProperty.MarginTop]; }
            set { this[CssProperty.MarginTop] = value; }
        }
        public virtual CssValue MarginBottom
        {
            get { return this[CssProperty.MarginBottom]; }
            set { this[CssProperty.MarginBottom] = value; }
        }
        public virtual CssValue MaxHeight
        {
            get { return this[CssProperty.MaxHeight]; }
            set { this[CssProperty.MaxHeight] = value; }
        }
        public virtual CssValue MaxWidth
        {
            get { return this[CssProperty.MaxWidth]; }
            set { this[CssProperty.MaxWidth] = value; }
        }
        public virtual CssValue MinHeight
        {
            get { return this[CssProperty.MinHeight]; }
            set { this[CssProperty.MinHeight] = value; }
        }
        public virtual CssValue MinWidth
        {
            get { return this[CssProperty.MinWidth]; }
            set { this[CssProperty.MinWidth] = value; }
        }
        public virtual CssValue Orphans
        {
            get { return this[CssProperty.Orphans]; }
            set { this[CssProperty.Orphans] = value; }
        }
        public virtual CssValue OutlineColor
        {
            get { return this[CssProperty.OutlineColor]; }
            set { this[CssProperty.OutlineColor] = value; }
        }
        public virtual CssValue OutlineStyle
        {
            get { return this[CssProperty.OutlineStyle]; }
            set { this[CssProperty.OutlineStyle] = value; }
        }
        public virtual CssValue OutlineWidth
        {
            get { return this[CssProperty.OutlineWidth]; }
            set { this[CssProperty.OutlineWidth] = value; }
        }
        public virtual CssValue Overflow
        {
            get { return this[CssProperty.Overflow]; }
            set { this[CssProperty.Overflow] = value; }
        }
        public virtual CssValue PaddingTop
        {
            get { return this[CssProperty.PaddingTop]; }
            set { this[CssProperty.PaddingTop] = value; }
        }
        public virtual CssValue PaddingRight
        {
            get { return this[CssProperty.PaddingRight]; }
            set { this[CssProperty.PaddingRight] = value; }
        }
        public virtual CssValue PaddingBottom
        {
            get { return this[CssProperty.PaddingBottom]; }
            set { this[CssProperty.PaddingBottom] = value; }
        }
        public virtual CssValue PaddingLeft
        {
            get { return this[CssProperty.PaddingLeft]; }
            set { this[CssProperty.PaddingLeft] = value; }
        }
        public virtual CssValue PageBreakAfter
        {
            get { return this[CssProperty.PageBreakAfter]; }
            set { this[CssProperty.PageBreakAfter] = value; }
        }
        public virtual CssValue PageBreakBefore
        {
            get { return this[CssProperty.PageBreakBefore]; }
            set { this[CssProperty.PageBreakBefore] = value; }
        }
        public virtual CssValue PageBreakInside
        {
            get { return this[CssProperty.PageBreakInside]; }
            set { this[CssProperty.PageBreakInside] = value; }
        }
        public virtual CssValue PauseAfter
        {
            get { return this[CssProperty.PauseAfter]; }
            set { this[CssProperty.PauseAfter] = value; }
        }
        public virtual CssValue PauseBefore
        {
            get { return this[CssProperty.PauseBefore]; }
            set { this[CssProperty.PauseBefore] = value; }
        }
        public virtual CssValue PitchRange
        {
            get { return this[CssProperty.PitchRange]; }
            set { this[CssProperty.PitchRange] = value; }
        }
        public virtual CssValue Pitch
        {
            get { return this[CssProperty.Pitch]; }
            set { this[CssProperty.Pitch] = value; }
        }
        public virtual CssValue PlayDuring
        {
            get { return this[CssProperty.PlayDuring]; }
            set { this[CssProperty.PlayDuring] = value; }
        }
        public virtual CssValue Position
        {
            get { return this[CssProperty.Position]; }
            set { this[CssProperty.Position] = value; }
        }
        public virtual CssValue Quotes
        {
            get { return this[CssProperty.Quotes]; }
            set { this[CssProperty.Quotes] = value; }
        }
        public virtual CssValue Richness
        {
            get { return this[CssProperty.Richness]; }
            set { this[CssProperty.Richness] = value; }
        }
        public virtual CssValue Right
        {
            get { return this[CssProperty.Right]; }
            set { this[CssProperty.Right] = value; }
        }
        public virtual CssValue SpeakHeader
        {
            get { return this[CssProperty.SpeakHeader]; }
            set { this[CssProperty.SpeakHeader] = value; }
        }
        public virtual CssValue SpeakNumeral
        {
            get { return this[CssProperty.SpeakNumeral]; }
            set { this[CssProperty.SpeakNumeral] = value; }
        }
        public virtual CssValue SpeakPunctuation
        {
            get { return this[CssProperty.SpeakPunctuation]; }
            set { this[CssProperty.SpeakPunctuation] = value; }
        }
        public virtual CssValue Speak
        {
            get { return this[CssProperty.Speak]; }
            set { this[CssProperty.Speak] = value; }
        }
        public virtual CssValue SpeechRate
        {
            get { return this[CssProperty.SpeechRate]; }
            set { this[CssProperty.SpeechRate] = value; }
        }
        public virtual CssValue Stress
        {
            get { return this[CssProperty.Stress]; }
            set { this[CssProperty.Stress] = value; }
        }
        public virtual CssValue TableLayout
        {
            get { return this[CssProperty.TableLayout]; }
            set { this[CssProperty.TableLayout] = value; }
        }
        public virtual CssValue TextAlign
        {
            get { return this[CssProperty.TextAlign]; }
            set { this[CssProperty.TextAlign] = value; }
        }
        public virtual CssValue TextDecoration
        {
            get { return this[CssProperty.TextDecoration]; }
            set { this[CssProperty.TextDecoration] = value; }
        }
        public virtual CssValue TextIndent
        {
            get { return this[CssProperty.TextIndent]; }
            set { this[CssProperty.TextIndent] = value; }
        }
        public virtual CssValue TextTransform
        {
            get { return this[CssProperty.TextTransform]; }
            set { this[CssProperty.TextTransform] = value; }
        }
        public virtual CssValue Top
        {
            get { return this[CssProperty.Top]; }
            set { this[CssProperty.Top] = value; }
        }
        public virtual CssValue UnicodeBidi
        {
            get { return this[CssProperty.UnicodeBidi]; }
            set { this[CssProperty.UnicodeBidi] = value; }
        }
        public virtual CssValue VerticalAlign
        {
            get { return this[CssProperty.VerticalAlign]; }
            set { this[CssProperty.VerticalAlign] = value; }
        }
        public virtual CssValue Visibility
        {
            get { return this[CssProperty.Visibility]; }
            set { this[CssProperty.Visibility] = value; }
        }
        public virtual CssValue VoiceFamily
        {
            get { return this[CssProperty.VoiceFamily]; }
            set { this[CssProperty.VoiceFamily] = value; }
        }
        public virtual CssValue Volume
        {
            get { return this[CssProperty.Volume]; }
            set { this[CssProperty.Volume] = value; }
        }
        public virtual CssValue WhiteSpace
        {
            get { return this[CssProperty.WhiteSpace]; }
            set { this[CssProperty.WhiteSpace] = value; }
        }
        public virtual CssValue Widows
        {
            get { return this[CssProperty.Widows]; }
            set { this[CssProperty.Widows] = value; }
        }
        public virtual CssValue Width
        {
            get { return this[CssProperty.Width]; }
            set { this[CssProperty.Width] = value; }
        }
        public virtual CssValue WordSpacing
        {
            get { return this[CssProperty.WordSpacing]; }
            set { this[CssProperty.WordSpacing] = value; }
        }
        public virtual CssValue ZIndex
        {
            get { return this[CssProperty.ZIndex]; }
            set { this[CssProperty.ZIndex] = value; }
        }
        #endregion

        public void CopyTo(CssPropertyValueDictionary target)
        {
            foreach (var item in _values)
            {
                target[item.Key] = item.Value;
            }
        }

        public CssValue Computed(string name)
        {
            throw new NotImplementedException();
        }
    }
}
