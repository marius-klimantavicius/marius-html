﻿#region License
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
using Marius.Html.Css.Parser;
using Marius.Html.Css.Values;
using Marius.Html.Css.Properties;
using Marius.Html.Css;
using Marius.Html.Css.Dom;

namespace Marius.Html.Tests.Css.Properties
{
    [TestFixture]
    public class AzimuthTests
    {
        private CssContext _context;

        [TestFixtureSetUp]
        public void Init()
        {
            _context = new CssContext();
        }

        [Test]
        public void ShouldAcceptSingleKeyword()
        {
            Assert.IsTrue(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("center") })));
            Assert.IsTrue(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("leftwards") })));
            Assert.IsTrue(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("inherit") })));
            Assert.IsTrue(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("behind") })));
            Assert.IsTrue(_context.Azimuth.Validate(new CssExpression(new[] { new CssAngle(10, CssUnits.Deg) })));
        }

        [Test]
        public void ShouldIgnoreInvalidValues()
        {
            Assert.IsFalse(_context.Azimuth.Validate(new CssExpression(new CssValue[0])));
            Assert.IsFalse(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("centruotas") })));
            Assert.IsFalse(_context.Azimuth.Validate(new CssExpression(new[] { new CssLength(10, CssUnits.Cm) })));
            Assert.IsFalse(_context.Azimuth.Validate(new CssExpression(new[] { new CssHexColor("FFFFFF") })));
            Assert.IsFalse(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("behind"), new CssIdentifier("center"), new CssIdentifier("left") })));
        }

        [Test]
        public void ShouldHandleValueWithBehind()
        {
            Assert.IsTrue(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("center"), new CssIdentifier("behind") })));
            Assert.IsTrue(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("behind"), new CssIdentifier("center") })));
            Assert.IsTrue(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("center-right"), new CssIdentifier("behind") })));
            Assert.IsTrue(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("behind"), new CssIdentifier("center-right") })));
        }

        [Test]
        public void ShouldIgnoreInvalidDoubleValues()
        {
            Assert.IsFalse(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("behind"), new CssIdentifier("behind") })));
            Assert.IsFalse(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("leftwards"), new CssIdentifier("behind") })));
            Assert.IsFalse(_context.Azimuth.Validate(new CssExpression(new[] { new CssIdentifier("behind"), new CssIdentifier("inherit") })));
        }       
    }
}
