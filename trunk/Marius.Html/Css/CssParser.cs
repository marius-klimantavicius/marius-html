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
using System.Linq;
using System.Text;
using System.Diagnostics.Contracts;

namespace Marius.Html.Css
{
    /*
     * TODO:
     *  must ignore any '@import'  rule that occurs inside a block or after any non-ignored statement other than an @charset or an @import rule. 
     */
    public class CssParser
    {
        private static readonly string[] EmptyStringArray = new string[0];

        private TokenBuffer _scanner;

        public CssParser(CssScanner scanner)
        {
            Contract.Requires(scanner != null);

            _scanner = new TokenBuffer(scanner);
        }

        public Stylesheet Parse()
        {
            _scanner.MoveNext();

            return Stylesheet();
        }

        private Stylesheet Stylesheet()
        {
            string charset = null;
            List<Import> imports = new List<Import>();
            List<Rule> rules = new List<Rule>();

            if (_scanner.Current == CssTokens.AtCharset)
                charset = Charset();

            SkipSgml();

            while (_scanner.Current == CssTokens.AtImport)
            {
                Import import = Import();
                if (import != null)
                    imports.Add(import);

                SkipSgml();
            }

            while (_scanner.Current != CssTokens.EOF)
            {
                Rule rule = Rule();
                if (rule != null)
                    rules.Add(rule);
                SkipSgml();
            }
            return new Stylesheet(charset, imports.ToArray(), rules.ToArray());
        }

        private Rule Rule()
        {
            if (_scanner.Current == CssTokens.AtPage)
                return Page();
            else if (_scanner.Current == CssTokens.AtMedia)
                return Media();
            else if (_scanner.Current == CssTokens.AtImport || _scanner.Current == CssTokens.AtKeyword)
            {
                SkipSemicolonOrBlock(true, true);
                SkipWhitespace();
            }
            else
                return Ruleset();
            return null;
        }

        private Ruleset Ruleset()
        {
            int nesting = 0;
            try
            {
                //ruleset
                //  : selector [ ',' S* selector ]*
                //    '{' S* declaration? [ ';' S* declaration? ]* '}' S*
                //  ;

                List<Selector> selectors = new List<Selector>();
                List<Declaration> decls = new List<Declaration>();

                Selector sel;

                sel = Selector();
                if (sel != null)
                    selectors.Add(sel);

                while (_scanner.Current == CssTokens.Comma)
                {
                    Match(CssTokens.Comma);
                    SkipWhitespace();
                    sel = Selector();
                    if (sel != null)
                        selectors.Add(sel);
                }

                Match(CssTokens.OpenBrace);
                nesting++;

                SkipWhitespace();

                MaybeDeclaration(decls);

                while (_scanner.Current == CssTokens.SemiColon)
                {
                    Match(CssTokens.SemiColon);

                    SkipWhitespace();
                    MaybeDeclaration(decls);
                }

                MatchEof(CssTokens.CloseBrace);
                nesting--;
                SkipWhitespace();

                return new Ruleset(selectors.ToArray(), decls.ToArray());
            }
            catch (CssParsingException)
            {
                SkipEndBlock(nesting);
                SkipWhitespace();
            }
            return null;
        }

        private void MaybeDeclaration(List<Declaration> decls)
        {
            Declaration decl = null;
            if (MatchesDeclarationStart())
                decl = Declaration();

            if (_scanner.Current != CssTokens.SemiColon && _scanner.Current != CssTokens.CloseBrace && _scanner.Current != CssTokens.EOF)
            {
                SkipSemicolonOrBlock(false, true);
                SkipWhitespace();
            }
            else if (decl != null)
            {
                decls.Add(decl);
            }
        }

        private Selector Selector()
        {
            //selector
            //  : simple_selector [ combinator selector | S+ [ combinator? selector ]? ]?
            //  ;

            Selector result;
            SimpleSelector selector;

            result = selector = SimpleSelector();

            if (_scanner.Current == CssTokens.Plus || _scanner.Current == CssTokens.More)
            {
                result = CombinedSelector(selector);
            }
            else if (_scanner.Current == CssTokens.Whitespace)
            {
                SkipWhitespace();

                if (_scanner.Current == CssTokens.Plus || _scanner.Current == CssTokens.More)
                {
                    result = CombinedSelector(selector);
                }
                else if (MatchesSelectorStart())
                {
                    Selector combined;

                    combined = Selector();

                    result = new ComplexSelector(selector, Css.Combinator.Descendant, combined);
                }
            }

            return result;
        }

        private Selector CombinedSelector(SimpleSelector selector)
        {
            Selector result;
            Combinator combinator;
            Selector combined;

            combinator = Combinator();
            combined = Selector();

            result = new ComplexSelector(selector, combinator, combined);
            return result;
        }

        private SimpleSelector SimpleSelector()
        {
            //simple_selector
            //  : element_name [ HASH | class | attrib | pseudo ]*
            //  | [ HASH | class | attrib | pseudo ]+
            //  ;

            ElementName name;
            List<Specifier> specifiers = new List<Specifier>();

            Specifier spec;

            if (_scanner.Current == CssTokens.Identifier || _scanner.Current == CssTokens.Star)
            {
                name = ElementName();

                while (MatchesSpecifierStart())
                {
                    spec = Specifier();
                    specifiers.Add(spec);
                }
            }
            else
            {
                name = StarElementName.Instance;

                spec = Specifier();
                specifiers.Add(spec);

                while (MatchesSpecifierStart())
                {
                    spec = Specifier();
                    specifiers.Add(spec);
                }
            }

            return new SimpleSelector(name, specifiers.ToArray());
        }

        private Specifier Specifier()
        {
            //[ HASH | class | attrib | pseudo ]
            switch (_scanner.Current)
            {
                case CssTokens.Hash:
                    return Match(CssTokens.Hash, s => new IdSpecifier(s.String));
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

        private Specifier Pseudo()
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
                    return Match(CssTokens.Identifier, s => new IdentifierPseudoSpecifier(s.String));
                }
                else if (_scanner.Current == CssTokens.Function)
                {
                    string function, argument = null;

                    function = Match(CssTokens.Function, s => s.String);
                    nesting++;

                    SkipWhitespace();
                    if (_scanner.Current == CssTokens.Identifier)
                    {
                        argument = Match(CssTokens.Identifier, s => s.String);
                        SkipWhitespace();
                    }
                    Match(CssTokens.CloseParen);
                    nesting--;

                    return new FunctionPseudoSpecifier(function, argument);
                }
                else
                {
                    throw new CssParsingException();
                }
            }
            catch (CssParsingException)
            {
                if (nesting > 0)
                    SkipParen(nesting);
                throw;
            }
        }

        private Specifier Attribute()
        {
            int nesting = 0;
            try
            {
                //attrib
                //  : '[' S* IDENT S* [ [ '=' | INCLUDES | DASHMATCH ] S*
                //    [ IDENT | STRING ] S* ]? ']'
                //  ;

                string name;
                AttributeFilter filter = null;

                Match(CssTokens.OpenBracket);
                nesting++;

                SkipWhitespace();
                name = Match(CssTokens.Identifier, s => s.String);
                SkipWhitespace();

                if (_scanner.Current == CssTokens.Equals || _scanner.Current == CssTokens.Includes || _scanner.Current == CssTokens.DashMatch)
                {
                    FilterOperator op;
                    string value;

                    switch (_scanner.Current)
                    {
                        case CssTokens.Equals:
                            op = FilterOperator.Equals;
                            break;
                        case CssTokens.Includes:
                            op = FilterOperator.Includes;
                            break;
                        case CssTokens.DashMatch:
                            op = FilterOperator.DashMatch;
                            break;
                        default:
                            throw new CssParsingException();
                    }
                    _scanner.MoveNext();

                    SkipWhitespace();

                    if (_scanner.Current == CssTokens.Identifier || _scanner.Current == CssTokens.String)
                    {
                        value = _scanner.Value.String;
                        _scanner.MoveNext();
                    }
                    else
                    {
                        throw new CssParsingException();
                    }

                    SkipWhitespace();

                    filter = new AttributeFilter(op, value);
                }

                Match(CssTokens.CloseBracket);
                nesting--;

                return new AttributeSpecifier(name, filter);
            }
            catch (CssParsingException)
            {
                if (nesting > 0)
                    SkipBracket(nesting);
                throw;
            }
        }

        private ClassSpecifier Class()
        {
            Match(CssTokens.Dot);
            return Match(CssTokens.Identifier, s => new ClassSpecifier(s.String));
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

        private ElementName ElementName()
        {
            switch (_scanner.Current)
            {
                case CssTokens.Identifier:
                    return Match(CssTokens.Identifier, s => new IdentifierElementName(s.String));
                case CssTokens.Star:
                    return Match(CssTokens.Star, s => StarElementName.Instance);
                default:
                    throw new CssParsingException();
            }
        }

        private Combinator Combinator()
        {
            //combinator
            //  : '+' S*
            //  | '>' S*
            //  ;

            Combinator result;

            switch (_scanner.Current)
            {
                case CssTokens.Plus:
                    result = Match(CssTokens.Plus, s => Css.Combinator.Sibling);
                    break;
                case CssTokens.More:
                    result = Match(CssTokens.Plus, s => Css.Combinator.Adjacent);
                    break;
                default:
                    throw new CssParsingException();
            }
            SkipWhitespace();
            return result;
        }

        private bool MatchesSelectorStart()
        {
            //selector
            //  : simple_selector [ combinator selector | S+ [ combinator? selector ]? ]?
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

        private Media Media()
        {
            int nesting = 0;

            try
            {
                //media
                //  : MEDIA_SYM S* media_list LBRACE S* ruleset* '}' S*
                //  ;

                string[] mediaList;
                List<Ruleset> rules = new List<Ruleset>();

                Match(CssTokens.AtMedia);
                SkipWhitespace();
                mediaList = MediaList();
                Match(CssTokens.OpenBrace);
                nesting++;

                SkipWhitespace();
                while (_scanner.Current != CssTokens.CloseBrace && _scanner.Current != CssTokens.EOF)
                {
                    if (_scanner.Current == CssTokens.Identifier || _scanner.Current == CssTokens.Star)
                    {
                        Ruleset ruleset = Ruleset();
                        if (ruleset != null)
                            rules.Add(ruleset);
                    }
                    else
                    {
                        // error skip till ';' or {}
                        SkipSemicolonOrBlock(true, true, nesting);
                        SkipWhitespace();
                    }
                }

                MatchEof(CssTokens.CloseBrace);
                nesting--;

                return new Media(mediaList, rules.ToArray());
            }
            catch (CssParsingException)
            {
                SkipSemicolonOrEndBlock(nesting);
                SkipWhitespace();
            }

            return null;
        }

        private Page Page()
        {
            int nesting = 0;
            try
            {
                //page
                //  : PAGE_SYM S* pseudo_page?
                //    '{' S* declaration? [ ';' S* declaration? ]* '}' S*
                //  ;

                string pseudo = null;
                List<Declaration> decls = new List<Declaration>();

                Match(CssTokens.AtPage);
                SkipWhitespace();

                if (_scanner.Current == CssTokens.Colon)
                    pseudo = PseudoPage();

                Match(CssTokens.OpenBrace);
                nesting++;

                SkipWhitespace();

                MaybeDeclaration(decls);

                while (_scanner.Current == CssTokens.SemiColon)
                {
                    Match(CssTokens.SemiColon);
                    SkipWhitespace();
                    MaybeDeclaration(decls);
                }

                MatchEof(CssTokens.CloseBrace);
                nesting--;
                SkipWhitespace();

                return new Page(pseudo, decls.ToArray());
            }
            catch (CssParsingException)
            {
                SkipSemicolonOrEndBlock(nesting);
                SkipWhitespace();
            }

            return null;
        }

        private Declaration Declaration()
        {
            try
            {
                //declaration
                //  : property ':' S* expr prio?
                //  ;
                // must handle error till ';'

                string property;
                Expression value;
                bool important = false;

                property = Property();
                Match(CssTokens.Colon);
                SkipWhitespace();
                value = Expression();
                if (_scanner.Current == CssTokens.Important)
                    important = Priority();

                return new Declaration(property, value, important);
            }
            catch (CssParsingException)
            {
                SkipSemicolonOrBlock(false, true);
                SkipWhitespace();
            }
            return null;
        }

        private Expression Expression()
        {
            // if function == true, allow end with ')' or EOF
            // otherwise with ';' or '}' or !important or EOF

            //expr
            //  : term [ operator? term ]*
            //  ;

            List<Term> terms = new List<Term>();

            Term single;

            single = Term();
            terms.Add(single);

            while (true)
            {
                if (_scanner.Current == CssTokens.Slash || _scanner.Current == CssTokens.Comma)
                {
                    single = Operator();
                    terms.Add(single);

                    single = Term();
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

            return new Expression(terms.ToArray());
        }

        private OperatorTerm Operator()
        {
            Operator op;

            switch (_scanner.Current)
            {
                case CssTokens.Slash:
                    op = Match(CssTokens.Slash, s => Css.Operator.Slash);
                    break;
                case CssTokens.Comma:
                    op = Match(CssTokens.Comma, s => Css.Operator.Comma);
                    break;
                default:
                    throw new CssParsingException();
            }

            SkipWhitespace();
            return new OperatorTerm(op);
        }

        private Term Term()
        {
            //term
            //  : unary_operator?
            //    [ NUMBER S* | PERCENTAGE S* | LENGTH S* | EMS S* | EXS S* | ANGLE S* |
            //      TIME S* | FREQ S* ]
            //  | STRING S* | IDENT S* | URI S* | hexcolor | function
            //  ;

            Term result = null;
            int sign = 1;

            switch (_scanner.Current)
            {
                case CssTokens.Plus:
                case CssTokens.Minus:
                    sign = UnaryOperator();
                    result = Dimension(sign);
                    SkipWhitespace();
                    break;
                case CssTokens.Number:
                case CssTokens.Percentage:
                case CssTokens.Length:
                case CssTokens.Ems:
                case CssTokens.Exs:
                case CssTokens.Angle:
                case CssTokens.Time:
                case CssTokens.Frequency:
                    result = Dimension(sign);
                    SkipWhitespace();
                    break;
                case CssTokens.String:
                    result = Match(CssTokens.String, s => new StringTerm(s.String));
                    SkipWhitespace();
                    break;
                case CssTokens.Identifier:
                    result = Match(CssTokens.Identifier, s => new IdentifierTerm(s.String));
                    SkipWhitespace();
                    break;
                case CssTokens.Uri:
                    result = Match(CssTokens.Uri, s => new UriTerm(s.String));
                    SkipWhitespace();
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

        private FunctionTerm Function()
        {
            int nesting = 0;
            try
            {
                //function
                //  : FUNCTION S* expr ')' S*
                //  ;

                string name;
                Expression arguments;

                name = Match(CssTokens.Function, s => s.String);
                nesting++;

                SkipWhitespace();
                arguments = Expression();
                MatchEof(CssTokens.CloseParen);
                nesting--;

                SkipWhitespace();

                return new FunctionTerm(name, arguments);
            }
            catch (CssParsingException)
            {
                if (nesting > 0)
                    SkipParen(nesting);
                throw;
            }
        }

        private HexColorTerm HexColor()
        {
            string result;

            result = Match(CssTokens.Hash, s => s.String);
            SkipWhitespace();

            return new HexColorTerm(result);
        }

        private DimensionTerm Dimension(int sign)
        {
            Dimension result;

            switch (_scanner.Current)
            {
                case CssTokens.Number:
                    result = Match(CssTokens.Number, s => new NumberDimension(s.Double));
                    break;
                case CssTokens.Percentage:
                    result = Match(CssTokens.Percentage, s => s.Dimension);
                    break;
                case CssTokens.Length:
                    result = Match(CssTokens.Length, s => s.Dimension);
                    break;
                case CssTokens.Ems:
                    result = Match(CssTokens.Ems, s => s.Dimension);
                    break;
                case CssTokens.Exs:
                    result = Match(CssTokens.Exs, s => s.Dimension);
                    break;
                case CssTokens.Angle:
                    result = Match(CssTokens.Angle, s => s.Dimension);
                    break;
                case CssTokens.Time:
                    result = Match(CssTokens.Time, s => s.Dimension);
                    break;
                case CssTokens.Frequency:
                    result = Match(CssTokens.Frequency, s => s.Dimension);
                    break;
                case CssTokens.Dimension:
                    result = Match(CssTokens.Dimension, s => s.Dimension);
                    break;
                default:
                    throw new CssParsingException();
            }
            SkipWhitespace();

            return new DimensionTerm(sign, result);
        }

        private int UnaryOperator()
        {
            if (_scanner.Current == CssTokens.Plus)
            {
                return Match(CssTokens.Plus, s => 1);
            }
            else if (_scanner.Current == CssTokens.Minus)
            {
                return Match(CssTokens.Minus, s => -1);
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
                case CssTokens.Ems:
                case CssTokens.Exs:
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
            SkipWhitespace();

            return true;
        }

        private string Property()
        {
            //property
            //  : IDENT S*
            //  ;

            string result;

            result = Match(CssTokens.Identifier, s => s.String);
            SkipWhitespace();

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
            SkipWhitespace();

            return result;
        }

        private Import Import()
        {
            try
            {
                // import
                //    :   IMPORT_SYM S* [STRING|URI] S* media_list? ';' S*
                //    ;
                string import;
                string[] mediaList = EmptyStringArray;

                Match(CssTokens.AtImport);
                SkipWhitespace();

                if (_scanner.Current == CssTokens.String)
                    import = Match(CssTokens.String, s => s.String);
                else if (_scanner.Current == CssTokens.Uri)
                    import = Match(CssTokens.Uri, s => s.String);
                else
                    throw new CssParsingException();

                SkipWhitespace();

                if (_scanner.Current == CssTokens.Identifier)
                    mediaList = MediaList();
                MatchEof(CssTokens.SemiColon);

                return new Import(import, mediaList);
            }
            catch (CssParsingException)
            {
                SkipSemicolonOrBlock(true, true);
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
                SkipWhitespace();

                single = Medium();
                result.Add(single);
            }
            return result.ToArray();
        }

        private string Medium()
        {
            string result;
            result = Match(CssTokens.Identifier, s => s.String);
            SkipWhitespace();
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
                SkipSemicolonOrBlock(true, true);
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

        private void SkipWhitespace()
        {
            while (_scanner.Current == CssTokens.Whitespace)
                _scanner.MoveNext();
        }

        private void SkipSgml()
        {
            while (_scanner.Current == CssTokens.Whitespace || _scanner.Current == CssTokens.Cdc || _scanner.Current == CssTokens.Cdo)
                _scanner.MoveNext();
        }

        private bool SkipBatch(Func<bool> until, bool strict, int nestingBrace = 0, int nestingBracket = 0, int nestingParen = 0)
        {
            int startingBraceNesting = nestingBrace;

            while (!until() || (nestingBrace > startingBraceNesting) || (nestingBracket + nestingParen) > 0)
            {
                switch (_scanner.Current)
                {
                    case CssTokens.EOF:
                        return true;
                    case CssTokens.OpenBrace:
                        nestingBrace++;
                        break;
                    case CssTokens.CloseBrace:
                        nestingBrace--;
                        if (nestingBrace < 0)
                            nestingBrace = 0;
                        break;
                    case CssTokens.OpenBracket:
                        nestingBracket++;
                        break;
                    case CssTokens.CloseBracket:
                        nestingBracket--;
                        if (nestingBracket < 0)
                            nestingBracket = 0;
                        break;
                    case CssTokens.OpenParen:
                        nestingParen++;
                        break;
                    case CssTokens.CloseParen:
                        nestingParen--;
                        if (nestingParen < 0)
                            nestingParen = 0;
                        break;
                }

                if (!strict || startingBraceNesting == 0)
                    if (until() && (nestingBracket + nestingParen) <= 0 && (nestingBrace <= startingBraceNesting))
                        return false;

                _scanner.MoveNext();
            }

            return _scanner.Current == CssTokens.CloseBrace;
        }

        private void SkipParen(int nesting)
        {
            SkipBatch(() => _scanner.Current == CssTokens.CloseParen, true, nestingParen : nesting);
        }

        private void SkipSemicolonOrEndBlock(int nesting)
        {
            SkipBatch(() => _scanner.Current == CssTokens.CloseBrace || _scanner.Current == CssTokens.SemiColon, true, nestingBrace : nesting);

            _scanner.MoveNext();
        }

        private void SkipEndBlock(int nesting)
        {
            SkipBatch(() => _scanner.Current == CssTokens.CloseBrace, true, nestingBrace : nesting);

            _scanner.MoveNext();
        }

        private void SkipBracket(int nesting)
        {
            SkipBatch(() => _scanner.Current == CssTokens.CloseBracket, true, nestingBracket : nesting);
        }

        /// <summary>
        /// Skips till ';' (and skips if eatSemicolon is true) or '{...}' (and skips if eatClosingBrace is true) or '...}' (never skips the end of current block)
        /// </summary>
        private void SkipSemicolonOrBlock(bool eatSemicolon, bool eatClosingBrace, int nesting = 0)
        {
            bool endOfCurrent = SkipBatch(() => _scanner.Current == CssTokens.SemiColon || _scanner.Current == CssTokens.CloseBrace, false, nestingBrace : nesting);
            if (!endOfCurrent)
            {
                if (eatSemicolon && _scanner.Current == CssTokens.SemiColon)
                    _scanner.MoveNext();
                else if (eatClosingBrace && _scanner.Current == CssTokens.CloseBrace)
                    _scanner.MoveNext();
            }
        }
    }

    public class CssParsingException: Exception
    {
        public CssParsingException()
        {
        }
    }

    public class TokenBuffer
    {
        private CssScanner _scanner;
        private CssTokens _current;

        public TokenBuffer(CssScanner scanner)
        {
            _scanner = scanner;
        }

        public CssTokens Current
        {
            get { return _current; }
        }

        public CssSourceToken Value
        {
            get { return _scanner.Value; }
        }

        public void MoveNext()
        {
            _current = _scanner.NextToken();
        }
    }
}
