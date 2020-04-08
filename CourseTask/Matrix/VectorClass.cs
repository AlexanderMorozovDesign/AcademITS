using System;
using System.Text;

namespace VectorClass
{
    public class Vector
    {
        private double[] elements;

        public Vector(int NumberOfElements)
        {
            if (NumberOfElements <= 0)
            {
                throw new ArgumentException("Значение аргумента NumberOfElement: " + NumberOfElements + " не может быть меньше или равно 0");
            }

            elements = new double[NumberOfElements];
        }

        public Vector(Vector v)
        {
            int NumberOfElements = v.GetSize();
            if (NumberOfElements <= 0)
            {
                throw new ArgumentException("Значение аргумента NumberOfElement: " + NumberOfElements + " не может быть меньше или равно 0");
            }

            elements = new double[NumberOfElements];

            for (int i = 0; i < NumberOfElements; i++)
            {
                elements[i] = v.elements[i];
            }
        }

        public Vector(double[] array)
        {
            int NumberOfElements = array.Length;
            if (NumberOfElements <= 0)
            {
                throw new ArgumentException("Значение аргумента NumberOfElement: " + NumberOfElements + " не может быть меньше или равно 0");
            }

            elements = new double[NumberOfElements];

            for (int i = 0; i < NumberOfElements; i++)
            {
                elements[i] = array[i];
            }
        }

        public Vector(int NumberOfElements, double[] array)
        {
            if (NumberOfElements <= 0)
            {
                throw new ArgumentException("Значение аргумента NumberOfElement: " + NumberOfElements + " не может быть меньше или равно 0");
            }

            elements = new double[NumberOfElements];

            if (NumberOfElements <= array.Length)
            {
                for (int i = 0; i < NumberOfElements; i++)
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

                for (int i = array.Length; i < NumberOfElements; i++)
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
                builder.Append(elements[i]);
                builder.Append(", ");
            }

            builder.Remove(builder.Length - 2, 1);

            builder.Append("}");

            return builder.ToString();
        }

        public void Add(Vector vector)
        {
            int NumberOfElements = Math.Min(elements.Length, vector.GetSize());
            if (NumberOfElements <= 0)
            {
                throw new ArgumentException("Значение аргумента NumberOfElement: " + NumberOfElements + " не может быть меньше или равно 0");
            }
            for (int i = 0; i < NumberOfElements; i++)
            {
                elements[i] += vector.elements[i];
            }
        }

        public void Subtract(Vector vector)
        {
            int NumbersOfElements = Math.Min(elements.Length, vector.GetSize());
            if (NumbersOfElements <= 0)
            {
                throw new ArgumentException("Значение аргумента NumbersOfElements: " + NumbersOfElements + " не может быть меньше или равно 0");
            }
            for (int i = 0; i < NumbersOfElements; i++)
            {
                elements[i] -= vector.elements[i];
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
            foreach (double element in elements)
            {
                length++;
            }
            return length;
        }

        public double GetElement(int index)
        {
            if (index < 0)
            {
                throw new ArgumentException("Значение аргумента index: " + index + " не может быть меньше или равно 0");
            }
            return elements[index];
        }

        public void SetElement(int index, double value)
        {
            if (index < 0)
            {
                throw new ArgumentException("Значение аргумента index: " + index + " не может быть меньше или равно 0");
            }
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

            Vector vector = (Vector)obj;

            if (elements.Length != vector.GetSize())
            {
                return false;
            }

            double epsilon = 10e-6;

            for (int i = 0; i < elements.Length; i++)
            {
                if (Math.Abs(elements[i] - vector.elements[i]) > epsilon)
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

            foreach (double element in elements)
            {
                hash = prime * hash + element.GetHashCode();
            }

            return hash;
        }

        public static Vector GetSumm(Vector vector1, Vector vector2)
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

        public static Vector GetDifference(Vector vector1, Vector vector2)
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
            int NumberOfElements = Math.Min(vector1.GetSize(), vector2.GetSize());
            double result = 0;

            for (int i = 0; i < NumberOfElements; i++)
            {
                result += vector1.GetElement(i) * vector2.GetElement(i);
            }

            return result;
        }
    }
}
