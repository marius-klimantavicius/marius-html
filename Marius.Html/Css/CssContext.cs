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
using Marius.Html.Css.Properties;
using Marius.Html.Css.Parser;
using Marius.Html.Css.Dom;
using System.Net;
using System.IO;
using Marius.Html.Css.Cascade;
using Marius.Html.Dom;

namespace Marius.Html.Css
{
    public partial class CssContext
    {
        public const string All = "all";
        public const string Screen = "screen";

        public CssContext()
        {
            FunctionFactory = new FunctionFactory();
            PseudoConditionFactory = new PseudoConditionFactory();

            Properties = new CssPropertyDictionary();
            InitProperties();
        }

        public virtual CssPropertyDictionary Properties { get; private set; }
        public virtual FunctionFactory FunctionFactory { get; set; }
        public virtual PseudoConditionFactory PseudoConditionFactory { get; set; }
        public virtual int MaxImportDepth { get { return 20; } }
        public virtual IComparer<CssPreparedStyle> StyleComparer { get { return DefaultStyleComparer.Instance; } }

        public virtual CssStylesheet ParseStylesheet(string source)
        {
            return ParseStylesheet(source, CssStylesheetSource.Author);
        }

        public virtual CssStylesheet ParseStylesheet(string source, CssStylesheetSource stylesheetSource)
        {
            CssScanner scanner = new CssScanner();
            scanner.SetSource(source, 0);

            CssParser parser = new CssParser(this, scanner);
            return parser.Parse(stylesheetSource);
        }

        public virtual bool IsMediaSupported(string[] media)
        {
            if (media == null || media.Length == 0)
                return true;

            for (int i = 0; i < media.Length; i++)
            {
                if (All.Equals(media[i], StringComparison.InvariantCultureIgnoreCase)
                    || Screen.Equals(media[i], StringComparison.InvariantCultureIgnoreCase))
                    return true;
            }
            return false;
        }

        public virtual CssStylesheet ImportStylesheet(string uri, CssStylesheetSource stylesheetSource)
        {
            try
            {
                string source = null;

                var request = WebRequest.Create(uri);
                var response = request.GetResponse();

                using (var stream = response.GetResponseStream())
                using (var reader = new StreamReader(stream))
                {
                    source = reader.ReadToEnd();
                }

                return this.ParseStylesheet(source, stylesheetSource);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public virtual CssPreparedStylesheet PrepareStylesheets(params CssStylesheet[] stylesheets)
        {
            StyleCacheManager manager = new StyleCacheManager(this, stylesheets);
            return manager.Prepare();
        }

        /// <summary>
        /// Applies recursively in any order
        /// </summary>
        public virtual void Apply(CssPreparedStylesheet sheet, INode box)
        {
            var current = box;
            bool down = true;

            while (true)
            {
                if (down)
                {
                    if (current.FirstChild != null)
                        current = current.FirstChild;
                    else
                        down = false;
                }
                else
                {
                    if (current.NextSibling == null)
                    {
                        current = current.Parent;
                        if (current == null || current == box)
                            break;
                        continue;
                    }
                    else
                    {
                        current = current.NextSibling;
                        down = true;
                    }
                }
                sheet.Apply(current);
            }
        }
    }
}
