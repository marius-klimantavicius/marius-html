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
using System.Diagnostics.Contracts;
using Marius.Html.Css.Parser;
using Marius.Html.Internal;

namespace Marius.Html.Css.Dom
{
    public class CssStylesheet: IEquatable<CssStylesheet>
    {
        public CssStylesheetSource Source { get; private set; }
        public CssRule[] Rules { get; private set; }

        public CssStylesheet(CssStylesheetSource source, CssRule[] rules)
        {
            Contract.Requires(rules != null);

            Source = source;
            Rules = rules;
        }

        public static CssStylesheet Parse(string source, CssStylesheetSource sheetSource = CssStylesheetSource.Author)
        {
            CssScanner scanner = new CssScanner();
            scanner.SetSource(source, 0);

            CssParser parser = new CssParser(scanner);
            return parser.Parse(sheetSource);
        }

        public static CssStylesheet Parse(CssContext context, string source, CssStylesheetSource sheetSource = CssStylesheetSource.Author)
        {
            CssScanner scanner = new CssScanner();
            scanner.SetSource(source, 0);

            CssParser parser = new CssParser(scanner, context);
            return parser.Parse(sheetSource);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < Rules.Length; i++)
            {
                sb.AppendLine(Rules[i].ToString());
            }

            return sb.ToString();
        }

        public bool Equals(CssStylesheet other)
        {
            return other.Rules.ArraysEqual(this.Rules);
        }

        public override bool Equals(object obj)
        {
            CssStylesheet o = obj as CssStylesheet;
            if (o == null)
                return false;
            return Equals(o);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode((object)Rules);
        }
    }
}
