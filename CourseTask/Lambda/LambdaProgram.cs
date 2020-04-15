using System;
using System.Collections.Generic;
using System.Linq;

namespace Lambda
{
    public class Person
    {
        public Person(string name, int age)
        {
            this.Name = name;
            this.Age = age;
        }

        public string Name
        {
            get;
        }

        public int Age
        {
            get;
        }
    }

    class LambdaProgram
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Создаем список из нескольких людей");

            List<Person> people = new List<Person> {
                new Person("Ivan", 13),
                new Person("Maria", 22),
                new Person("Maria", 16),
                new Person("Konstantin", 35),
                new Person("Ekaterina", 17),
                new Person("Aleksandr", 28),
                new Person("Mikhail", 33),
                new Person("Maria", 47),
                new Person("Ekaterina", 23),
                new Person("Dmitriy", 29)
            };

            people.Add(new Person("Dmitriy", 22));

            foreach (Person person in people)
            {
                Console.WriteLine("Имя: " + person.Name + ", возраст: " + person.Age);
            }

            Console.WriteLine("");

            Console.WriteLine("Всего человек для обработки: " + people.Count);

            Console.WriteLine("");

            List<string> uniqueNamesOfPeople = people.Select(p => p.Name).Distinct().ToList();

            Console.WriteLine("Всего человек с уникальными именами: " + uniqueNamesOfPeople.Count);

            Console.WriteLine("");

            Console.WriteLine("Уникальные имена: " + string.Join(", ", uniqueNamesOfPeople));

            Console.WriteLine("");

            var minorPeople = people.Where(p => p.Age < 18);

            Console.WriteLine("Всего человек младше 18 лет: " + minorPeople.Count());

            Console.WriteLine("");

            var minorPeopleAvgAge = minorPeople.Select(p => p.Age).Average();

            Console.WriteLine("Средний возраст людей младше 18 лет: " + Math.Round(minorPeopleAvgAge, 2));

            Console.WriteLine("");

            var mappedDictionary = people.GroupBy(p => p.Name).ToDictionary(g => g.Key, g => g.Average(x => x.Age));

            var mapped = people.GroupBy(p => p.Name).Select(g => new { Name = g.Key, AvgAge = g.Select(p => p.Age).Average() });

            Console.WriteLine("Map using dictionary:");

            foreach (var map in mappedDictionary)
            {
                Console.WriteLine("Имя (ключ): " + map.Key + ", cредний возраст (значение): " + Math.Round(map.Value, 2));
            }

            Console.WriteLine("");

            Console.WriteLine("Map using anonymous type:");

            foreach (var map in mapped)
            {
                Console.WriteLine("Имя (ключ): " + map.Name + ", cредний возраст (значение): " + Math.Round(map.AvgAge, 2));
            }

            Console.WriteLine("");

            var middlePeople = people.Where(x => x.Age >= 20 && x.Age <= 45).OrderByDescending(y => y.Age).Select(z => z.Name);

            Console.WriteLine("Имена в порядке убывания возраста (от 20 до 45): " + string.Join(", ", middlePeople.ToList()));

            Console.WriteLine("");

            Console.WriteLine("Введите сколько корней чисел нужно посчитать и вывести на экран:");
            int i = int.Parse(Console.ReadLine());

            foreach (double root in GetRoots().Take(i).ToList())
            {
                Console.WriteLine(root);
            }

            Console.WriteLine("Введите сколько чисел Фибоначчи нужно сгенерировать и вывести на экран:");
            int y = int.Parse(Console.ReadLine());

            foreach (double root in GetFibonacci().Take(y).ToList())
            {
                Console.WriteLine(root);
            }
        }

        public static IEnumerable<double> GetRoots()
        {
            int i = 0;
            while (true)
            {
                yield return Math.Sqrt(i);
                ++i;
            }
        }

        public static IEnumerable<double> GetFibonacci()
        {
            int n1 = 0;
            int n2 = 1;


            while (true)
            {
                var n1Temp = n1;
                n1 = n2;
                n2 = n1Temp + n2;

                yield return n2 - n1;
            }
        }
    }
}
