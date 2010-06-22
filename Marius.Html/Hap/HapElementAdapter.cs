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
    public class HapElementAdapter: ElementAdapter<HtmlDocument>
    {
        public override Element Transform(HtmlDocument documentRoot)
        {
            HtmlNode root = documentRoot.DocumentNode;

            if (root.NodeType == HtmlNodeType.Document)
                // not the best way, afraid of too deep recursion
                return new DocumentElement(CreateChildren(root.ChildNodes));
            else if (root.NodeType == HtmlNodeType.Element)
                return new Element(root.Name, CreateAttributes(root.Attributes), CreateChildren(root.ChildNodes));

            return null;
        }

        private AttributeCollection CreateAttributes(HtmlAttributeCollection attributes)
        {
            // those will be overriden, however I am not sure for how long this architecture is going to stay
            // so just in case...
            string id = AttributeValue(attributes["id"]);
            string @class = AttributeValue(attributes["class"]);
            string style = AttributeValue(attributes["style"]);

            return new AttributeCollection(id, @class, style, attributes.Select(s => new Tuple<string, string>(s.Name, s.Value)).ToList());
        }

        private string AttributeValue(HtmlAttribute attribute)
        {
            if (attribute == null)
                return null;
            return attribute.Value;
        }

        private Element[] CreateChildren(HtmlNodeCollection children)
        {
            List<Element> result = new List<Element>();

            for (int i = 0; i < children.Count; i++)
            {
                var current = children[i];
                switch (current.NodeType)
                {
                    case HtmlNodeType.Element:
                        result.Add(new Element(current.Name, CreateAttributes(current.Attributes), CreateChildren(current.ChildNodes)));
                        break;
                    case HtmlNodeType.Text:
                        result.Add(new TextElement(((HtmlTextNode)current).Text));
                        break;
                }
            }

            return result.ToArray();
        }
    }
}
