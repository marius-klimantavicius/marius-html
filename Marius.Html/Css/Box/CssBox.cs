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
using System.Collections.Generic;

namespace Marius.Html.Css.Box
{
    public partial class CssBox: ITreeNode<CssBox>
    {
        private CssContext _context;

        private CssPropertyValueDictionary _properties;
        private CssPropertyValueDictionary _firstLineProperties;
        private CssPropertyValueDictionary _firstLetterProperties;

        private CssComputedValueDictionary _computed;
        private CssUsedValueDictionary _used;

        public CssBox(CssContext context)
        {
            _context = context;

            _inheritedChildren = new LinkedList<CssBox>();

            _properties = new CssPropertyValueDictionary();
            _firstLineProperties = new CssPropertyValueDictionary();
            _firstLetterProperties = new CssPropertyValueDictionary();

            _computed = new CssComputedValueDictionary(this);
            RawWords = new List<CssBoxWord>();
        }

        public CssContext Context { get { return _context; } }

        public CssPropertyValueDictionary Properties { get { return _properties; } }
        public CssComputedValueDictionary Computed { get { return _computed; } }
        public CssUsedValueDictionary Used { get { return _used; } }

        public CssPropertyValueDictionary FirstLineProperties { get { return _firstLineProperties; } }
        public CssPropertyValueDictionary FirstLetterProperties { get { return _firstLetterProperties; } }

        public bool IsRunIn { get; set; }
        public virtual bool IsReplaced { get { return false; } }

        public List<CssBoxWord> RawWords { get; private set; }
        public void AddRawWords(params CssBoxWord[] words)
        {
            RawWords.AddRange(words);
        }
    }
}
