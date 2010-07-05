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
using System.Dynamic;
using Marius.Html.Dom;
using System.Linq.Expressions;

namespace Marius.Html.Tests.Support
{
    public class ElementDynamicObject: DynamicObject
    {
        private string _name;
        private List<ElementAttribute> _attributes;
        private List<object> _children;

        public ElementDynamicObject(string name)
            : this(name, null, null)
        {
        }

        private ElementDynamicObject(string name, List<ElementAttribute> attributes)
            : this(name, attributes, null)
        {
        }

        private ElementDynamicObject(string name, List<ElementAttribute> attributes, List<object> children)
        {
            _name = name;
            _attributes = attributes;
            _children = children;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            List<ElementAttribute> attributes = _attributes ?? new List<ElementAttribute>();
            for (int i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] is ElementAttribute)
                    attributes.Add((ElementAttribute)indexes[i]);
                else if (indexes[i] is string)
                    attributes.Add(new ElementAttribute((string)indexes[i]));
                else if (indexes[i] is AttributeDynamicObject)
                    attributes.Add(new ElementAttribute(((AttributeDynamicObject)indexes[i]).Name));
            }

            result = new ElementDynamicObject(_name, attributes);
            return true;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            List<object> children = _children ?? new List<object>();

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is ElementDynamicObject)
                    children.Add((ElementDynamicObject)args[i]);
                else if (args[i] is string)
                    children.Add(new TextNode((string)args[i]));
            }

            result = new ElementDynamicObject(_name, _attributes, children);
            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            result = null;
            if (binder.Type != typeof(Element) && binder.Type != typeof(Node))
                return false;

            AttributeCollection attributes;
            if (_attributes == null)
                attributes = new AttributeCollection();
            else
            {
                string id = null, klass = null, style = null;

                for (int i = 0; i < _attributes.Count; i++)
                {
                    if (StringComparer.InvariantCultureIgnoreCase.Equals(_attributes[i].Name, "id"))
                        id = _attributes[i].Value;
                    if (StringComparer.InvariantCultureIgnoreCase.Equals(_attributes[i].Name, "class"))
                        klass = _attributes[i].Value;
                    if (StringComparer.InvariantCultureIgnoreCase.Equals(_attributes[i].Name, "style"))
                        style = _attributes[i].Value;
                }

                attributes = new AttributeCollection(id, klass, style, _attributes);
            }

            Element item = new Element(_name, attributes);

            if (_children != null)
            {
                dynamic child;
                for (int i = 0; i < _children.Count; i++)
                {
                    child = _children[i];
                    item.Append(child);
                }
            }

            result = item;
            return true;
        }

        public override DynamicMetaObject GetMetaObject(Expression parameter)
        {
            return base.GetMetaObject(parameter);
        }
    }
}
