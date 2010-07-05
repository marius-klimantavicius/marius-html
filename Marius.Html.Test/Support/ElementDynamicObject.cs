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

        public ElementDynamicObject(string name)
        {
            _name = name;
        }

        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            List<ElementAttribute> attributes = new List<ElementAttribute>();

            for (int i = 0; i < indexes.Length; i++)
            {
                if (indexes[i] is string)
                    attributes.Add(new ElementAttribute((string)indexes[i], null));
                else if (indexes[i] is ElementAttribute)
                    attributes.Add((ElementAttribute)indexes[i]);
            }

            result = new ElementNode(_name, attributes);
            return true;
        }

        public override bool TryInvoke(InvokeBinder binder, object[] args, out object result)
        {
            List<INode> children = NodeListFromArgs(args);

            result = new ElementNode(_name, children);
            return true;
        }

        public override bool TryConvert(ConvertBinder binder, out object result)
        {
            if (binder.Type != typeof(INode))
                return base.TryConvert(binder, out result);

            result = new ElementNode(_name, null, null);
            return true;
        }

        public static List<INode> NodeListFromArgs(object[] args)
        {
            List<INode> children = new List<INode>();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] is string)
                    children.Add(new TextNode((string)args[i]));
                else if (args[i] is ElementDynamicObject)
                    children.Add(NodeFromDynamic((ElementDynamicObject)args[i]));
                else if (args[i] is INode)
                    children.Add((INode)args[i]);
            }
            return children;
        }

        private static INode NodeFromDynamic(ElementDynamicObject elem)
        {
            return new ElementNode(elem._name, null, null);
        }
    }
}
