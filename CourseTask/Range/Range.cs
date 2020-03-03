using System;
using System.Text;

namespace Range
{
    class Range
    {
        public double From
        {
            get;
            set;
        }

        public double To
        {
            get;
            set;
        }

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
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("({0};{1})", From, To);

            return builder.ToString();
        }


        public Range Intersect(Range range2)
        {
            double epsilon = 10e-6; ; 
            double resultFrom = Math.Max(From, range2.From);
            double resultTo = Math.Min(To, range2.To);

            if (resultFrom > resultTo || Math.Abs(resultFrom - resultTo) < epsilon)
            {
                return null;
            }

            return new Range(resultFrom, resultTo);
        }

        public Range[] Unite(Range range2)
        {
            if (From < range2.From && To < range2.From)
            {
                return new Range[] { new Range(From, To), new Range(range2.From, range2.To) };
            }

            if (range2.From < From && range2.To < From)
            {
                return new Range[] { new Range(range2.From, range2.To), new Range(From, To) };
            }
                
            double resultFrom = Math.Min(From, range2.From); 
            double resultTo = Math.Max(To, range2.To);

            return new Range[] { new Range(resultFrom, resultTo) };
        }

        public Range[] Substract(Range range2)
        {
            double epsilon = 10e-6; 

            Range intersection = Intersect(range2); 

            if (intersection == null)
            {
                return new Range[] { new Range(From, To) };
            }

            if (Math.Abs(intersection.From - range2.From) < epsilon && Math.Abs(intersection.To - range2.To) < epsilon) 
            {
                if (Math.Abs(From - range2.From) < epsilon) 
                {
                    return new Range[] { new Range(range2.To, To) };
                }

                if (Math.Abs(To - range2.To) < epsilon)
                {
                    return new Range[] { new Range(From, range2.From) };
                }

                return new Range[] { new Range(From, range2.From), new Range(range2.To, To) };
            }

            if (Math.Abs(intersection.From - From) < epsilon && Math.Abs(intersection.To - To) < epsilon)
            {
                return new Range[] { };
            }

            if (Math.Abs(intersection.From - range2.From) < epsilon)
            {
                return new Range[] { new Range(From, range2.From) };
            }

            return new Range[] { new Range(range2.To, To) };
        }
    }
}
