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

namespace Marius.Html.Css.Layout.BoxGeneration
{
    public class CssFixBlockBoxesStep: IGeneratorStep
    {
        private CssContext _context;

        public void Execute(CssContext context, CssBox root)
        {
            _context = context;
            FixBlockBoxes(root);
            _context = null;
        }

        private void FixBlockBoxes(CssBox box)
        {
            CssBox current = box.FirstChild;
            while (current != null)
            {
                FixBlockBoxes(current);
                current = current.NextSibling;
            }

            if (CssUtils.IsBlock(box) || CssUtils.IsInlineBlock(box)) // TODO: inline table??
                FixBlockBox(box);
        }

        private void FixBlockBox(CssBox box)
        {
            bool hasBlock = false;
            CssBox start, end;

            start = end = null;

            CssBox current = box.FirstChild;
            while (current != null)
            {
                CssBox next = current.NextSibling;

                if (CssUtils.IsBlock(current))
                {
                    hasBlock = true;
                    if (start != null)
                        ReplaceWithAnonymousBlock(box, start, end);

                    start = end = null;
                }
                else
                {
                    if (start == null)
                        start = end = current;
                    else
                        end = current;
                }

                current = next;
            }

            if (start != null && hasBlock)
                ReplaceWithAnonymousBlock(box, start, end);
        }

        private void ReplaceWithAnonymousBlock(CssBox parent, CssBox start, CssBox end)
        {
            var block = new CssAnonymousBlockBox(_context);

            parent.Wrap(start, end, block);
        }
    }
}
