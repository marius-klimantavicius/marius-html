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

namespace Marius.Html.Css.Values
{
    public static class CssKeywords
    {
        public static readonly CssIdentifier Inherit = new CssIdentifier("inherit");
        public static readonly CssIdentifier None = new CssIdentifier("none");
        public static readonly CssIdentifier Transparent = new CssIdentifier("transparent");
        public static readonly CssIdentifier Auto = new CssIdentifier("auto");
        public static readonly CssIdentifier Hidden = new CssIdentifier("hidden");
        public static readonly CssIdentifier Both = new CssIdentifier("both");
        public static readonly CssIdentifier Initial = new CssIdentifier("initial");

        public static readonly CssIdentifier LeftSide = new CssIdentifier("left-side");
        public static readonly CssIdentifier FarLeft = new CssIdentifier("far-left");
        public static readonly CssIdentifier Left = new CssIdentifier("left");
        public static readonly CssIdentifier CenterLeft = new CssIdentifier("center-left");
        public static readonly CssIdentifier Center = new CssIdentifier("center");
        public static readonly CssIdentifier CenterRight = new CssIdentifier("center-right");
        public static readonly CssIdentifier Right = new CssIdentifier("right");
        public static readonly CssIdentifier FarRight = new CssIdentifier("far-right");
        public static readonly CssIdentifier RightSide = new CssIdentifier("right-side");

        public static readonly CssIdentifier Behind = new CssIdentifier("behind");
        public static readonly CssIdentifier Leftwards = new CssIdentifier("leftwards");
        public static readonly CssIdentifier Rightwards = new CssIdentifier("rightwards");

        public static readonly CssIdentifier Scroll = new CssIdentifier("scroll");
        public static readonly CssIdentifier Fixed = new CssIdentifier("fixed");

        public static readonly CssIdentifier Aqua = new CssIdentifier("aqua");
        public static readonly CssIdentifier Black = new CssIdentifier("black");
        public static readonly CssIdentifier Blue = new CssIdentifier("blue");
        public static readonly CssIdentifier Fuchsia = new CssIdentifier("fuchsia");
        public static readonly CssIdentifier Gray = new CssIdentifier("gray");
        public static readonly CssIdentifier Green = new CssIdentifier("green");
        public static readonly CssIdentifier Lime = new CssIdentifier("lime");
        public static readonly CssIdentifier Maroon = new CssIdentifier("maroon");
        public static readonly CssIdentifier Navy = new CssIdentifier("navy");
        public static readonly CssIdentifier Olive = new CssIdentifier("olive");
        public static readonly CssIdentifier Orange = new CssIdentifier("orange");
        public static readonly CssIdentifier Purple = new CssIdentifier("purple");
        public static readonly CssIdentifier Red = new CssIdentifier("red");
        public static readonly CssIdentifier Silver = new CssIdentifier("silver");
        public static readonly CssIdentifier Teal = new CssIdentifier("teal");
        public static readonly CssIdentifier White = new CssIdentifier("white");
        public static readonly CssIdentifier Yellow = new CssIdentifier("yellow");

        public static readonly CssIdentifier Top = new CssIdentifier("top");
        public static readonly CssIdentifier Bottom = new CssIdentifier("bottom");

        public static readonly CssIdentifier Repeat = new CssIdentifier("repeat");
        public static readonly CssIdentifier RepeatX = new CssIdentifier("repeat-x");
        public static readonly CssIdentifier RepeatY = new CssIdentifier("repeat-y");
        public static readonly CssIdentifier NoRepeat = new CssIdentifier("no-repeat");

        public static readonly CssIdentifier Collapse = new CssIdentifier("collapse");
        public static readonly CssIdentifier Separate = new CssIdentifier("separate");

        public static readonly CssIdentifier Dotted = new CssIdentifier("dotted");
        public static readonly CssIdentifier Dashed = new CssIdentifier("dashed");
        public static readonly CssIdentifier Solid = new CssIdentifier("solid");
        public static readonly CssIdentifier Double = new CssIdentifier("double");
        public static readonly CssIdentifier Groove = new CssIdentifier("groove");
        public static readonly CssIdentifier Ridge = new CssIdentifier("rigde");
        public static readonly CssIdentifier Inset = new CssIdentifier("inset");
        public static readonly CssIdentifier Outset = new CssIdentifier("outset");

        public static readonly CssIdentifier Thin = new CssIdentifier("thin");
        public static readonly CssIdentifier Medium = new CssIdentifier("medium");
        public static readonly CssIdentifier Thick = new CssIdentifier("thick");

        public static readonly CssIdentifier Normal = new CssIdentifier("normal");
        public static readonly CssIdentifier OpenQuote = new CssIdentifier("open-quote");
        public static readonly CssIdentifier CloseQuote = new CssIdentifier("close-quote");
        public static readonly CssIdentifier NoOpenQuote = new CssIdentifier("no-open-quote");
        public static readonly CssIdentifier NoCloseQuote = new CssIdentifier("no-close-quote");
    }
}
