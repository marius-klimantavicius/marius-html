﻿#region License
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

namespace Marius.Html.Css
{
    public sealed class CssPropertyDictionary
    {
        private Dictionary<string, CssPropertyHandler> _properties;

        public CssPropertyDictionary()
        {
            _properties = new Dictionary<string, CssPropertyHandler>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void Add(CssPropertyHandler handler)
        {
            Add(handler.Property, handler);
        }

        public bool Remove(string key)
        {
            return _properties.Remove(key);
        }

        public CssPropertyHandler this[string key]
        {
            get
            {
                if (_properties.ContainsKey(key))
                    return _properties[key];
                return CssNullPropertyHandler.Instance;
            }
            set
            {
                if (!_properties.ContainsKey(key))
                    _properties.Add(key, value);
                else
                    _properties[key] = value;
            }
        }

        private void Add(string key, CssPropertyHandler value)
        {
            _properties.Add(key, value);
        }
    }
}
