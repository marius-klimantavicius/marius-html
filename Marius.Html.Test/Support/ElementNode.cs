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
using Marius.Html.Css;
using System.Dynamic;

namespace Marius.Html.Tests.Support
{
    public class ElementNode: DynamicObject, IElementNode
    {
        private AttributeCollection _attributes;

        public string TagName { get; private set; }
        public string Id { get { return _attributes.Id; } }

        public IAttributeCollection Attributes { get { return _attributes; } }

        public NodeType NodeType { get { return NodeType.Element; } }

        public INode Parent { get; set; }

        public INode PreviousSibling { get; set; }
        public INode NextSibling { get; set; }

        public INode FirstChild { get; set; }
        public INode LastChild { get; set; }

        public IWithStyle Style { get; private set; }
        public IWithStyle FirstLineStyle { get; private set; }
        public IWithStyle FirstLetterStyle { get; private set; }
        public IWithStyle BeforeStyle { get; private set; }
        public IWithStyle AfterStyle { get; private set; }

        public ElementNode(string tagName, IList<ElementAttribute> attributes)
            : this(tagName, attributes, null)
        {
        }

        public ElementNode(string tagName, IList<INode> nodes)
            : this(tagName, null, nodes)
        {
        }

        public ElementNode(string tagName, IList<ElementAttribute> attributes, IList<INode> children)
        {
            TagName = tagName;

            Style = new StyleInfo();
            FirstLineStyle = new StyleInfo();
            FirstLetterStyle = new StyleInfo();
            BeforeStyle = new StyleInfo();
            AfterStyle = new StyleInfo();

            _attributes = new AttributeCollection(attributes);

            if (children != null)
            {
                dynamic prev = null;
                for (int i = 0; i < children.Count; i++)
                {
                    dynamic current = children[i];

                    current.Parent = this;

                    if (i == 0)
                        FirstChild = current;

                    current.PreviousSibling = prev;
                    if (prev != null)
                        prev.NextSibling = current;

                    prev = current;
                }

                LastChild = prev;
            }
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            var nodes = ElementDynamicObject.NodeListFromArgs(args);

            dynamic prev = LastChild;
            for (int i = 0; i < nodes.Count; i++)
            {
                dynamic current = nodes[i];
                current.PreviousSibling = prev;
                if (prev != null)
                    prev.NextSibling = current;
            }

            LastChild = prev;

            result = this;
            return true;
        }

        private void Append(INode node)
        {
            throw new NotImplementedException();
        }
    }
}
