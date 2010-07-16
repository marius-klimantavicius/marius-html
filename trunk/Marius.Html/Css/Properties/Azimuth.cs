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
using System.Text;
using System.Linq;
using Marius.Html.Css.Dom;
using Marius.Html.Css.Values;
using Marius.Html.Internal;

namespace Marius.Html.Css.Properties
{
    public partial class Azimuth: CssSimplePropertyHandler
    {
        private static readonly Dictionary<CssValue, double> _angles;
        private static readonly Dictionary<CssValue, double> _behindAngles;

        public override bool IsInherited
        {
            get { return true; }
        }

        public override CssValue Initial
        {
            get { return CssKeywords.Center; }
        }

        static Azimuth()
        {
            _behindAngles = new Dictionary<CssValue, double>();
            _angles = new Dictionary<CssValue, double>();

            _behindAngles.Add(CssKeywords.LeftSide, 270);
            _behindAngles.Add(CssKeywords.FarLeft, 240);
            _behindAngles.Add(CssKeywords.Left, 220);
            _behindAngles.Add(CssKeywords.CenterLeft, 200);
            _behindAngles.Add(CssKeywords.Center, 180);
            _behindAngles.Add(CssKeywords.CenterRight, 160);
            _behindAngles.Add(CssKeywords.Right, 140);
            _behindAngles.Add(CssKeywords.FarRight, 120);
            _behindAngles.Add(CssKeywords.RightSide, 90);

            _angles.Add(CssKeywords.LeftSide, 270);
            _angles.Add(CssKeywords.FarLeft, 300);
            _angles.Add(CssKeywords.Left, 320);
            _angles.Add(CssKeywords.CenterLeft, 340);
            _angles.Add(CssKeywords.Center, 0);
            _angles.Add(CssKeywords.CenterRight, 20);
            _angles.Add(CssKeywords.Right, 40);
            _angles.Add(CssKeywords.FarRight, 60);
            _angles.Add(CssKeywords.RightSide, 90);
        }

        public Azimuth(CssContext context)
            : base(context)
        {
        }

        public override void Apply(IWithStyle box, CssValue value)
        {
            box.Azimuth = value;
        }

        public override CssValue Parse(CssExpression expression)
        {
            if (Match(expression, CssKeywords.Inherit))
                return CssKeywords.Inherit;

            CssValue value = null;
            if (MatchAny(expression, new[] { CssKeywords.Leftwards, CssKeywords.Rightwards }, ref value))
                return value;

            if (MatchAngle(expression, ref value))
                return value;

            bool hasBehind = false, hasPosition = false;
            for (int i = 0; i < 2; i++)
            {
                if (Match(expression, CssKeywords.Behind))
                {
                    if (hasBehind)
                        return null;
                    hasBehind = true;
                }
                else if (MatchAny(expression, new[] { CssKeywords.LeftSide, CssKeywords.FarLeft, CssKeywords.Left, CssKeywords.CenterLeft, CssKeywords.Center, CssKeywords.CenterRight, CssKeywords.Right, CssKeywords.FarRight, CssKeywords.RightSide }, ref value))
                {
                    if (hasPosition)
                        return null;
                    hasPosition = true;
                }
            }

            if (hasPosition || hasBehind)
            {
                if (!hasPosition)
                    value = CssKeywords.Center;
                return new CssAzimuth(value, hasBehind);
            }

            return null;
        }

        public override CssValue Compute(Box.CssBox box)
        {
            var specified = box.Properties.Azimuth;

            if (specified is CssAzimuth)
            {
                double angle = 0;
                CssAzimuth value = (CssAzimuth)specified;

                Dictionary<CssValue, double> angles;

                if (value.IsBehind)
                    angles = _behindAngles;
                else
                    angles = _angles;

                if (angles.TryGetValue(value.Position, out angle))
                    return new CssAngle(angle, CssUnits.Deg);

                if (CssKeywords.Leftwards.Equals(value.Position) || CssKeywords.Rightwards.Equals(value.Position))
                {
                    CssValue parent = Inherit(box, null);

                    if (!(parent is CssAngle))
                        throw new CssInvalidStateException();

                    angle = Utils.ToDegrees((CssAngle)parent);

                    if (CssKeywords.Leftwards.Equals(value.Position))
                        angle -= 20;
                    else if (CssKeywords.Rightwards.Equals(value.Position))
                        angle += 20;

                    return Normalize(angle);
                }
            }
            else if (specified is CssAngle)
            {
                CssAngle value = (CssAngle)specified;
                
                // TODO: this is deviation from spec, which says that angle can be value between -360 and 360 deg
                // this code allows any angle specified (maybe should throw away value with angle outside the spec value)
                // however this should be done in earlier stages
                if (value.Units == CssUnits.Deg && (value.Value >= -360 && value.Value <= 360))
                {
                    if (value.Value >= 0)
                        return value;
                    return Normalize(value.Value);
                }

                double angle = Utils.ToDegrees(value);
                return Normalize(angle);
            }

            return Inherit(box, specified);
        }

        private CssValue Normalize(double angle)
        {
            if (angle < 0)
            {
                while (angle < 0)
                    angle += 360;
            }
            else if (angle > 360)
            {
                while (angle > 360)
                    angle -= 360;
            }

            return new CssAngle(angle, CssUnits.Deg);
        }
    }
}
