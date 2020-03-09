using System;
using System.Text;

namespace Vector
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
                {
                    return false;
                }
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
            Console.WriteLine("\nИзменим третий (индексация с нуля) элемент вектора на 10");
            vector1.SetElement(3, 10);
            Console.WriteLine("Вектор стал = {0}", vector1);
            Console.WriteLine("Пятый (индексация с нуля) элемент вектора = {0}", vector1.GetElement(5));

            Console.Write("\nВведите количество элементов во втором векторе: ");
            int n = Int32.Parse(Console.ReadLine());
            Vector vector2 = new Vector(n);

            for (int i = 0; i < n; i++)
            {
                Console.Write("Введите {0}-ый элемент вектора: ", i);
                double value = Double.Parse(Console.ReadLine());
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

            Vector vec3 = Vector.AddVectors(vector1, vector2);
            Console.WriteLine("Статический метод сложения двух векторов (Вектор1 + Вектор2): {0}", Vector.AddVectors(vector1, vector2));
            Console.WriteLine("Статический метод вычитания двух векторов (Вектор1 - Вектор2): {0}", Vector.SubtractVectors(vector1, vector2));
            Console.WriteLine("Статический метод получения скалярного произведения двух векторов (Вектор1, Вектор2): {0}", Vector.DotProduct(vector1, vector2));

            Console.ReadLine();
        }
    }
}
