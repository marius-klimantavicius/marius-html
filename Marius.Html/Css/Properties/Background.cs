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
    public class Background: CssProperty
    {
        public static readonly ParseFunc<Background> Parse;

        public BackgroundColor BackgroundColor { get; private set; }
        public BackgroundImage BackgroundImage { get; private set; }
        public BackgroundRepeat BackgroundRepeat { get; private set; }
        public BackgroundPosition BackgroundPosition { get; private set; }
        public BackgroundAttachment BackgroundAttachment { get; private set; }

        static Background()
        {
            ParseFunc<Background> colorFunc = (e, c) => BackgroundColor.Parse(e, c.BackgroundColor);
            ParseFunc<Background> imageFunc = (e, c) => BackgroundImage.Parse(e, c.BackgroundImage);
            ParseFunc<Background> repeatFunc = (e, c) => BackgroundRepeat.Parse(e, c.BackgroundRepeat);
            ParseFunc<Background> positionFunc = (e, c) => BackgroundPosition.Parse(e, c.BackgroundPosition);
            ParseFunc<Background> attachmentFunc = (e, c) => BackgroundAttachment.Parse(e, c.BackgroundAttachment);

            var inherit = CssPropertyParser.Match<Background>(CssValue.Inherit, (s, c) =>
                {
                    c.BackgroundAttachment = new BackgroundAttachment(s);
                    c.BackgroundColor = new BackgroundColor(s);
                    c.BackgroundImage = new BackgroundImage(s);
                    c.BackgroundPosition = new BackgroundPosition(s, s);
                    c.BackgroundRepeat = new BackgroundRepeat(s);
                });

            var shand = CssPropertyParser.Pipe<Background>(colorFunc, imageFunc, repeatFunc, positionFunc, attachmentFunc);

            Parse = CssPropertyParser.Any(inherit, shand);
        }

        public Background()
            : this(new BackgroundColor(), new BackgroundImage(), new BackgroundPosition(), new BackgroundRepeat(), new BackgroundAttachment())
        {
        }

        public Background(BackgroundColor color, BackgroundImage image, BackgroundPosition position, BackgroundRepeat repeat, BackgroundAttachment attachment)
        {
            BackgroundColor = color;
            BackgroundImage = image;
            BackgroundPosition = position;
            BackgroundRepeat = repeat;
            BackgroundAttachment = attachment;
        }

        public static Background Create(CssExpression expression, bool full = true)
        {
            Background result = new Background();
            if (Parse(expression, result))
            {
                if (full && expression.Current != null)
                    return null;

                return result;
            }

            return null;
        }
    }
}
