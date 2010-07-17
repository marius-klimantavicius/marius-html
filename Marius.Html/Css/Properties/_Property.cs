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

namespace Marius.Html.Css.Properties
{
    public partial class Azimuth
    {
        public sealed override string Property
        {
            get { return CssProperty.Azimuth; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Azimuth = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Azimuth;
        }
    }

    public partial class Background
    {
        public sealed override string Property
        {
            get { return CssProperty.Background; }
        }
    }

    public partial class BackgroundAttachment
    {
        public sealed override string Property
        {
            get { return CssProperty.BackgroundAttachment; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BackgroundAttachment = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BackgroundAttachment;
        }
    }

    public partial class BackgroundColor
    {
        public sealed override string Property
        {
            get { return CssProperty.BackgroundColor; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BackgroundColor = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BackgroundColor;
        }
    }

    public partial class BackgroundImage
    {
        public sealed override string Property
        {
            get { return CssProperty.BackgroundImage; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BackgroundImage = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BackgroundImage;
        }
    }

    public partial class BackgroundPosition
    {
        public sealed override string Property
        {
            get { return CssProperty.BackgroundPosition; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BackgroundPosition = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BackgroundPosition;
        }
    }

    public partial class BackgroundRepeat
    {
        public sealed override string Property
        {
            get { return CssProperty.BackgroundRepeat; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BackgroundRepeat = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BackgroundRepeat;
        }
    }

    public partial class Border
    {
        public sealed override string Property
        {
            get { return CssProperty.Border; }
        }
    }

    public partial class BorderCollapse
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderCollapse; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderCollapse = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderCollapse;
        }
    }

    public partial class BorderColor
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderColor; }
        }
    }

    public partial class BorderTopColor
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderTopColor; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderTopColor = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderTopColor;
        }
    }

    public partial class BorderRightColor
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderRightColor; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderRightColor = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderRightColor;
        }
    }

    public partial class BorderBottomColor
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderBottomColor; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderBottomColor = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderBottomColor;
        }
    }

    public partial class BorderLeftColor
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderLeftColor; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderLeftColor = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderLeftColor;
        }
    }

    public partial class BorderSpacing
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderSpacing; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderSpacing = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderSpacing;
        }
    }

    public partial class BorderStyle
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderStyle; }
        }
    }

    public partial class BorderTopStyle
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderTopStyle; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderTopStyle = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderTopStyle;
        }
    }

    public partial class BorderRightStyle
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderRightStyle; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderRightStyle = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderRightStyle;
        }
    }

    public partial class BorderBottomStyle
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderBottomStyle; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderBottomStyle = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderBottomStyle;
        }
    }

    public partial class BorderLeftStyle
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderLeftStyle; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderLeftStyle = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderLeftStyle;
        }
    }

    public partial class BorderWidth
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderWidth; }
        }
    }

    public partial class BorderTopWidth
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderTopWidth; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderTopWidth = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderTopWidth;
        }
    }

    public partial class BorderRightWidth
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderRightWidth; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderRightWidth = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderRightWidth;
        }
    }

    public partial class BorderBottomWidth
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderBottomWidth; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderBottomWidth = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderBottomWidth;
        }
    }

    public partial class BorderLeftWidth
    {
        public sealed override string Property
        {
            get { return CssProperty.BorderLeftWidth; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.BorderLeftWidth = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.BorderLeftWidth;
        }
    }

    public partial class Bottom
    {
        public sealed override string Property
        {
            get { return CssProperty.Bottom; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Bottom = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Bottom;
        }
    }

    public partial class CaptionSide
    {
        public sealed override string Property
        {
            get { return CssProperty.CaptionSide; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.CaptionSide = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.CaptionSide;
        }
    }

    public partial class Clear
    {
        public sealed override string Property
        {
            get { return CssProperty.Clear; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Clear = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Clear;
        }
    }

    public partial class Clip
    {
        public sealed override string Property
        {
            get { return CssProperty.Clip; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Clip = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Clip;
        }
    }

    public partial class Color
    {
        public sealed override string Property
        {
            get { return CssProperty.Color; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Color = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Color;
        }
    }

    public partial class Content
    {
        public sealed override string Property
        {
            get { return CssProperty.Content; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Content = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Content;
        }
    }

    public partial class CounterReset
    {
        public sealed override string Property
        {
            get { return CssProperty.CounterReset; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.CounterReset = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.CounterReset;
        }
    }

    public partial class CounterIncrement
    {
        public sealed override string Property
        {
            get { return CssProperty.CounterIncrement; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.CounterIncrement = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.CounterIncrement;
        }
    }

    public partial class Cue
    {
        public sealed override string Property
        {
            get { return CssProperty.Cue; }
        }
    }

    public partial class CueAfter
    {
        public sealed override string Property
        {
            get { return CssProperty.CueAfter; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.CueAfter = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.CueAfter;
        }
    }

    public partial class CueBefore
    {
        public sealed override string Property
        {
            get { return CssProperty.CueBefore; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.CueBefore = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.CueBefore;
        }
    }

    public partial class Cursor
    {
        public sealed override string Property
        {
            get { return CssProperty.Cursor; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Cursor = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Cursor;
        }
    }

    public partial class Direction
    {
        public sealed override string Property
        {
            get { return CssProperty.Direction; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Direction = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Direction;
        }
    }

    public partial class Display
    {
        public sealed override string Property
        {
            get { return CssProperty.Display; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Display = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Display;
        }
    }

    public partial class Elevation
    {
        public sealed override string Property
        {
            get { return CssProperty.Elevation; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Elevation = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Elevation;
        }
    }

    public partial class EmptyCells
    {
        public sealed override string Property
        {
            get { return CssProperty.EmptyCells; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.EmptyCells = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.EmptyCells;
        }
    }

    public partial class Float
    {
        public sealed override string Property
        {
            get { return CssProperty.Float; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Float = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Float;
        }
    }

    public partial class Font
    {
        public sealed override string Property
        {
            get { return CssProperty.Font; }
        }
    }

    public partial class FontFamily
    {
        public sealed override string Property
        {
            get { return CssProperty.FontFamily; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.FontFamily = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.FontFamily;
        }
    }

    public partial class FontSize
    {
        public sealed override string Property
        {
            get { return CssProperty.FontSize; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.FontSize = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.FontSize;
        }
    }

    public partial class FontStyle
    {
        public sealed override string Property
        {
            get { return CssProperty.FontStyle; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.FontStyle = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.FontStyle;
        }
    }

    public partial class FontVariant
    {
        public sealed override string Property
        {
            get { return CssProperty.FontVariant; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.FontVariant = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.FontVariant;
        }
    }

    public partial class FontWeight
    {
        public sealed override string Property
        {
            get { return CssProperty.FontWeight; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.FontWeight = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.FontWeight;
        }
    }

    public partial class Height
    {
        public sealed override string Property
        {
            get { return CssProperty.Height; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Height = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Height;
        }
    }

    public partial class Left
    {
        public sealed override string Property
        {
            get { return CssProperty.Left; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Left = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Left;
        }
    }

    public partial class LetterSpacing
    {
        public sealed override string Property
        {
            get { return CssProperty.LetterSpacing; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.LetterSpacing = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.LetterSpacing;
        }
    }

    public partial class LineHeight
    {
        public sealed override string Property
        {
            get { return CssProperty.LineHeight; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.LineHeight = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.LineHeight;
        }
    }

    public partial class ListStyle
    {
        public sealed override string Property
        {
            get { return CssProperty.ListStyle; }
        }
    }

    public partial class ListStyleImage
    {
        public sealed override string Property
        {
            get { return CssProperty.ListStyleImage; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.ListStyleImage = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.ListStyleImage;
        }
    }

    public partial class ListStylePosition
    {
        public sealed override string Property
        {
            get { return CssProperty.ListStylePosition; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.ListStylePosition = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.ListStylePosition;
        }
    }

    public partial class ListStyleType
    {
        public sealed override string Property
        {
            get { return CssProperty.ListStyleType; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.ListStyleType = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.ListStyleType;
        }
    }

    public partial class Margin
    {
        public sealed override string Property
        {
            get { return CssProperty.Margin; }
        }
    }

    public partial class MarginTop
    {
        public sealed override string Property
        {
            get { return CssProperty.MarginTop; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.MarginTop = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.MarginTop;
        }
    }

    public partial class MarginRight
    {
        public sealed override string Property
        {
            get { return CssProperty.MarginRight; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.MarginRight = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.MarginRight;
        }
    }

    public partial class MarginBottom
    {
        public sealed override string Property
        {
            get { return CssProperty.MarginBottom; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.MarginBottom = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.MarginBottom;
        }
    }

    public partial class MarginLeft
    {
        public sealed override string Property
        {
            get { return CssProperty.MarginLeft; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.MarginLeft = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.MarginLeft;
        }
    }

    public partial class MaxHeight
    {
        public sealed override string Property
        {
            get { return CssProperty.MaxHeight; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.MaxHeight = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.MaxHeight;
        }
    }

    public partial class MaxWidth
    {
        public sealed override string Property
        {
            get { return CssProperty.MaxWidth; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.MaxWidth = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.MaxWidth;
        }
    }

    public partial class MinHeight
    {
        public sealed override string Property
        {
            get { return CssProperty.MinHeight; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.MinHeight = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.MinHeight;
        }
    }

    public partial class MinWidth
    {
        public sealed override string Property
        {
            get { return CssProperty.MinWidth; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.MinWidth = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.MinWidth;
        }
    }

    public partial class Orphans
    {
        public sealed override string Property
        {
            get { return CssProperty.Orphans; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Orphans = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Orphans;
        }
    }

    public partial class Outline
    {
        public sealed override string Property
        {
            get { return CssProperty.Outline; }
        }
    }

    public partial class OutlineColor
    {
        public sealed override string Property
        {
            get { return CssProperty.OutlineColor; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.OutlineColor = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.OutlineColor;
        }
    }

    public partial class OutlineStyle
    {
        public sealed override string Property
        {
            get { return CssProperty.OutlineStyle; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.OutlineStyle = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.OutlineStyle;
        }
    }

    public partial class OutlineWidth
    {
        public sealed override string Property
        {
            get { return CssProperty.OutlineWidth; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.OutlineWidth = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.OutlineWidth;
        }
    }

    public partial class Overflow
    {
        public sealed override string Property
        {
            get { return CssProperty.Overflow; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Overflow = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Overflow;
        }
    }

    public partial class Padding
    {
        public sealed override string Property
        {
            get { return CssProperty.Padding; }
        }
    }

    public partial class PaddingTop
    {
        public sealed override string Property
        {
            get { return CssProperty.PaddingTop; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PaddingTop = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PaddingTop;
        }
    }

    public partial class PaddingRight
    {
        public sealed override string Property
        {
            get { return CssProperty.PaddingRight; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PaddingRight = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PaddingRight;
        }
    }

    public partial class PaddingBottom
    {
        public sealed override string Property
        {
            get { return CssProperty.PaddingBottom; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PaddingBottom = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PaddingBottom;
        }
    }

    public partial class PaddingLeft
    {
        public sealed override string Property
        {
            get { return CssProperty.PaddingLeft; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PaddingLeft = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PaddingLeft;
        }
    }

    public partial class PageBreakAfter
    {
        public sealed override string Property
        {
            get { return CssProperty.PageBreakAfter; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PageBreakAfter = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PageBreakAfter;
        }
    }

    public partial class PageBreakBefore
    {
        public sealed override string Property
        {
            get { return CssProperty.PageBreakBefore; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PageBreakBefore = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PageBreakBefore;
        }
    }

    public partial class PageBreakInside
    {
        public sealed override string Property
        {
            get { return CssProperty.PageBreakInside; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PageBreakInside = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PageBreakInside;
        }
    }

    public partial class Pause
    {
        public sealed override string Property
        {
            get { return CssProperty.Pause; }
        }
    }

    public partial class PauseAfter
    {
        public sealed override string Property
        {
            get { return CssProperty.PauseAfter; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PauseAfter = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PauseAfter;
        }
    }

    public partial class PauseBefore
    {
        public sealed override string Property
        {
            get { return CssProperty.PauseBefore; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PauseBefore = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PauseBefore;
        }
    }

    public partial class Pitch
    {
        public sealed override string Property
        {
            get { return CssProperty.Pitch; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Pitch = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Pitch;
        }
    }

    public partial class PitchRange
    {
        public sealed override string Property
        {
            get { return CssProperty.PitchRange; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PitchRange = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PitchRange;
        }
    }

    public partial class PlayDuring
    {
        public sealed override string Property
        {
            get { return CssProperty.PlayDuring; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.PlayDuring = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.PlayDuring;
        }
    }

    public partial class Position
    {
        public sealed override string Property
        {
            get { return CssProperty.Position; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Position = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Position;
        }
    }

    public partial class Quotes
    {
        public sealed override string Property
        {
            get { return CssProperty.Quotes; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Quotes = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Quotes;
        }
    }

    public partial class Richness
    {
        public sealed override string Property
        {
            get { return CssProperty.Richness; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Richness = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Richness;
        }
    }

    public partial class Right
    {
        public sealed override string Property
        {
            get { return CssProperty.Right; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Right = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Right;
        }
    }

    public partial class Speak
    {
        public sealed override string Property
        {
            get { return CssProperty.Speak; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Speak = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Speak;
        }
    }

    public partial class SpeakHeader
    {
        public sealed override string Property
        {
            get { return CssProperty.SpeakHeader; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.SpeakHeader = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.SpeakHeader;
        }
    }

    public partial class SpeakNumeral
    {
        public sealed override string Property
        {
            get { return CssProperty.SpeakNumeral; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.SpeakNumeral = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.SpeakNumeral;
        }
    }

    public partial class SpeakPunctuation
    {
        public sealed override string Property
        {
            get { return CssProperty.SpeakPunctuation; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.SpeakPunctuation = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.SpeakPunctuation;
        }
    }

    public partial class SpeechRate
    {
        public sealed override string Property
        {
            get { return CssProperty.SpeechRate; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.SpeechRate = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.SpeechRate;
        }
    }

    public partial class Stress
    {
        public sealed override string Property
        {
            get { return CssProperty.Stress; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Stress = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Stress;
        }
    }

    public partial class TableLayout
    {
        public sealed override string Property
        {
            get { return CssProperty.TableLayout; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.TableLayout = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.TableLayout;
        }
    }

    public partial class TextAlign
    {
        public sealed override string Property
        {
            get { return CssProperty.TextAlign; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.TextAlign = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.TextAlign;
        }
    }

    public partial class TextDecoration
    {
        public sealed override string Property
        {
            get { return CssProperty.TextDecoration; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.TextDecoration = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.TextDecoration;
        }
    }

    public partial class TextIndent
    {
        public sealed override string Property
        {
            get { return CssProperty.TextIndent; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.TextIndent = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.TextIndent;
        }
    }

    public partial class TextTransform
    {
        public sealed override string Property
        {
            get { return CssProperty.TextTransform; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.TextTransform = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.TextTransform;
        }
    }

    public partial class Top
    {
        public sealed override string Property
        {
            get { return CssProperty.Top; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Top = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Top;
        }
    }

    public partial class UnicodeBidi
    {
        public sealed override string Property
        {
            get { return CssProperty.UnicodeBidi; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.UnicodeBidi = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.UnicodeBidi;
        }
    }

    public partial class VerticalAlign
    {
        public sealed override string Property
        {
            get { return CssProperty.VerticalAlign; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.VerticalAlign = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.VerticalAlign;
        }
    }

    public partial class Visibility
    {
        public sealed override string Property
        {
            get { return CssProperty.Visibility; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Visibility = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Visibility;
        }
    }

    public partial class VoiceFamily
    {
        public sealed override string Property
        {
            get { return CssProperty.VoiceFamily; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.VoiceFamily = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.VoiceFamily;
        }
    }

    public partial class Volume
    {
        public sealed override string Property
        {
            get { return CssProperty.Volume; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Volume = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Volume;
        }
    }

    public partial class WhiteSpace
    {
        public sealed override string Property
        {
            get { return CssProperty.WhiteSpace; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.WhiteSpace = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.WhiteSpace;
        }
    }

    public partial class Widows
    {
        public sealed override string Property
        {
            get { return CssProperty.Widows; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Widows = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Widows;
        }
    }

    public partial class Width
    {
        public sealed override string Property
        {
            get { return CssProperty.Width; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.Width = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.Width;
        }
    }

    public partial class WordSpacing
    {
        public sealed override string Property
        {
            get { return CssProperty.WordSpacing; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.WordSpacing = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.WordSpacing;
        }
    }

    public partial class ZIndex
    {
        public sealed override string Property
        {
            get { return CssProperty.ZIndex; }
        }

        public sealed override void SetValue(IWithStyle box, CssValue value)
        {
            box.ZIndex = value;
        }

        public sealed override CssValue GetValue(IWithStyle box)
        {
            return box.ZIndex;
        }
    }

}