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

namespace Marius.Html.Css.Values
{
    public class CssExpression: IEquatable<CssExpression>
    {
        private int _currentIndex;

        public CssValue[] Items { get; private set; }

        public CssValue Current
        {
            get
            {
                if (_currentIndex >= Items.Length)
                    return null;
                return Items[_currentIndex];
            }
        }

        public CssExpression(CssValue[] items)
        {
            Items = items;
        }

        public bool MoveNext()
        {
            _currentIndex++;

            if (_currentIndex >= Items.Length)
                return false;
            
            return true;
        }

        public void Reset()
        {
            _currentIndex = 0;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Items.Length; i++)
            {
                sb.Append(Items[i].ToString());
                if (Items[i].ValueType == CssValueType.Comma || Items[i].ValueType == CssValueType.Slash)
                    sb.Append(' ');
            }

            return sb.ToString();
        }

        private char OperatorToString(CssValueType op)
        {
            switch (op)
            {
                case CssValueType.Comma:
                    return ',';
                case CssValueType.Slash:
                    return '\\';
                default:
                    throw new ArgumentException();
            }
        }

        public bool Equals(CssExpression other)
        {
            if (other == null)
                return false;

            return other.Items.ArraysEqual(this.Items);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;
            return Equals((CssExpression)obj);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode((object)Items);
        }
    }
}
