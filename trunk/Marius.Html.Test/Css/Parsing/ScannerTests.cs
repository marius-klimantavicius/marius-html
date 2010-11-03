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
using Marius.Html.Css.Parser;

namespace Marius.Html.Tests.Css.Parsing
{
    [TestFixture]
    public class ScannerTests
    {
        [Test]
        public void TestIdentifiers()
        {
            CssScanner scanner = new CssScanner();
            scanner.SetSource(@"simple with-dash \starting_with\_escape \30 _starting_with_unicodeescape \0000302  fun( \34(", 0);

            Expecting(scanner, ID("simple"), W(), ID("with-dash"), W(), ID(@"starting_with_escape"),
                W(), ID(@"0_starting_with_unicodeescape"), W(), ID(@"02"), W(), F("fun"), W(), F(@"4"));

            scanner.SetSource(@"\000030", 0);
            Expecting(scanner, ID(@"0"));
        }


        [Test]
        public void TestUnicodeEscapes()
        {
            CssScanner scanner = new CssScanner();
            scanner.SetSource(@"\20  \30  \031  \0032  \00033  \000034  @u(", 0);

            Expecting(scanner, ID(@" "), W(), ID(@"0"), W(), ID(@"1"), W(), ID(@"2"), W(), ID(@"3"), W(), ID(@"4"), W(), AT("u"), C(CssTokens.OpenParen));
        }

        [Test]
        public void TestUrls()
        {
            CssScanner scanner = new CssScanner();
            //  u|\\0{0,4}(55|75)(\r\n|[ \t\r\n\f])?|\\u
            //  r|\\0{0,4}(52|72)(\r\n|[ \t\r\n\f])?|\\r
            //  l|\\0{0,4}(4c|6c)(\r\n|[ \t\r\n\f])?|\\l
            scanner.SetSource(@"\0055\72\00004C (http://programuok.com) \ur\l(   """") url(


""ar mes ()@#$%^&*()_ ka=zko nesuprantame""

)
url() URl( http:// asd) \u\072 L(url(progra)
", 0);
            Expecting(scanner, U("http://programuok.com"), W(), U(""), W(), U("ar mes ()@#$%^&*()_ ka=zko nesuprantame"),
                W(), F("url"), C(CssTokens.CloseParen), W(), F("URl"), W(), ID("http"), C(CssTokens.Colon), C(CssTokens.Slash), C(CssTokens.Slash), W(), ID("asd"), C(CssTokens.CloseParen), W(), F("urL"),
                U("progra"), W());
        }

        [Test]
        public void TestEscapesShouldNotBeDoubleDecoded()
        {
            CssScanner scanner = new CssScanner();
            scanner.SetSource(@"\5C 30", 0);

            Expecting(scanner, ID("\\30"));

            scanner.SetSource(@"\\30", 0);
            Expecting(scanner, ID("\\30"));
        }

        private ExpectedToken U(string url)
        {
            return new ExpectedToken(CssTokens.Uri, url);
        }

        private ExpectedToken F(string value)
        {
            return new ExpectedToken(CssTokens.Function, value);
        }

        private ExpectedToken C(CssTokens token)
        {
            return new ExpectedToken(token, null);
        }

        private ExpectedToken AT(string value)
        {
            return new ExpectedToken(CssTokens.AtKeyword, value);
        }

        private void Expecting(CssScanner scanner, params ExpectedToken[] expected)
        {
            CssTokens token;
            for (int i = 0; i < expected.Length; i++)
            {
                token = scanner.NextToken();
                Assert.AreEqual(expected[i].Token, token);
                if (expected[i].Value != null)
                    Assert.AreEqual(expected[i].Value, scanner.Value.Value);
            }
        }

        private ExpectedToken W(string value = null)
        {
            return new ExpectedToken(CssTokens.Whitespace, value);
        }

        private ExpectedToken ID(string value)
        {
            return new ExpectedToken(CssTokens.Identifier, value);
        }
    }

    public class ExpectedToken
    {
        public CssTokens Token { get; private set; }
        public string Value { get; private set; }

        public ExpectedToken(CssTokens token, string value)
        {
            Token = token;
            Value = value;
        }
    }
}
