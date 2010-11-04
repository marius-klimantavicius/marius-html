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
using Marius.Html.Css.Layout.BoxGeneration;

namespace Marius.Html.Css.Layout
{
    public class CssBoxGenerator
    {
        private CssContext _context;
        private IGeneratorStep[] _steps;

        public CssBoxGenerator(CssContext context)
        {
            _context = context;

            List<IGeneratorStep> steps = new List<IGeneratorStep>();
            InitializeGeneratorSteps(steps);
            _steps = steps.ToArray();
        }

        protected virtual void InitializeGeneratorSteps(IList<IGeneratorStep> steps)
        {
            steps.Add(new CssRunInBoxesStep());
            steps.Add(new CssFixInlineBoxesStep());
            steps.Add(new CssFixBlockBoxesStep());
            steps.Add(new CssExpandGeneratedContentStep());
            steps.Add(new CssSplitWordsStep());
        }

        public virtual CssInitialBox Generate(INode root)
        {
            CssInitialBox result = new CssInitialBox(_context);

            AddNode(result, root);

            for (int i = 0; i < _steps.Length; i++)
                _steps[i].Execute(_context, result);

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
    }
}
