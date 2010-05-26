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
using System.Text.RegularExpressions;

namespace Marius.Html.Css
{
    public class CssScanner
    {
        private const string kNewLine = @"(\r\n|\n|\r|\f)";
        private const string kSingleWhitespace = @"([ \n\r\t\f])";
        private const string kWhitespace = @"([ \n\r\t\f]*)";

        private const string kNonAscii = @"[\u0080-\uFFFF]";

        private const string kHex = @"([0-9a-fA-F])";
        private const string kUnicodeEscape = @"(\\" + kHex + @"{1,6}(\r\n|" + kSingleWhitespace + @")?)";
        private const string kSimpleEscape = @"(\\[^\n\r\f0-9a-fA-F])";
        private const string kNewlineEscape = @"(\\" + kNewLine + @")";

        private const string kNumber = @"([0-9]+|[0-9]*\.[0-9]+)";
        private const string kNameStart = @"([_a-z]|" + kNonAscii + @"|" + kUnicodeEscape + @"|" + kSimpleEscape + @")";
        private const string kNameChar = @"([_a-z0-9-]|" + kNonAscii + @"|" + kUnicodeEscape + @"|" + kSimpleEscape + @")";
        private const string kIdentifier = @"(-?" + kNameStart + kNameChar + @"*)";
        private const string kDimension = @"((?<number>" + kNumber + @")(?<dimension>" + kIdentifier + @"))";

        private static readonly Regex UnicodeEscape = new Regex(kUnicodeEscape, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Multiline);
        private static readonly Regex SimpleEscape = new Regex(kSimpleEscape, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Multiline);
        private static readonly Regex NewlineEscape = new Regex(kNewlineEscape, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Multiline);
        private static readonly Regex DimensionRegex = new Regex(kDimension, RegexOptions.Compiled | RegexOptions.IgnoreCase | RegexOptions.ExplicitCapture | RegexOptions.Multiline);

        private CssTokenizer _tokenizer = new CssTokenizer();

        public CssSourceToken Value { get; private set; }

        public void SetSource(string source, int index)
        {
            _tokenizer.SetSource(source, index);
            Value = null;
        }

        public CssTokens NextToken()
        {
            CssTokens token;

            token = _tokenizer.NextToken();

            switch (token)
            {
                case CssTokens.Invalid:
                    return Invalid(_tokenizer.Value);
                case CssTokens.String:
                    return String(_tokenizer.Value);
                case CssTokens.Number:
                    return Number(_tokenizer.Value);
                case CssTokens.Percentage:
                    return Percentage(_tokenizer.Value);
                case CssTokens.Dimension:
                    return Dimension(_tokenizer.Value);
                case CssTokens.Hash:
                    return Hash(_tokenizer.Value);
                case CssTokens.AtKeyword:
                    return AtKeyword(_tokenizer.Value);
                case CssTokens.Identifier:
                    return Identifier(_tokenizer.Value);
                case CssTokens.Function:
                    return Function(_tokenizer.Value);
                case CssTokens.SurogateExclamation:
                    return ExclamationKeyword(_tokenizer.Value);
                case CssTokens.Comment:
                case CssTokens.SurogateUrl:
                case CssTokens.ExclamationKeyword:
                    throw new NotSupportedException("Token must not be returned from CssTokenizer");
                case CssTokens.Unknown:
                    return Token(token, _tokenizer.Value);
                default:
                    return Token(token);
            }
        }

        private CssTokens Number(string text)
        {
            return Token(CssTokens.Number, double.Parse(text));
        }

        private CssTokens String(string text)
        {
            text = UnescapeString(text);
            return Token(CssTokens.String, text);
        }

        private CssTokens Invalid(string text)
        {
            text = text.Substring(1);
            text = Unescape(text, true);
            return Token(CssTokens.Invalid, text);
        }

        private CssTokens Identifier(string text)
        {
            text = Unescape(text, false);
            return Token(CssTokens.Identifier, text);
        }

        private CssTokens Hash(string text)
        {
            text = Unescape(text.Substring(1), false);
            return Token(CssTokens.Hash, text);
        }

        private CssTokens AtKeyword(string text)
        {
            text = Unescape(text.Substring(1), false);
            switch (text.ToUpperInvariant())
            {
                case "IMPORT":
                    return Token(CssTokens.AtImport);
                case "PAGE":
                    return Token(CssTokens.AtPage);
                case "MEDIA":
                    return Token(CssTokens.AtMedia);
                case "CHARSET":                         //TODO: not according to spec, should be "@charset " is "@{ident=>'charset'}"
                    return Token(CssTokens.AtCharset);
                default:
                    return Token(CssTokens.AtKeyword, text);
            }
        }

        private CssTokens ExclamationKeyword(string text)
        {
            _tokenizer.PushState();

            CssTokens token;
            do
            {
                token = _tokenizer.NextToken();
            } while (token == CssTokens.Whitespace);

            if (token != CssTokens.Identifier)
            {
                _tokenizer.PopState(false);
                return Token(CssTokens.Unknown, text);
            }

            _tokenizer.PopState(true);

            string name = Unescape(_tokenizer.Value, false);
            switch (name.ToUpperInvariant())
            {
                case "IMPORTANT":
                    return Token(CssTokens.Important);
                default:
                    return Token(CssTokens.ExclamationKeyword, name);
            }
        }

        private CssTokens Dimension(string text)
        {
            var match = DimensionRegex.Match(text);
            var number = double.Parse(match.Groups["number"].Value);
            var dim = match.Groups["dimension"].Value;

            switch (dim.ToUpperInvariant())
            {
                case "EM":
                    return Token(CssTokens.Ems, new EmsDimension(number));
                case "EX":
                    return Token(CssTokens.Exs, new ExsDimension(number));
                case "PX":
                    return Token(CssTokens.Length, new LengthDimension(number, LengthUnits.Px));
                case "CM":
                    return Token(CssTokens.Length, new LengthDimension(number, LengthUnits.Cm));
                case "MM":
                    return Token(CssTokens.Length, new LengthDimension(number, LengthUnits.Mm));
                case "IN":
                    return Token(CssTokens.Length, new LengthDimension(number, LengthUnits.In));
                case "PT":
                    return Token(CssTokens.Length, new LengthDimension(number, LengthUnits.Pt));
                case "PC":
                    return Token(CssTokens.Length, new LengthDimension(number, LengthUnits.Pc));
                case "DEG":
                    return Token(CssTokens.Angle, new AngleDimension(number, AngleUnits.Deg));
                case "RAD":
                    return Token(CssTokens.Angle, new AngleDimension(number, AngleUnits.Rad));
                case "GRAD":
                    return Token(CssTokens.Angle, new AngleDimension(number, AngleUnits.Grad));
                case "MS":
                    return Token(CssTokens.Time, new TimeDimension(number, TimeUnits.Ms));
                case "S":
                    return Token(CssTokens.Time, new TimeDimension(number, TimeUnits.S));
                case "HZ":
                    return Token(CssTokens.Frequency, new FrequencyDimension(number, FrequencyUnits.Hz));
                case "KHZ":
                    return Token(CssTokens.Frequency, new FrequencyDimension(number, FrequencyUnits.KHz));
                default:
                    return Token(CssTokens.Dimension, new UnknownDimension(number, dim));
            }
        }

        private CssTokens Percentage(string text)
        {
            text = text.Substring(0, text.Length - 1);
            return Token(CssTokens.Percentage, new Percentage(double.Parse(text)));
        }

        private CssTokens Function(string text)
        {
            text = text.Substring(0, text.Length - 1);
            text = Unescape(text, false);
            switch (text.ToUpperInvariant())
            {
                case "URL":
                    return FunctionUri(text);
                default:
                    break;
            }
            return Token(CssTokens.Function, text);
        }

        private CssTokens FunctionUri(string name)
        {
            CssTokens token;

            _tokenizer.PushState();

            token = _tokenizer.NextTokenOrUrl();

            if (token == CssTokens.Whitespace)
                token = _tokenizer.NextTokenOrUrl();

            string url = _tokenizer.Value;
            if (token != CssTokens.String && token != CssTokens.SurogateUrl)
            {
                _tokenizer.PopState(false);
                return Token(CssTokens.Function, name);
            }

            if (token == CssTokens.String)
                url = UnescapeString(url);
            else
                url = Unescape(url, false);

            token = _tokenizer.NextToken();

            if (token == CssTokens.Whitespace)
                token = _tokenizer.NextToken();

            if (token != CssTokens.CloseParen)
            {
                _tokenizer.PopState(false);

                return Token(CssTokens.Function, name);
            }

            _tokenizer.PopState(true);
            return Token(CssTokens.Uri, url);
        }

        private CssTokens Token(CssTokens token)
        {
            return Token<object>(token, null);
        }

        private CssTokens Token<T>(CssTokens token, T value)
        {
            Value = new CssSourceToken();
            Value.Value = value;
            return token;
        }

        private string Unescape(string value, bool inString)
        {
            var result = UnicodeEscape.Replace(value, m =>
                {
                    string val = m.Value.Substring(1).TrimEnd();
                    try
                    {
                        return char.ConvertFromUtf32(Convert.ToInt32(val, 16));
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        return ((char)0xFFFD).ToString();
                    }
                });

            result = SimpleEscape.Replace(result, m =>
                {
                    return m.Value.Substring(1);
                });

            if (inString)
                result = NewlineEscape.Replace(result, m =>
                    {
                        return "";
                    });

            return result;
        }

        private string UnescapeString(string text)
        {
            if (text.Length > 1 && text[0] == text[text.Length - 1])
                text = text.Substring(1, text.Length - 2);
            else if (text.Length > 0)
                text = text.Substring(1);
            text = Unescape(text, true);
            return text;
        }
    }
}
