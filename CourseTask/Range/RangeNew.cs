using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Range
{
    class RangeNew
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите два вещественных числа для первого числового диапазона");

            Console.Write("Начало диапазона = ");
            double from = Convert.ToDouble(Console.ReadLine());

            Console.Write("Конец диапазона = ");
            double to = Convert.ToDouble(Console.ReadLine());

            Range firstRange = new Range(from, to);

            Console.WriteLine("Введите два вещественных числа для второго числового диапазона");

            Console.Write("Начало диапазона = ");
            from = Convert.ToDouble(Console.ReadLine());

            Console.Write("Конец диапазона = ");
            to = Convert.ToDouble(Console.ReadLine());

            Range secondRange = new Range(from, to);

            Range intersection = Range.Intersection(firstRange, secondRange);
            Range[] union = Range.Union(firstRange, secondRange);
            Range[] difference = Range.Difference(firstRange, secondRange);

            if (intersection != null)
            {
                Console.WriteLine("Пересечение первого и второго дипазона: {0} - {1}", intersection.From, intersection.To);
            }
            else
            {
                Console.WriteLine("Пересечение первого и второго дипазона отсутсвует");
            }

            Console.WriteLine("Объединение первого и второго дипазона: {0} - {1}", union[0].From, union[0].To);

            if (union.Length > 1)
            {
                Console.WriteLine("                                        {0} - {1}", union[1].From, union[1].To);
            }

            if (difference != null)
            {
                Console.WriteLine("Разность первого и второго дипазона: {0} - {1}", difference[0].From, difference[0].To);

                if (difference.Length > 1)
                {
                    Console.WriteLine("                                     {0} - {1}", difference[1].From, difference[1].To);
                }
            }
            else
            {
                Console.WriteLine("разность первого и второго дипазона: нулевая");
            }

            Console.ReadKey();
        }
    }
}
