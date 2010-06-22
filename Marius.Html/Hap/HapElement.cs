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

namespace Marius.Html.Hap
{
    public class HapElement: IElement
    {
        public static readonly HapElement[] EmptyElements = new HapElement[0];

        private HapElement _parent;
        private HapElement[] _children;

        public virtual ElementType ElementType { get { return ElementType.Element; } }

        public string Name { get; private set; }
        public string Id { get { return Attributes.Id; } }
        public string Class { get { return Attributes.Class; } }
        public string Style { get { return Attributes.Style; } }

        public AttributeCollection Attributes { get; private set; }

        public IElement Parent { get { return _parent; } }
        public IElement[] Children { get { return _children; } }

        // used for testing
        public HapElement(string name)
            : this(name, null, null)
        {
        }

        public HapElement(string name, string id)
            : this(name, new AttributeCollection(id, null, null, null), null)
        {
        }

        public HapElement(string name, AttributeCollection attributes, HapElement[] children)
        {
            Name = name;
            Attributes = attributes ?? AttributeCollection.Empty;
            _children = children ?? EmptyElements;

            if (children != null)
            {
                for (int i = 0; i < Children.Length; i++)
                    _children[i]._parent = this;
            }
        }
    }
}
