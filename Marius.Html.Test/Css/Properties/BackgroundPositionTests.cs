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
using System.Text;
using NUnit.Framework;
using Marius.Html.Css.Properties;
using Marius.Html.Css.Values;
using System.Linq.Expressions;

namespace Marius.Html.Tests.Css.Properties
{
    [TestFixture]
    public class BackgroundPositionTests
    {
        //[Test]
        //public void ShouldAcceptSingleValue()
        //{
        //    BackgroundPosition a;
        //    a = BackgroundPosition.Create(new CssExpression(new[] { "left".AsIdent()  }));
        //    Assert.IsNotNull(a);
        //    Assert.AreEqual("left".AsIdent(), a.Horizontal);
        //    Assert.AreEqual("center".AsIdent(), a.Vertical);

        //    a = BackgroundPosition.Create(new CssExpression(new[] { "top".AsIdent() }));
        //    Assert.IsNotNull(a);
        //    Assert.AreEqual("center".AsIdent(), a.Horizontal);
        //    Assert.AreEqual("top".AsIdent(), a.Vertical);

        //    a = BackgroundPosition.Create(new CssExpression(new[] { (20.0).AsPerc() }));
        //    Assert.IsNotNull(a);
        //    Assert.AreEqual((20.0).AsPerc(), a.Horizontal);
        //    Assert.AreEqual("center".AsIdent(), a.Vertical);


        //    a = BackgroundPosition.Create(new CssExpression(new[] { CssKeywords.Inherit }));
        //    Assert.IsNotNull(a);
        //    Assert.AreEqual(CssKeywords.Inherit, a.Horizontal);
        //    Assert.AreEqual(CssKeywords.Inherit, a.Vertical);
        //}

        //[Test]
        //public void ShouldAcceptTwoValues()
        //{
        //    BackgroundPosition a;

        //    a = BackgroundPosition.Create(new CssExpression(new[] { "left".AsIdent(), "bottom".AsIdent() }));
        //    Assert.IsNotNull(a);
        //    Assert.AreEqual("left".AsIdent(), a.Horizontal);
        //    Assert.AreEqual("bottom".AsIdent(), a.Vertical);

        //    a = BackgroundPosition.Create(new CssExpression(new[] { "bottom".AsIdent(), "left".AsIdent() }));
        //    Assert.IsNotNull(a);
        //    Assert.AreEqual("left".AsIdent(), a.Horizontal);
        //    Assert.AreEqual("bottom".AsIdent(), a.Vertical);

        //    a = BackgroundPosition.Create(new CssExpression(new[] { "center".AsIdent(), "left".AsIdent() }));
        //    Assert.IsNotNull(a);
        //    Assert.AreEqual("left".AsIdent(), a.Horizontal);
        //    Assert.AreEqual("center".AsIdent(), a.Vertical);

        //    a = BackgroundPosition.Create(new CssExpression(new CssValue[] { "center".AsIdent(), (20.0).AsPerc() }));
        //    Assert.IsNotNull(a);
        //    Assert.AreEqual("center".AsIdent(), a.Horizontal);
        //    Assert.AreEqual((20.0).AsPerc(), a.Vertical);

        //    a = BackgroundPosition.Create(new CssExpression(new CssValue[] { (20.0).AsPerc(), "center".AsIdent(), }));
        //    Assert.IsNotNull(a);
        //    Assert.AreEqual((20.0).AsPerc(), a.Horizontal);
        //    Assert.AreEqual("center".AsIdent(), a.Vertical);
        //}

        //[Test]
        //public void ShouldNotAcceptInvalidValues()
        //{
        //    Assert.IsNull(BackgroundPosition.Create(new CssExpression(new CssValue[] { (20.0).AsPerc(), "left".AsIdent() })));
        //    Assert.IsNull(BackgroundPosition.Create(new CssExpression(new CssValue[] { (20.0).AsPerc(), "inherit".AsIdent() })));
        //    Assert.IsNull(BackgroundPosition.Create(new CssExpression(new CssValue[] { "left".AsIdent(), "left".AsIdent() })));
        //    Assert.IsNull(BackgroundPosition.Create(new CssExpression(new CssValue[] { "pupa".AsIdent() })));
        //}
    }

    public static class Extensions
    {
        public static CssIdentifier AsIdent(this string value)
        {
            return new CssIdentifier(value);
        }

        public static CssPercentage AsPerc(this double value)
        {
            return new CssPercentage(value);
        }
    }
}
