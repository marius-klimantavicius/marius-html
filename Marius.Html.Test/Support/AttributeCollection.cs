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

namespace Marius.Html.Tests.Support
{
    public class AttributeCollection: IAttributeCollection
    {
        private Dictionary<string, string> _attributes;

        public string Id { get; private set; }
        public string Style { get; private set; }
        public string Class { get; private set; }

        public AttributeCollection(IEnumerable<ElementAttribute> attributes)
        {
            _attributes = new Dictionary<string, string>();

            if (attributes != null)
            {
                foreach (var item in attributes)
                {
                    if (StringComparer.InvariantCultureIgnoreCase.Equals("style", item.Name))
                        Style = item.Value;
                    else if (StringComparer.InvariantCultureIgnoreCase.Equals("class", item.Name))
                        Class = item.Value;
                    else if (StringComparer.InvariantCultureIgnoreCase.Equals("id", item.Name))
                        Id = item.Value;

                    this[item.Name] = item.Value;
                }
            }
        }

        public string this[string attributeName]
        {
            get
            {
                string value;
                if (!_attributes.TryGetValue(attributeName, out value))
                    return null;
                return value;
            }
            set
            {
                if (_attributes.ContainsKey(attributeName))
                    _attributes[attributeName] = value;
                else
                    _attributes.Add(attributeName, value);
            }
        }

        public bool Contains(string attributeName)
        {
            return _attributes.ContainsKey(attributeName);
        }
    }
}
