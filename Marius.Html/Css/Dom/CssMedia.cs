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
using Marius.Html.Internal;

namespace Marius.Html.Css.Dom
{
    public class CssMedia: CssRule
    {
        public string[] MediaList { get; private set; }
        public CssStyle[] Ruleset { get; private set; }

        public sealed override CssRuleType RuleType
        {
            get { return CssRuleType.Media; }
        }

        public CssMedia(string[] mediaList, CssStyle[] ruleset)
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

        public override bool Equals(CssRule other)
        {
            CssMedia o = other as CssMedia;
            if (o == null)
                return false;

            return o.MediaList.ArraysEqual(this.MediaList) && o.Ruleset.ArraysEqual(this.Ruleset);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(MediaList, Ruleset, RuleType);
        }
    }
}
