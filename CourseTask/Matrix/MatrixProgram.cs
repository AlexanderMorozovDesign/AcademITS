using System;
using System.Text;
using MatrixN;
using VectorN;

namespace MatrixProgram
{
    class MatrixProgram
    {
        static void Main(string[] args)
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Console.WriteLine("Исходная матрица1 {0}", m1);
            Console.WriteLine("Количество строк и столбцов матрицы1 {0}, {1}", m1.GetNumberOfRows(), m1.GetNumberOfElements());
            Console.WriteLine("Вторая строка матрицы1 {0}", m1.GetRow(1));
            Console.WriteLine("Второй столбец матрицы1 {0}", m1.GetColumn(1));

            m1.Transpose();
            Console.WriteLine("Транспонированная матрица1 {0}", m1);
            m1.MultiplicationVectors(3);
            Console.WriteLine("Матрица1 умноженная на 3 {0}", m1);

            Vector v = new Vector(new double[] { 3, 5 });
            m1.Multiply(v);
            Console.WriteLine("Матрица1 умноженная на вектор {0} = {1}", v, m1);

            Matrix m2 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 7, 6 }, { 7, 3, 9 } });
            Console.WriteLine("Исходная матрица2 {0}", m2);
            Console.WriteLine("Определитель матрицы2 {0}", m2.GetDeterminant());

            Matrix m3 = new Matrix(new double[,] { { 5, 2, 8 }, { 2, 2, 4 }, { 0, 6, 5 } });
            Console.WriteLine("Исходная матрица3 {0}", m3);

            m2.Add(m3);
            Console.WriteLine("Матрица2 += Матрица3 = {0}", m2);
            m3.Subtract(m2);
            Console.WriteLine("Матрица3 -= Матрица2 = {0}", m3);

            Console.WriteLine("Статические методы");
            Console.WriteLine("Матрица2 + Матрица3 = {0}", Matrix.AddMatrixes(m2, m3));
            Console.WriteLine("Матрица2 - Матрица3 = {0}", Matrix.SubtractMatrixes(m2, m3));
            Console.WriteLine("Матрица2 * Матрица3 = {0}", Matrix.GetComposition(m2, m3));

            Console.ReadLine();
        }
    }
}
