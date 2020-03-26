using System;

namespace List
{
    class ListProgram
    {
        static void Main(string[] args)
        {
            const int k = 5;
            List.SimpleLinkedList<string> LinkList = new List.SimpleLinkedList<string>();

            Console.WriteLine("Ввод данных: ");

            for (int i = 1; i <= k; ++i)
            {
                LinkList.AddToBack(Console.ReadLine().ToString());
            }

            LinkList.PrintList();
            LinkList.AddToFront("1");
            LinkList.ReverseList();

            Console.ReadKey();
        }
    }
}
