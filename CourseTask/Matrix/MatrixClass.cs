using System;
using System.Text;
using VectorN;

namespace Matrix
{
    public class Matrix
    {
        private Vector[] rows;

        public Matrix(int NumberOfRows, int NumbersOfElements)
        {
            if ((NumberOfRows <= 0) || (NumbersOfElements <= 0))
            {
                throw new ArgumentException("Значение аргумента не может быть меньше или равно 0");
            }
            rows = new Vector[NumberOfRows];

            for (int i = 0; i < NumberOfRows; i++)
            {
                rows[i] = new Vector(NumbersOfElements);
            }
        }

        public Matrix(Matrix m)
        {
            int NumbersOfRows = m.rows.Length;
            if (NumbersOfRows <= 0)
            {
                throw new ArgumentException("Значение аргумента не может быть меньше или равно 0");
            }
            rows = new Vector[NumbersOfRows];

            for (int i = 0; i < NumbersOfRows; i++)
            {
                rows[i] = new Vector(m.rows[i]);
            }
        }

        public Matrix(double[,] array)
        {
            int NumbersOfRows = array.GetLength(0);
            int NumbersOfElements = array.GetLength(1);
            if ((NumbersOfRows <= 0) || (NumbersOfElements <= 0))
            {
                throw new ArgumentException("Значение аргумента не может быть меньше или равно 0");
            }
            rows = new Vector[NumbersOfRows];

            for (int i = 0; i < NumbersOfRows; i++)
            {
                rows[i] = new Vector(NumbersOfElements);

                for (int j = 0; j < NumbersOfElements; j++)
                {
                    rows[i].SetElement(j, array[i, j]);

                }
            }
        }

        public Matrix(Vector[] vectors)
        {
            int n = vectors.Length;
            rows = new Vector[n];

            for (int i = 0; i < n; i++)
            {
                rows[i] = new Vector(vectors[i]);
            }
        }
        public int GetNumberOfRows()
        {
            return rows.Length;
        }
        public int GetNumberOfElements()
        {
            return rows[0].GetSize();
        }

        public Vector GetRow(int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Значение аргумента не может быть меньше или равно 0");
            }
            if (index >= GetNumberOfRows())
            {
                throw new ArgumentException("Количество строк " + GetNumberOfRows() + " в матрице меньше, чем в Index " + index);
            }
            Vector Result = new Vector(rows[index]);
            return Result;
        }

        public void SetRow(int index, Vector v)
        {
            if (v.GetLength() != rows.GetLength(index))
            {
                throw new ArgumentException("размер вектора-аргумента отличается от размера вектора в матрице");
            }
            if (index < 0)
            {
                throw new ArgumentException("Значение аргумента не может быть меньше или равно 0");
            }
            if (GetNumberOfElements() < v.GetSize())
            {
                throw new ArgumentException("Размер массива больше количества столбцов в матрице");
            }

            if (index >= GetNumberOfRows())
            {
                throw new ArgumentException("Количество строк " + GetNumberOfRows() + " в матрице меньше, чем в Index  " + index);
            }

            int NumbersOfElements = v.GetSize();

            for (int i = 0; i < NumbersOfElements; i++)
            {
                rows[index].SetElement(i, v.GetElement(i));
            }

            for (int i = NumbersOfElements; i < GetNumberOfElements(); i++)
            {
                rows[index].SetElement(i, 0);
            }
        }

        public Vector GetColumn(int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Значение аргумента не может быть меньше или равно 0");
            }
            int NumbersOfRows = GetNumberOfRows();

            if (index >= GetNumberOfElements())
            {
                throw new ArgumentException("Количество столбцов " + NumbersOfRows + " в матрице меньше, чем в Index " + index);
            }

            Vector column = new Vector(NumbersOfRows);

            for (int i = 0; i < NumbersOfRows; i++)
            {
                column.SetElement(i, rows[i].GetElement(index));
            }

            return column;
        }

        public void Transpose()
        {
            int NumberOfRows = GetNumberOfRows();
            int NumberOfElements = GetNumberOfElements();

            Vector[] transposedRows = new Vector[NumberOfElements];

            for (int i = 0; i < NumberOfElements; i++)
            {
                transposedRows[i] = GetColumn(i);
            }

            rows = transposedRows;
        }

        public void MultiplicationVectors(double number)
        {
            int NumbersOfRows = GetNumberOfRows();

            for (int i = 0; i < NumbersOfRows; i++)
            {
                rows[i].Multiply(number);
            }
        }

        public double GetDeterminant()
        {
            int n = GetNumberOfRows();
            int m = GetNumberOfElements();

            if (m != n)
            {
                throw new Exception("Определитель можно вычислить только для квадратной матрицы");
            }

            Matrix tmpMatrix = new Matrix(this);

            int permutations = 0;
            double epsilon = 1e-9;

            for (int i = 0; i < n - 1; i++)
            {
                if (Math.Abs(tmpMatrix.rows[i].GetElement(i)) < epsilon)
                {
                    int t = 1;

                    while (i + t < n && Math.Abs(tmpMatrix.rows[i + t].GetElement(i)) < epsilon)
                    {
                        t++;
                    }

                    if (i + t < n)
                    {
                        Vector tmpVector = new Vector(tmpMatrix.rows[i]);
                        tmpMatrix.SetRow(i, tmpMatrix.rows[t]);
                        tmpMatrix.SetRow(t, tmpVector);

                        permutations++;
                    }
                    else
                    {
                        break;
                    }
                }

                for (int k = i + 1; k < n; k++)
                {
                    double factor = tmpMatrix.rows[k].GetElement(i) / tmpMatrix.rows[i].GetElement(i);

                    Vector tmpVector = new Vector(tmpMatrix.rows[i]);
                    tmpVector.Multiply(factor);
                    tmpMatrix.rows[k].Subtract(tmpVector);
                }
            }

            double determinant = Math.Pow(-1, permutations);

            for (int i = 0; i < n; i++)
            {
                determinant *= tmpMatrix.rows[i].GetElement(i);
            }

            return determinant;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append("{ ");

            for (int i = 0; i < rows.Length; i++)
            {
                builder.Append(rows[i]);
                builder.Append(", ");

            }

            builder.Remove(builder.Length - 2, 1);
            builder.Append("}");

            return builder.ToString();
        }

        public void Multiply(Vector v)
        {
            int NumbersOfRow = GetNumberOfRows();
            int NumbersOFElements = GetNumberOfElements();

            if (NumbersOFElements != v.GetSize())
            {
                throw new ArgumentException("Размерность вектора не совпадает с количеством столбцов в матрице");
            }

            Vector[] resultRows = new Vector[1];
            resultRows[0] = new Vector(NumbersOfRow);

            for (int i = 0; i < NumbersOfRow; i++)
            {
                resultRows[0].SetElement(i, Vector.DotProduct(rows[i], v));
            }

            rows = resultRows;

        }

        public void Add(Matrix matrix)
        {
            int NumberOfRows = GetNumberOfRows();
            int NumberOfElements = GetNumberOfElements();

            if (NumberOfRows != matrix.GetNumberOfRows() || NumberOfElements != matrix.GetNumberOfElements())
            {
                throw new ArgumentException("Размерности матриц не совпадают");
            }

            for (int i = 0; i < NumberOfRows; i++)
            {
                rows[i].Add(matrix.rows[i]);
            }
        }

        public void Subtract(Matrix matrix)
        {
            int NumbersOfRows = GetNumberOfRows();
            int NumbersOfElement = GetNumberOfElements();

            if (NumbersOfRows != matrix.GetNumberOfRows() || NumbersOfElement != matrix.GetNumberOfElements())
            {
                throw new ArgumentException("Размерности матриц не совпадают");
            }

            for (int i = 0; i < NumbersOfRows; i++)
            {
                rows[i].Subtract(matrix.rows[i]);
            }
        }

        public static Matrix AddMatrixes(Matrix matrix1, Matrix matrix2)
        {
            int NumberOfRows = matrix1.GetNumberOfRows();
            int NumbersOfElements = matrix1.GetNumberOfElements();

            if (NumberOfRows != matrix2.GetNumberOfRows() || NumbersOfElements != matrix2.GetNumberOfElements())
            {
                throw new ArgumentException("Размерности матриц не совпадают");
            }

            Matrix resultMatrix = new Matrix(matrix1);
            resultMatrix.Add(matrix2);

            return resultMatrix;
        }

        public static Matrix SubtractMatrixes(Matrix matrix1, Matrix matrix2)
        {
            int NombersOfRows = matrix1.GetNumberOfRows();
            int NumbersOfElments = matrix1.GetNumberOfElements();

            if (NombersOfRows != matrix2.GetNumberOfRows() || NumbersOfElments != matrix2.GetNumberOfElements())
            {
                throw new ArgumentException("Размерности матриц не совпадают");
            }

            Matrix resultMatrix = new Matrix(matrix1);
            resultMatrix.Subtract(matrix2);

            return resultMatrix;
        }

        public static Matrix GetComposition(Matrix matrix1, Matrix matrix2)
        {
            int NumbersOfRowsFirestMatrix = matrix1.GetNumberOfRows();
            int NumberOfElmentsFirstMatrix = matrix1.GetNumberOfElements();

            int NumbersOfRowsSecondMatrix = matrix2.GetNumberOfRows();
            int NumbersOfElementsSecondMatrix = matrix2.GetNumberOfElements();

            if (NumberOfElmentsFirstMatrix != NumbersOfRowsSecondMatrix)
            {
                throw new ArgumentException("Размерности матриц не совпадают");
            }

            Matrix resultMatrix = new Matrix(NumbersOfRowsFirestMatrix, NumbersOfElementsSecondMatrix);

            for (int i = 0; i < NumbersOfRowsFirestMatrix; i++)
            {
                for (int j = 0; j < NumbersOfElementsSecondMatrix; j++)
                {
                    double val = Vector.DotProduct(matrix1.GetRow(i), matrix2.GetColumn(j));
                    resultMatrix.rows[i].SetElement(j, val);
                }
            }

            return resultMatrix;
        }
    }
}
