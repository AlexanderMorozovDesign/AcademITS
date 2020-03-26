using System;
using Shapes.ShapeClasses;
using Shapes.Comparers;

namespace Shapes
{
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

            IShape[] shapes = { rectangle1, square1, triangle, circle1, square2, rectangle2, circle2 };

            Console.WriteLine("Заданный массив фигур:");
            foreach (IShape figure in shapes)
            {
                Console.WriteLine(figure + ", хеш код: " + figure.GetHashCode() + ", площадь: " + figure.GetArea());
            }

            Array.Sort(shapes, new AreasComparer()); 
            Console.WriteLine("Фигура с наибольшей площадью: {0}", shapes[shapes.Length - 1]);

            Array.Sort(shapes, new PerimetersComparer());
            Console.WriteLine("Фигура со вторым по величине периметром: {0}", shapes[shapes.Length - 2]);

            Console.WriteLine("Сравним все фигуры между собой:");
            foreach (IShape shape1 in shapes)
            {
                foreach (IShape shape2 in shapes)
                {
                    Console.WriteLine("Сравниваем {0} и {1}, результат {2}", shape1, shape2, shape1 == shape2);
                }
            }

            Console.ReadLine();
        }
    }
}
