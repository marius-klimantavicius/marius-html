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
    public abstract class Node
    {
        public Node Parent { get; private set; }

        public Node NextSibling { get; private set; }
        public Node PreviousSibling { get; private set; }

        public NodeList Children { get; private set; }
        public Node FirstChild { get { return Children[0]; } }
        public Node LastChild { get { return Children[Children.Count - 1]; } }

        public Node()
        {
            Children = new NodeList();
        }

        public Node InsertBefore(Node newChild, Node refChild)
        {
            if (refChild == null)
                return Append(newChild);

            int index = Children[refChild];
            if (index < 0)
                throw new DomException();

            Children.InsertAt(index, newChild);

            newChild.NextSibling = newChild.PreviousSibling = null;

            if (index > 0)
            {
                Children[index - 1].NextSibling = newChild;
                newChild.PreviousSibling = Children[index - 1];
            }

            if ((index + 1) < Children.Count)
            {
                Children[index + 1].PreviousSibling = newChild;
                newChild.NextSibling = Children[index + 1];
            }

            newChild.Parent = this;
            return newChild;
        }

        public Node Append(Node newChild)
        {
            Children.InsertAt(Children.Count, newChild);

            newChild.NextSibling = newChild.PreviousSibling = null;

            if (Children.Count > 1)
            {
                Children[Children.Count - 1].NextSibling = newChild;
                newChild.PreviousSibling = Children[Children.Count - 1];
            }

            newChild.Parent = this;
            return newChild;
        }
    }
}
