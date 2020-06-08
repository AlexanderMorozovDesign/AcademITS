using System;
using System.Text;

namespace VectorNamespace
{
    public class Vector
    {
        private double[] elements;  //������ ��� �������� ��������� �������

        //������������
        public Vector(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("�������� ���������: "+length+", ��� ������ ���� ������ 0", "length");
            }

            elements = new double[length]; //������� ������ ��� ������ ��������� �������
        }

        public Vector(Vector initialVector)     //����������� �����������
        {
            int length = initialVector.GetSize();       //����������� �������, ������� ����������
            elements = new double[length];   //������� ������ ��� ������ ��������� �������

            Array.Copy(initialVector.elements, elements, length);
        }

        public Vector(double[] array)
        {
            int length = array.Length;              //����������� �������, ������� ���������� � ������
            if (length == 0)
            {
                throw new ArgumentException("����� ������� ��� �������� �������: "+length+", ���������� ������� � ����� ������������", "array");
            }

            elements = new double[length];          //������� ������ ��� ������ ��������� �������
            Array.Copy(array, elements, length);
        }

        public Vector(int length, double[] array)
        {
            if (length <= 0)
            {
                throw new ArgumentException("�������� ��������� length �� ����� ���� ������ ��� ����� 0");
            }

            elements = new double[length];              //������� ������ ��� ������ ��������� �������
            if (length <= array.Length)
            {
                Array.Copy(array, elements, length);
            }
            else
            {
                Array.Copy(array, elements, array.Length);
            }
        }

        public int GetSize()                     //���������� ������ �������
        {
            return elements.Length;
        }

        public override string ToString()       //��������������� ������� ToString, ����� �������� ������� ���������� { 1, 2, 3, 4 }
        {
            StringBuilder builder = new StringBuilder();    //��� ����� ������� ���� ������
            builder.Append("{ ");                           //���������� � ������ ������ { � �������
            for (int i = 0; i < elements.Length; i++)           //���������� � ����� � ������ ����� ������� �������� ���������
                builder.Append(elements[i]+", ");

            builder.Remove(builder.Length - 2, 1);             //������ ��������� �������

            builder.Append("}");                               //���������� � ������ ������������� ������

            return builder.ToString();                          //������� ���������� � ���� ������
        }

        public void Add(Vector vectorToAdd)                    //����������� � ������� ������� �������
        {
            int length = Math.Max(elements.Length, vectorToAdd.GetSize());

            if (length > elements.Length)           //����������� ����� �������� �������, ���� ��� ����������
                Array.Resize(ref elements, length);

            for (int i = 0; i < vectorToAdd.GetSize(); i++)   //��������� � ��������� ������� �������� ������� �������
                elements[i] += vectorToAdd.elements[i];
        }

        public void Subtract(Vector vectorToSubtract)                    //��������� �� ������� ������� �������
        {
            int length = Math.Max(elements.Length, vectorToSubtract.GetSize());

            if (length > elements.Length)           //����������� ����� �������� �������, ���� ��� ����������
                Array.Resize(ref elements, length);

            for (int i = 0; i < vectorToSubtract.GetSize(); i++)   //�������� �� ��������� ������� �������� ������� �������
                elements[i] -= vectorToSubtract.elements[i];
        }

        public void Multiply(double number)              //��������� ������� �� ������
        {
            for (int i = 0; i < elements.Length; i++)   //�������� ������ ��������� ������� �� ���������� ������
                elements[i] *= number;
        }

        public void Reverse()                   //�������� ������� (��������� ���� ��������� �� -1)
        {
            Multiply(-1);
        }

        public double GetModulus()                //��������� ����� ������� (�������: ������ �� ����� ��������� ���� ���������)
        {
            double modulus = 0;
            foreach (double element in elements)  //���������� ����� ��������� ���� ��������� �������
                modulus += element * element;
            //���������� ����� �� ����� ���������
            return Math.Sqrt(modulus); ;
        }

        public double GetElement(int index)    //��������� ���������� ������� �� �������
        {
            return elements[index];
        }

        public void SetElement(int index, double value)    //��������� ���������� ������� �� �������
        {
            elements[index] = value;
        }

        public override bool Equals(object obj)                      //��������������� ������ Equals
        {
            // ��������� ��� �������� ��� ������
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            // ������� null � ������� ������ �������
            if (ReferenceEquals(obj, null) || obj.GetType() != GetType())
            {
                return false;
            }

            // ������� ������ � Vector            
            Vector otherVector = (Vector)obj;

            // ��������� ��������� ������������
            if (elements.Length != otherVector.GetSize())
                return false;

            //������� ����������� ���������� ��������
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] != otherVector.elements[i])    //���� �������� �� �����, ������ � ������ false
                    return false;
            }
            return true;   //�� ����� ����� ����� ����� ������, ���� ������� �����
        }

        public override int GetHashCode()                      //��������������� ������ GetHashCode
        {
            int prime = 23;                                    //�������� ������� �����
            int hash = 1;
            foreach (double elemnt in elements)                 //� ����� �� ��������� �� ������ ��������� hash
                hash = prime * hash + elemnt.GetHashCode();

            return hash;
        }

        public static Vector GetSum(Vector vector1, Vector vector2)   //����������� ����� �������� ���� ��������
        {
            Vector result = new Vector(vector1);                 //��������� ��� �������� ������� ������� � ������ sum

            result = new Vector(vector1);                 //��������� ��� �������� ������� ������� � ������ sum
            result.Add(vector2);                          //�������� � ������� sum ������ ������
            return result;
        }

        public static Vector GetSubtract(Vector vector1, Vector vector2)   //����������� ����� ��������� ���� ��������
        {
            Vector result = new Vector(vector1);                 //��������� ��� �������� ������� ������� � ������ result;
            result.Subtract(vector2);                     //������ �� ������� result ������ ������, ���� ����������� vector2 ������, �� vector1 ����������
            return result;
        }

        public static double DotProduct(Vector vector1, Vector vector2)   //����������� ����� ���������� ���������� ������������ ���� �������
        {
            int length = Math.Min(vector1.GetSize(), vector2.GetSize());       //���������� ��������� � ��������� ������������ - ����������� ������ ���������� �������, �.�. ����������� �������� ����
            double result = 0;

            for (int i = 0; i < length; i++)                                  //���������� ����� �� ������������ ��������� ���� �������� � ����������� ���������
                result += vector1.GetElement(i) * vector2.GetElement(i);

            return result;
        }
    }
}