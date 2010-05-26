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
    public class Stylesheet
    {
        public string Charset { get; private set; }
        public Import[] Imports { get; private set; }
        public Rule[] Rules { get; private set; }

        public Stylesheet(string charset, Import[] imports, Rule[] rules)
        {
            Contract.Requires(imports != null);
            Contract.Requires(rules != null);

            Charset = charset;
            Imports = imports;
            Rules = rules;
        }

        public static Stylesheet Parse(string source)
        {
            CssScanner scanner = new CssScanner();
            scanner.SetSource(source, 0);

            CssParser parser = new CssParser(scanner);
            return parser.Parse();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (Charset != null)
                sb.Append("@charset ").Append(Charset).AppendLine(";");

            for (int i = 0; i < Imports.Length; i++)
            {
                sb.AppendLine(Imports[i].ToString());
            }

            for (int i = 0; i < Rules.Length; i++)
            {
                sb.AppendLine(Rules[i].ToString());
            }

            return sb.ToString();
        }
    }

    public class Import
    {
        public string Uri { get; private set; }
        public string[] MediaList { get; private set; }

        public Import(string uri, string[] mediaList)
        {
            Uri = uri;
            MediaList = mediaList;
        }

        public override string ToString()
        {
            return string.Format("@import \"{0}\" {1};", Uri.Escape(), String.Join(", ", MediaList));
        }
    }

    public abstract class Rule
    {
        public abstract RuleType RuleType { get; }
    }

    public enum RuleType
    {
        Media,
        Page,
        Ruleset,
    }

    public class Media: Rule
    {
        public string[] MediaList { get; private set; }
        public Ruleset[] Ruleset { get; private set; }

        public sealed override RuleType RuleType
        {
            get { return RuleType.Media; }
        }

        public Media(string[] mediaList, Ruleset[] ruleset)
        {
            MediaList = mediaList;
            Ruleset = ruleset;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("@media {0}", String.Join(", ", MediaList));
            sb.AppendLine();
            sb.AppendLine("{");

            for (int i = 0; i < Ruleset.Length; i++)
            {
                sb.AppendLine(Ruleset[i].ToString());
            }

            sb.AppendLine("}");

            return sb.ToString();
        }
    }

    public class Page: Rule
    {
        public string PseudoPage { get; private set; }
        public Declaration[] Declarations { get; private set; }

        public sealed override RuleType RuleType
        {
            get { return RuleType.Page; }
        }

        public Page(string pseudoPage, Declaration[] declarations)
        {
            PseudoPage = pseudoPage;
            Declarations = declarations;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("@page");
            if (PseudoPage != null)
                sb.Append(":").Append(PseudoPage);
            sb.AppendLine();
            sb.AppendLine("{");

            sb.AppendLine(String.Join(";" + Environment.NewLine, (object[])Declarations));
            sb.AppendLine("}");

            return sb.ToString();
        }
    }

    public class Ruleset: Rule
    {
        public Selector[] Selectors { get; private set; }
        public Declaration[] Declarations { get; private set; }

        public sealed override RuleType RuleType
        {
            get { return RuleType.Ruleset; }
        }

        public Ruleset(Selector[] selectors, Declaration[] declarations)
        {
            Selectors = selectors;
            Declarations = declarations;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(String.Join(", ", (object[])Selectors));

            sb.AppendLine("{");

            sb.AppendLine(String.Join(";" + Environment.NewLine, (object[])Declarations));

            sb.AppendLine("}");

            return sb.ToString();
        }
    }

    public class Declaration
    {
        public string Property { get; private set; }
        public Expression Value { get; private set; }
        public bool Important { get; private set; }

        public Declaration(string property, Expression value, bool important)
        {
            Property = property;
            Value = value;
            Important = important;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Property).Append(": ").Append(Value.ToString());
            if (Important)
                sb.Append(" !important");
            return sb.ToString();
        }
    }

    public class Expression
    {
        public Term[] Terms { get; private set; }

        public Expression(Term[] terms)
        {
            Terms = terms;
        }

        public override string ToString()
        {
            return string.Join(" ", (object[])Terms);
        }
    }

    public abstract class Term
    {
        public abstract TermType Type { get; }
    }

    public enum TermType
    {
        Operator,
        AngleDimension,
        LengthDimension,
        TimeDimension,
        FrequencyDimension,
        PercentageDimension,
        EmsDimension,
        ExsDimension,
        NumberDimension,
        String,
        Identifier,
        Uri,
        HexColor,
        Function,
        Unknown
    }

    public class OperatorTerm: Term
    {
        public Operator Operator { get; private set; }

        public OperatorTerm(Operator op)
        {
            Operator = op;
        }

        public sealed override TermType Type
        {
            get { return TermType.Operator; }
        }

        public override string ToString()
        {
            switch (Operator)
            {
                case Operator.Comma:
                    return ",";
                case Operator.Slash:
                    return "/";
                default:
                    return "?";
            }
        }
    }

    public enum Operator
    {
        Comma,
        Slash,
    }

    public enum UnaryOperator
    {
        Plus,
        Minus,
    }

    public class DimensionTerm: Term
    {
        public int Sign { get; private set; }
        public Dimension Dimension { get; private set; }

        public DimensionTerm(int sign, Dimension dimension)
        {
            Sign = sign;
            Dimension = dimension;
        }

        public sealed override TermType Type
        {
            get
            {
                switch (Dimension.Type)
                {
                    case DimensionType.Length:
                        return TermType.LengthDimension;
                    case DimensionType.Ems:
                        return TermType.EmsDimension;
                    case DimensionType.Exs:
                        return TermType.ExsDimension;
                    case DimensionType.Angle:
                        return TermType.AngleDimension;
                    case DimensionType.Time:
                        return TermType.TimeDimension;
                    case DimensionType.Frequency:
                        return TermType.FrequencyDimension;
                    case DimensionType.Percentage:
                        return TermType.PercentageDimension;
                    case DimensionType.Number:
                        return TermType.NumberDimension;
                    default:
                        return TermType.Unknown;
                }
            }
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Sign < 0 ? "-1" : "", Dimension.ToString());
        }
    }

    public class StringTerm: Term
    {
        public string Value { get; private set; }

        public StringTerm(string value)
        {
            Value = value;
        }

        public sealed override TermType Type
        {
            get { return TermType.String; }
        }

        public override string ToString()
        {
            return "\"" + Value.Escape() + "\"";
        }
    }

    public class IdentifierTerm: Term
    {
        public string Name { get; private set; }

        public IdentifierTerm(string name)
        {
            Name = name;
        }

        public sealed override TermType Type
        {
            get { return TermType.Identifier; }
        }

        public override string ToString()
        {
            return Name.EscapeIdentifier();
        }
    }

    public class UriTerm: Term
    {
        public string Uri { get; private set; }

        public UriTerm(string uri)
        {
            Uri = uri;
        }

        public sealed override TermType Type
        {
            get { return TermType.Uri; }
        }

        public override string ToString()
        {
            return string.Format("@url(\"{0}\")", Uri.Escape());
        }
    }

    public class HexColorTerm: Term
    {
        public string Value { get; private set; }

        public HexColorTerm(string value)
        {
            Value = value;
        }

        public sealed override TermType Type
        {
            get { return TermType.HexColor; }
        }

        public override string ToString()
        {
            return string.Format("#{0}", Value.EscapeIdentifier());
        }
    }

    public class FunctionTerm: Term
    {
        public string Name { get; private set; }
        public Expression Arguments { get; private set; }

        public FunctionTerm(string name, Expression arguments)
        {
            Name = name;
            Arguments = arguments;
        }

        public sealed override TermType Type
        {
            get { return TermType.Function; }
        }

        public override string ToString()
        {
            return string.Format("{0}({1})", Name.EscapeIdentifier(), Arguments.ToString());
        }
    }

    public abstract class Selector
    {
    }

    public class SimpleSelector: Selector
    {
        public ElementName ElementName { get; private set; }
        public Specifier[] Specifiers { get; private set; }

        public SimpleSelector(ElementName name, Specifier[] specifiers)
        {
            ElementName = name;
            Specifiers = specifiers;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(ElementName.ToString());
            sb.Append(String.Join("", (object[])Specifiers));
            return sb.ToString();
        }
    }

    public abstract class ElementName
    {
    }

    public class IdentifierElementName: ElementName
    {
        public string Name { get; private set; }

        public IdentifierElementName(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return Name.EscapeIdentifier();
        }
    }

    public class StarElementName: ElementName
    {
        public static readonly StarElementName Instance = new StarElementName();

        private StarElementName()
        {
        }

        public override string ToString()
        {
            return "*";
        }
    }

    public class ComplexSelector: Selector
    {
        public SimpleSelector Selector { get; private set; }
        public Combinator Combinator { get; private set; }
        public Selector Combined { get; private set; }

        public ComplexSelector(SimpleSelector selector, Css.Combinator combinator, Css.Selector combined)
        {
            Selector = selector;
            Combinator = combinator;
            Combined = combined;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Selector.ToString()).Append(" ");

            if (Combinator == Combinator.Sibling)
                sb.Append("+ ");
            else if (Combinator == Combinator.Adjacent)
                sb.Append("> ");

            sb.Append(Combined.ToString());
            return sb.ToString();
        }
    }

    public enum Combinator
    {
        Sibling,    // s + s
        Adjacent,   // s > s
        Descendant, // s s
    }

    public abstract class Specifier
    {
        public abstract SpecifierType SpecifierType { get; }
    }

    public enum SpecifierType
    {
        Id,
        Class,
        Attribute,
        PseudoId,
        PseudoFunction,

    }

    public class IdSpecifier: Specifier
    {
        public string Id { get; private set; }

        public sealed override SpecifierType SpecifierType
        {
            get { return SpecifierType.Id; ; }
        }

        public IdSpecifier(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return string.Format("#{0}", Id.EscapeIdentifier());
        }
    }

    public class ClassSpecifier: Specifier
    {
        public string Name { get; private set; }

        public sealed override SpecifierType SpecifierType
        {
            get { return SpecifierType.Class; ; }
        }

        public ClassSpecifier(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return string.Format(".{0}", Name.EscapeIdentifier());
        }
    }

    public class AttributeSpecifier: Specifier
    {
        public string Name { get; private set; }
        public AttributeFilter Filter { get; private set; }

        public sealed override SpecifierType SpecifierType
        {
            get { return SpecifierType.Attribute; ; }
        }

        public AttributeSpecifier(string name, AttributeFilter filter)
        {
            Name = name;
            Filter = filter;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[").Append(Name.EscapeIdentifier());
            if (Filter != null)
                sb.Append(Filter);
            sb.Append("]");
            return sb.ToString();
        }
    }

    public enum FilterOperator
    {
        Equals,
        DashMatch,
        Includes,
    }

    public class AttributeFilter
    {
        public FilterOperator Operator { get; private set; }
        public string Value { get; private set; }

        public AttributeFilter(FilterOperator op, string value)
        {
            Operator = op;
            Value = value;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            switch (Operator)
            {
                case FilterOperator.Equals:
                    sb.Append("=");
                    break;
                case FilterOperator.DashMatch:
                    sb.Append("|=");
                    break;
                case FilterOperator.Includes:
                    sb.Append("~=");
                    break;
                default:
                    break;
            }
            sb.Append("\"").Append(Value.Escape()).Append("\"");
            return sb.ToString();
        }
    }

    public abstract class PseudoSpecifier: Specifier
    {
    }

    public class IdentifierPseudoSpecifier: PseudoSpecifier
    {
        public string Name { get; private set; }

        public sealed override SpecifierType SpecifierType
        {
            get { return SpecifierType.PseudoId; ; }
        }

        public IdentifierPseudoSpecifier(string name)
        {
            Name = name;
        }

        public override string ToString()
        {
            return string.Format(":{0}", Name.EscapeIdentifier());
        }
    }

    public class FunctionPseudoSpecifier: PseudoSpecifier
    {
        public string Function { get; private set; }
        public string Argument { get; private set; }    // according to spec only one Identifier argument is allowed

        public sealed override SpecifierType SpecifierType
        {
            get { return SpecifierType.PseudoFunction; ; }
        }

        public FunctionPseudoSpecifier(string function, string argument)
        {
            Function = function;
            Argument = argument;
        }

        public override string ToString()
        {
            return string.Format(":{0}({1})", Function.EscapeIdentifier(), (Argument ?? "").EscapeIdentifier());
        }
    }

    public static class StringUtils
    {
        public static string Escape(this string value)
        {
            // \"([^\n\r\f\\"]|\\{nl}|{escape})*\"
            StringBuilder sb = new StringBuilder(value);

            sb.Replace("\n", "\\A ");
            sb.Replace("\r", "\\D ");
            sb.Replace("\f", "\\C ");
            sb.Replace("\"", "\\\"");

            return sb.ToString();
        }

        public static string EscapeIdentifier(this string value)
        {
            //-?{nmstart}{nmchar}*
            //[_a-z]|{nonascii}|{escape}
            //[_a-z0-9-]|{nonascii}|{escape}

            StringBuilder sb = new StringBuilder();

            int start = 0;
            if (start < value.Length)
            {
                if (value[start] == '-')
                {
                    sb.Append(value[start]);
                    start++;
                }
            }

            if (start < value.Length)
            {
                if (value[start] == '_' || (Char.ToLowerInvariant(value[start]) >= 'a' && Char.ToLowerInvariant(value[start]) <= 'z') || (int)value[start] >= 0x80)
                    sb.Append(value[start]);
                else
                    sb.AppendFormat("\\{0} ", (Char.ConvertToUtf32(value, start)).ToString("X"));
                start++;
            }

            for (int i = start; i < value.Length; i++)
            {
                if (value[i] == '_' || value[i] == '-' || (Char.ToLowerInvariant(value[i]) >= 'a' && Char.ToLowerInvariant(value[i]) <= 'z') || (int)value[i] >= 0x80 || (value[i] >= '0' && value[i] <= '9'))
                    sb.Append(value[i]);
                else
                    sb.AppendFormat("\\{0} ", (Char.ConvertToUtf32(value, start)).ToString("X"));
            }

            return sb.ToString();
        }
    }
}
