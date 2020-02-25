using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Range
{
    class ProgramRange
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Введите два вещественных числа для числового диапазона");

            Console.Write("Начало диапазона = ");
            double from = Convert.ToDouble(Console.ReadLine());

            Console.Write("Конец диапазона = ");
            double to = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Введите число для проверки принадлежности данному диапазону чисел");

            Console.Write("Введите число = ");
            double number = Convert.ToDouble(Console.ReadLine());

            Range range = new Range(from, to);

            Console.WriteLine("Начало диапазона = " + range.From);
            Console.WriteLine("Конец диапазона = " + range.To);
            Console.WriteLine("Длина диапазона = " + range.GetLength());
            Console.WriteLine("Метод вычисляет принадлежит ли введенное число диапазону (true = да / false = нет) - " + range.IsInside(number));

            Console.ReadKey();
        }
    }
}
