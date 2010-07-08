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

namespace Marius.Html.Css.Layout
{
    public class CssBoxGenerator
    {
        private CssContext _context;

        public CssBoxGenerator(CssContext context)
        {
            _context = context;
        }

        public virtual CssInitialBox Generate(INode root)
        {
            CssInitialBox result = new CssInitialBox(_context);

            AddNode(result, root);

            FixBoxes(result);

            return result;
        }

        private void FixBoxes(CssBox box)
        {
            throw new NotImplementedException();
        }

        private void AddNode(CssBox parent, INode node)
        {
            CssBox current = null;
            if (node.NodeType == NodeType.Text)
            {
                string text = ((ITextNode)node).Data;
                if (string.IsNullOrWhiteSpace(text))
                    current = new CssAnonymousSpaceBox(_context, text);
                else
                    current = new CssAnonymousInlineBox(_context, text);

                Debug.Assert(node.FirstChild == null);
                Debug.Assert(!node.Style.HasStyle);
                Debug.Assert(!node.FirstLineStyle.HasStyle);
                Debug.Assert(!node.FirstLetterStyle.HasStyle);
                Debug.Assert(!node.BeforeStyle.HasStyle);
                Debug.Assert(!node.AfterStyle.HasStyle);
            }
            else
            {
                current = new CssBox(_context);
                
                if (node.FirstLineStyle.HasStyle)
                    node.FirstLineStyle.CopyTo(current.FirstLineProperties);

                if (node.FirstLetterStyle.HasStyle)
                    node.FirstLetterStyle.CopyTo(current.FirstLetterProperties);

                if (node.BeforeStyle.HasStyle)
                {
                    if (!node.BeforeStyle.Content.Equals(CssKeywords.None))
                    {
                        CssGeneratedBox before = new CssGeneratedBox(_context);
                        node.BeforeStyle.CopyTo(before.Properties);
                        current.Append(before);
                    }
                }

                INode child = node.FirstChild;
                while (child != null)
                {
                    AddNode(current, child);
                    child = child.NextSibling;
                }

                if (node.AfterStyle.HasStyle)
                {
                    if (!node.AfterStyle.Content.Equals(CssKeywords.None))
                    {
                        CssGeneratedBox after = new CssGeneratedBox(_context);
                        node.AfterStyle.CopyTo(after.Properties);
                        current.Append(after);
                    }
                }
            }

            parent.Append(current);
        }
    }
}
