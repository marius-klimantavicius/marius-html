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
using System.Dynamic;
using Marius.Html.Dom;
using Marius.Html.Dom.Simple;

namespace Marius.Html.Tests.Support
{
    public class ElementStub: DynamicObject, IElement
    {
        private string _name;
        private IAttributeCollection _attributes;
        private List<INode> _children;
        private INode _parent;
        private INode _next, _prev;

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
            get { return _children.ToArray(); }
        }

        public INode FirstChild
        {
            get
            {
                if (_children.Count == 0)
                    return null;
                return _children[0];
            }
        }

        public INode LastChild
        {
            get
            {
                if (_children.Count == 0)
                    return null;
                return _children[_children.Count - 1];
            }
        }

        public INode NextSibling
        {
            get { return _next; }
            set { _next = value; }
        }

        public INode PreviousSibling
        {
            get { return _prev; }
            set { _prev = value; }
        }

        public INode Parent
        {
            get { return _parent; }
            set { _parent = value; }
        }

        public ElementStub(string name, ElementAttribute[] attributes)
        {
            _name = name;

            string id = null, klass = null, style = null;

            for (int i = 0; i < attributes.Length; i++)
            {
                var a = attributes[i];

                if (StringComparer.InvariantCultureIgnoreCase.Equals("id", a.Name))
                    id = a.Value;
                if (StringComparer.InvariantCultureIgnoreCase.Equals("class", a.Name))
                    klass = a.Value;
                if (StringComparer.InvariantCultureIgnoreCase.Equals("style", a.Name))
                    style = a.Value;
            }

            _attributes = new AttributeCollection(id, klass, style, attributes);
            _children = new List<INode>();
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == null || (!(args[i] is ElementStub) && !(args[i] is TextStub) && !(args[i] is string) && !(args[i] is ElementObject)))
                    continue;

                if (args[i] is TextStub)
                {
                    AddLast((TextStub)args[i]);
                }
                else if (args[i] is ElementStub)
                {
                    AddLast((ElementStub)args[i]);
                }
                else if (args[i] is string)
                {
                    AddLast(new TextStub((string)args[i]));
                }
                else if (args[i] is ElementObject)
                {
                    ElementObject e = (ElementObject)args[i];
                    AddLast(new ElementStub(e.Name, new ElementAttribute[0]));
                }
            }

            result = this;
            return true;
        }

        private void AddLast(TextStub text)
        {
            text.Parent = this;

            var prev = LastChild;

            if (prev is TextStub)
            {
                ((TextStub)prev).NextSibling = text;
            }
            else if (prev is ElementStub)
            {
                ((ElementStub)prev).NextSibling = text;
            }

            text.PreviousSibling = prev;
            _children.Add(text);
        }

        private void AddLast(ElementStub node)
        {
            node.Parent = this;
            var prev = LastChild;

            if (prev is TextStub)
            {
                ((TextStub)prev).NextSibling = node;
            }
            else if (prev is ElementStub)
            {
                ((ElementStub)prev).NextSibling = node;
            }

            node.PreviousSibling = prev;
            _children.Add(node);
        }

        public override string ToString()
        {
            return string.Format("<{0} {1}>[child count: {2}]</{0}>", _name, _attributes, _children.Count);
        }
    }
}