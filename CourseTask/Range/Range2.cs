using System;

namespace Range
{
    class Range2
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите два вещественных числа для первого числового диапазона");

            Console.Write("Начало диапазона = ");
            double from = Convert.ToDouble(Console.ReadLine());

            Console.Write("Конец диапазона = ");
            double to = Convert.ToDouble(Console.ReadLine());

            Range range1 = new Range(from, to);

            Console.WriteLine("Введите два вещественных числа для второго числового диапазона");

            Console.Write("Начало диапазона = ");
            from = Convert.ToDouble(Console.ReadLine());

            Console.Write("Конец диапазона = ");
            to = Convert.ToDouble(Console.ReadLine());

            Range range2 = new Range(from, to);

            Range intersection = range1.GetIntersection(range2);
            Range[] union = range1.GetUnion(range2);
            Range[] difference = range1.GetDifference(range2);

            if (intersection != null)
            {
                Console.WriteLine("Пересечение первого и второго диапазона: {0}", intersection);
            }
            else
            {
                Console.WriteLine("Пересечение первого и второго диапазона отсутсвует");
            }

            Console.WriteLine("Объединение первого и второго диапазона: {0}", union[0]);

            if (union.Length > 1)
            {
                Console.WriteLine("{0} ", union[1]);
            }

            if (difference.Length > 0)
            {
                Console.WriteLine("Разность первого и второго диапазона: {0}", difference[0]);

                if (difference.Length > 1)
                {
                    Console.WriteLine("{0}", difference[1]);
                }
            }
            else
            {
                Console.WriteLine("разность первого и второго диапазона: нулевая");
            }

            Console.ReadKey();
        }
    }
}
