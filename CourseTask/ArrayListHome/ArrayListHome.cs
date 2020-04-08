using System;
using System.IO;

namespace ArrayListHome
{
    class LinkedList<T>
    {

        private class ListNode                      
        {
            public int data;
            public ListNode next;

            public ListNode(int data, ListNode next)  
            {
                this.data = data;
                this.next = next;
            }
        }

        private int ListCount = 0;
        private ListNode Head = null;
        private ListNode Finish = null;

        public int GetCount                        
        {
            get { return ListCount; }
        }

        public void AddToBack(int data)               
        {
            if (Head == null)
            {
                Head = new ListNode(data, null);
                Finish = Head;
            }
            else
            {
                Finish.next = new ListNode(data, null);
                Finish = Finish.next;
            }
            ++ListCount;
        }

        public void ClearList()                       
        {
            Head = null;
            Finish = null;
            ListCount = 0;
        }

        public void AddFromFile(string FileName)          
        {
            if (Head != null)
            {
                ClearList();
            }
            else
            {
                StreamReader sr = new StreamReader(FileName);
                try
                {
                    while (!sr.EndOfStream)
                    {
                        AddToBack(Convert.ToInt32(sr.ReadLine()));
                        ++ListCount;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    sr.Close();
                }
            }
        }

        public void DeleteOfValue(int data)              
        {
            if (data == 0)
            {
                throw new ArgumentNullException(nameof(data));
            }
            ListNode currnode = Head, prevnode = null;

            while (currnode != null)
            {
                if (currnode.data.Equals(data))
                {
                    if (prevnode != null)
                    {
                        prevnode.next = currnode.next;
                        if (currnode.next == null)
                        {
                            Finish = prevnode;
                        }
                    }
                    else
                    {
                        Head = Head.next;
                        if (Head == null)
                        {
                            Finish = null;
                        }
                    }
                    ListCount--;

                }

                prevnode = currnode;
                currnode = currnode.next;
            }
        }

        public bool IsEven(int num)                               
        {
            if (num == 0)
            {
                throw new ArgumentNullException(nameof(num));
            }
            else
            {
                double ost = num % 2;
                if (ost == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public void DeleteEven()                                      
        {
            for (ListNode r = Head; r != null; r = r.next)
            {
                if (IsEven(r.data) == true)
                {
                    DeleteOfValue(r.data);
                }
            }
        }

        public bool IsExist(int num)                                   
        {
            Boolean a = false;

            for (ListNode r = Head; r != null; r = r.next)
            {
                if (r.data != num)
                {
                    a = false;
                }
                else
                {
                    a = true;
                }
            }

            return a;
        }

        public void CopyToArray(int[] array, int Index) 
        {
            ListNode currnode = Head;

            while (currnode != null)
            {
                int a = 0;

                for (int i = 0; i < array.Length; i++)
                {
                    if (array[i] == currnode.data)
                    {
                        a = 1;
                        break;
                    }
                    else
                    {
                        a = 0;
                    }
                }
                if (a == 0)
                {
                    array[Index++] = currnode.data;
                }

                currnode = currnode.next;
            }
        }

        public void PrintList()                     
        {
            for (ListNode r = Head; r != null; r = r.next)
            {
                Console.Write("{0} ", r.data);
            }    
        }
    }

    class ArrayListHome
    {
        static void Main(string[] args)
        {
            global::ArrayListHome.LinkedList<int> ListOne = new LinkedList<int>();                  
            int[] nums = new int[15];

            Console.WriteLine("Чтение данных из файла");
            ListOne.AddFromFile("data.txt");                                                
            Console.WriteLine("\n");
            Console.WriteLine("Вывод списка\n");
            ListOne.PrintList();                                        
            Console.WriteLine("\n");
            Console.WriteLine("Удаление четных элементов\n");
            ListOne.DeleteEven();                                                                                           
            ListOne.PrintList();
            Console.WriteLine("\n");
            ListOne.CopyToArray(nums, 0);                               

            Console.WriteLine("Вывод массива\n");

            foreach (var item in nums)
            {
                Console.WriteLine(item.ToString());                    
            }
        }
    }
}
