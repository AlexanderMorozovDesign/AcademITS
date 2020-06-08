using System;
using System.Text;

namespace VectorNamespace
{
    public class Vector
    {
        private double[] elements;  //массив для хранения элементов вектора

        //Конструкторы
        public Vector(int length)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Значение аргумента: "+length+", оно должно быть больше 0", "length");
            }

            elements = new double[length]; //Выделим память под массив элементов вектора
        }

        public Vector(Vector initialVector)     //конструктор копирования
        {
            int length = initialVector.GetSize();       //размерность вектора, который копируется
            elements = new double[length];   //Выделим память под массив элементов вектора

            Array.Copy(initialVector.elements, elements, length);
        }

        public Vector(double[] array)
        {
            int length = array.Length;              //размерность массива, который копируется в вектор
            if (length == 0)
            {
                throw new ArgumentException("Длина массива для создания вектора: "+length+", невозможно создать с такой размерностью", "array");
            }

            elements = new double[length];          //Выделим память под массив элементов вектора
            Array.Copy(array, elements, length);
        }

        public Vector(int length, double[] array)
        {
            if (length <= 0)
            {
                throw new ArgumentException("Значение аргумента length не может быть меньше или равно 0");
            }

            elements = new double[length];              //Выделим память под массив элементов вектора
            if (length <= array.Length)
            {
                Array.Copy(array, elements, length);
            }
            else
            {
                Array.Copy(array, elements, array.Length);
            }
        }

        public int GetSize()                     //Возвращает размер вектора
        {
            return elements.Length;
        }

        public override string ToString()       //Переопределение функции ToString, чтобы значения вектора выводились { 1, 2, 3, 4 }
        {
            StringBuilder builder = new StringBuilder();    //Где будем строить нашу строку
            builder.Append("{ ");                           //добавление в строку первой { и пробела
            for (int i = 0; i < elements.Length; i++)           //Добавление в цикле в строку через запятую значений элементов
                builder.Append(elements[i]+", ");

            builder.Remove(builder.Length - 2, 1);             //удалим последнюю запятую

            builder.Append("}");                               //добавление в строку закрывающейся скобки

            return builder.ToString();                          //Возврат результата в виде строки
        }

        public void Add(Vector vectorToAdd)                    //Прибавление к вектору другого вектора
        {
            int length = Math.Max(elements.Length, vectorToAdd.GetSize());

            if (length > elements.Length)           //Увеличиваем длину текущего вектора, если это необходимо
                Array.Resize(ref elements, length);

            for (int i = 0; i < vectorToAdd.GetSize(); i++)   //прибавлям к элементам вектора значения другого вектора
                elements[i] += vectorToAdd.elements[i];
        }

        public void Subtract(Vector vectorToSubtract)                    //Вычитание из вектора другого вектора
        {
            int length = Math.Max(elements.Length, vectorToSubtract.GetSize());

            if (length > elements.Length)           //Увеличиваем длину текущего вектора, если это необходимо
                Array.Resize(ref elements, length);

            for (int i = 0; i < vectorToSubtract.GetSize(); i++)   //вычитаем из элементов вектора значения другого вектора
                elements[i] -= vectorToSubtract.elements[i];
        }

        public void Multiply(double number)              //Умножение вектора на скаляр
        {
            for (int i = 0; i < elements.Length; i++)   //умножаем каждый элементов вектора на переданный скаляр
                elements[i] *= number;
        }

        public void Reverse()                   //Разворот вектора (умножение всех компонент на -1)
        {
            Multiply(-1);
        }

        public double GetModulus()                //Получение длины вектора (формула: корень из суммы квадратов всех элементов)
        {
            double modulus = 0;
            foreach (double element in elements)  //накопление суммы квадратов всех элементов вектора
                modulus += element * element;
            //вычисление корня из суммы квадратов
            return Math.Sqrt(modulus); ;
        }

        public double GetElement(int index)    //Получение компоненты вектора по индексу
        {
            return elements[index];
        }

        public void SetElement(int index, double value)    //Установка компоненты вектора по индексу
        {
            elements[index] = value;
        }

        public override bool Equals(object obj)                      //Переопределение функци Equals
        {
            // проверили что передали сам объект
            if (ReferenceEquals(obj, this))
            {
                return true;
            }

            // отсеяли null и объекты других классов
            if (ReferenceEquals(obj, null) || obj.GetType() != GetType())
            {
                return false;
            }

            // привели объект к Vector            
            Vector otherVector = (Vector)obj;

            // проверили равенство размерностей
            if (elements.Length != otherVector.GetSize())
                return false;

            //Сравним поэлементно компоненты векторов
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] != otherVector.elements[i])    //Если элементы не равны, выйдем и вернем false
                    return false;
            }
            return true;   //До этого места можем дойти только, если вектора равны
        }

        public override int GetHashCode()                      //Переопределение функци GetHashCode
        {
            int prime = 23;                                    //Выбираем простое число
            int hash = 1;
            foreach (double elemnt in elements)                 //в цикле по алгоритму из лекции вычисляем hash
                hash = prime * hash + elemnt.GetHashCode();

            return hash;
        }

        public static Vector GetSum(Vector vector1, Vector vector2)   //Статический метод сложения двух векторов
        {
            Vector result = new Vector(vector1);                 //Скопируем все элементы первого вектора в вектор sum

            result = new Vector(vector1);                 //Скопируем все элементы первого вектора в вектор sum
            result.Add(vector2);                          //Прибавим к вектору sum второй вектор
            return result;
        }

        public static Vector GetSubtract(Vector vector1, Vector vector2)   //Статический метод вычитания двух векторов
        {
            Vector result = new Vector(vector1);                 //Скопируем все элементы первого вектора в вектор result;
            result.Subtract(vector2);                     //Вычтем из вектора result второй вектор, если размерность vector2 больше, то vector1 увеличится
            return result;
        }

        public static double DotProduct(Vector vector1, Vector vector2)   //Статический метод вычисления скалярного произведения двух векторв
        {
            int length = Math.Min(vector1.GetSize(), vector2.GetSize());       //количество слагаемых в скалярном произведение - размерность самого маленького вектора, т.к. недостающие элементы нули
            double result = 0;

            for (int i = 0; i < length; i++)                                  //накопление суммы из произведений элементов двух векторов с одинаковыми индексами
                result += vector1.GetElement(i) * vector2.GetElement(i);

            return result;
        }
    }
}