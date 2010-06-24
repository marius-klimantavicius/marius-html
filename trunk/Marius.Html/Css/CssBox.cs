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
using Marius.Html.Css.Values;
using System.Runtime.CompilerServices;
using Marius.Html.Dom;

namespace Marius.Html.Css
{
    public class CssBox
    {
        private CssPropertyValueDictionary _properties;
        private CssContext _context;

        public CssBox(CssContext context)
        {
            _context = context;
            _properties = new CssPropertyValueDictionary(_context);

            FirstLineStyle = new StyleHolder(_context);
            FirstLetterStyle = new StyleHolder(_context);
            Style = new StyleHolder(_context);
        }

        public CssBox Parent { get; set; }
        public CssBox FirstChild { get; set; }
        public CssBox LastChild { get; set; }
        public CssBox PreviousSibling { get; set; }
        public CssBox NextSibling { get; set; }

        public INode Node { get; set; }
        public IElement Element { get { return Node as IElement; } }

        public CssPropertyValueDictionary Properties { get; private set; }

        public StyleHolder FirstLineStyle { get; private set; }
        public StyleHolder FirstLetterStyle { get; private set; }
        public StyleHolder Style { get; private set; }
        public StyleHolder BeforeStyle { get; private set; }
        public StyleHolder AfterStyle { get; private set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Element != null)
            {
                var e = Element;
                sb.AppendFormat("<{0} id='{1}' class='{2}' style='{3}'>: ", e.Name, e.Id, e.Class, e.Style);
            }

            sb.AppendFormat("Style: {0}", Style);

            return sb.ToString();
        }
    }
}
