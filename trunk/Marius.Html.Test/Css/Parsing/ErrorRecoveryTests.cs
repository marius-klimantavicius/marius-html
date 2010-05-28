#region License
/*
Distributed under the terms of an MIT-style license:

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
using Marius.Html.Css;
using Marius.Html.Css.Dom;

namespace Marius.Html.Tests.Css.Parsing
{
    [TestFixture]
    public class ErrorRecoveryTests
    {
        [Test]
        public void UnexpectedSemicolonShouldBeConsideredPartOfNextRule_EvilTest()
        {
            var s = CssStylesheet.Parse(@"
.demo1 { color: green; };
.demo1 { color: red; }
");
            var e = CssStylesheet.Parse(@".demo1 { color: green; }");
            AssertStylesheetsEqual(e, s);
        }

        [Test]
        public void AllOpenConstructsShouldBeClosedOnEOF()
        {
            var s = CssStylesheet.Parse(@"
@media screen {
    p:before#m[id][lang|='lt'] { content: 'Hello\
");
            var e = CssStylesheet.Parse(@"
@media screen {
    p:before#m[id][lang|='lt'] { content: 'Hello' }
}
");
            AssertStylesheetsEqual(e, s);
        }

        [Test]
        public void ShouldIgnoreRulesetWithInvalidTokenInSelectorList()
        {
            var s = CssStylesheet.Parse(@"
h1, h2 {color: green }
h3, h4 & h5 {color: red }
h6 {color: black }
h1 { color: red; rotation: 70minutes }
");
            var e = CssStylesheet.Parse(@"
h1, h2 {color: green }
h6 {color: black }
h1 { color: red; rotation: 70minutes }
");
            AssertStylesheetsEqual(e, s);
        }

        [Test]
        public void IgnoreUnknownAtRules()
        {
            var s = CssStylesheet.Parse(@"
@three-dee {
  @background-lighting {
    azimuth: 30deg;
    elevation: 190deg;
  }
  h1 { color: red }
}
h1 { color: blue }
");
            var e = CssStylesheet.Parse(@"
h1 { color: blue }
");
            AssertStylesheetsEqual(e, s);
        }

        [Test]
        public void IgnoreMalformedStatements()
        {
            var s = CssStylesheet.Parse(@"
p @here {color: red}     /* ruleset with unexpected at-keyword '@here' */
@foo @bar;               /* at-rule with unexpected at-keyword '@bar' */
}} {{ - }}               /* ruleset with unexpected right brace */
) ( {} ) p {color: red } /* ruleset with unexpected right parenthesis */
");
            var e = CssStylesheet.Parse(@"");
            AssertStylesheetsEqual(e, s);
        }

        [Test]
        public void IgnoreMalformedDeclarations()
        {
            var s = CssStylesheet.Parse(@"
p { color:green }
p { color:green; color }  /* malformed declaration missing ':', value */
p { color:red;   color; color:green }  /* same with expected recovery */
p { color:green; color: } /* malformed declaration missing value */
p { color:red;   color:; color:green } /* same with expected recovery */
p { color:green; color{;color:maroon} } /* unexpected tokens { } */
p { color:red;   color{;color:maroon}; color:green } /* same with recovery */
");
            var e = CssStylesheet.Parse(@"
p { color:green }
p { color:green }
p { color:red; color:green }
p { color:green }
p { color:red; color:green }
p { color:green }
p { color:red; color:green }
");
            AssertStylesheetsEqual(e, s);
        }

        [Test]
        public void IgnoreMisplacedImports()
        {
            var s = CssStylesheet.Parse(@"
@import 'subs.css';
h1 { color: blue }

@media print {
  @import 'print-main.css';
  body { font-size: 10pt }
}
h1 {color: blue }

@import 'list.css';
");
            var e = CssStylesheet.Parse(@"
@import 'subs.css';
h1 { color: blue }

@media print {
  body { font-size: 10pt }
}
h1 { color: blue }
");
            AssertStylesheetsEqual(e, s);
        }

        [Test]
        public void EnsureThatWhitespacesAreHandledCorrectly()
        {
            var s = CssStylesheet.Parse(@"
P:first-letter{ color: blue; }
P:first-letter:hover { color: blue; }
P:first-letter { color: blue; } /* note the space */
P:hover:first-letter { color: blue; } /* note the ordering */
");
            var e = CssStylesheet.Parse(@"
P:first-letter { color: blue; }
P:first-letter:hover { color: blue; }
P:first-letter { color: blue; }
P:hover:first-letter { color: blue; }
");
            AssertStylesheetsEqual(e, s);
        }

        [Test]
        public void IgnoreStatementsWithSgmlCommentsInInvalidLocations()
        {
            var s = CssStylesheet.Parse(@"
OL { list-style-type: lower-alpha; }

<!--

    .a { color: green; background: white none; }
<!--.b { color: green; background: white none; } --> <!-- --> <!--
    .c { color: green; background: white none; }

<!--
.d { color: green; background: white none; }
-->

    .e { color: green; background: white none; }


  	<!--	.f { color: green; background: white none; }-->
-->.g { color: green; background: white none; }<!--
    .h { color: green; background: white none; }
-->-->-->-->-->-->.i { color: green; background: white none; }-->-->-->-->

<!-- .j { color: green; background: white none; } -->

<!--
     .k { color: green; background: white none; }
-->

    .xa <!-- { color: yellow; background: red none; }

    .xb { color: yellow -->; background: red none <!--; }

    .xc { <!-- color: yellow; --> background: red none; }

    .xd { <!-- color: yellow; background: red none -->; }

 <! -- .xe { color: yellow; background: red none; }

--> <!--       --> <!-- -- >

  .xf { color: yellow; background: red none; }

");
            var e = CssStylesheet.Parse(@"
OL { list-style-type: lower-alpha; }
.a { color: green; background: white none; }
.b { color: green; background: white none; }
.c { color: green; background: white none; }
.d { color: green; background: white none; }
.e { color: green; background: white none; }
.f { color: green; background: white none; }
.g { color: green; background: white none; }
.h { color: green; background: white none; }
.i { color: green; background: white none; }
.j { color: green; background: white none; }
.k { color: green; background: white none; }
.xb { }
.xc { }
.xd { }
");
            AssertStylesheetsEqual(e, s);
        }

        [Test]
        public void TestSelectorConditions()
        {
            var s = CssStylesheet.Parse(@"
a > b, a {}
a + b {}
a:id + b[a] {}
a b {}
a b c {}
#id #r {}
");
            Console.WriteLine(s);
        }

        private void AssertStylesheetsEqual(CssStylesheet e, CssStylesheet s)
        {
            bool eq = e.Equals(s);
            Assert.True(eq);
        }
    }
}
