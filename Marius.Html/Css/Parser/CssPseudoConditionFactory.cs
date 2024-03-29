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
using Marius.Html.Css.Dom;
using Marius.Html.Css.Parser;
using Marius.Html.Css.Selectors;

namespace Marius.Html.Css.Parser
{
    public class CssPseudoConditionFactory
    {
        public static readonly CssPseudoIdentifier FirstChild = new CssPseudoIdentifier("first-child");
        public static readonly CssPseudoIdentifier Link = new CssPseudoIdentifier("link");
        public static readonly CssPseudoIdentifier Visited = new CssPseudoIdentifier("visited");
        public static readonly CssPseudoIdentifier Hover = new CssPseudoIdentifier("hover");
        public static readonly CssPseudoIdentifier Active = new CssPseudoIdentifier("active");
        public static readonly CssPseudoIdentifier Focus = new CssPseudoIdentifier("focus");

        public static readonly CssPseudoIdentifier FirstLine = new CssPseudoIdentifier("first-line");
        public static readonly CssPseudoIdentifier FirstLetter = new CssPseudoIdentifier("first-letter");
        public static readonly CssPseudoIdentifier Before = new CssPseudoIdentifier("before");
        public static readonly CssPseudoIdentifier After = new CssPseudoIdentifier("after");

        public virtual CssCondition PseudoIdentifierCondition(string identifier)
        {
            CssPseudoValue condition = new CssPseudoIdentifier(identifier);

            switch (identifier.ToUpperInvariant())
            {
                case "FIRST-CHILD":
                case "LINK":
                case "VISITED":
                case "HOVER":
                case "ACTIVE":
                case "FOCUS":
                    return new CssPseudoClassCondition(condition);
                case "FIRST-LINE":
                case "FIRST-LETTER":
                case "BEFORE":
                case "AFTER":
                    return new CssPseudoElementCondition(condition);
            }

            throw new CssParsingException();
        }

        public virtual CssCondition PseudoFunctionCondition(string function, string argument)
        {
            CssPseudoValue condition = new CssPseudoFunction(function, argument);
            switch (function.ToUpperInvariant())
            {
                case "LANG":
                    return new CssPseudoClassCondition(condition);
            }

            throw new CssParsingException();
        }

        public virtual bool IsFirstLine(CssPseudoElementCondition condition)
        {
            return FirstLine.Equals(condition.Condition);
        }

        public virtual bool IsFirstLetter(CssPseudoElementCondition condition)
        {
            return FirstLetter.Equals(condition.Condition);
        }

        public virtual bool IsBefore(CssPseudoElementCondition condition)
        {
            return Before.Equals(condition.Condition);
        }

        public virtual bool IsAfter(CssPseudoElementCondition condition)
        {
            return After.Equals(condition.Condition);
        }
    }
}
