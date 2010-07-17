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
using Marius.Html.Css.Values;
using Marius.Html.Internal;

namespace Marius.Html.Css.Properties
{
    public partial class Elevation: CssSimplePropertyHandler
    {
        public override bool IsInherited
        {
            get { return true; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.Level; }
        }

        public Elevation(CssContext context)
            : base(context)
        {
        }

        public override CssValue Parse(CssExpression expression)
        {
            CssValue result = null;
            if (MatchAngle(expression, ref result))
                return result;

            if (MatchAny(expression, new[] { CssKeywords.Above, CssKeywords.Level, CssKeywords.Below, CssKeywords.Lower, CssKeywords.Higher }, ref result))
                return result;

            return MatchInherit(expression);
        }

        public override CssValue Compute(Box.CssBox box)
        {
            var specified = GetValue(box.Properties);
            if (specified == null)
                return base.Compute(box);

            if (specified.ValueGroup == CssValueGroup.Angle)
            {
                CssAngle value = (CssAngle)specified;
                if (value.Units == CssUnits.Deg && value.Value >= -90 && value.Value <= 90)
                    return value;

                double angle = Utils.ToDegrees(value);
                return Normalize(angle);
            }
            else if (specified.ValueGroup == CssValueGroup.Identifier)
            {
                if (CssKeywords.Below.Equals(specified))
                    return new CssAngle(-90, CssUnits.Deg);
                else if (CssKeywords.Level.Equals(specified))
                    return new CssAngle(0, CssUnits.Deg);
                else if (CssKeywords.Above.Equals(specified))
                    return new CssAngle(90, CssUnits.Deg);
                else if (CssKeywords.Higher.Equals(specified) || CssKeywords.Lower.Equals(specified))
                {
                    var inherited = Inherited(box);
                    if (inherited.ValueGroup != CssValueGroup.Angle)
                        throw new CssInvalidStateException();

                    double angle = Utils.ToDegrees((CssAngle)inherited);
                    if (CssKeywords.Higher.Equals(specified))
                        angle += 10;
                    else if (CssKeywords.Lower.Equals(specified))
                        angle -= 10;

                    return Normalize(angle);
                }
            }

            return base.Compute(box);
        }

        private CssValue Normalize(double angle)
        {
            if (double.IsInfinity(angle) || double.IsNaN(angle))
                throw new CssInvalidStateException();

            if (angle < -1000000 || angle > 1000000)
                throw new CssInvalidStateException();

            while (angle < -90)
            {
                angle += 90;
            }

            while (angle > 90)
            {
                angle -= 90;
            }

            return new CssAngle(angle, CssUnits.Deg);
        }
    }
}
