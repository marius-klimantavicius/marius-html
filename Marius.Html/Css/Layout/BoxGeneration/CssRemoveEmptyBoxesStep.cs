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
using Marius.Html.Css.Box;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Layout.BoxGeneration
{
    public class CssRemoveEmptyBoxesStep: IGeneratorStep
    {
        public void Execute(CssContext context, CssBox root)
        {
            RemoveEmptyBoxes(root);
        }

        private void RemoveEmptyBoxes(CssBox box)
        {
            if (box == null)
                return;

            var current = box.FirstChild;
            var prev = current;
            while (current != null)
            {
                current = current.NextSibling;

                CssAnonymousSpaceBox space = prev as CssAnonymousSpaceBox;
                if (space != null)
                {
                    var ws = space.Computed.WhiteSpace;
                    if (CssKeywords.Normal.Equals(ws) || CssKeywords.Nowrap.Equals(ws))
                        box.Remove(space);
                    else
                    {
                        bool hasNewline = space.Text.IndexOfAny(new[] { '\r', '\n' }) != -1;
                        if (!hasNewline && CssKeywords.PreLine.Equals(ws))
                            box.Remove(space);
                    }
                }
                else
                {
                    if (prev != null)
                        RemoveEmptyBoxes(prev);
                }

                prev = current;
            }
        }
    }
}
