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
    public class CssUniversalSelector: CssSimpleSelector
    {
        public static readonly CssSpecificity UniversalSpecificity = new CssSpecificity(0, 0, 0, 0);
        public static readonly CssUniversalSelector Instance = new CssUniversalSelector();

        public override CssSelectorType SelectorType
        {
            get { return CssSelectorType.Universal; }
        }

        private CssUniversalSelector()
        {

        }

        public override string ToString()
        {
            return "*";
        }

        public override CssSpecificity Specificity
        {
            get { return UniversalSpecificity; }
        }

        public override bool Equals(CssSelector other)
        {
            CssUniversalSelector o = other as CssUniversalSelector;
            if (o == null)
                return false;
            return true;
        }

        public override int GetHashCode()
        {
            return 0x57A4;
        }
    }
}
