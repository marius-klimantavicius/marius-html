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
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Marius.Html.Css.Values;

namespace Marius.Html.Css.Box.Debug
{
    public partial class BoxDebugDisplay: Form
    {
        private CssBox _box;

        public CssBox Box
        {
            get { return _box; }
            set
            {
                _box = value;

                FillTree(_box);
            }
        }

        private void FillTree(CssBox box)
        {
            tree.Nodes.Clear();

            TreeNode node = new TreeNode(BoxText(box));
            node.Tag = box;

            tree.Nodes.Add(node);

            var current = box.FirstChild;
            while (current != null)
            {
                FillTree(node, current);
                current = current.NextSibling;
            }
        }

        private void FillTree(TreeNode parentNode, CssBox box)
        {
            TreeNode node = new TreeNode(BoxText(box));
            node.Tag = box;

            parentNode.Nodes.Add(node);

            var current = box.FirstChild;
            while (current != null)
            {
                FillTree(node, current);
                current = current.NextSibling;
            }
        }

        private string BoxText(CssBox box)
        {
            if (box is CssAnonymousBlockBox)
            {
                return string.Format("<anonymous block>");
            }
            else if (box is CssAnonymousInlineBox)
            {
                return string.Format("{0}", ((CssAnonymousInlineBox)box).Text);
            }
            else if (box is CssAnonymousSpaceBox)
            {
                return string.Format("<anonymous spaces>");
            }
            else if (box is CssGeneratedBox)
            {
                return string.Format("<generated>");
            }
            else if (box is CssInitialBox)
            {
                return string.Format("<initial>");
            }
            else if (box is CssSplitBox)
            {
                return string.Format("<split>: {0}", ((CssSplitBox)box).Part);
            }
            else
            {
                return "<box>";
            }
        }

        public BoxDebugDisplay()
        {
            InitializeComponent();
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            CssBox box = (CssBox)e.Node.Tag;
            if (box == null)
                return;

            properties.SelectedObject = box.Properties;
        }
    }
}
