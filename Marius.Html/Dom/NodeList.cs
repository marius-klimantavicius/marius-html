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
    public class NodeList
    {
        private Node[] _nodes;
        private int _count;

        public Node this[int index]
        {
            get
            {
                if (index < 0)
                    return null;
                if (index >= _count)
                    return null;

                return _nodes[index];
            }
        }

        public int this[Node node]
        {
            get { return Array.IndexOf<Node>(_nodes, node); }
        }

        public int Count
        {
            get { return _count; }
        }

        public NodeList()
        {
            _nodes = new Node[3];
            _count = 0;
        }

        public void InsertAt(int index, Node node)
        {
            if (index > _count)
                throw new ArgumentException();

            if (_count + 1 >= _nodes.Length)
                Array.Resize(ref _nodes, _count + 10);

            Array.Copy(_nodes, index, _nodes, index + 1, _count - index);
            _nodes[index] = node;
            _count++;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < _count; i++)
            {
                sb.Append(_nodes[i].ToString());
            }

            return sb.ToString();
        }
    }
}
