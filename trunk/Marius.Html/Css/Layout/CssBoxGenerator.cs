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
using Marius.Html.Dom;
using Marius.Html.Css.Values;
using Marius.Html.Css.Box;
using System.Diagnostics;
using Marius.Html.Internal;

namespace Marius.Html.Css.Layout
{
    public class CssBoxGenerator
    {
        private CssContext _context;

        private Dictionary<CssBox, bool> _hasBlock;

        public CssBoxGenerator(CssContext context)
        {
            _context = context;
        }

        public virtual CssInitialBox Generate(INode root)
        {
            CssInitialBox result = new CssInitialBox(_context);

            AddNode(result, root);

            _hasBlock = new Dictionary<CssBox, bool>();

            RunInBoxes(result);
            FixInlineBoxes(result);
            FixBlockBoxes(result);

            _hasBlock = null;

            return result;
        }

        protected virtual void AddNode(CssBox parent, INode node)
        {
            CssBox current = null;
            if (node.NodeType == NodeType.Text)
                current = CreateTextBox((ITextNode)node);
            else if (node.NodeType == NodeType.Element)
                current = CreateElementBox((IElementNode)node);
            else if (node.NodeType == NodeType.Image)
                current = CreateImageBox((IImageNode)node);
            else
                current = CreateCustomBox(node);

            if (current != null)
                parent.Append(current);
        }

        protected virtual CssBox CreateCustomBox(INode node)
        {
            return null;
        }

        protected virtual CssBox CreateImageBox(IImageNode node)
        {
            CssImageBox result = new CssImageBox(_context);

            // TODO: implement

            return result;
        }

        protected virtual CssBox CreateElementBox(IElementNode node)
        {
            CssBox result = new CssBox(_context);
            node.Style.CopyTo(result.Properties);

            if (node.FirstLineStyle.HasStyle)
                node.FirstLineStyle.CopyTo(result.FirstLineProperties);

            if (node.FirstLetterStyle.HasStyle)
                node.FirstLetterStyle.CopyTo(result.FirstLetterProperties);

            if (node.BeforeStyle.HasStyle)
            {
                if (!node.BeforeStyle.Content.Equals(CssKeywords.None))
                {
                    CssGeneratedBox before = new CssGeneratedBox(_context);
                    node.BeforeStyle.CopyTo(before.Properties);
                    result.Append(before);
                }
            }

            INode child = node.FirstChild;
            while (child != null)
            {
                AddNode(result, child);
                child = child.NextSibling;
            }

            if (node.AfterStyle.HasStyle)
            {
                if (!node.AfterStyle.Content.Equals(CssKeywords.None))
                {
                    CssGeneratedBox after = new CssGeneratedBox(_context);
                    node.AfterStyle.CopyTo(after.Properties);
                    result.Append(after);
                }
            }
            return result;
        }

        protected virtual CssBox CreateTextBox(ITextNode node)
        {
            CssBox result;

            string text = node.Data;
            if (string.IsNullOrWhiteSpace(text))
                result = new CssAnonymousSpaceBox(_context, text);
            else
                result = new CssAnonymousInlineBox(_context, text);

            Debug.Assert(node.FirstChild == null);
            Debug.Assert(!node.Style.HasStyle);
            Debug.Assert(!node.FirstLineStyle.HasStyle);
            Debug.Assert(!node.FirstLetterStyle.HasStyle);
            Debug.Assert(!node.BeforeStyle.HasStyle);
            Debug.Assert(!node.AfterStyle.HasStyle);

            return result;
        }

        private void RunInBoxes(CssBox box)
        {
            CssBox current = box.FirstChild;
            while (current != null)
            {
                RunInBoxes(current);
                current = current.NextSibling;
            }

            current = box.FirstChild;
            while (current != null)
            {
                CssBox next = current.NextSibling;

                var display = current.Computed.Display;
                if (display.Equals(CssKeywords.RunIn))
                {
                    bool rfixed = false;

                    //1. If the run-in box contains a block box, the run-in box becomes a block box.
                    if (ContainsBlockBox(current))
                    {
                        current.Properties.Display = CssKeywords.Block;
                        rfixed = true;
                    }

                    //2. If a sibling block box (that does not float and is not absolutely positioned) follows the run-in box, the run-in box becomes the first inline box of the block box. A run-in cannot run in to a block that already starts with a run-in or that itself is a run-in.
                    if (!rfixed && next != null && IsBlockBox(next))
                    {
                        var nfloat = next.Computed.Float;
                        var npos = next.Computed.Position;

                        if (nfloat.Equals(CssKeywords.None)
                            && !(npos.Equals(CssKeywords.Absolute) || npos.Equals(CssKeywords.Fixed)))
                        {
                            if (next.FirstChild == null || (!next.FirstChild.IsRunIn))
                            {
                                current.Properties.Display = CssKeywords.Inline;
                                current.IsRunIn = true;
                                current.InheritanceParent = current.Parent;

                                next.Insert(current);
                                rfixed = true;
                            }
                        }
                    }

                    //3. Otherwise, the run-in box becomes a block box.
                    if (!rfixed)
                    {
                        current.Properties.Display = CssKeywords.Block;
                        rfixed = true;
                    }
                }

                current = next;
            }
        }

        private void FixBlockBoxes(CssBox box)
        {
            CssBox current = box.FirstChild;
            while (current != null)
            {
                FixBlockBoxes(current);
                current = current.NextSibling;
            }

            if (IsBlockBox(box))
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

                if (IsBlockBox(current))
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
                if (!IsBlockBox(current))
                    FixInlineBoxes(current);
                current = next;
            }

            current = box.FirstChild;
            while (current != null)
            {
                if (!IsBlockBox(current))
                    SplitInlineBox(ref current);

                current = current.NextSibling;
            }
        }

        private void ReplaceWithAnonymousBlock(CssBox parent, CssBox start, CssBox end)
        {
            var block = new CssAnonymousBlockBox(_context);

            parent.Wrap(start, end, block);
        }

        private void SplitInlineBox(ref CssBox box)
        {
            CssBox current = box.FirstChild;
            while (current != null)
            {
                if (!IsBlockBox(current))
                    SplitInlineBox(ref current);
                current = current.NextSibling;
            }

            CssBox block;
            current = box.FirstChild;
            while (current != null)
            {
                if (IsBlockBox(current))
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

        private bool ContainsBlockBox(CssBox box)
        {
            if (_hasBlock.ContainsKey(box))
                return _hasBlock[box];

            CssBox current = box.FirstChild;
            while (current != null)
            {
                if (IsBlockBox(current))
                {
                    _hasBlock.Add(box, true);
                    return true;
                }

                current = current.NextSibling;
            }

            current = box.FirstChild;
            while (current != null)
            {
                if (ContainsBlockBox(current))
                {
                    _hasBlock.Add(box, true);
                    return true;
                }
                current = current.NextSibling;
            }

            return false;
        }

        private bool IsBlockBox(CssBox box)
        {
            // TODO: performance, this method might be called frequently and might need optimizations
            // for now leaving as is as it is simpler and no one knows if this code survives for long
            var display = box.Computed.Display;
            return display.Equals(CssKeywords.Block)
                || display.Equals(CssKeywords.ListItem)
                || display.Equals(CssKeywords.Table);
        }
    }
}
