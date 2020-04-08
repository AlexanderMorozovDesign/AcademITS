using System;
using System.Text;


namespace Vector
{
    class VectorProgram
    {
        static void Main(string[] args)
        {
            double[] array = { 1, -2, 3, -4, 5 };
            Vector vector1 = new Vector(7, array);

            Console.WriteLine("Вектор1 = {0}", vector1);
            Console.WriteLine("Размерность = {0}", vector1.GetSize());
            Console.WriteLine("hash = {0}", vector1.GetHashCode());
            Console.WriteLine("Длина вектора = {0}", vector1.GetLength());
            Console.WriteLine("Изменим элемент с индексом 3 вектора на 10");
            vector1.SetElement(3, 10);
            Console.WriteLine("Вектор стал = {0}", vector1);
            Console.WriteLine("Пятый (индексация с нуля) элемент вектора = {0}", vector1.GetElement(5));

            Console.WriteLine("Введите количество элементов во втором векторе: ");
            int n = int.Parse(Console.ReadLine());
            Vector vector2 = new Vector(n);

            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите {0}-ый элемент вектора: ", i);
                double value = double.Parse(Console.ReadLine());
                vector2.SetElement(i, value);
            }

            Console.WriteLine("Вектор2 = {0}", vector2);
            Console.WriteLine("Вектор1 == Вектор2: {0}", vector1.Equals(vector2));
            vector1.Add(vector2);
            Console.WriteLine("Вектор1 += Вектор2:  {0}", vector1);
            vector2.Subtract(vector1);
            Console.WriteLine("Вектор2 -= Вектор1:  {0}", vector2);
            vector1.Multiply(3);
            Console.WriteLine("Умножение Вектора1 на 3:  {0}", vector1);

            Vector vec3 = Vector.GetSumm(vector1, vector2);
            Console.WriteLine("Статический метод сложения двух векторов (Вектор1 + Вектор2): {0}", Vector.GetSumm(vector1, vector2));
            Console.WriteLine("Статический метод вычитания двух векторов (Вектор1 - Вектор2): {0}", Vector.GetDifference(vector1, vector2));
            Console.WriteLine("Статический метод получения скалярного произведения двух векторов (Вектор1, Вектор2): {0}", Vector.DotProduct(vector1, vector2));

            Console.ReadLine();
        }
    }
}
