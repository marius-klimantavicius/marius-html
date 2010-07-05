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

namespace Marius.Html.Dom
{
    public class Element: Node
    {
        public string Name { get; private set; }

        public AttributeCollection Attributes { get; private set; }

        public string Id { get { return Attributes.Id; } set { Attributes.Id = value; } }
        public string Class { get { return Attributes.Class; } set { Attributes.Class = value; } }
        public string Style { get { return Attributes.Style; } set { Attributes.Style = value; } }

        public Element(string tagName)
            : this(tagName, (string)null)
        {
        }

        public Element(string tagName, string id)
            : this(tagName, new AttributeCollection(id))
        {
        }

        public Element(string tagName, ElementAttribute[] attributes)
            : this(tagName, new AttributeCollection(null, null, null, attributes))
        {

        }

        public Element(string tagName, AttributeCollection attributes)
        {
            Name = tagName;
            Attributes = attributes;
        }

        public override string ToString()
        {
            return string.Format("<{0} {1}>{2}</{0}>", Name, Attributes, Children);
        }
    }
}
