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

namespace Marius.Html.Css.Parser
{
    public class StringSource: IInputSource
    {
        private char[] _source;
        private int _index;
        private Stack<int> _state = new Stack<int>();
        private StringBuilder _value = new StringBuilder();

        public char this[int index]
        {
            get
            {
                if ((_index + 1 + index) < _source.Length && (_index + 1 + index) >= 0)
                    return _source[(_index + 1 + index)];
                return '\0';
            }
        }

        public char Current
        {
            get { if (_index >= 0 && _index < _source.Length) return _source[_index]; return '\0'; }
        }

        public string Value
        {
            get
            {
                return _value.ToString();
            }
        }

        public bool Eof { get { return _index >= _source.Length; } }

        public StringSource(string source, int startIndex)
        {
            _source = source.ToCharArray();
            _index = startIndex;
            _value = new StringBuilder();
        }

        public void ClearValue()
        {
            _value.Clear();
        }

        public void MoveNext()
        {
            if (_index < _source.Length)
                _value.Append(_source[_index]);
            _index++;
        }

        public void Skip(int count)
        {
            for (int i = 0; i < count; i++)
            {
                if (_index < _source.Length)
                    _value.Append(_source[_index]);
                _index++;
            }
        }

        public void PushState()
        {
            _state.Push(_index);
        }

        public void PopState(bool discard)
        {
            int index = _state.Pop();
            if (!discard)
                _index = index;
        }
    }
}
