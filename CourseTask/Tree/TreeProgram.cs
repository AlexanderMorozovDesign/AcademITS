using System;

namespace Tree
{
    class TreeProgram
    {
        static void Main(string[] args)
        {
            BinaryTree<int> integerTree = new BinaryTree<int>();

            Random rand = new Random();

            for (int i = 0; i < 20; i++)
            {
                int value = rand.Next(100);
                Console.WriteLine("Добавление {0}", value);
                integerTree.Add(value);
            }

            Console.WriteLine();
            Console.WriteLine("Количество узлов: {0}", integerTree.Count);
            Console.WriteLine("Наибольшее значение: {0}", integerTree.MaxValue);
            Console.WriteLine("Наименьшее значение: {0}", integerTree.MinValue);
            Console.WriteLine("Обход в ширину:");
            Console.WriteLine(string.Join(" ", integerTree.Preorder()));
            Console.WriteLine("Обход в глубину:");
            Console.WriteLine(string.Join(" ", integerTree.Postorder()));
            Console.WriteLine("Симметричный обход:");
            Console.WriteLine(string.Join(" ", integerTree.Inorder()));
            Console.WriteLine("Обход с рекурсией:");
            Console.WriteLine(string.Join(" ", integerTree.Levelorder()));

            Console.WriteLine();
            integerTree.PrintTree();
            Console.WriteLine();

            Console.ReadKey(true);
        }
    }
}
