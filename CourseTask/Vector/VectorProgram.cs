using System;

namespace VectorNamespace
{   class VectorProgram
    {
        static void Main(string[] args)
        {
            double[] array = { 1, -2, 3, -4, 5 };
            Vector vec1 = new Vector(7, array);

            Console.WriteLine("Вектор1 = {0}", vec1);
            Console.WriteLine("размерность = {0}", vec1.GetSize());
            Console.WriteLine("hash = {0}", vec1.GetHashCode());
            Console.WriteLine("длина вектора = {0}", vec1.GetModulus());
            
            Console.WriteLine();
            Console.WriteLine("Изменим элемент вектора с индексом 3 на 10");
            vec1.SetElement(3, 10);
            Console.WriteLine("вектор стал = {0}", vec1);
            Console.WriteLine("элемент вектора с индексом 5 = {0}", vec1.GetElement(5));
            
            Console.WriteLine();
            Console.Write("Введите количество элементов во втором векторе: ");
            int n = int.Parse(Console.ReadLine());
            Vector vec2 = new Vector(n);

            for (int i = 0; i < n; i++)
            {
                Console.Write("введите {0}-ый елемент вектора: ", i);
                double val = double.Parse(Console.ReadLine());
                vec2.SetElement(i, val);
            }

            Console.WriteLine("Вектор2 = {0}", vec2);
            Console.WriteLine("Вектор1 == Вектор2: {0}", vec1.Equals(vec2));
            vec1.Add(vec2);
            Console.WriteLine("Вектор1 += Вектор2:  {0}", vec1);
            vec2.Subtract(vec1);
            Console.WriteLine("Вектор2 -= Вектор1:  {0}", vec2);
            vec1.Multiply(3);
            Console.WriteLine("Умножение Вектора1 на 3:  {0}", vec1);

            Vector vec3 = Vector.GetSum(vec1, vec2);
            Console.WriteLine("Статический метод сложения двух векторов (Вектор1 + Вектор2): {0}", Vector.GetSum(vec1, vec2));
            Console.WriteLine("Статический метод вычитания двух векторов (Вектор1 - Вектор2): {0}", Vector.GetSubtract(vec1, vec2));
            Console.WriteLine("Статический метод получения скалярного произведения двух векторов (Вектор1, Вектор2): {0}", Vector.DotProduct(vec1, vec2));

            Console.ReadLine();
        }
    }
}
