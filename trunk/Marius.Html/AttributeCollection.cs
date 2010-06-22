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

namespace Marius.Html
{
    public class AttributeCollection
    {
        private static readonly Tuple<string, string>[] EmptyArray = new Tuple<string, string>[0];
        public static readonly AttributeCollection Empty = new AttributeCollection(null, null, null, null);

        private Dictionary<string, string> _attributes;
        private Tuple<string, string>[] _attributeArray;

        public string Id { get; private set; }
        public string Class { get; private set; }
        public string Style { get; private set; }

        public AttributeCollection(string id, string @class, string style, IList<Tuple<string, string>> attributes)
        {
            Id = id;
            Class = @class;
            Style = style;

            _attributes = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

            if (attributes != null)
            {
                _attributeArray = attributes.ToArray();

                // how should I handle duplicate attributes? [currently the LAST value will be used]
                for (int i = 0; i < attributes.Count; i++)
                {
                    if (!_attributes.ContainsKey(attributes[i].Item1))
                        _attributes.Add(attributes[i].Item1, attributes[i].Item2);
                    else
                        _attributes[attributes[i].Item1] = attributes[i].Item2;
                }

                // do not rely on attributes to provide id, class and style [attribute names might be different]
            }
            else
            {
                _attributeArray = EmptyArray;
            }
        }

        public virtual int Count { get { return _attributeArray.Length; } }

        public virtual bool ContainsAttribute(string name)
        {
            return _attributes.ContainsKey(name);
        }

        public virtual Tuple<string, string> this[int index]
        {
            get
            {
                if (index >= _attributeArray.Length)
                    return null;
                return _attributeArray[index];
            }
        }

        public virtual string this[string attribute]
        {
            get
            {
                if (!_attributes.ContainsKey(attribute))
                    return null;
                return _attributes[attribute];
            }
        }
    }
}
