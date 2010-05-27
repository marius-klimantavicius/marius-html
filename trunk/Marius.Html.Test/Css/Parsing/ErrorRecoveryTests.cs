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
    p:before { content: 'Hello\
");
            var e = CssStylesheet.Parse(@"
@media screen {
    p:before { content: 'Hello' }
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

        private void AssertStylesheetsEqual(CssStylesheet e, CssStylesheet s)
        {
            Assert.Fail();
        }

        /*
        // must be identical (including order or rules and declarations)
        private static void AssertStylesheetsEqual(CssStylesheet expected, CssStylesheet actual)
        {
            Assert.AreEqual(expected.Charset, actual.Charset);
            Assert.AreEqual(expected.Imports.Length, actual.Imports.Length);
            Assert.AreEqual(expected.Rules.Length, actual.Rules.Length);

            for (int i = 0; i < expected.Imports.Length; i++)
            {
                Import expimp = expected.Imports[i], actimp = actual.Imports[i];
                Assert.AreEqual(expimp.MediaList.Length, actimp.MediaList.Length);
                Assert.AreEqual(expimp.Uri, actimp.Uri);

                AssertStringListsEqual(expimp.MediaList, actimp.MediaList);
            }

            for (int i = 0; i < expected.Rules.Length; i++)
            {
                Rule exprule = expected.Rules[i], actrule = actual.Rules[i];

                Assert.IsInstanceOf(exprule.GetType(), actrule);
                Assert.AreEqual(exprule.RuleType, actrule.RuleType);

                switch (exprule.RuleType)
                {
                    case RuleType.Media:
                        AssertMediasEqual((Media)exprule, (Media)actrule);
                        break;
                    case RuleType.Page:
                        AssertPagesEqual((Page)exprule, (Page)actrule);
                        break;
                    case RuleType.Ruleset:
                        AssertRulesetsEqual((Ruleset)exprule, (Ruleset)actrule);
                        break;
                    default:
                        Assert.Inconclusive("Unknown RuleType: {0}", exprule.RuleType);
                        break;
                }
            }
        }

        private static void AssertRulesetsEqual(Ruleset expected, Ruleset actual)
        {
            AssertSelectorListsEqual(expected.Selectors, actual.Selectors);
            AssertDeclarationsEqual(expected.Declarations, actual.Declarations);
        }

        private static void AssertSelectorListsEqual(Selector[] expected, Selector[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                var expsel = expected[i];
                var actsel = actual[i];

                AssertSelectorsEqual(expsel, actsel);
            }
        }

        private static void AssertSelectorsEqual(Selector expected, Selector actual)
        {
            Assert.IsInstanceOf(expected.GetType(), actual);

            if (expected is SimpleSelector)
            {
                AssertSimpleSelectorsEqual((SimpleSelector)expected, (SimpleSelector)actual);
            }
            else if (expected is ComplexSelector)
            {
                AssertComplexSelectorsEqual((ComplexSelector)expected, (ComplexSelector)actual);
            }
            else
                Assert.Inconclusive("Unknown selector type: {0}", expected.GetType());
        }

        private static void AssertComplexSelectorsEqual(ComplexSelector expected, ComplexSelector actual)
        {
            AssertSelectorsEqual(expected.Selector, actual.Selector);
            Assert.AreEqual(expected.Combinator, actual.Combinator);
            AssertSelectorsEqual(expected.Combined, actual.Combined);
        }

        private static void AssertSimpleSelectorsEqual(SimpleSelector expected, SimpleSelector actual)
        {
            Assert.IsInstanceOf(expected.ElementName.GetType(), actual.ElementName);

            if (expected.ElementName is IdentifierElementName)
            {
                var expname = (IdentifierElementName)expected.ElementName;
                var actname = (IdentifierElementName)actual.ElementName;
                Assert.AreEqual(expname.Name, actname.Name);
            }
            else if (!(expected.ElementName is StarElementName))
                Assert.Inconclusive("Unknown ElementName: {0}", expected.ElementName.GetType());

            Assert.AreEqual(expected.Specifiers.Length, actual.Specifiers.Length);

            for (int i = 0; i < expected.Specifiers.Length; i++)
            {
                var expspec = expected.Specifiers[i];
                var actspec = actual.Specifiers[i];

                Assert.IsInstanceOf(expspec.GetType(), actspec);
                switch (expspec.SpecifierType)
                {
                    case SpecifierType.Id:
                        Assert.AreEqual(((IdSpecifier)expspec).Id, ((IdSpecifier)actspec).Id);
                        break;
                    case SpecifierType.Class:
                        Assert.AreEqual(((ClassSpecifier)expspec).Name, ((ClassSpecifier)actspec).Name);
                        break;
                    case SpecifierType.Attribute:
                        AssertAttributeSpecifiersEqual((AttributeSpecifier)expspec, (AttributeSpecifier)actspec);
                        break;
                    case SpecifierType.PseudoId:
                        Assert.AreEqual(((IdentifierPseudoSpecifier)expspec).Name, ((IdentifierPseudoSpecifier)actspec).Name);
                        break;
                    case SpecifierType.PseudoFunction:
                        Assert.AreEqual(((FunctionPseudoSpecifier)expspec).Function, ((FunctionPseudoSpecifier)actspec).Function);
                        Assert.AreEqual(((FunctionPseudoSpecifier)expspec).Argument, ((FunctionPseudoSpecifier)actspec).Argument);
                        break;
                    default:
                        Assert.Inconclusive("Unknown specifier type: {0}", expspec.SpecifierType);
                        break;
                }
            }
        }

        private static void AssertAttributeSpecifiersEqual(AttributeSpecifier expected, AttributeSpecifier actual)
        {
            Assert.AreEqual(expected.Name, actual.Name);

            if (expected.Filter == null && actual.Filter == null)
                return;

            Assert.False(expected.Filter == null || actual.Filter == null);
            Assert.AreEqual(expected.Filter.Operator, actual.Filter.Operator);
            Assert.AreEqual(expected.Filter.Value, actual.Filter.Value);
        }

        private static void AssertPagesEqual(Page expected, Page actual)
        {
            Assert.AreEqual(expected.PseudoPage, actual.PseudoPage);
            AssertDeclarationsEqual(expected.Declarations, actual.Declarations);
        }

        private static void AssertDeclarationsEqual(Declaration[] expected, Declaration[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                var expdecl = expected[i];
                var actdecl = actual[i];

                Assert.AreEqual(expdecl.Property, actdecl.Property);
                Assert.AreEqual(expdecl.Important, actdecl.Important);
                AssertExpressionsEqual(expdecl.Value, actdecl.Value);
            }
        }

        private static void AssertExpressionsEqual(Expression expected, Expression actual)
        {
            Assert.AreEqual(expected.Terms.Length, actual.Terms.Length);

            for (int i = 0; i < expected.Terms.Length; i++)
            {
                var expterm = expected.Terms[i];
                var actterm = actual.Terms[i];

                Assert.AreEqual(expterm.Type, actterm.Type);
                Assert.IsInstanceOf(expterm.GetType(), actterm);

                switch (expterm.Type)
                {
                    case TermType.Operator:
                        Assert.AreEqual(((OperatorTerm)expterm).Operator, ((OperatorTerm)actterm).Operator);
                        break;
                    case TermType.AngleDimension:
                        Assert.AreEqual(((DimensionTerm)expterm).Sign, ((DimensionTerm)actterm).Sign);
                        Assert.AreEqual(((AngleDimension)((DimensionTerm)expterm).Dimension).Value, ((AngleDimension)((DimensionTerm)actterm).Dimension).Value);
                        Assert.AreEqual(((AngleDimension)((DimensionTerm)expterm).Dimension).Units, ((AngleDimension)((DimensionTerm)actterm).Dimension).Units);
                        break;
                    case TermType.LengthDimension:
                        Assert.AreEqual(((DimensionTerm)expterm).Sign, ((DimensionTerm)actterm).Sign);
                        Assert.AreEqual(((LengthDimension)((DimensionTerm)expterm).Dimension).Value, ((LengthDimension)((DimensionTerm)actterm).Dimension).Value);
                        Assert.AreEqual(((LengthDimension)((DimensionTerm)expterm).Dimension).Units, ((LengthDimension)((DimensionTerm)actterm).Dimension).Units);
                        break;
                    case TermType.TimeDimension:
                        Assert.AreEqual(((DimensionTerm)expterm).Sign, ((DimensionTerm)actterm).Sign);
                        Assert.AreEqual(((TimeDimension)((DimensionTerm)expterm).Dimension).Value, ((TimeDimension)((DimensionTerm)actterm).Dimension).Value);
                        Assert.AreEqual(((TimeDimension)((DimensionTerm)expterm).Dimension).Units, ((TimeDimension)((DimensionTerm)actterm).Dimension).Units);
                        break;
                    case TermType.FrequencyDimension:
                        Assert.AreEqual(((DimensionTerm)expterm).Sign, ((DimensionTerm)actterm).Sign);
                        Assert.AreEqual(((FrequencyDimension)((DimensionTerm)expterm).Dimension).Value, ((FrequencyDimension)((DimensionTerm)actterm).Dimension).Value);
                        Assert.AreEqual(((FrequencyDimension)((DimensionTerm)expterm).Dimension).Units, ((FrequencyDimension)((DimensionTerm)actterm).Dimension).Units);
                        break;
                    case TermType.PercentageDimension:
                        Assert.AreEqual(((DimensionTerm)expterm).Sign, ((DimensionTerm)actterm).Sign);
                        Assert.AreEqual(((Percentage)((DimensionTerm)expterm).Dimension).Value, ((Percentage)((DimensionTerm)actterm).Dimension).Value);
                        break;
                    case TermType.EmsDimension:
                        Assert.AreEqual(((DimensionTerm)expterm).Sign, ((DimensionTerm)actterm).Sign);
                        Assert.AreEqual(((EmsDimension)((DimensionTerm)expterm).Dimension).Value, ((EmsDimension)((DimensionTerm)actterm).Dimension).Value);
                        break;
                    case TermType.ExsDimension:
                        Assert.AreEqual(((DimensionTerm)expterm).Sign, ((DimensionTerm)actterm).Sign);
                        Assert.AreEqual(((ExsDimension)((DimensionTerm)expterm).Dimension).Value, ((ExsDimension)((DimensionTerm)actterm).Dimension).Value);
                        break;
                    case TermType.NumberDimension:
                        Assert.AreEqual(((DimensionTerm)expterm).Sign, ((DimensionTerm)actterm).Sign);
                        Assert.AreEqual(((NumberDimension)((DimensionTerm)expterm).Dimension).Value, ((NumberDimension)((DimensionTerm)actterm).Dimension).Value);
                        break;
                    case TermType.String:
                        Assert.AreEqual(((StringTerm)expterm).Value, ((StringTerm)actterm).Value);
                        break;
                    case TermType.Identifier:
                        Assert.AreEqual(((IdentifierTerm)expterm).Name, ((IdentifierTerm)actterm).Name);
                        break;
                    case TermType.Uri:
                        Assert.AreEqual(((UriTerm)expterm).Uri, ((UriTerm)actterm).Uri);
                        break;
                    case TermType.HexColor:
                        Assert.AreEqual(((HexColorTerm)expterm).Value, ((HexColorTerm)actterm).Value);
                        break;
                    case TermType.Function:
                        Assert.AreEqual(((FunctionTerm)expterm).Name, ((FunctionTerm)actterm).Name);
                        AssertExpressionsEqual(((FunctionTerm)expterm).Arguments, ((FunctionTerm)actterm).Arguments);
                        break;
                    case TermType.Unknown:
                        Assert.Fail("Unknown term encountered");
                        break;
                    default:
                        Assert.Inconclusive("Unknown term type: {0}", expterm.Type);
                        break;
                }
            }
        }

        private static void AssertMediasEqual(Media expected, Media actual)
        {
            Assert.AreEqual(expected.Ruleset.Length, actual.Ruleset.Length);

            for (int i = 0; i < expected.Ruleset.Length; i++)
            {
                AssertRulesetsEqual(expected.Ruleset[i], actual.Ruleset[i]);
            }

            AssertStringListsEqual(expected.MediaList, actual.MediaList);
        }

        private static void AssertStringListsEqual(string[] expected, string[] actual)
        {
            if (expected == null && actual == null)
                return;

            Assert.False(expected == null || actual == null);

            Assert.AreEqual(expected.Length, actual.Length);
            for (int i = 0; i < expected.Length; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }*/
    }
}
