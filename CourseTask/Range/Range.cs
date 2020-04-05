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
            double To1 = To;
            double From1 = From;

            if (To1 == range.To && From1 == range.From)
            {
                return new Range[] { };
            }
            else
            {
                if (From1 <= range.From)
                {
                    if (From1 == range.From)
                    {
                        if (To1 < range.To)
                        {
                            return new Range[] { new Range(To1, range.To) };
                        }
                        else
                        {
                            return new Range[] { new Range(range.To, To1) };
                        }
                    }
                    else
                    {
                        if (To1 == range.To)
                        {
                            return new Range[] { new Range(From1, range.From) };
                        }
                        else
                        {
                            if (To1 < range.To)
                            {
                                return new Range[] { new Range(From1, range.From), new Range(To1, range.To) };
                            }
                            else
                            {
                                return new Range[] { new Range(From1, range.From), new Range(range.To, To1) };
                            }
                        }
                    }
                }
                else
                {
                    if (To1 == range.To)
                    {
                        return new Range[] { new Range(range.From, From1) };
                    }
                    else
                    {
                        if (To1 < range.To)
                        {
                            return new Range[] { new Range(range.From, From), new Range(To1, range.To) };
                        }
                        else
                        {
                            return new Range[] { new Range(range.From, From), new Range(range.To, To1) };
                        }
                    }
                }
            }
        }
    }
}
