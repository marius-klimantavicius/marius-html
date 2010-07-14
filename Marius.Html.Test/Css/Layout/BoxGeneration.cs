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
using Marius.Html.Tests.Support;
using Marius.Html.Css;
using Marius.Html.Dom;
using Marius.Html.Css.Box;
using Marius.Html.Css.Values;

namespace Marius.Html.Tests.Css.Layout
{
    [TestFixture]
    public class BoxGeneration: BaseTestsWithDom
    {
        CssContext _context;

        [TestFixtureSetUp]
        public void Setup()
        {
            _context = new CssContext();
        }

        [Test]
        public void BlockBoxShouldContainOnlyBlockBoxes()
        {
            INode root = body(span("span text"), div("div text"), span("more span"));
            var sheet = _context.ParseStylesheet(@"
body, div { display: block }
span { display: inline }
");
            var prep = _context.PrepareStylesheets(sheet);
            _context.Apply(prep, root);

            var box = _context.GenerateBoxes(root);
            Assert.IsNotNull(box);

            var bodyBox = box.FirstChild;
            Assert.IsNotNull(bodyBox);
            Assert.IsNotInstanceOf<CssAnonymousBlockBox>(bodyBox);

            var anonSpanBox = bodyBox.FirstChild;
            Assert.IsNotNull(anonSpanBox);
            Assert.IsInstanceOf<CssAnonymousBlockBox>(anonSpanBox);

            var divBox = anonSpanBox.NextSibling;
            Assert.IsNotNull(divBox);
            Assert.IsNotInstanceOf<CssAnonymousBlockBox>(divBox);

            AssertThatBlocksAreCorrect(box);
        }

        [Test]
        public void InlineShouldBeBrokenAroundIfContainsBlock()
        {
            INode root = body(span("span text"), div("div text"), div("div text"), span("middle span"), div("div text"), "simple text");
            var sheet = _context.ParseStylesheet(@"
div { display: block }
body, span { display: inline }
");
            var prep = _context.PrepareStylesheets(sheet);
            _context.Apply(prep, root);

            var box = _context.GenerateBoxes(root);
            Assert.IsNotNull(box);

            AssertThatBlocksAreCorrect(box);
        }

        private void AssertThatBlocksAreCorrect(CssBox box)
        {
            bool hasInline = false;
            bool hasBlock = false;

            var current = box.FirstChild;
            while (current != null)
            {
                if (IsBlockBox(box))
                    hasBlock = true;
                else
                    hasInline = true;

                current = current.NextSibling;
            }

            Assert.True(hasInline && !hasBlock || hasBlock && !hasInline || !hasInline && !hasBlock);

            current = box.FirstChild;
            while (current != null)
            {
                if (IsBlockBox(current))
                    AssertThatBlocksAreCorrect(current);
                else
                    AssertThatInlinesAreCorrect(current);

                current = current.NextSibling;
            }
        }

        private void AssertThatInlinesAreCorrect(CssBox current)
        {
            Assert.IsFalse(IsBlockBox(current));

            bool hasBlock = false;
            current.RecurseTree(s => hasBlock = hasBlock || IsBlockBox(s));
            Assert.IsFalse(hasBlock);
        }

        private static bool IsBlockBox(CssBox box)
        {
            var display = box.Properties.Computed(CssProperty.Display);
            return display.Equals(CssKeywords.Block)
                || display.Equals(CssKeywords.ListItem)
                || display.Equals(CssKeywords.Table);
        }
    }
}
