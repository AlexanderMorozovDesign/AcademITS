using System;
using System.Text;
using VectorNamespace;

namespace MatrixNamespace
{
    class Matrix
    {
        private Vector[] rows;            //массив строк матрицы, где будут хранится её элементы

        //Конструкторы
        public Matrix(int numberOfRows, int numberOfColumns)
        {
            if (numberOfRows <= 0)
            {
                throw new ArgumentException("Значение аргумента: " + numberOfRows + ", оно должно быть больше 0", "numberOfRows");
            }
            if (numberOfColumns <= 0)
            {
                throw new ArgumentException("Значение аргумента: " + numberOfColumns + ", оно должно быть больше 0", "numberOfColumns");
            }

            rows = new Vector[numberOfRows];             //выделяем память под n-строк матрицы
            for (int i = 0; i < numberOfRows; i++)
                rows[i] = new Vector(numberOfColumns);        //каждая строка содержит m-элементов
        }

        public Matrix(Matrix initialMatrix)                //Конструктор копирования
        {
            int n = initialMatrix.rows.Length;                  //количество строк в матрице m
            rows = new Vector[n];                 //выделяем память под n-строк матрицы
            for (int i = 0; i < n; i++)
                rows[i] = new Vector(initialMatrix.rows[i]);          //копируем каждую строку матрицы m
        }

        public Matrix(double[,] array)
        {
            int n = array.GetLength(0);                //количество строк
            int m = array.GetLength(1);            //количество столбцов

            rows = new Vector[n];             //выделяем память под n-строк матрицы
            for (int i = 0; i < n; i++)
            {
                rows[i] = new Vector(m);        //каждая строка содержит m-элементов
                for (int j = 0; j < m; j++)
                    rows[i].SetElement(j, array[i, j]);     //присвоим значение элемента массива array соответсвующему элементу вектора rows[i]
            }
        }

        public Matrix(Vector[] initialVectors)
        {
            int n = initialVectors.Length;
            rows = new Vector[n];             //выделяем память под n-строк матрицы
            for (int i = 0; i < n; i++)
                rows[i] = new Vector(initialVectors[i]);   //Используя конструктор копирования, присвоим каждой строке матрице элемент массива векторов
        }

        //Получение количества строк
        public int GetNumberOfRows()
        {
            return rows.Length;
        }

        //Получение количества столбцов
        public int GetNumberOfColumns()
        {
            return rows[0].GetSize(); 
        }        

        //Получение вектора-строки по индексу
        public Vector GetRow(int index)
        {
            if (index >= GetNumberOfRows() || index < 0)
            {
                throw new IndexOutOfRangeException("Значение аргумента: " + index + ", оно не может быть меньше 0 или больше количества строк в матрице");
            }
            return new Vector(rows[index]);
        }

        //Задание вектора-строки по индексу
        public void SetRow(int index, Vector v)
        {
            if (GetNumberOfColumns() != v.GetSize())                  //если количество элементов в векторе-строке не соответствует числу элементов в строке матрицы
                throw new ArgumentException("Количество элементов вектора, переданного в качестве аргумента: " + v.GetSize() + ", не соответствует размеру матрицы", "v");
            if (index >= GetNumberOfRows() || index < 0)
                throw new IndexOutOfRangeException("Значение аргумента: " + index + ", оно не может быть меньше 0 или больше количества строк в матрице");

            int numberOfElements = v.GetSize();                                    //количество элементов в векторе-строке
            for (int i = 0; i < numberOfElements; i++)                             //скопируем все элементы из вектора-строка в строку матрицы
                rows[index].SetElement(i, v.GetElement(i));            
        }

        //Получение вектора-столбца по индексу
        public Vector GetColumn(int index)
        {
            int numberOfElements = GetNumberOfRows();                      //количество строк в матрице => количество элементов в векторе-столбце
            if (index >= GetNumberOfColumns() || index < 0)
                throw new IndexOutOfRangeException("Значение аргумента: " + index + ", оно не может быть меньше 0 или больше количества столбцов в матрице");
            Vector column = new Vector(numberOfElements);
            for (int i = 0; i < numberOfElements; i++)
                column.SetElement(i, rows[i].GetElement(index));           //каждый элемент столбца => index-элемент строки матрицы
            return column;
        }

        //Тракнспонирование матрицы
        public void Transpose()
        {
            int numberOfRows = GetNumberOfRows();                 //количество строк
            int numberOfColumns = GetNumberOfColumns();                 //количество столбцов

            Vector[] transposedRows = new Vector[numberOfColumns];     //массив векторов-строк транспонированной матрицы
            for (int i = 0; i < numberOfColumns; i++)
            {
                transposedRows[i] = GetColumn(i);         //каждому вектору-строке транспонированной матрицы присвоим вектор столбец текущей матрицы
            }
            rows = transposedRows;                        //изменим массив векторов-строк матрицы на транспонированный
        }

        //Умножение на скаляр
        public void Multiply(double number)
        {
            int numberOfRows = GetNumberOfRows();                         //количество векторов-строк
            for (int i = 0; i < numberOfRows; i++)
                rows[i].Multiply(number);              //каждую вектор-стороку умножим на число
        }

        //Вычисления определителя
        public double GetDeterminant()
        {
            int numberOfRows = GetNumberOfRows();
            int numberOfColumns = GetNumberOfColumns();
            if (numberOfColumns != numberOfRows)
                throw new Exception("Определитель можно вычислить только для квадратной матрицы");

            Matrix tmpMatrix = new Matrix(this);

            int permutations = 0; //количество перестановок строк
            double epsilon = 1e-9; //точность

            for (int i = 0; i < numberOfRows - 1; i++)
            {
                if (Math.Abs(tmpMatrix.rows[i].GetElement(i)) < epsilon) //Если элемент стоящий на диагонали равен 0
                {
                    //Находим строку снизу, с которой можно поменять местами текущую строку, чтобы элемент на диагонали не был равен 0
                    int t = 1;
                    while (i + t < numberOfRows && Math.Abs(tmpMatrix.rows[i + t].GetElement(i)) < epsilon)
                    {
                        t++;
                    }

                    if (i + t < numberOfRows)  //нашли строку, с которой нужно совершить перестановку текущей строки
                    {
                        //меняем местами строки i и t
                        Vector tmpVector = new Vector(tmpMatrix.rows[i]);
                        tmpMatrix.SetRow(i, tmpMatrix.rows[t]);
                        tmpMatrix.SetRow(t, tmpVector);

                        permutations++;   //Увеличиваем счетчик перестановок                    
                    }
                    else  //вышли за границу массива (перестановка не поможет избавиться от нуля на диагонали)
                    {
                        break;
                    }
                }

                //Шаг прямого хода метода Гауса
                for (int k = i + 1; k < numberOfRows; k++)
                {
                    double factor = tmpMatrix.rows[k].GetElement(i) / tmpMatrix.rows[i].GetElement(i);
                    //вычтем из k-й строки i-ю, умноженную на factor
                    Vector tmpVector = new Vector(tmpMatrix.rows[i]);
                    tmpVector.Multiply(factor);
                    tmpMatrix.rows[k].Subtract(tmpVector);
                }
            }

            //Определитель - произведение всех элементов, лежащих на диагонали матрицы, знак зависит от количества перестановк строк
            double determinant = Math.Pow(-1, permutations);
            for (int i = 0; i < numberOfRows; i++)
                determinant *= tmpMatrix.rows[i].GetElement(i);

            return determinant;
        }

        public override string ToString()       //Переопределение функции ToString, чтобы значения вектора выводились { {1, 2}, {3, 4} }
        {
            StringBuilder builder = new StringBuilder();    //Где будем строить нашу строку
            builder.Append("{ ");                           //добавление в строку первой { и пробела
            for (int i = 0; i < rows.Length; i++)           //Добавление в цикле в строку через запятую значений элементов
                builder.Append(rows[i]+", ");

            builder.Remove(builder.Length - 2, 1);             //удалим последнюю запятую
            builder.Append("}");                               //добавление в строку закрывающейся скобки
            return builder.ToString();                          //Возврат результата в виде строки
        }

        //Умножение на вектор
        public Vector Multiply(Vector v)
        {
            int numberOfRows = GetNumberOfRows();                        //количество строк
            int numberOfColumns = GetNumberOfColumns();                        //количество столбцов
            if (numberOfColumns != v.GetSize())
                throw new ArgumentException("Размерность вектора, переданного в качестве аргумента: " + v.GetSize() + ", не совпадает с количеством столбцов в матрице", "v");
                
            Vector resultVector = new Vector(numberOfRows);            //количество элементов в строке
            //каждый элемент вектора resultRows[0] - скалярное произведение вектора-строки матрицы и вектора v
            for (int i = 0; i < numberOfRows; i++)
                resultVector.SetElement(i, Vector.DotProduct(rows[i], v));

            return resultVector;
        }

        //сложение матриц
        public void Add(Matrix matrix)
        {
            int numberOfRows = GetNumberOfRows();                 //количество строк
            int numberOfColumns = GetNumberOfColumns();                 //количество столбцов
            if (numberOfRows != matrix.GetNumberOfRows() || numberOfColumns != matrix.GetNumberOfColumns())    //если количество столбцов и строк в матрицах не равны
                throw new ArgumentException("Размерность матрицы, переданной в качестве аргумента: [" + matrix.GetNumberOfRows() + ", " + matrix.GetNumberOfColumns() + "], не совпадает с исходной матрицией", "matrix");
                
            //складываем соответствующие элементы матриц построчно
            for (int i = 0; i < numberOfRows; i++)
                rows[i].Add(matrix.rows[i]);
        }

        //вычитание матриц
        public void Subtract(Matrix matrix)
        {
            int numberOfRows = GetNumberOfRows();                 //количество строк
            int numberOfColumns = GetNumberOfColumns();                 //количество столбцов
            if (numberOfRows != matrix.GetNumberOfRows() || numberOfColumns != matrix.GetNumberOfColumns())    //если количество столбцов и строк в матрицах не равны
                throw new ArgumentException("Размерность матрицы, переданной в качестве аргумента: [" + matrix.GetNumberOfRows() + ", " + matrix.GetNumberOfColumns() + "], не совпадает с исходной матрицией", "matrix");

            //вычитаем соответствующие элементы матриц построчно
            for (int i = 0; i < numberOfRows; i++)
                rows[i].Subtract(matrix.rows[i]);
        }

        //статический метод сложения матриц
        public static Matrix GetSum(Matrix matrix1, Matrix matrix2)
        {
            int numberOfRows = matrix1.GetNumberOfRows();                 //количество строк в matrix1
            int numberOfColumns = matrix1.GetNumberOfColumns();                 //количество столбцов в matrix1
            if (numberOfRows != matrix2.GetNumberOfRows() || numberOfColumns != matrix2.GetNumberOfColumns())    //если количество столбцов и строк в матрицах не равны
                throw new ArgumentException("Размерности матриц, переданных в качестве аргументов, не совпадают", "matrix1, matrix2");

            Matrix resultMatrix = new Matrix(matrix1);   //новая матрица создана копированием matrix1
            resultMatrix.Add(matrix2);

            return resultMatrix;
        }

        //статический метод вычитания матриц
        public static Matrix GetSubtract(Matrix matrix1, Matrix matrix2)
        {
            int numberOfRows = matrix1.GetNumberOfRows();                 //количество строк в matrix1
            int numberOfColumns = matrix1.GetNumberOfColumns();                 //количество столбцов в matrix1
            if (numberOfRows != matrix2.GetNumberOfRows() || numberOfColumns != matrix2.GetNumberOfColumns())    //если количество столбцов и строк в матрицах не равны
                throw new ArgumentException("Размерности матриц, переданных в качестве аргументов, не совпадают", "matrix1, matrix2");

            Matrix resultMatrix = new Matrix(matrix1);   //новая матрица создана копированием matrix1
            resultMatrix.Subtract(matrix2);

            return resultMatrix;
        }

        //статический метод умножения матриц
        public static Matrix GetMultiplication(Matrix matrix1, Matrix matrix2)
        {
            int numberOfRows1 = matrix1.GetNumberOfRows();                    //количество строк в matrix1
            int numberOfColumns1 = matrix1.GetNumberOfColumns();                 //количество столбцов в matrix1

            int numberOfRows2 = matrix2.GetNumberOfRows();                 //количество строк в matrix2
            int numberOfColumns2 = matrix2.GetNumberOfColumns();                 //количество столбцов в matrix2
            if (numberOfColumns1 != numberOfRows2)    //если количество столбцов в matrix1 не равно количеству строк в matrix2
                throw new ArgumentException("Размерности матриц, переданных в качестве аргументов, не совпадают", "matrix1, matrix2");

            Matrix resultMatrix = new Matrix(numberOfRows1, numberOfColumns2);   //размерность новой матрицы n1xm2;
            //каждый элемент новый матрицы - скалярное произведение соответствующих строки матрицы matrix1 и столбца matrix2
            for (int i = 0; i < numberOfRows1; i++)
            {
                Vector rowOfMatrix1 = matrix1.GetRow(i);
                for (int j = 0; j < numberOfColumns2; j++)
                {
                    double val = Vector.DotProduct(rowOfMatrix1, matrix2.GetColumn(j));
                    resultMatrix.rows[i].SetElement(j, val);
                }
            }

            return resultMatrix;
        }
    }

    class MatrixProgram
    {
        static void Main(string[] args)
        {
            Matrix m1 = new Matrix(new double[,] {{1,2,3}, {4,5,6}});
            Console.WriteLine("Исходная матрица1 {0}", m1);
            Console.WriteLine("Количество строк и столбцов матрицы1 {0}, {1}", m1.GetNumberOfRows(), m1.GetNumberOfColumns());
            Console.WriteLine("Вторая строка матрицы1 {0}", m1.GetRow(1));
            Console.WriteLine("Второй столбец матрицы1 {0}", m1.GetColumn(1));

            m1.Transpose();
            Console.WriteLine("Транспонированная матрица1 {0}", m1);
            m1.Multiply(3);
            Console.WriteLine("Матрица1 умноженная на 3 {0}", m1);

            Vector v = new Vector(new double[] { 3, 5 });
            Console.WriteLine("Матрица1 умноженная на вектор {0} = {1}", v, m1.Multiply(v));

            Matrix m2 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 7, 6 }, {7, 3, 9 } });
            Console.WriteLine("Исходная матрица2 {0}", m2);
            Console.WriteLine("Определитель матрицы2 {0}", m2.GetDeterminant());

            Matrix m3 = new Matrix(new double[,] { { 5, 2, 8 }, { 2, 2, 4 }, { 0, 6, 5 } });
            Console.WriteLine("Исходная матрица3 {0}", m3);

            m2.Add(m3);
            Console.WriteLine("Матрица2 += Матрица3 = {0}", m2);
            m3.Subtract(m2);
            Console.WriteLine("Матрица3 -= Матрица2 = {0}", m3);

            Console.WriteLine("Статические методы");
            Console.WriteLine("Матрица2 + Матрица3 = {0}", Matrix.GetSum(m2, m3));
            Console.WriteLine("Матрица2 - Матрица3 = {0}", Matrix.GetSubtract(m2, m3));
            Console.WriteLine("Матрица2 * Матрица3 = {0}", Matrix.GetMultiplication(m2, m3));

            Console.ReadLine();
        }
    }
}
