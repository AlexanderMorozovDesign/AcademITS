using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shapes
{
    public interface IShape
    {
        double GetWidth();
        double GetHeight();
        double GetArea();
        double GetPerimeter();
    }

    public class Square : IShape
    {
        private double sideLength;

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
            return "Квардрат с длиной стороны = " + sideLength.ToString();
        }

        public override bool Equals(object o)
        {
            if (ReferenceEquals(o, this))
            {
                return true;
            }

            if (ReferenceEquals(o, null) || o.GetType() != this.GetType())
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

    public class Triangle : IShape
    {
        private double x1, y1;
        private double x2, y2;
        private double x3, y3;

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
            return Math.Max(y1, Math.Max(y2, y3)) - Math.Min(y1, Math.Min(y2, y3)); ;
        }

        public double GetArea()
        {
            double a = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)); 
            double b = Math.Sqrt((x3 - x1) * (x3 - x1) + (y3 - y1) * (y3 - y1)); 
            double c = Math.Sqrt((x3 - x2) * (x3 - x2) + (y3 - y2) * (y3 - y2));

            double p = (a + b + c) / 2;
            double S = Math.Sqrt(p * (p - a) * (p - b) * (p - c));

            return S;
        }

        public double GetPerimeter()
        {
            double a = Math.Sqrt((x2 - x1) * (x2 - x1) + (y2 - y1) * (y2 - y1)); 
            double b = Math.Sqrt((x3 - x1) * (x3 - x1) + (y3 - y1) * (y3 - y1));
            double c = Math.Sqrt((x3 - x2) * (x3 - x2) + (y3 - y2) * (y3 - y2)); 

            double Perimeter = a + b + c;

            return Perimeter;
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

            if (ReferenceEquals(o, null) || o.GetType() != this.GetType())
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

    public class Rectangle : IShape
    {
        private double width;
        private double height;

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
            return "Прямоугольник с шириной = " + width.ToString() + ", высотой = " + height.ToString();
        }

        public override bool Equals(object o)
        {
            if (ReferenceEquals(o, this))
            {
                return true;
            }

            if (ReferenceEquals(o, null) || o.GetType() != this.GetType())
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

    public class Circle : IShape
    {
        private double radius;

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
            return "Окружность с радиусом = " + radius.ToString();
        }

        public override bool Equals(object o)
        {
            if (ReferenceEquals(o, this))
            {
                return true;
            }

            if (ReferenceEquals(o, null) || o.GetType() != this.GetType())
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

    class AreasComparer : IComparer<IShape>
    {
        public int Compare(IShape s1, IShape s2)
        {
            if (s1.GetArea() < s2.GetArea())
            {
                return 1;
            }
            else if (s1.GetArea() > s2.GetArea())
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    class PerimetersComparer : IComparer<IShape>
    {
        public int Compare(IShape s1, IShape s2)
        {
            if (s1.GetPerimeter() < s2.GetPerimeter())
            {
                return 1;
            }
            else if (s1.GetPerimeter() > s2.GetPerimeter())
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }

    class Shapes
    {
        static void Main(string[] args)
        {
            Rectangle rectangle1 = new Rectangle(2, 3);
            Rectangle rectangle2 = new Rectangle(4, 5);
            Square square1 = new Square(5);
            Square square2 = new Square(1);
            Circle circle1 = new Circle(2);
            Triangle triangle = new Triangle(0, 0, 3, 0, 2, 2);
            Circle circle2 = new Circle(2);

            IShape[] figures = { rectangle1, square1, triangle, circle1, square2, rectangle2, circle2 };

            Console.WriteLine("Заданный массив фигур:");

            foreach (IShape figure in figures)
            {
                Console.WriteLine(figure.ToString() + ", хеш код: " + figure.GetHashCode() + ", площадь: " + figure.GetArea());
            }
                
            Array.Sort(figures, new AreasComparer());
            Console.WriteLine("Фигура с наибольшей площадью: {0}", figures[0].ToString());

            Array.Sort(figures, new PerimetersComparer());
            Console.WriteLine("Фигура со вторым по величине периметром: {0}", figures[1].ToString());

            Console.WriteLine("Сравним все фигуры между собой:");

            foreach (IShape figure1 in figures)
            {
                foreach (IShape figure2 in figures)
                {
                    Console.WriteLine("Сравниваем {0} и {1}, результат {2}", figure1.ToString(), figure2.ToString(), figure1 == figure2);
                }  
            }

            Console.ReadLine();
        }
    }
}
