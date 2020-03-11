namespace Shapes.ShapeClasses
{
    public class Rectangle : IShape
    {
        private readonly double width;
        private readonly double height;

        public Rectangle(double width, double height)
        {
            this.width = width;
            this.height = height;
        }

        public double GetWidth()
        {
            return width;
        }

        public double GetHeight()
        {
            return height;
        }

        public double GetArea()
        {
            return width * height;
        }

        public double GetPerimeter()
        {
            return (width + height) * 2;
        }

        public override string ToString()
        {
            return "Прямоугольник с шириной = " + width + ", высотой = " + height;
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
          
            Rectangle r = (Rectangle)o;

            return (width == r.width && height == r.height);
        }

        public override int GetHashCode()
        {
            int prime = 19;
            int hash = 1;
            hash = prime * hash + width.GetHashCode();
            hash = prime * hash + height.GetHashCode();

            return hash;
        }
    }
}
