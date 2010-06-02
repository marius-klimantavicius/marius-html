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
using Marius.Html.Css.Selectors;

namespace Marius.Html.Css.Parser
{
    public static class ParserHelper
    {
        public static bool SkipBatch(this TokenBuffer scanner, Func<bool> until, bool strict, int nestingBrace = 0, int nestingBracket = 0, int nestingParen = 0)
        {
            int startingBraceNesting = nestingBrace;

            while (!until() || (nestingBrace > startingBraceNesting) || (nestingBracket + nestingParen) > 0)
            {
                switch (scanner.Current)
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

                scanner.MoveNext();
            }

            return scanner.Current == CssTokens.CloseBrace;
        }

        public static void SkipParen(this TokenBuffer scanner, int nesting)
        {
            SkipBatch(scanner, () => scanner.Current == CssTokens.CloseParen, true, nestingParen : nesting);
        }

        public static void SkipSemicolonOrEndBlock(this TokenBuffer scanner, int nesting)
        {
            SkipBatch(scanner, () => scanner.Current == CssTokens.CloseBrace || scanner.Current == CssTokens.SemiColon, true, nestingBrace : nesting);

            scanner.MoveNext();
        }

        public static void SkipEndBlock(this TokenBuffer scanner, int nesting)
        {
            SkipBatch(scanner, () => scanner.Current == CssTokens.CloseBrace, true, nestingBrace : nesting);

            scanner.MoveNext();
        }

        public static void SkipBracket(this TokenBuffer scanner, int nesting)
        {
            SkipBatch(scanner, () => scanner.Current == CssTokens.CloseBracket, true, nestingBracket : nesting);
        }

        /// <summary>
        /// Skips till ';' (and skips if eatSemicolon is true) or '{...}' (and skips if eatClosingBrace is true) or '...}' (never skips the end of current block)
        /// </summary>
        public static void SkipSemicolonOrBlock(this TokenBuffer scanner, bool eatSemicolon, bool eatClosingBrace, int nesting = 0)
        {
            bool endOfCurrent = SkipBatch(scanner, () => scanner.Current == CssTokens.SemiColon || scanner.Current == CssTokens.CloseBrace, false, nestingBrace : nesting);
            if (!endOfCurrent)
            {
                if (eatSemicolon && scanner.Current == CssTokens.SemiColon)
                    scanner.MoveNext();
                else if (eatClosingBrace && scanner.Current == CssTokens.CloseBrace)
                    scanner.MoveNext();
            }
        }

        public static void SkipWhitespace(this TokenBuffer scanner)
        {
            while (scanner.Current == CssTokens.Whitespace)
                scanner.MoveNext();
        }

        public static void SkipSgml(this TokenBuffer scanner)
        {
            while (scanner.Current == CssTokens.Whitespace || scanner.Current == CssTokens.Cdc || scanner.Current == CssTokens.Cdo)
                scanner.MoveNext();
        }
    }
}
