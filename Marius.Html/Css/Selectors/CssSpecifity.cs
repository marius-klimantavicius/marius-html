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

namespace Marius.Html.Css.Selectors
{
    public class CssSpecificity: IComparable<CssSpecificity>
    {
        public readonly int A, B, C, D;

        public CssSpecificity(int a, int b, int c, int d)
        {
            A = a;
            B = b;
            C = c;
            D = d;
        }

        public int CompareTo(CssSpecificity other)
        {
            if (this.A != other.A)
                return this.A - other.A;

            if (this.B != other.B)
                return this.B - other.B;

            if (this.C != other.C)
                return this.C - other.C;

            if (this.D != other.D)
                return this.D - other.D;

            return 0;
        }

        public override string ToString()
        {
            return string.Format("{0}, {1}, {2}, {3}", A, B, C, D);
        }

        public override bool Equals(object obj)
        {
            CssSpecificity other = obj as CssSpecificity;
            if (other == null)
                return false;
            return CompareTo(other) == 0;
        }

        public override int GetHashCode()
        {
            return (A << 24) + (B << 16) + (C << 8) + D;
        }

        public static CssSpecificity operator +(CssSpecificity a, CssSpecificity b)
        {
            return new CssSpecificity(a.A + b.A, a.B + b.B, a.C + b.C, a.D + b.D);
        }
    }
}
