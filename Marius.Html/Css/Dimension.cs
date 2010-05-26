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

namespace Marius.Html.Css
{
    public abstract class Dimension
    {
        public abstract DimensionType Type { get; }
    }

    public enum DimensionType
    {
        Unknown,
        Length,
        Ems,
        Exs,
        Angle,
        Time,
        Frequency,
        Percentage,
        Number
    }

    public class NumberDimension: Dimension
    {
        public double Value { get; private set; }

        public NumberDimension(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public override DimensionType Type
        {
            get { return DimensionType.Number; }
        }
    }

    public class LengthDimension: Dimension
    {
        public double Value { get; private set; }
        public LengthUnits Units { get; private set; }

        public LengthDimension(double value, LengthUnits units)
        {
            Value = value;
            Units = units;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Value, Units);
        }

        public override DimensionType Type
        {
            get { return DimensionType.Length; }
        }
    }

    public class EmsDimension: Dimension
    {
        public double Value { get; private set; }

        public EmsDimension(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}Em", Value);
        }

        public override DimensionType Type
        {
            get { return DimensionType.Ems; }
        }
    }

    public class ExsDimension: Dimension
    {
        public double Value { get; private set; }

        public ExsDimension(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}Ex", Value);
        }

        public override DimensionType Type
        {
            get { return DimensionType.Exs; }
        }
    }

    public class AngleDimension: Dimension
    {
        public double Value { get; private set; }
        public AngleUnits Units { get; private set; }

        public AngleDimension(double value, AngleUnits units)
        {
            Value = value;
            Units = units;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Value, Units);
        }

        public override DimensionType Type
        {
            get { return DimensionType.Angle; }
        }
    }

    public enum AngleUnits
    {
        Deg,
        Rad,
        Grad,
    }

    public class TimeDimension: Dimension
    {
        public double Value { get; private set; }
        public TimeUnits Units { get; private set; }

        public TimeDimension(double value, TimeUnits units)
        {
            Value = value;
            Units = units;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Value, Units);
        }

        public override DimensionType Type
        {
            get { return DimensionType.Time; }
        }
    }

    public enum TimeUnits
    {
        Ms,
        S,
    }

    public class FrequencyDimension: Dimension
    {
        public double Value { get; private set; }
        public FrequencyUnits Units { get; private set; }

        public FrequencyDimension(double value, FrequencyUnits units)
        {
            Value = value;
            Units = units;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Value, Units);
        }

        public override DimensionType Type
        {
            get { return DimensionType.Frequency; }
        }
    }

    public enum FrequencyUnits
    {
        Hz,
        KHz,
    }

    public class UnknownDimension: Dimension
    {
        public double Value { get; private set; }
        public string Units { get; private set; }

        public UnknownDimension(double value, string units)
        {
            Value = value;
            Units = units;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}", Value, Units);
        }

        public override DimensionType Type
        {
            get { return DimensionType.Unknown; }
        }
    }

    public class Percentage: Dimension
    {
        public double Value { get; private set; }

        public Percentage(double value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("{0}%", Value);
        }

        public override DimensionType Type
        {
            get { return DimensionType.Percentage; }
        }
    }
}
