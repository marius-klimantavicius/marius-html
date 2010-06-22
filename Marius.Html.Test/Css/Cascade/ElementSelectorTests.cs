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
using Marius.Html.Css.Selectors;
using Marius.Html.Css.Dom;
using Marius.Html.Css.Cascade;
using Marius.Html.Css.Values;
using Marius.Html.Hap;

namespace Marius.Html.Tests.Css.Cascade
{
    [TestFixture]
    public class ElementSelectorTests
    {
        private CssContext _context;

        [TestFixtureSetUp]
        public void Setup()
        {
            _context = new CssContext();
        }

        [Test]
        public void ShouldMatchSingleElement()
        {
            CssBox box = new CssBox(_context);
            box.Element = new HapElement("a");

            var sheet = _context.ParseStylesheet("a { color: green }");
            var prep = _context.PrepareStylesheets(sheet);

            var decls = prep.GetAplicableDeclarations(box);
            Assert.IsNotNull(decls);
            Assert.AreEqual(1, decls.Count);
            Assert.AreEqual("color", decls[0].Property);
            Assert.IsFalse(decls[0].Important);

            Assert.IsTrue(_context.Color.Apply(box, decls[0].Value));
            Assert.AreEqual(CssKeywords.Green, box.Color);
        }

        [Test]
        public void ShouldIgnoreCase()
        {
            CssBox box = new CssBox(_context);
            box.Element = new HapElement("a");

            var sheet = _context.ParseStylesheet("A { color: green }");
            var prep = _context.PrepareStylesheets(sheet);

            var decls = prep.GetAplicableDeclarations(box);
            Assert.IsNotNull(decls);
            Assert.AreEqual(1, decls.Count);
            Assert.AreEqual("color", decls[0].Property);
            Assert.IsFalse(decls[0].Important);

            Assert.IsTrue(_context.Color.Apply(box, decls[0].Value));
            Assert.AreEqual(CssKeywords.Green, box.Color);
        }
    }
}
