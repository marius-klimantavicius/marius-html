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

namespace Marius.Html.Css.Parser
{
    public class CssTokenizer
    {
        private const int UnicodeEscapeMaxLength = 6;

        private InputSource _source;
        private string _value;
        private int _start, _end;

        public string Value
        {
            get
            {
                if (_value == null) _value = _source.Value(_start, _end);
                return _value;
            }
        }

        private char Current
        {
            get
            {
                return _source.Current;
            }
        }

        public InputSource Peek { get { return _source; } }

        public int Position { get { return _source.Position; } }

        public CssTokenizer(string source, int index)
        {
            _source = new InputSource(source, index);
        }

        public void PushState()
        {
            _source.PushState();
        }

        public void PopState(bool discard)
        {
            _source.PopState(discard);
        }

        private void Skip(int count)
        {
            _source.Skip(count);
        }

        private void MoveNext()
        {
            Skip(1);
        }

        public CssTokens NextToken()
        {
            CssTokens token;

            do
            {
                _value = null;
                _start = Position;
                token = Next(false);
                _end = Position;

            } while (token == CssTokens.Comment);

            return token;
        }

        public CssTokens NextTokenOrUrl()
        {
            CssTokens token;

            do
            {
                _value = null;
                _start = Position;
                token = Next(true);
                _end = Position;

            } while (token == CssTokens.Comment);

            return token;
        }

        private CssTokens Next(bool maybeUrl)
        {
            if (_source.Eof)
                return CssTokens.EOF;

            if (maybeUrl && MatchesUrl())
            {
                while (MatchesUrl())
                {
                    if (MatchesEscape())
                        SkipEscape(false);
                    MoveNext();
                }
                return CssTokens.SurogateUrl;
            }

            switch (Current)
            {
                case '\n':
                case '\r':
                case '\t':
                case '\f':
                case ' ':
                    return Whitespace();
                case '/':
                    return Comment();
                case '<':
                    return Cdo();
                case '-':
                    return Cdc();
                case '~':
                    return Includes();
                case '|':
                    return DashMatch();
                case '"':
                    return StringDouble();
                case '\'':
                    return StringSingle();
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                case '.':
                    return Number();
                case '#':
                    return Hash();
                case '@':
                    return AtKeyword();
                case '!':
                    return Exclamation();
                case '\\':
                    return Identifier(true);
                default:
                    //[_a-z]|{nonascii}
                    if (MatchesIdentifierStart())
                        return Identifier(true);
                    else
                        return SingleCharacter();
            }
        }

        private CssTokens SingleCharacter()
        {
            CssTokens result = CssTokens.Unknown;

            switch (Current)
            {
                case ';':
                    result = CssTokens.SemiColon;
                    break;
                case '{':
                    result = CssTokens.OpenBrace;
                    break;
                case '}':
                    result = CssTokens.CloseBrace;
                    break;
                case ':':
                    result = CssTokens.Colon;
                    break;
                case '/':
                    result = CssTokens.Slash;
                    break;
                case '\\':
                    result = CssTokens.Backslash;
                    break;
                case ',':
                    result = CssTokens.Comma;
                    break;
                case '+':
                    result = CssTokens.Plus;
                    break;
                case '-':
                    result = CssTokens.Minus;
                    break;
                case '>':
                    result = CssTokens.More;
                    break;
                case '<':
                    result = CssTokens.Less;
                    break;
                case '~':
                    result = CssTokens.Tilde;
                    break;
                case '.':
                    result = CssTokens.Dot;
                    break;
                case '*':
                    result = CssTokens.Star;
                    break;
                case '[':
                    result = CssTokens.OpenBracket;
                    break;
                case ']':
                    result = CssTokens.CloseBracket;
                    break;
                case '(':
                    result = CssTokens.OpenParen;
                    break;
                case ')':
                    result = CssTokens.CloseParen;
                    break;
                case '=':
                    result = CssTokens.Equals;
                    break;
            }

            MoveNext();
            return result;
        }

        private CssTokens Identifier(bool allowFunction = false)
        {
            if (Current == '\\')
            {
                if (!MatchesEscape())
                {
                    MoveNext();
                    return CssTokens.Backslash;
                }

                SkipEscape(false);
            }

            MoveNext();
            while (MatchesNameChar())
            {
                if (MatchesEscape())
                    SkipEscape(false);

                MoveNext();
            }

            if (allowFunction && Current == '(')
            {
                MoveNext();
                return CssTokens.Function;
            }

            return CssTokens.Identifier;
        }

        private CssTokens Exclamation()
        {
            MoveNext();

            // CssScanner will handle exclamation keyword
            return CssTokens.SurogateExclamation;
        }

        private CssTokens AtKeyword()
        {
            MoveNext();

            if (MatchesIdentifierStart())
            {
                Identifier();
                return CssTokens.AtKeyword;
            }

            return CssTokens.Unknown;
        }

        private CssTokens Hash()
        {
            MoveNext();

            if (MatchesNameChar())
            {
                SkipName();
                return CssTokens.Hash;
            }

            return CssTokens.Unknown;
        }

        private CssTokens Number()
        {
            // [0-9]+|[0-9]*\.[0-9]+
            if (Current == '.' && !(Peek[0] >= '0' && Peek[0] <= '9'))
            {
                MoveNext();
                return CssTokens.Dot;
            }

            while (Current >= '0' && Current <= '9')
                MoveNext();

            if (Current == '.')
            {
                if (!(Peek[0] >= '0' && Peek[0] <= '9'))
                    return CssTokens.Number;

                MoveNext();
                while (Current >= '0' && Current <= '9')
                    MoveNext();
            }

            if (Current == '%')
                return CssTokens.Percentage;

            if (MatchesIdentifierStart())
            {
                Identifier();
                return CssTokens.Dimension;
            }

            return CssTokens.Number;
        }

        private CssTokens StringSingle()
        {
            // \'([^\n\r\f\\']|\\{nl}|{escape})*\'
            while (true)
            {
                MoveNext();
                // must close all strings at the end of stylesheet
                if (_source.Eof)
                    return CssTokens.String;

                switch (Current)
                {
                    case '\r':
                    case '\n':
                    case '\f':
                        return CssTokens.Invalid;
                    case '\'':
                        MoveNext();
                        return CssTokens.String;
                    case '\\':
                        SkipEscape(true);
                        break;
                }
            }
        }

        private CssTokens StringDouble()
        {
            //\"([^\n\r\f\\"]|\\{nl}|{escape})*\"
            while (true)
            {
                MoveNext();
                // must close all strings at the end of stylesheet
                if (_source.Eof)
                    return CssTokens.String;

                switch (Current)
                {
                    case '\r':
                    case '\n':
                    case '\f':
                        return CssTokens.Invalid;
                    case '"':
                        MoveNext();
                        return CssTokens.String;
                    case '\\':
                        SkipEscape(true);
                        break;
                }
            }
        }

        private CssTokens DashMatch()
        {
            MoveNext();
            if (Current == '=')
            {
                MoveNext();
                return CssTokens.DashMatch;
            }

            return CssTokens.Unknown;
        }

        private CssTokens Includes()
        {
            MoveNext();
            if (Current == '=')
            {
                MoveNext();
                return CssTokens.Includes;
            }

            return CssTokens.Tilde;
        }

        private CssTokens Cdc()
        {
            // -->
            MoveNext();
            if (Current == '-' && Peek[0] == '>')
            {
                Skip(2);
                return CssTokens.Cdc;
            }
            else if (Current != '-' && MatchesIdentifierStart())
            {
                return Identifier();
            }

            return CssTokens.Minus;
        }

        private CssTokens Cdo()
        {
            // <!--
            MoveNext(); // skip <
            if (Current == '!' && Peek[0] == '-' && Peek[1] == '-')
            {
                Skip(3);
                return CssTokens.Cdo;
            }
            return CssTokens.Less;
        }

        private CssTokens Whitespace()
        {
            while (true)
            {
                MoveNext();
                switch (Current)
                {
                    case '\n':
                    case '\r':
                    case '\t':
                    case '\f':
                    case ' ':
                        break;
                    default:
                        return CssTokens.Whitespace;
                }
            }
        }

        private CssTokens Comment()
        {
            // we are at '/'
            MoveNext();

            if (Current == '*')
            {
                bool seenStar = false;
                while (!_source.Eof)
                {
                    MoveNext();
                    switch (Current)
                    {
                        case '*':
                            seenStar = true;
                            break;
                        case '/':
                            if (seenStar)
                            {
                                MoveNext();
                                return CssTokens.Comment;
                            }
                            goto default;
                        default:
                            seenStar = false;
                            break;
                    }
                }
                return CssTokens.Comment;
            }

            return CssTokens.Slash;
        }

        private bool MatchesUrl()
        {
            // ([!#$%&*-~]|{nonascii}|{escape})*
            switch (Current)
            {
                case '!':
                case '#':
                case '$':
                case '%':
                case '&':
                    return true;
            }

            if (Current >= '*' && Current <= '~')
                return true;

            if ((int)Current >= 0x80)
                return true;

            if (MatchesEscape())
                return true;

            return false;
        }

        private bool MatchesNameChar()
        {
            //[_a-z0-9-]|{nonascii}|{escape}
            var c = Char.ToLowerInvariant(Current);
            if (Current == '-' || Current == '_' || (c >= 'a' && c <= 'z') || (c >= '0' && c <= '9'))
                return true;

            if ((int)Current >= 0x80)
                return true;

            if (MatchesEscape())
                return true;

            return false;
        }

        private bool MatchesIdentifierStart()
        {
            if (Current == '-' || Current == '_' || (char.ToLowerInvariant(Current) >= 'a' && char.ToLowerInvariant(Current) <= 'z')
                || ((int)Current > 0x80))
                return true;
            if (Current == '\\' && MatchesEscape())
                return true;
            return false;
        }

        private bool MatchesEscape()
        {
            if (Current != '\\')
                return false;
            /*
            private const string kUnicode = @"\\{hex}{1,6}(\r\n|[ \t\r\n\f])?";
            private const string kEscape = @"{unicode}|\\[^\n\r\f\0-9a-f]";
            */

            switch (Peek[0])
            {
                case '\n':
                case '\r':
                case '\f':
                    return false;
            }
            return true;
        }

        private void SkipName()
        {
            // NOTE: Normally Skip* stops at LAST valid position,
            // SkipName skips it ALL (Current is next after name)
            while (MatchesNameChar())
                MoveNext();
        }

        private void SkipEscape(bool inString)
        {
            Contract.Assume(Current == '\\');

            var c = char.ToLowerInvariant(Peek[0]);
            if ((c >= '0' && c <= '9') || (c >= 'a' && c <= 'f'))
                SkipUnicodeEscape();
            else if (inString && Peek[0] == '\r')
            {
                MoveNext();
                if (Peek[0] == '\n')
                    MoveNext();
            }
            else if (inString && (Peek[0] == '\n' || Peek[0] == '\f'))
            {
                MoveNext();
            }
            else
            {
                MoveNext();
            }
        }

        private void SkipUnicodeEscape()
        {
            for (int i = 0; i < UnicodeEscapeMaxLength; i++)
            {
                var c = char.ToLowerInvariant(Peek[0]);
                if (!(c >= '0' && c <= '9') && !(c >= 'a' && c <= 'f'))
                    break;
                MoveNext();
            }

            // optional whitespace
            switch (Peek[0])
            {
                case '\r':
                    if (Peek[1] == '\n')
                        MoveNext();
                    goto case '\n';
                case '\n':
                case '\t':
                case '\f':
                case ' ':
                    MoveNext();
                    break;
            }
        }
    }
}
