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
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace Marius.Html.Css.Box
{
    public class CssBoxWord
    {
        private static readonly Regex kSpaces = new Regex(@"(\s+)", RegexOptions.Compiled);
        private static readonly Regex kNewline = new Regex(@"(\r?\n)", RegexOptions.Compiled);

        private static readonly CssBoxWord Newline = new CssBoxWord(CssBoxWordType.Newline, null, null);

        public CssBoxWordType Type { get; private set; }
        public string Text { get; private set; }
        public CssBox Box { get; private set; }

        private CssBoxWord(CssBoxWordType type, string text, CssBox box)
        {
            Type = type;
            Text = text;
            Box = box;
        }


        private static CssBoxWord Space(string spaces, CssBox box)
        {
            return new CssBoxWord(CssBoxWordType.Space, spaces, box);
        }

        private static CssBoxWord Word(string word, CssBox box)
        {
            return new CssBoxWord(CssBoxWordType.Word, word, box);
        }

        private static CssBoxWord FromBox(CssBox box)
        {
            return new CssBoxWord(CssBoxWordType.Box, null, box);
        }

        public static CssBoxWord[] Create(CssAnonymousInlineBox box)
        {
            var words = kSpaces.Split(box.Text);
            List<CssBoxWord> result = new List<CssBoxWord>(words.Length);

            for (int i = 0; i < words.Length; i++)
            {
                if (kSpaces.IsMatch(words[i]))
                {
                    var spaces = kNewline.Split(words[i]);
                    for (int k = 0; k < spaces.Length; k++)
                    {
                        if (kNewline.IsMatch(spaces[k]))
                            result.Add(CssBoxWord.Newline);
                        else
                            result.Add(CssBoxWord.Space(spaces[k], box));
                    }
                }
                else
                {
                    result.Add(CssBoxWord.Word(words[i], box));
                }
            }

            return result.ToArray();
        }

        public static CssBoxWord[] Create(CssAnonymousSpaceBox box)
        {
            Debug.Assert(string.IsNullOrEmpty(box.Text) || kSpaces.IsMatch(box.Text));

            var spaces = kNewline.Split(box.Text);
            List<CssBoxWord> result = new List<CssBoxWord>(spaces.Length);

            for (int i = 0; i < spaces.Length; i++)
            {
                if (kNewline.IsMatch(spaces[i]))
                    result.Add(CssBoxWord.Newline);
                else
                    result.Add(CssBoxWord.Space(spaces[i], box));
            }

            return result.ToArray();
        }

        public static CssBoxWord CreateRaw(CssBox box)
        {
            return CssBoxWord.FromBox(box);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            switch (Type)
            {
                case CssBoxWordType.Space:
                    sb.Append("[Spaces] '").Append(Text).Append('\'');
                    break;
                case CssBoxWordType.Word:
                    sb.Append(Text);
                    break;
                case CssBoxWordType.Box:
                    sb.Append("[Box] ").Append(Box);
                    break;
                case CssBoxWordType.Newline:
                    sb.Append("[Newline]");
                    break;
                default:
                    break;
            }

            return sb.ToString();
        }
    }

    public enum CssBoxWordType
    {
        Space,
        Word,
        Box,
        Newline,
    }
}
