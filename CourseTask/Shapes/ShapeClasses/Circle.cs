using System;

namespace Shapes.ShapeClasses
{
    public class Circle : IShape
    {
        private readonly double radius;

        public Circle(double radius)
        {
            this.radius = radius;
        }

        public double GetWidth()
        {
            return radius * 2;
        }

        public double GetHeight()
        {
            return radius * 2;
        }

        public double GetArea()
        {
            return Math.PI * radius * radius;
        }

        public double GetPerimeter()
        {
            return 2 * Math.PI * radius;
        }

        public override string ToString()
        {
            return "Окружность с радиусом = " + radius;
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
           
            Circle c = (Circle)o;

            return radius == c.radius;
        }

        public override int GetHashCode()
        {
            int prime = 19;
            int hash = prime + radius.GetHashCode();

            return hash;
        }
    }
}
