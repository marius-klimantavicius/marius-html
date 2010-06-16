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
using Marius.Html.Internal;
using Marius.Html.Css.Dom;
using System.Net;
using System.IO;

namespace Marius.Html.Css.Cascade
{
    public class CssPreparedStylesheet
    {
        private int _ruleIndex = 0;
        private List<CssPreparedStyle> _styles = new List<CssPreparedStyle>();

        protected virtual void Prepare(CssStylesheet sheet)
        {
            // split all rules into important/normal declarations
            // order rules according to their specificity
            // inline style (attribute style='') will not appear in the cache, it will be directly attached to the element

            _ruleIndex = 0;
            for (int i = 0; i < sheet.Rules.Length; i++)
            {
                AddRule(sheet.Rules[i], sheet.Source);
            }
        }

        protected virtual void AddRule(CssRule rule, CssStylesheetSource source)
        {
            switch (rule.RuleType)
            {
                case CssRuleType.Import:
                    AddImport((CssImport)rule, source);
                    break;
                case CssRuleType.Media:
                    AddMedia((CssMedia)rule, source);
                    break;
                case CssRuleType.Style:
                    AddStyle((CssStyle)rule, source);
                    break;
            }
        }

        private void AddStyle(CssStyle style, CssStylesheetSource source)
        {
            List<CssDeclaration> normal = new List<CssDeclaration>();
            List<CssDeclaration> important = new List<CssDeclaration>();

            for (int i = 0; i < style.Declarations.Length; i++)
            {
                var decl = style.Declarations[i];
                if (decl.Important)
                    important.Add(decl);
                else
                    normal.Add(decl);
            }

            if (normal.Count == 0 && important.Count == 0)
                return;

            for (int i = 0; i < style.Selectors.Length; i++)
            {
                var sel = style.Selectors[i];

                if (normal.Count > 0)
                    _styles.Add(new CssPreparedStyle(source, sel, false, _ruleIndex++, normal.ToArray()));
                else if (important.Count > 0)
                    _styles.Add(new CssPreparedStyle(source, sel, false, _ruleIndex++, important.ToArray()));
            }
        }

        private void AddMedia(CssMedia media, CssStylesheetSource source)
        {
            if (!media.MediaList.Any(s => s.Equals("all", StringComparison.InvariantCultureIgnoreCase) || s.Equals("screen", StringComparison.InvariantCultureIgnoreCase)))
                return;

            for (int i = 0; i < media.Ruleset.Length; i++)
            {
                AddRule(media.Ruleset[i], source);
            }
        }

        private int _importDepth = 0;
        private const int MaxImportDepth = 20;

        private void AddImport(CssImport import, CssStylesheetSource source)
        {
            if (!import.MediaList.Any(s => s.Equals("all", StringComparison.InvariantCultureIgnoreCase) || s.Equals("screen", StringComparison.InvariantCultureIgnoreCase)))
                return;

            if (_importDepth >= MaxImportDepth)
                return;

            _importDepth++;

            var request = WebRequest.Create(import.Uri);
            var response = request.GetResponse();

            string cssSource = null;

            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
            {
                cssSource = reader.ReadToEnd();
            }

            CssStylesheet sheet = CssStylesheet.Parse(cssSource, source);
            Prepare(sheet);

            _importDepth--;
        }
    }
}
