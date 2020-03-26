namespace Shapes.ShapeClasses
{
    public class Square : IShape
    {
        private readonly double sideLength;

        public Square(double length)
        {
            sideLength = length;
        }

        public double GetWidth()
        {
            return sideLength;
        }

        public double GetHeight()
        {
            return sideLength;
        }

        public double GetArea()
        {
            return sideLength * sideLength;
        }

        public double GetPerimeter()
        {
            return sideLength * 4;
        }

        public override string ToString()
        {
            return "Квардрат с длиной стороны = " + sideLength;
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
           
            Square s = (Square)o;

            return sideLength == s.sideLength;
        }

        public override int GetHashCode()
        {
            int prime = 19;
            int hash = prime + sideLength.GetHashCode();

            return hash;
        }
    }
}
