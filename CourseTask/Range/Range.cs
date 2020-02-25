using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static Range Intersection(Range firstRange, Range secondRange)
        {
            double[] doubleArray = { firstRange.From, firstRange.To, secondRange.From, secondRange.To };
            Array.Sort(doubleArray);

            double point = (doubleArray[1] + doubleArray[2]) / 2;

            if (firstRange.IsInside(point))
            {
                return new Range(doubleArray[1], doubleArray[2]);
            }

            return null;
        }

        public static Range[] Union(Range firstRange, Range secondRange)
        {
            Range[] unionRange;

            double[] doubleArray = { firstRange.From, firstRange.To, secondRange.From, secondRange.To };
            Array.Sort(doubleArray);

            double point = (doubleArray[1] + doubleArray[2]) / 2;

            if (firstRange.IsInside(point))
            {
                unionRange = new Range[1];
                unionRange[0] = new Range(doubleArray[0], doubleArray[3]);

            }
            else
            {
                unionRange = new Range[2];
                unionRange[0] = new Range(doubleArray[0], doubleArray[1]);
                unionRange[1] = new Range(doubleArray[2], doubleArray[3]);
            }

            return unionRange;
        }

        public static Range[] Difference(Range firstRange, Range secondRange)
        {
            Range[] differenceRange;

            double[] doubleArray = { firstRange.From, firstRange.To, secondRange.From, secondRange.To };
            Array.Sort(doubleArray);

            double point = (doubleArray[1] + doubleArray[2]) / 2;

            if (!firstRange.IsInside(point))
            {
                differenceRange = new Range[1];
                differenceRange[0] = new Range(firstRange.From, firstRange.To);

                return differenceRange;
            }

            if (firstRange.From == doubleArray[0] && firstRange.To == doubleArray[3])
            {
                differenceRange = new Range[2];
                differenceRange[0] = new Range(firstRange.From, secondRange.From);
                differenceRange[1] = new Range(secondRange.To, firstRange.To);

                return differenceRange;
            }

            if (secondRange.From == doubleArray[0] && secondRange.To == doubleArray[3])
            {
                return null;
            }

            if (firstRange.From == doubleArray[0])
            {
                differenceRange = new Range[1];
                differenceRange[0] = new Range(firstRange.From, secondRange.From);
            }
            else
            {
                differenceRange = new Range[1];
                differenceRange[0] = new Range(secondRange.To, firstRange.To);
            }

            return differenceRange;
        }
    }
}
