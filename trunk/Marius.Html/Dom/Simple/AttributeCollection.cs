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
    public class AttributeCollection: IAttributeCollection
    {
        public static readonly AttributeCollection Empty = new AttributeCollection();
        public static IAttribute[] EmptyAttributes = new IAttribute[0];

        private string _id;
        private string _class;
        private string _style;

        private IAttribute[] _attributes;
        private Dictionary<string, IAttribute> _attributeMap;

        public string Id
        {
            get { return _id; }
        }

        public string Class
        {
            get { return _class; }
        }

        public string Style
        {
            get { return _style; }
        }

        public int Count
        {
            get { return _attributes.Length; }
        }

        public IAttribute this[int index]
        {
            get
            {
                if (index >= _attributes.Length)
                    return null;
                return _attributes[index];
            }
        }

        public IAttribute this[string name]
        {
            get
            {
                IAttribute result;
                if (!_attributeMap.TryGetValue(name, out result))
                    return null;
                return result;
            }
        }

        public string ValueOf(string name)
        {
            IAttribute attribute = this[name];
            if (attribute == null)
                return null;
            return attribute.Value;
        }

        public bool Contains(string name)
        {
            return _attributeMap.ContainsKey(name);
        }

        public AttributeCollection()
        {
            _attributeMap = new Dictionary<string, IAttribute>();
            _attributes = EmptyAttributes;
        }

        public AttributeCollection(string id)
            : this(id, null, null)
        {
        }

        public AttributeCollection(string id, string @class, string style)
            : this(id, @class, style, null)
        {
        }

        public AttributeCollection(string id, string @class, string style, IAttribute[] attributes)
        {
            _id = id;
            _class = @class;
            _style = style;

            _attributes = attributes ?? EmptyAttributes;
            _attributeMap = new Dictionary<string, IAttribute>(StringComparer.InvariantCultureIgnoreCase);

            for (int i = 0; i < _attributes.Length; i++)
            {
                if (_attributeMap.ContainsKey(_attributes[i].Name))
                    _attributeMap[_attributes[i].Name] = _attributes[i];
                else
                    _attributeMap.Add(_attributes[i].Name, _attributes[i]); 
            }
        }

        public override string ToString()
        {
            return String.Join<ElementAttribute>(" ", (IList<ElementAttribute>)_attributes);
        }
    }
}
