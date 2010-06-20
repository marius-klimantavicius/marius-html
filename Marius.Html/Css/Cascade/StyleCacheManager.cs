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
    public sealed class StyleCacheManager
    {
        private int _index;
        private CssContext _context;
        private CssStylesheet[] _stylesheets;
        private int _importDepth;
        private List<CssPreparedStyle> _styles;

        public StyleCacheManager(CssContext context, CssStylesheet[] stylesheets)
        {
            _context = context;
            _stylesheets = stylesheets;

            _styles = new List<CssPreparedStyle>();
        }

        public CssPreparedStylesheet Prepare()
        {
            for (int i = 0; i < _stylesheets.Length; i++)
            {
                PrepareSingle(_stylesheets[i]);
            }

            return new CssPreparedStylesheet(_context, _styles);
        }

        private void PrepareSingle(CssStylesheet sheet)
        {
            var rules = sheet.Rules;
            for (int i = 0; i < rules.Length; i++)
            {
                PrepareRule(rules[i], sheet.Source);
            }
        }

        private void PrepareRule(CssRule rule, CssStylesheetSource source)
        {
            switch (rule.RuleType)
            {
                case CssRuleType.Import:
                    PrepareImport((CssImport)rule, source);
                    break;
                case CssRuleType.Media:
                    PrepareMedia((CssMedia)rule, source);
                    break;
                case CssRuleType.Style:
                    PrepareStyle((CssStyle)rule, source);
                    break;
            }
        }

        private void PrepareStyle(CssStyle style, CssStylesheetSource source)
        {
            List<CssDeclaration> normal = new List<CssDeclaration>();
            List<CssDeclaration> important = new List<CssDeclaration>();

            var decls = style.Declarations;
            for (int i = 0; i < decls.Length; i++)
            {
                if (decls[i].Important)
                    important.Add(decls[i]);
                else
                    normal.Add(decls[i]);
            }

            var sels = style.Selectors;
            for (int i = 0; i < sels.Length; i++)
            {
                if (important.Count > 0)
                    _styles.Add(new CssPreparedStyle(source, sels[i], true, _index++, important));

                if (normal.Count > 0)
                    _styles.Add(new CssPreparedStyle(source, sels[i], false, _index++, normal));
            }
        }

        private void PrepareMedia(CssMedia media, CssStylesheetSource source)
        {
            if (!_context.IsMediaSupported(media.MediaList))
                return;

            var rules = media.Rules;
            for (int i = 0; i < rules.Length; i++)
            {
                PrepareRule(rules[i], source);
            }
        }

        private void PrepareImport(CssImport import, CssStylesheetSource source)
        {
            if (!_context.IsMediaSupported(import.MediaList))
                return;

            if (_importDepth >= _context.MaxImportDepth)
                return;

            _importDepth++;

            CssStylesheet sheet = _context.ImportStylesheet(import.Uri, source);
            if (sheet == null)
                return;

            PrepareSingle(sheet);

            _importDepth--;
        }
    }
}
