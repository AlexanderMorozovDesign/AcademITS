using System;
using System.Text;

namespace Matrix
{
    class MatrixProgram
    {
        class Vector
        {
            private double[] elements;  

            public Vector(int n)
            {
                if (n <= 0)
                {
                    throw new ArgumentException("Значение аргумента не может быть меньше или равно 0");
                }

                elements = new double[n];
            }

            public Vector(Vector v)     
            {
                int n = v.GetSize();       
                elements = new double[n];

                for (int i = 0; i < n; i++)
                {
                    elements[i] = v.elements[i];
                }
            }

            public Vector(double[] array)
            {
                int n = array.Length;             
                elements = new double[n];       

                for (int i = 0; i < n; i++)
                {
                    elements[i] = array[i];
                }
            }

            public Vector(int n, double[] array)
            {
                if (n <= 0)
                {
                    throw new ArgumentException("Значение аргумента не может быть меньше или равно 0");
                }

                elements = new double[n];             

                if (n <= array.Length)
                {
                    for (int i = 0; i < n; i++)
                    {
                        elements[i] = array[i];
                    }
                }
                else
                {
                    for (int i = 0; i < array.Length; i++)
                    {
                        elements[i] = array[i];
                    }

                    for (int i = array.Length; i < n; i++)
                    {
                        elements[i] = 0;
                    }
                }
            }

            public int GetSize()                    
            {
                return elements.Length;
            }

            public override string ToString()      
            {
                StringBuilder builder = new StringBuilder();    
                builder.Append("{ ");  
                
                for (int i = 0; i < elements.Length; i++)
                {
                    builder.AppendFormat("{0}, ", elements[i].ToString());
                }

                builder.Remove(builder.Length - 2, 1);             

                builder.Append("}");                               

                return builder.ToString();                          
            }

            public void Add(Vector v)                    
            {
                int n = Math.Min(elements.Length, v.GetSize());

                for (int i = 0; i < n; i++)
                {
                    elements[i] += v.elements[i];
                }
            }

            public void Subtract(Vector v)                   
            {
                int n = Math.Min(elements.Length, v.GetSize());

                for (int i = 0; i < n; i++)
                {
                    elements[i] -= v.elements[i];
                }
            }

            public void Multiply(double number)              
            {
                for (int i = 0; i < elements.Length; i++)
                {
                    elements[i] *= number;
                }
            }

            public void Reverse()                 
            {
                Multiply(-1);
            }

            public double GetLength()               
            {
                double length = 0;
                for (int i = 0; i < elements.Length; i++)
                {
                    length += elements[i] * elements[i];
                }

                length = Math.Sqrt(length);           

                return length;
            }

            public double GetElement(int index)    
            {
                return elements[index];
            }

            public void SetElement(int index, double value)    
            {
                elements[index] = value;
            }

            public override bool Equals(object obj)                      
            {
                if (ReferenceEquals(obj, this))
                {
                    return true;
                }

                if (ReferenceEquals(obj, null) || obj.GetType() != this.GetType())
                {
                    return false;
                }
          
                Vector v = (Vector)obj;

                if (elements.Length != v.GetSize())
                {
                    return false;
                }

                double epsilon = 10e-6;  
                
                for (int i = 0; i < elements.Length; i++)
                {
                    if (Math.Abs(elements[i] - v.elements[i]) > epsilon)    
                        return false;
                }
                return true;   
            }

            public override int GetHashCode()                      
            {
                int prime = 23;                                    
                int hash = 1;

                for (int i = 0; i < elements.Length; i++)
                {
                    hash = prime * hash + elements[i].GetHashCode();
                }

                return hash;
            }

            public static Vector AddVectors(Vector vector1, Vector vector2)   
            {
                Vector sum;

                if (vector1.GetSize() > vector2.GetSize())       
                {
                    sum = new Vector(vector1);                 
                    sum.Add(vector2); 
                    
                    return sum;
                }

                sum = new Vector(vector2);                  
                sum.Add(vector1);     
                
                return sum;
            }

            public static Vector SubtractVectors(Vector vector1, Vector vector2)   
            {
                Vector result;

                if (vector1.GetSize() > vector2.GetSize())       
                {
                    result = new Vector(vector1);                 
                    result.Subtract(vector2); 
                    
                    return result;
                }

                result = new Vector(vector2);                   
                result.Subtract(vector1);                        
                result.Reverse(); 
                
                return result;
            }

            public static double DotProduct(Vector vector1, Vector vector2)   
            {
                int n = Math.Min(vector1.GetSize(), vector2.GetSize());       
                double sum = 0;

                for (int i = 0; i < n; i++)
                {
                    sum += vector1.GetElement(i) * vector2.GetElement(i);
                }

                return sum;
            }
        }

        class Matrix
        {
            private Vector[] rows;        

            public Matrix(int n, int m)
            {
                rows = new Vector[n];  
                
                for (int i = 0; i < n; i++)
                {
                    rows[i] = new Vector(m);
                }
            }

            public Matrix(Matrix m)               
            {
                int n = m.rows.Length;                  
                rows = new Vector[n];  
                
                for (int i = 0; i < n; i++)
                {
                    rows[i] = new Vector(m.rows[i]);
                }
            }

            public Matrix(double[,] array)
            {
                int n = array.GetLength(0);               
                int m = array.GetLength(1);          

                rows = new Vector[n]; 
                
                for (int i = 0; i < n; i++)
                {
                    rows[i] = new Vector(m); 
                    
                    for (int j = 0; j < m; j++)
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

            public int GetSize(int dimention)
            {
                if (dimention == 0)
                {
                    return rows.Length;
                }

                return rows[0].GetSize();          
            }

            public Vector GetRow(int index)
            {
                if (index >= GetSize(0))
                    throw new ArgumentException("Количество строк в матрице меньше, чем " + index.ToString());
                return rows[index];
            }

            public void SetRow(int index, Vector v)
            {
                if (GetSize(1) < v.GetSize())
                {
                    throw new ArgumentException("Размер массива больше количества столцов в матрице");
                }

                if (index >= GetSize(0))
                {
                    throw new ArgumentException("Количество строк в матрице меньше, чем " + index.ToString());
                }

                int n = v.GetSize();  
                
                for (int i = 0; i < n; i++)
                {
                    rows[index].SetElement(i, v.GetElement(i));
                }

                for (int i = n; i < GetSize(1); i++)
                {
                    rows[index].SetElement(i, 0);
                }
            }

            public Vector GetColumn(int index)
            {
                int n = GetSize(0); 
                
                if (index >= GetSize(1))
                {
                    throw new ArgumentException("Количество столбцов в матрице меньше, чем " + index.ToString());
                }

                Vector column = new Vector(n);

                for (int i = 0; i < n; i++)
                {
                    column.SetElement(i, rows[i].GetElement(index));
                }
    
                return column;
            }

            public void Transpose()
            {
                int n = GetSize(0);                 
                int m = GetSize(1);                

                Vector[] transposedRows = new Vector[m];  

                for (int i = 0; i < m; i++)
                {
                    transposedRows[i] = GetColumn(i);         
                }
                rows = transposedRows;                        
            }

            public void Scale(double number)
            {
                int n = GetSize(0);  
                
                for (int i = 0; i < n; i++)
                {
                    rows[i].Multiply(number);
                }
            }

            public double GetDeterminant()
            {
                int n = GetSize(0);
                int m = GetSize(1);

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
                    builder.AppendFormat("{0}, ", rows[i].ToString());
                }

                builder.Remove(builder.Length - 2, 1);            
                builder.Append("}"); 
                
                return builder.ToString();                          
            }

            public void Multiply(Vector v)
            {
                int n = GetSize(0);                     
                int m = GetSize(1);  
                
                if (m != v.GetSize())
                {
                    throw new ArgumentException("Размерность вектора не совпадает с количеством столбцов в матрице");
                }

                Vector[] resultRows = new Vector[1];    
                resultRows[0] = new Vector(n);            
                                                         
                for (int i = 0; i < n; i++)
                {
                    resultRows[0].SetElement(i, Vector.DotProduct(rows[i], v));
                }

                rows = resultRows;

                Transpose();
            }

            public void Add(Matrix summand)
            {
                int n = GetSize(0);                 
                int m = GetSize(1);      
                
                if (n != summand.GetSize(0) || m != summand.GetSize(1))
                {
                    throw new ArgumentException("Размерности матриц не совпадают");
                }

                for (int i = 0; i < n; i++)
                {
                    rows[i].Add(summand.rows[i]);
                }
            }

            public void Subtract(Matrix subtrahend)
            {
                int n = GetSize(0);                 
                int m = GetSize(1); 
                
                if (n != subtrahend.GetSize(0) || m != subtrahend.GetSize(1))
                {
                    throw new ArgumentException("Размерности матриц не совпадают");
                }

                for (int i = 0; i < n; i++)
                {
                    rows[i].Subtract(subtrahend.rows[i]);
                }
            }

            public static Matrix AddMatrixes(Matrix matrix1, Matrix matrix2)
            {
                int n = matrix1.GetSize(0);                 
                int m = matrix1.GetSize(1);    
                
                if (n != matrix2.GetSize(0) || m != matrix2.GetSize(1))
                {
                    throw new ArgumentException("Размерности матриц не совпадают");
                }

                Matrix resultMatrix = new Matrix(matrix1);  
                resultMatrix.Add(matrix2);

                return resultMatrix;
            }

            public static Matrix SubtractMatrixes(Matrix matrix1, Matrix matrix2)
            {
                int n = matrix1.GetSize(0);                 
                int m = matrix1.GetSize(1);    
                
                if (n != matrix2.GetSize(0) || m != matrix2.GetSize(1))
                {
                    throw new ArgumentException("Размерности матриц не совпадают");
                }

                Matrix resultMatrix = new Matrix(matrix1); 
                resultMatrix.Subtract(matrix2);

                return resultMatrix;
            }

            public static Matrix MultiplyMatrixes(Matrix matrix1, Matrix matrix2)
            {
                int n1 = matrix1.GetSize(0);                 
                int m1 = matrix1.GetSize(1);                

                int n2 = matrix2.GetSize(0);                 
                int m2 = matrix2.GetSize(1);     
                
                if (m1 != n2)
                {
                    throw new ArgumentException("Размерности матриц не совпадают");
                }

                Matrix resultMatrix = new Matrix(n1, m2);   
                                                           
                for (int i = 0; i < n1; i++)
                {
                    for (int j = 0; j < m2; j++)
                    {
                        double val = Vector.DotProduct(matrix1.GetRow(i), matrix2.GetColumn(j));
                        resultMatrix.rows[i].SetElement(j, val);
                    }
                }

                return resultMatrix;
            }
        }

        static void Main(string[] args)
        {
            Matrix m1 = new Matrix(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            Console.WriteLine("Исходная матрица1 {0}", m1);
            Console.WriteLine("Количество строк и столбцов матрицы1 {0}, {1}", m1.GetSize(0), m1.GetSize(1));
            Console.WriteLine("Вторая строка матрицы1 {0}", m1.GetRow(1));
            Console.WriteLine("Второй столбец матрицы1 {0}", m1.GetColumn(1));

            m1.Transpose();
            Console.WriteLine("Транспонированная матрица1 {0}", m1);
            m1.Scale(3);
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
            Console.WriteLine("Матрица2 * Матрица3 = {0}", Matrix.MultiplyMatrixes(m2, m3));

            Console.ReadLine();
        }
    }
}
