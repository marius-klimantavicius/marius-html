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
    public class AttributeCollection
    {
        private SortedDictionary<string, ElementAttribute> _attributes;
        private ElementAttribute[] _attributeArray;

        public string Id { get; set; }
        public string Class { get; set; }
        public string Style { get; set; }

        public string this[string attributeName]
        {
            get
            {
                var att = GetAttributeNode(attributeName);
                if (att == null)
                    return null;
                return att.Value;
            }
            set
            {
                var att = GetAttributeNode(attributeName);
                if (att == null)
                    SetAttributeNode(new ElementAttribute(attributeName, value));
                else
                    att.Value = value;
            }
        }

        public AttributeCollection()
            : this(null, null, null)
        {
        }

        public AttributeCollection(string id)
            : this(id, null, null)
        {
        }

        public AttributeCollection(string id, string klass, string style)
            : this(id, klass, style, null)
        {
        }

        public AttributeCollection(string id, string klass, string style, IEnumerable<ElementAttribute> attributes)
        {
            Id = id;
            Class = klass;
            Style = style;

            _attributes = new SortedDictionary<string, ElementAttribute>();

            if (attributes != null)
            {
                foreach (var item in attributes)
                {
                    SetAttributeNode(item);
                }
            }
        }

        public ElementAttribute GetAttributeNode(string name)
        {
            ElementAttribute value;
            if (_attributes.TryGetValue(name, out value))
                return value;

            return null;
        }

        public void SetAttributeNode(ElementAttribute att)
        {
            if (_attributes.ContainsKey(att.Name))
                _attributes[att.Name] = att;
            else
                _attributes.Add(att.Name, att);

            _attributeArray = null;
        }

        public bool Contains(string name)
        {
            return _attributes.ContainsKey(name);
        }

        public ElementAttribute[] ToArray()
        {
            if (_attributeArray == null)
            {
                _attributeArray = new ElementAttribute[_attributes.Count];
                _attributes.Values.CopyTo(_attributeArray, 0);
            }

            return _attributeArray;
        }

        public override string ToString()
        {
            return String.Join(" ", (object[])ToArray());
        }
    }
}
