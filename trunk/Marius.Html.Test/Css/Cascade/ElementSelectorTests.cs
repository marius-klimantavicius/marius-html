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
            CssBox box = new CssBox();
            box.Element = new Element("a", null, null);

            var sheet = CssStylesheet.Parse(_context, "a { color: green }");
            var prep = StyleCacheManager.Prepare(_context, sheet);

            var decls = prep.GetAplicableDeclarations(box);
            Assert.IsNotNull(decls);
            Assert.AreEqual(1, decls.Count);
            Assert.AreEqual("color", decls[0].Property);
            Assert.IsFalse(decls[0].Important);

            Assert.IsTrue(_context.Color.Apply(_context, box, decls[0].Value));
            Assert.AreEqual(CssKeywords.Green, box.Color);
        }

        [Test]
        public void ShouldIgnoreCase()
        {
            CssBox box = new CssBox();
            box.Element = new Element("a", null, null);

            var sheet = CssStylesheet.Parse(_context, "A { color: green }");
            var prep = StyleCacheManager.Prepare(_context, sheet);

            var decls = prep.GetAplicableDeclarations(box);
            Assert.IsNotNull(decls);
            Assert.AreEqual(1, decls.Count);
            Assert.AreEqual("color", decls[0].Property);
            Assert.IsFalse(decls[0].Important);

            Assert.IsTrue(_context.Color.Apply(_context, box, decls[0].Value));
            Assert.AreEqual(CssKeywords.Green, box.Color);
        }
    }
}
