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
using Marius.Html.Css.Dom;

namespace Marius.Html.Css.Cascade
{
    public class CssStyleComparer: IComparer<CssPreparedStyle>
    {
        public static readonly CssStyleComparer Instance = new CssStyleComparer();

        public int Compare(CssPreparedStyle x, CssPreparedStyle y)
        {
            if (object.ReferenceEquals(x, y))
                return 0;

            int xweight, yweight;
            xweight = Importance(x);
            yweight = Importance(y);

            if (xweight != yweight)
                return xweight - yweight;

            int speccomp = x.Selector.Specificity.CompareTo(y.Selector.Specificity);
            if (speccomp != 0)
                return speccomp;

            return x.Index - y.Index;
        }

        private int Importance(CssPreparedStyle s)
        {
            /*
               1 1. user agent declarations
               2 1.1 user agent important
               3 2. user normal declarations
               4 3. author normal declarations
               5 4. author important declarations
               6 5. user important declarations 
            */
            switch (s.Source)
            {
                case CssStylesheetSource.Agent:
                    if (s.IsImportant)
                        return 2;
                    return 1;
                case CssStylesheetSource.Author:
                    if (s.IsImportant)
                        return 5;
                    return 4;
                case CssStylesheetSource.User:
                    if (s.IsImportant)
                        return 6;
                    return 3;
            }
            throw new CssInvalidStateException();
        }
    }
}
