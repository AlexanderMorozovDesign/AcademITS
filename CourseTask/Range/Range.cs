using System;

namespace Range
{
    class Range
    {
        public double From { get; set; }

        public double To { get; set; }

        public Range(double from, double to)
        {
            From = from;
            To = to;
        }

        public double GetLength()
        {
            return To - From;
        }

        public bool IsInside(double number)
        {
            return number >= From && number <= To;
        }

        public override string ToString()
        {
            return string.Format("({0}; {1})", From, To);
        }

        public Range GetIntersection(Range range)
        {
            double resultFrom = Math.Max(From, range.From);
            double resultTo = Math.Min(To, range.To);

            if (resultFrom >= resultTo)
            {
                return null;
            }

            return new Range(resultFrom, resultTo);
        }

        public Range[] GetUnion(Range range)
        {
            if (From < range.From && To < range.From)
            {
                return new Range[] { new Range(From, To), new Range(range.From, range.To) };
            }

            if (range.From < From && range.To < From)
            {
                return new Range[] { new Range(range.From, range.To), new Range(From, To) };
            }

            return new Range[] { new Range(Math.Min(From, range.From), Math.Max(To, range.To)) };
        }

        public Range[] GetDifference(Range range)
        {
            Range intersection = GetIntersection(range);

            if (intersection == null)
            {
                return new Range[] { new Range(From, To) };
            }

            if (intersection.From == From && intersection.To == To)
            {
                return new Range[] { };
            }

            if (intersection.From == range.From && intersection.To == range.To)
            {
                if (From == range.From)
                {
                    return new Range[] { new Range(range.To, To) };
                }

                if (To == range.To)
                {
                    return new Range[] { new Range(From, range.From) };
                }

                return new Range[] { new Range(From, range.From), new Range(range.To, To) };
            }

            if (intersection.From == range.From)
            {
                return new Range[] { new Range(From, range.From) };
            }

            return new Range[] { new Range(range.To, To) };
        }
    }
}
