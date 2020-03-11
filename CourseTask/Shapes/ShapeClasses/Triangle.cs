using System;
using System.Text;

namespace Shapes.ShapeClasses
{
    public class Triangle : IShape
    {
        private readonly double x1;
        private readonly double y1;
        private readonly double x2;
        private readonly double y2;
        private readonly double x3;
        private readonly double y3;

        private double GetSideLength(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1));
        }

        public Triangle(double x1, double y1, double x2, double y2, double x3, double y3)
        {
            this.x1 = x1;
            this.y1 = y1;

            this.x2 = x2;
            this.y2 = y2;

            this.x3 = x3;
            this.y3 = y3;
        }

        public double GetWidth()
        {
            return Math.Max(x1, Math.Max(x2, x3)) - Math.Min(x1, Math.Min(x2, x3));
        }

        public double GetHeight()
        {
            return Math.Max(y1, Math.Max(y2, y3)) - Math.Min(y1, Math.Min(y2, y3));
        }

        public double GetArea()
        {
            double a = GetSideLength(x1, y1, x2, y2); 
            double b = GetSideLength(x1, y1, x3, y3); 
            double c = GetSideLength(x2, y2, x3, y3); 

            double p = (a + b + c) / 2;
            double s = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

            return s;
        }

        public double GetPerimeter()
        {
            double a = GetSideLength(x1, y1, x2, y2);
            double b = GetSideLength(x1, y1, x3, y3); 
            double c = GetSideLength(x2, y2, x3, y3); 

            double perimeter = a + b + c;

            return perimeter;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("Треугольник с координатами ({0},{1}), ({2},{3}), ({4},{5})", x1, y1, x2, y2, x3, y3);

            return builder.ToString();
        }

        public override bool Equals(object o)
        {           
            if (ReferenceEquals(o, this))
            {
                return true;
            }

            if (ReferenceEquals(o, null) || o.GetType() != GetType())
            {
                return false;
            }
          
            Triangle t = (Triangle)o;

            return (x1 == t.x1 && x2 == t.x2 && x3 == t.x3 && y1 == t.y1 && y2 == t.y2 && y3 == t.y3);
        }

        public override int GetHashCode()
        {
            int prime = 19;
            int hash = 1;

            hash = prime * hash + x1.GetHashCode();
            hash = prime * hash + y1.GetHashCode();

            hash = prime * hash + x2.GetHashCode();
            hash = prime * hash + y2.GetHashCode();

            hash = prime * hash + x3.GetHashCode();
            hash = prime * hash + y3.GetHashCode();

            return hash;
        }
    }
}
