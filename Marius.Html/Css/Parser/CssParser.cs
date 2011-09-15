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
using System.Diagnostics.Contracts;
using Marius.Html.Css.Dom;
using Marius.Html.Css.Values;
using Marius.Html.Css.Selectors;

namespace Marius.Html.Css.Parser
{
    public partial class CssParser
    {
        private static readonly string[] EmptyStringArray = new string[0];

        private TokenBuffer _scanner;
        private CssContext _context;

        public CssParser(CssScanner scanner)
            : this(new CssContext(), scanner)
        {
        }

        public CssParser(CssContext context, CssScanner scanner)
        {
            Contract.Requires(scanner != null);

            _context = context;
            _scanner = new TokenBuffer(scanner);
        }

        public CssStylesheet Parse(CssStylesheetSource source)
        {
            _scanner.MoveNext();

            return Stylesheet(source);
        }

        private CssStylesheet Stylesheet(CssStylesheetSource source)
        {
            string charset = null;
            List<CssRule> rules = new List<CssRule>();

            if (_scanner.Current == CssTokens.AtCharset)
            {
                charset = Charset();
                if (charset != null)
                    rules.Add(new CssCharset(charset));
            }

            _scanner.SkipSgml();

            while (_scanner.Current == CssTokens.AtImport)
            {
                CssImport import = Import();
                if (import != null)
                    rules.Add(import);

                _scanner.SkipSgml();
            }

            while (_scanner.Current != CssTokens.EOF)
            {
                CssRule rule = Rule();
                if (rule != null)
                    rules.Add(rule);
                _scanner.SkipSgml();
            }
            return new CssStylesheet(source, rules.ToArray());
        }

        private CssRule Rule()
        {
            if (_scanner.Current == CssTokens.AtPage)
                return Page();
            else if (_scanner.Current == CssTokens.AtMedia)
                return Media();
            else if (_scanner.Current == CssTokens.AtImport || _scanner.Current == CssTokens.AtKeyword)
            {
                _scanner.SkipSemicolonOrBlock(true, true);
                _scanner.SkipWhitespace();
            }
            else
                return Ruleset();
            return null;
        }

        private CssStyle Ruleset()
        {
            int nesting = 0;
            try
            {
                //ruleset
                //  : selector [ ',' S* selector ]*
                //    '{' S* declaration? [ ';' S* declaration? ]* '}' S*
                //  ;

                List<CssSelector> selectors = new List<CssSelector>();

                CssSelector sel;

                sel = Selector();
                if (sel != null)
                    selectors.Add(sel);

                while (_scanner.Current == CssTokens.Comma)
                {
                    Match(CssTokens.Comma);
                    _scanner.SkipWhitespace();
                    sel = Selector();
                    if (sel != null)
                        selectors.Add(sel);
                }

                Match(CssTokens.OpenBrace);
                nesting++;

                CssDeclaration[] decls = Declarations();

                MatchEof(CssTokens.CloseBrace);
                nesting--;
                _scanner.SkipWhitespace();

                return new CssStyle(selectors.ToArray(), decls);
            }
            catch (CssParsingException)
            {
                _scanner.SkipEndBlock(nesting);
                _scanner.SkipWhitespace();
            }
            return null;
        }

        // for style='<declarations>' parsing
        public CssDeclaration[] Declarations()
        {
            List<CssDeclaration> decls = new List<CssDeclaration>();

            _scanner.SkipWhitespace();

            MaybeDeclaration(decls);

            while (_scanner.Current == CssTokens.SemiColon)
            {
                Match(CssTokens.SemiColon);

                _scanner.SkipWhitespace();
                MaybeDeclaration(decls);
            }

            return decls.ToArray();
        }

        private void MaybeDeclaration(List<CssDeclaration> decls)
        {
            CssDeclaration decl = null;
            if (MatchesDeclarationStart())
                decl = Declaration();

            if (_scanner.Current != CssTokens.SemiColon && _scanner.Current != CssTokens.CloseBrace && _scanner.Current != CssTokens.EOF)
            {
                _scanner.SkipSemicolonOrBlock(false, true);
                _scanner.SkipWhitespace();
            }
            else if (decl != null)
            {
                decls.Add(decl);
            }
        }

        private CssSelector Selector()
        {
            //selector
            //  : simple_selector [ combinator selector | S+ [ combinator? selector ]? ]?
            //  ;

            CssSelector result;
            CssSimpleSelector selector;

            result = selector = SimpleSelector();

            if (_scanner.Current == CssTokens.Plus || _scanner.Current == CssTokens.More)
            {
                result = ComplexSelector(selector);
            }
            else if (_scanner.Current == CssTokens.Whitespace)
            {
                _scanner.SkipWhitespace();

                if (_scanner.Current == CssTokens.Plus || _scanner.Current == CssTokens.More)
                {
                    result = ComplexSelector(selector);
                }
                else if (MatchesSelectorStart())
                {
                    CssSelector combined;

                    combined = Selector();

                    result = new CssDescendantSelector(selector, combined);
                }
            }

            // TODO: need to check whether pseudo-element selector is appended only to the last simple_selector
            return result;
        }

        private CssSelector ComplexSelector(CssSimpleSelector first)
        {
            CssSelectorCombinator combinator;
            CssSelector second;

            combinator = Combinator();
            second = Selector();

            switch (combinator)
            {
                case CssSelectorCombinator.Sibling:
                    return new CssSiblingSelector(first, second);
                case CssSelectorCombinator.Child:
                    return new CssChildSelector(first, second);
            }

            throw new CssInvalidStateException();
        }

        private CssSimpleSelector SimpleSelector()
        {
            //simple_selector
            //  : element_name [ HASH | class | attrib | pseudo ]*
            //  | [ HASH | class | attrib | pseudo ]+
            //  ;

            CssSimpleSelector result;
            CssCondition[] conditions;

            if (_scanner.Current == CssTokens.Identifier || _scanner.Current == CssTokens.Star)
            {
                result = ElementName();

                if (MatchesSpecifierStart())
                {
                    conditions = Conditions();
                    result = new CssConditionalSelector(result, conditions);
                }
            }
            else
            {
                result = CssUniversalSelector.Instance;
                conditions = Conditions();

                result = new CssConditionalSelector(result, conditions);
            }

            return result;
        }

        private CssCondition[] Conditions()
        {
            CssCondition other;

            List<CssCondition> result = new List<CssCondition>();

            other = Specifier();
            result.Add(other);

            while (MatchesSpecifierStart())
            {
                other = Specifier();
                result.Add(other);
            }

            return result.ToArray();
        }

        private CssCondition Specifier()
        {
            //[ HASH | class | attrib | pseudo ]
            switch (_scanner.Current)
            {
                case CssTokens.Hash:
                    return Match(CssTokens.Hash, s => new CssIdCondition(s.String));
                case CssTokens.Dot:
                    return Class();
                case CssTokens.OpenBracket:
                    return Attribute();
                case CssTokens.Colon:
                    return Pseudo();
                default:
                    throw new CssParsingException();
            }
        }

        private CssCondition Pseudo()
        {
            int nesting = 0;
            try
            {
                //pseudo
                //  : ':' [ IDENT | FUNCTION S* [IDENT S*]? ')' ]
                //  ;

                Match(CssTokens.Colon);
                if (_scanner.Current == CssTokens.Identifier)
                {
                    return Match(CssTokens.Identifier, s => _context.PseudoConditionFactory.PseudoIdentifierCondition(s.String));
                }
                else if (_scanner.Current == CssTokens.Function)
                {
                    string function, argument = null;

                    function = Match(CssTokens.Function, s => s.String);
                    nesting++;

                    _scanner.SkipWhitespace();
                    if (_scanner.Current == CssTokens.Identifier)
                    {
                        argument = Match(CssTokens.Identifier, s => s.String);
                        _scanner.SkipWhitespace();
                    }
                    Match(CssTokens.CloseParen);
                    nesting--;

                    return _context.PseudoConditionFactory.PseudoFunctionCondition(function, argument);
                }
                else
                {
                    throw new CssParsingException();
                }
            }
            catch (CssParsingException)
            {
                if (nesting > 0)
                    _scanner.SkipParen(nesting);
                throw;
            }
        }

        private CssCondition Attribute()
        {
            int nesting = 0;
            try
            {
                //attrib
                //  : '[' S* IDENT S* [ [ '=' | INCLUDES | DASHMATCH ] S*
                //    [ IDENT | STRING ] S* ]? ']'
                //  ;

                string name;

                Match(CssTokens.OpenBracket);
                nesting++;

                _scanner.SkipWhitespace();
                name = Match(CssTokens.Identifier, s => s.String);
                _scanner.SkipWhitespace();

                Func<string, CssCondition> create = null;
                string value = null;

                if (_scanner.Current == CssTokens.Equals || _scanner.Current == CssTokens.Includes || _scanner.Current == CssTokens.DashMatch)
                {

                    switch (_scanner.Current)
                    {
                        case CssTokens.Equals:
                            create = s => new CssAttributeCondition(name, s);
                            break;
                        case CssTokens.Includes:
                            create = s => new CssIncludesAttributeCondition(name, s);
                            break;
                        case CssTokens.DashMatch:
                            create = s => new CssBeginHyphenAttributeCondition(name, s);
                            break;
                        default:
                            throw new CssInvalidStateException();
                    }
                    _scanner.MoveNext();

                    _scanner.SkipWhitespace();

                    if (_scanner.Current == CssTokens.Identifier || _scanner.Current == CssTokens.String)
                    {
                        value = _scanner.Value.String;
                        _scanner.MoveNext();
                    }
                    else
                    {
                        throw new CssParsingException();
                    }

                    _scanner.SkipWhitespace();
                }

                Match(CssTokens.CloseBracket);
                nesting--;

                if (create != null && value != null)
                    return create(value);

                return new CssAttributeCondition(name, value);
            }
            catch (CssParsingException)
            {
                if (nesting > 0)
                    _scanner.SkipBracket(nesting);
                throw;
            }
        }

        private CssClassCondition Class()
        {
            Match(CssTokens.Dot);
            return Match(CssTokens.Identifier, s => new CssClassCondition(s.String));
        }

        private bool MatchesSpecifierStart()
        {
            switch (_scanner.Current)
            {
                case CssTokens.Hash:
                case CssTokens.Dot:
                case CssTokens.OpenBracket:
                case CssTokens.Colon:
                    return true;
            }
            return false;
        }

        private CssSimpleSelector ElementName()
        {
            switch (_scanner.Current)
            {
                case CssTokens.Identifier:
                    return Match(CssTokens.Identifier, s => new CssElementSelector(s.String));
                case CssTokens.Star:
                    return Match(CssTokens.Star, s => CssUniversalSelector.Instance);
                default:
                    throw new CssParsingException();
            }
        }

        private CssSelectorCombinator Combinator()
        {
            //combinator
            //  : '+' S*
            //  | '>' S*
            //  ;

            CssSelectorCombinator result;

            switch (_scanner.Current)
            {
                case CssTokens.Plus:
                    result = Match(CssTokens.Plus, s => CssSelectorCombinator.Sibling);
                    break;
                case CssTokens.More:
                    result = Match(CssTokens.More, s => CssSelectorCombinator.Child);
                    break;
                default:
                    throw new CssParsingException();
            }
            _scanner.SkipWhitespace();
            return result;
        }

        private bool MatchesSelectorStart()
        {
            //first
            //  : simple_selector [ combinator first | S+ [ combinator? first ]? ]?
            //  ;
            //simple_selector
            //  : element_name [ HASH | class | attrib | pseudo ]*
            //  | [ HASH | class | attrib | pseudo ]+
            //  ;
            //class
            //  : '.' IDENT
            //  ;
            //element_name
            //  : IDENT | '*'
            //  ;
            //attrib
            //  : '[' S* IDENT S* [ [ '=' | INCLUDES | DASHMATCH ] S*
            //    [ IDENT | STRING ] S* ]? ']'
            //  ;
            //pseudo
            //  : ':' [ IDENT | FUNCTION S* [IDENT S*]? ')' ]
            //  ;

            switch (_scanner.Current)
            {
                case CssTokens.Identifier:
                case CssTokens.Star:
                case CssTokens.Hash:
                case CssTokens.Dot:
                case CssTokens.OpenBracket:
                case CssTokens.Colon:
                    return true;
            }
            return false;
        }

        private CssMedia Media()
        {
            int nesting = 0;

            try
            {
                //media
                //  : MEDIA_SYM S* media_list LBRACE S* ruleset* '}' S*
                //  ;

                string[] mediaList;
                List<CssStyle> rules = new List<CssStyle>();

                Match(CssTokens.AtMedia);
                _scanner.SkipWhitespace();
                mediaList = MediaList();
                Match(CssTokens.OpenBrace);
                nesting++;

                _scanner.SkipWhitespace();
                while (_scanner.Current != CssTokens.CloseBrace && _scanner.Current != CssTokens.EOF)
                {
                    if (_scanner.Current == CssTokens.Identifier || _scanner.Current == CssTokens.Star)
                    {
                        CssStyle ruleset = Ruleset();
                        if (ruleset != null)
                            rules.Add(ruleset);
                    }
                    else
                    {
                        // error skip till ';' or {}
                        _scanner.SkipSemicolonOrBlock(true, true, nesting);
                        _scanner.SkipWhitespace();
                    }
                }

                MatchEof(CssTokens.CloseBrace);
                nesting--;

                return new CssMedia(mediaList, rules.ToArray());
            }
            catch (CssParsingException)
            {
                _scanner.SkipSemicolonOrEndBlock(nesting);
                _scanner.SkipWhitespace();
            }

            return null;
        }

        private CssPage Page()
        {
            int nesting = 0;
            try
            {
                //page
                //  : PAGE_SYM S* pseudo_page?
                //    '{' S* declaration? [ ';' S* declaration? ]* '}' S*
                //  ;

                string pseudo = null;
                List<CssDeclaration> decls = new List<CssDeclaration>();

                Match(CssTokens.AtPage);
                _scanner.SkipWhitespace();

                if (_scanner.Current == CssTokens.Colon)
                    pseudo = PseudoPage();

                Match(CssTokens.OpenBrace);
                nesting++;

                _scanner.SkipWhitespace();

                MaybeDeclaration(decls);

                while (_scanner.Current == CssTokens.SemiColon)
                {
                    Match(CssTokens.SemiColon);
                    _scanner.SkipWhitespace();
                    MaybeDeclaration(decls);
                }

                MatchEof(CssTokens.CloseBrace);
                nesting--;
                _scanner.SkipWhitespace();

                return new CssPage(pseudo, decls.ToArray());
            }
            catch (CssParsingException)
            {
                _scanner.SkipSemicolonOrEndBlock(nesting);
                _scanner.SkipWhitespace();
            }

            return null;
        }

        private CssDeclaration Declaration()
        {
            try
            {
                //declaration
                //  : property ':' S* expr prio?
                //  ;
                // must handle error till ';'

                string property;
                CssExpression value;
                bool important = false;

                property = Property();
                Match(CssTokens.Colon);
                _scanner.SkipWhitespace();
                value = Expression();
                if (_scanner.Current == CssTokens.Important)
                    important = Priority();

                return new CssDeclaration(property, value, important);
            }
            catch (CssParsingException)
            {
                _scanner.SkipSemicolonOrBlock(false, true);
                _scanner.SkipWhitespace();
            }
            return null;
        }

        private CssExpression Expression()
        {
            // if function == true, allow end with ')' or EOF
            // otherwise with ';' or '}' or !important or EOF

            //expr
            //  : term [ operator? term ]*
            //  ;

            List<CssValue> terms = new List<CssValue>();

            CssValue single;

            single = Term();
            terms.Add(single);

            while (true)
            {
                if (_scanner.Current == CssTokens.Slash || _scanner.Current == CssTokens.Comma)
                {
                    CssOperator op = Operator();
                    single = Term();

                    terms.Add(op);
                    terms.Add(single);
                }
                else if (IsTermStart())
                {
                    single = Term();

                    terms.Add(single);
                }
                else
                {
                    break;
                }
            }

            return new CssExpression(terms.ToArray());
        }

        private CssOperator Operator()
        {
            CssOperator op;

            switch (_scanner.Current)
            {
                case CssTokens.Slash:
                    op = Match(CssTokens.Slash, s => CssOperator.Slash);
                    break;
                case CssTokens.Comma:
                    op = Match(CssTokens.Comma, s => CssOperator.Comma);
                    break;
                default:
                    throw new CssParsingException();
            }

            _scanner.SkipWhitespace();
            return op;
        }

        private CssValue Term()
        {
            //term
            //  : unary_operator?
            //    [ NUMBER S* | PERCENTAGE S* | LENGTH S* | EMS S* | EXS S* | ANGLE S* |
            //      TIME S* | FREQ S* ]
            //  | STRING S* | IDENT S* | URI S* | hexcolor | function
            //  ;

            CssValue result = null;
            CssSignOperator sign = CssSignOperator.Plus;

            switch (_scanner.Current)
            {
                case CssTokens.Plus:
                case CssTokens.Minus:
                    sign = UnaryOperator();
                    result = Dimension(sign);
                    _scanner.SkipWhitespace();
                    break;
                case CssTokens.Number:
                case CssTokens.Percentage:
                case CssTokens.Length:
                case CssTokens.Angle:
                case CssTokens.Time:
                case CssTokens.Frequency:
                    result = Dimension(CssSignOperator.Plus);
                    _scanner.SkipWhitespace();
                    break;
                case CssTokens.String:
                    result = Match(CssTokens.String, s => new CssString(s.String));
                    _scanner.SkipWhitespace();
                    break;
                case CssTokens.Identifier:
                    result = Match(CssTokens.Identifier, s => new CssIdentifier(s.String));
                    _scanner.SkipWhitespace();
                    break;
                case CssTokens.Uri:
                    result = Match(CssTokens.Uri, s => new CssUri(s.String));
                    _scanner.SkipWhitespace();
                    break;
                case CssTokens.Hash:
                    result = HexColor();
                    break;
                case CssTokens.Function:
                    result = Function();
                    break;
                default:
                    throw new CssParsingException();
            }

            return result;
        }

        private CssValue Function()
        {
            int nesting = 0;
            try
            {
                //function
                //  : FUNCTION S* expr ')' S*
                //  ;

                string name;
                CssExpression arguments;

                name = Match(CssTokens.Function, s => s.String);
                nesting++;

                _scanner.SkipWhitespace();
                arguments = Expression();
                MatchEof(CssTokens.CloseParen);
                nesting--;

                _scanner.SkipWhitespace();

                return _context.FunctionFactory.Function(name, arguments);
            }
            catch (CssParsingException)
            {
                if (nesting > 0)
                    _scanner.SkipParen(nesting);
                throw;
            }
        }

        private CssColor HexColor()
        {
            string result;

            result = Match(CssTokens.Hash, s => s.String);
            _scanner.SkipWhitespace();

            if ((result.Length == 3 || result.Length == 6) && result.ToUpperInvariant().All(s => (s >= '0' && s <= '9') || (s >= 'A' && s <= 'F')))
                return new CssHexColor(result);

            throw new CssParsingException();
        }

        private CssValue Dimension(CssSignOperator sign)
        {
            CssValue result;

            switch (_scanner.Current)
            {
                case CssTokens.Number:
                    result = Match(CssTokens.Number, s => new CssNumber(Signed(s.Double, sign)));
                    break;
                case CssTokens.Percentage:
                    result = Match(CssTokens.Percentage, s => new CssPercentage(Signed(s.Dimension.Value, sign)));
                    break;
                case CssTokens.Length:
                    result = Match(CssTokens.Length, s => new CssLength(Signed(s.Dimension.Value, sign), s.Dimension.Units));
                    break;
                case CssTokens.Angle:
                    result = Match(CssTokens.Angle, s => new CssAngle(Signed(s.Dimension.Value, sign), s.Dimension.Units));
                    break;
                case CssTokens.Time:
                    result = Match(CssTokens.Time, s => new CssTime(Signed(s.Dimension.Value, sign), s.Dimension.Units));
                    break;
                case CssTokens.Frequency:
                    result = Match(CssTokens.Frequency, s => new CssFrequency(Signed(s.Dimension.Value, sign), s.Dimension.Units));
                    break;
                case CssTokens.Dimension:
                    result = Match(CssTokens.Dimension, s => new CssDimension(Signed(s.Dimension.Value, sign), s.Dimension.DimensionString));
                    break;
                default:
                    throw new CssParsingException();
            }
            _scanner.SkipWhitespace();

            return result;
        }

        private float Signed(float value, CssSignOperator sign)
        {
            switch (sign)
            {
                case CssSignOperator.Plus:
                    return value;
                case CssSignOperator.Minus:
                    return -value;
                default:
                    throw new ArgumentException("sign");
            }
        }

        private CssSignOperator UnaryOperator()
        {
            if (_scanner.Current == CssTokens.Plus)
            {
                return Match(CssTokens.Plus, s => CssSignOperator.Plus);
            }
            else if (_scanner.Current == CssTokens.Minus)
            {
                return Match(CssTokens.Minus, s => CssSignOperator.Minus);
            }
            throw new CssParsingException();
        }

        private bool IsTermStart()
        {
            switch (_scanner.Current)
            {
                case CssTokens.String:
                case CssTokens.Number:
                case CssTokens.Percentage:
                case CssTokens.Hash:
                case CssTokens.Identifier:
                case CssTokens.Function:
                case CssTokens.Length:
                case CssTokens.Angle:
                case CssTokens.Time:
                case CssTokens.Frequency:
                case CssTokens.Uri:
                case CssTokens.Plus:
                case CssTokens.Minus:
                    return true;
            }
            return false;
        }

        private bool Priority()
        {
            //prio
            //  : IMPORTANT_SYM S*
            //  ;

            Match(CssTokens.Important);
            _scanner.SkipWhitespace();

            return true;
        }

        private string Property()
        {
            //property
            //  : IDENT S*
            //  ;

            string result;

            result = Match(CssTokens.Identifier, s => s.String);
            _scanner.SkipWhitespace();

            return result;
        }

        private bool MatchesDeclarationStart()
        {
            //declaration
            //  : property ':' S* expr prio?
            //  ;

            //property
            //  : IDENT S*
            //  ;
            return _scanner.Current == CssTokens.Identifier;
        }

        private string PseudoPage()
        {
            //pseudo_page
            //  : ':' IDENT S*
            //  ;
            string result;

            Match(CssTokens.Colon);
            result = Match(CssTokens.Identifier, s => s.String);
            _scanner.SkipWhitespace();

            return result;
        }

        private CssImport Import()
        {
            try
            {
                // import
                //    :   IMPORT_SYM S* [STRING|URI] S* media_list? ';' S*
                //    ;
                string import;
                string[] mediaList = EmptyStringArray;

                Match(CssTokens.AtImport);
                _scanner.SkipWhitespace();

                if (_scanner.Current == CssTokens.String)
                    import = Match(CssTokens.String, s => s.String);
                else if (_scanner.Current == CssTokens.Uri)
                    import = Match(CssTokens.Uri, s => s.String);
                else
                    throw new CssParsingException();

                _scanner.SkipWhitespace();

                if (_scanner.Current == CssTokens.Identifier)
                    mediaList = MediaList();
                MatchEof(CssTokens.SemiColon);

                return new CssImport(import, mediaList);
            }
            catch (CssParsingException)
            {
                _scanner.SkipSemicolonOrBlock(true, true);
            }

            return null;
        }

        private string[] MediaList()
        {
            List<string> result = new List<string>();
            string single;

            single = Medium();
            result.Add(single);

            while (_scanner.Current == CssTokens.Comma)
            {
                Match(CssTokens.Comma);
                _scanner.SkipWhitespace();

                single = Medium();
                result.Add(single);
            }
            return result.ToArray();
        }

        private string Medium()
        {
            string result;
            result = Match(CssTokens.Identifier, s => s.String);
            _scanner.SkipWhitespace();
            return result;
        }

        private string Charset()
        {
            try
            {
                string charset;

                Match(CssTokens.AtCharset);
                charset = Match(CssTokens.String, (s) => s.String);
                MatchEof(CssTokens.SemiColon);

                return charset;
            }
            catch (CssParsingException)
            {
                _scanner.SkipSemicolonOrBlock(true, true);
            }
            return null;
        }

        private void MatchEof(CssTokens token)
        {
            if (_scanner.Current != token && _scanner.Current != CssTokens.EOF)
                throw new CssParsingException();

            _scanner.MoveNext();
        }

        private void Match(CssTokens token)
        {
            if (_scanner.Current != token)
                throw new CssParsingException();

            _scanner.MoveNext();
        }

        private T Match<T>(CssTokens token, Func<CssSourceToken, T> onSuccess = null)
        {
            if (_scanner.Current != token)
                throw new CssParsingException();

            T result = default(T);
            if (onSuccess != null)
                result = onSuccess(_scanner.Value);

            _scanner.MoveNext();
            return result;
        }
    }
}
