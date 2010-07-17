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
using Marius.Html.Css.Values;
using Marius.Html.Css.Box;

namespace Marius.Html.Css
{
    public abstract class CssSimplePropertyHandler: CssPropertyHandler
    {
        public abstract bool IsInherited { get; }
        public abstract CssValue Initial { get; }

        public CssSimplePropertyHandler(CssContext context)
            : base(context)
        {
        }

        public override bool Validate(CssExpression expression)
        {
            CssValue value = Parse(expression);
            if (value == null || !Valid(expression))
                return false;

            return true;
        }

        public override bool Apply(IWithStyle box, CssExpression expression)
        {
            CssValue value = Parse(expression);
            if (value == null || !Valid(expression))
                return false;

            SetValue(box, value);

            return true;
        }

        public abstract CssValue Parse(CssExpression expression);

        public abstract void SetValue(IWithStyle box, CssValue value);
        public abstract CssValue GetValue(IWithStyle box);

        // default implementation: as specified
        public virtual CssValue Compute(CssBox box)
        {
            var value = GetValue(box.Properties);
            if ((value == null && IsInherited) || CssKeywords.Inherit.Equals(value))
            {
                if (box.InheritanceParent != null)
                    return Compute(box.InheritanceParent);
                if (box.Parent != null)
                    return Compute(box.Parent);
                return Initial;
            }

            if (value == null)
                return Initial;

            return value;
        }
    }
}
