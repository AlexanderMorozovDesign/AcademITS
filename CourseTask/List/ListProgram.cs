using System;

namespace List
{
    class ListProgram
    {
        static void Main(string[] args)
        {
            const int k = 5;
            List.SimpleLinkedList<string> LinkList = new List.SimpleLinkedList<string>();

            Console.WriteLine("Ввод данных:");

            for (int i = 1; i <= k; ++i)
            {
                LinkList.AddToBack(Console.ReadLine().ToString());
            }                

            LinkList.PrintList();                                       
            LinkList.AddToFront("в начало");                             
            Console.WriteLine("\n");
            LinkList.PrintList();

            LinkList.InsertOfIndex(4, "четыре");                        
            Console.WriteLine("\n");
            LinkList.PrintList();

            LinkList.ReverseList();                                    
            Console.WriteLine("\n");
            LinkList.PrintList();

            LinkList.DeleteOfValue("в начало");                      
            Console.WriteLine("\n");
            LinkList.PrintList();

            Console.ReadKey();
        }
    }
}
