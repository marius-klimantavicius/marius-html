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
using NUnit.Framework;
using Marius.Html.Css;
using Marius.Html.Css.Dom;
using Marius.Html.Css.Values;
using Marius.Html.Hap;
using Marius.Html.Dom;
using System.Linq.Expressions;
using Marius.Html.Tests.Support;

namespace Marius.Html.Tests.Css.Cascade
{
    [TestFixture]
    public class DefaultComparerTests: BaseTestsWithDom
    {
        private CssContext _context;

        [TestFixtureSetUp]
        public void Init()
        {
            _context = new CssContext();
        }

        [Test]
        public void ShouldOrderAccordingImporance()
        {
            var agents = _context.ParseStylesheet(@"
#id { color: red; border-width: 3px !important }
", CssStylesheetSource.Agent);
            var users = _context.ParseStylesheet(@"
#id { color: green; border-width: 4px !important }
", CssStylesheetSource.User);
            var authors = _context.ParseStylesheet(@"
#id { color: blue; border-width: 5px !important }
", CssStylesheetSource.Author);

            var prep = _context.PrepareStylesheets(agents, users, authors);

            INode node = a[id <= "id", expected <= "color: blue, border-width: 4px"];

            var decls = prep.GetAplicableDeclarations(node);
            Assert.AreEqual(6, decls.Count);

            _context.Apply(prep, node);

            Assert.AreEqual(new CssLength(4, CssUnits.Px), node.Style.BorderTopWidth);
            Assert.AreEqual(new CssLength(4, CssUnits.Px), node.Style.BorderLeftWidth);
            Assert.AreEqual(new CssLength(4, CssUnits.Px), node.Style.BorderBottomWidth);
            Assert.AreEqual(new CssLength(4, CssUnits.Px), node.Style.BorderRightWidth);

            Assert.AreEqual(CssKeywords.Blue, node.Style.Color);
        }

        [Test]
        public void IfRulesHaveSameImporanceAndSpecificityOrderByIndex()
        {
            var s = _context.ParseStylesheet(@"
            #id { color: red; background-color: rgb(100, 100, 100) }
            #id { color: green }
            #id { background-color: black }
            #id { color: blue }
            ", CssStylesheetSource.Author);

            var prep = _context.PrepareStylesheets(s);

            INode node = a[id <= "id", expected <= "background-color: black; color: blue"];

            var decls = prep.GetAplicableDeclarations(node);
            Assert.AreEqual(5, decls.Count);

            _context.Apply(prep, node);

            Assert.AreEqual(CssKeywords.Black, node.Style.BackgroundColor);
            Assert.AreEqual(CssKeywords.Blue, node.Style.Color);
        }

        [Test]
        public void MoreSpecificRuleShouldBeApplied()
        {
            var s = _context.ParseStylesheet(@"
            a#id { color: green }
            #id { color: red }
            ", CssStylesheetSource.Author);

            var prep = _context.PrepareStylesheets(s);

            INode node = a[id <= "id", expected <= "color: green"];

            var decls = prep.GetAplicableDeclarations(node);
            Assert.AreEqual(2, decls.Count);

            _context.Apply(prep, node);

            Assert.AreEqual(CssKeywords.Green, node.Style.Color);
        }
    }
}
