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

namespace Marius.Html.Css.Parser
{
    public enum CssTokens
    {
        EOF,
        Whitespace,
        Comment,
        Cdo,
        Cdc,
        Includes,
        DashMatch,
        Invalid,
        String,
        Number,
        Percentage,
        Dimension,
        Hash,
        AtKeyword,
        ExclamationKeyword,
        Identifier,
        Function,
        AtMedia,
        AtPage,
        AtImport,
        AtCharset,
        Important,
        Length,
        Angle,
        Time,
        Frequency,
        Uri,
        Unknown,
        SurogateUrl,
        SurogateExclamation,
        
        SemiColon,
        OpenBrace,
        CloseBrace,
        Colon,
        Slash,      // /  
        Backslash,  // \
        Comma,
        Plus,
        Minus,
        More,       // >
        Less,       // <
        Tilde,      // ~
        Equals,
        Dot,
        Star,
        OpenBracket,
        CloseBracket,
        OpenParen,
        CloseParen,
    }
}
