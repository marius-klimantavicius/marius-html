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
    public class CssFixInlineBoxesStep: IGeneratorStep
    {
        public void Execute(CssContext context, CssBox root)
        {
            FixInlineBoxes(root);
        }

        private void FixInlineBoxes(CssBox box)
        {
            /*
             * NOT sure how to understand this:
             * When an inline box contains a block box, 
             * the inline box (and its inline ancestors within the same line box) are broken around the block. 
             * The line boxes before the break and after the break are enclosed in anonymous boxes, 
             * and the block box becomes a sibling of those anonymous boxes. When such an inline box is affected by relative positioning, 
             * the relative positioning also affects the block box. 
             */

            var current = box.FirstChild;
            while (current != null)
            {
                var next = current.NextSibling;
                if (!CssUtils.IsBlock(current))
                    FixInlineBoxes(current);
                current = next;
            }

            current = box.FirstChild;
            while (current != null)
            {
                if (!CssUtils.IsBlock(current))
                    SplitInlineBox(ref current);

                current = current.NextSibling;
            }
        }

        private void SplitInlineBox(ref CssBox box)
        {
            CssBox current = box.FirstChild;
            while (current != null)
            {
                if (!CssUtils.IsBlock(current))
                    SplitInlineBox(ref current);
                current = current.NextSibling;
            }

            CssBox block;
            current = box.FirstChild;
            while (current != null)
            {
                if (CssUtils.IsBlock(current))
                {
                    block = current;
                    current = current.NextSibling;

                    if (block.PreviousSibling == null)
                    {
                        // first
                        if (block.InheritanceParent == null)
                            block.InheritanceParent = block.Parent;
                        box.Parent.InsertBefore(block, box);
                    }
                    else if (block.NextSibling == null)
                    {
                        if (block.InheritanceParent == null)
                            block.InheritanceParent = block.Parent;
                        box.Parent.InsertAfter(block, box);
                    }
                    else
                    {
                        CssBox.Split(ref box, block);
                        current = null;
                    }
                }
                else
                {
                    current = current.NextSibling;
                }
            }
        }
    }
}
