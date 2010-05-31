﻿#region License
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

namespace Marius.Html.Css.Selectors
{
    public class CssPseudoElementCondition: CssCondition
    {
        private static readonly CssSpecificity PseudoElementSpecificty = new CssSpecificity(0, 0, 0, 1);

        public CssPseudoValue Condition { get; private set; }

        public sealed override CssConditionType ConditionType
        {
            get { return CssConditionType.PseudoElement; }
        }

        public override CssSpecificity Specificity
        {
            get { return PseudoElementSpecificty; }
        }

        public CssPseudoElementCondition(CssPseudoValue condition)
        {
            Condition = condition;
        }

        public override bool Equals(CssCondition other)
        {
            CssPseudoElementCondition o = other as CssPseudoElementCondition;
            if (o == null)
                return false;

            return o.Condition.Equals(this.Condition);
        }

        public override int GetHashCode()
        {
            return Utils.GetHashCode(Condition, ConditionType);
        }

        public override string ToString()
        {
            return string.Format(":{0}", Condition);
        }
    }
}