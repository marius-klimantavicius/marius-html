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

namespace Marius.Html.Dom.Simple
{
    public class Element: IElement
    {
        public static readonly INode[] EmptyChildren = new INode[0];

        private string _name;
        private AttributeCollection _attributes;

        private INode[] _children;
        private INode _nextSibling;
        private INode _previousSibling;
        private INode _parent;

        public string Name
        {
            get { return _name; }
        }

        public IAttributeCollection Attributes
        {
            get { return _attributes; }
        }

        public string Id
        {
            get { return _attributes.Id; }
        }

        public string Class
        {
            get { return _attributes.Class; }
        }

        public string Style
        {
            get { return _attributes.Style; }
        }

        public INode[] Children
        {
            get { return _children; }
        }

        public INode FirstChild
        {
            get
            {
                if (_children == null || _children.Length == 0)
                    return null;
                return _children[0];
            }
        }

        public INode LastChild
        {
            get
            {
                if (_children == null || _children.Length == 0)
                    return null;
                return _children[_children.Length - 1];
            }
        }

        public INode NextSibling
        {
            get { return _nextSibling; }
        }

        public INode PreviousSibling
        {
            get { return _previousSibling; }
        }

        public INode Parent
        {
            get { return _parent; }
        }

        public Element(string name)
        {
            _name = name;
            _attributes = AttributeCollection.Empty;
            _children = EmptyChildren;
        }

        public Element(string name, string id)
        {
            _name = name;
            _attributes = new AttributeCollection(id);
            _children = EmptyChildren;
        }

        public Element(string name, INode[] children)
        {
            _name = name;
            _attributes = AttributeCollection.Empty;
            _children = children ?? EmptyChildren;

            for (int i = 0; i < _children.Length; i++)
            {
                if (_children[i] is Element)
                {
                    Element e = (Element)_children[i];

                    e._parent = this;
                    if (i > 0)
                        e._previousSibling = _children[i - 1];
                    if ((i + 1) < _children.Length)
                        e._nextSibling = _children[i + 1];
                }
                // TODO: change, as if there is text node, this will work incorrectly
            }
        }
    }
}
