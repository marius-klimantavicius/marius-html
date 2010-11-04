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
    public class CssSplitWordsStep: IGeneratorStep
    {
        public void Execute(CssContext context, CssBox root)
        {
            if (!CssUtils.IsBlock(root))
                return; // we cannot split to words without block currently (this implies that InitialRoot should be block)

            new Engine(context).ProcessBlock(root);
        }

        private class Engine
        {
            private CssContext _context;
            private Stack<CssBox> _blockStack;
            private CssBox Block { get { return _blockStack.Peek(); } }

            public Engine(CssContext context)
            {
                _context = context;
                _blockStack = new Stack<CssBox>();
            }

            public void ProcessBlock(CssBox box)
            {
                PushBlock(box);

                var current = box.FirstChild;
                while (current != null)
                {
                    if (CssUtils.IsBlock(current))
                        ProcessBlock(current);
                    else
                        ProcessInline(current);

                    current = current.NextSibling;
                }

                PopBlock();
            }

            private void ProcessInline(CssBox box)
            {
                /* we have the following cases:
                 *      Normal inline - Traverse or if text box: split
                 *      InlineTable/InlineBlock - Add it as word, traverse as block
                 */

                if (box.Computed.Display.Equals(CssKeywords.Inline))
                {
                    // simple inline or text?
                    if (box is CssAnonymousInlineBox)
                    {
                        Block.AddRawWords(CssBoxWord.Create((CssAnonymousInlineBox)box));
                    }
                    else if (box is CssAnonymousSpaceBox)
                    {
                        Block.AddRawWords(CssBoxWord.Create((CssAnonymousSpaceBox)box));
                    }
                    else
                    {
                        CssBox current = box.FirstChild;
                        while (current != null)
                        {
                            ProcessInline(current);
                            current = current.NextSibling;
                        }
                    }
                }
                else
                {                    
                    // we have to add this as word to current block, and traverse this as block
                    // TODO: what about table??
                    Block.AddRawWords(CssBoxWord.CreateRaw(box));
                    ProcessBlock(box);  // automatically pushes a new box to be current block
                }
            }

            private void PushBlock(CssBox newCurrent)
            {
                _blockStack.Push(newCurrent);
            }

            private void PopBlock()
            {
                _blockStack.Pop();
            }
        }
    }
}
