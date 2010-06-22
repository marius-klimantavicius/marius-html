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
        public override Document Transform(HtmlDocument documentRoot)
        {
            HtmlNode root = documentRoot.DocumentNode;

            // not the best way, afraid of too deep recursion
            if (root.NodeType == HtmlNodeType.Document)
                return new Document(CreateChildren(root.ChildNodes));
            else if (root.NodeType == HtmlNodeType.Element)
                return new Document(new HapElement(root.Name, CreateAttributes(root.Attributes), CreateChildren(root.ChildNodes)));
            else if (root.NodeType == HtmlNodeType.Text)
                return new Document(new HapTextElement(((HtmlTextNode)root).Text));

            return null;
        }

        private AttributeCollection CreateAttributes(HtmlAttributeCollection attributes)
        {
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

        private HapElement[] CreateChildren(HtmlNodeCollection children)
        {
            List<HapElement> result = new List<HapElement>();

            for (int i = 0; i < children.Count; i++)
            {
                var current = children[i];
                switch (current.NodeType)
                {
                    case HtmlNodeType.Element:
                        result.Add(new HapElement(current.Name, CreateAttributes(current.Attributes), CreateChildren(current.ChildNodes)));
                        break;
                    case HtmlNodeType.Text:
                        result.Add(new HapTextElement(((HtmlTextNode)current).Text));
                        break;
                }
            }

            return result.ToArray();
        }
    }
}
