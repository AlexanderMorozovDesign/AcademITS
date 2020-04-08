using System;
using System.Collections;
using System.Collections.Generic;

namespace ArrayList
{
    class ArrayList
    {
        public interface IList<T>  
        {
            void TrimExcess();   
            void Add(T value);   
        }

        public class List<T> : IList<T>, IEnumerable<T>  
        {

            private ListNode head = null;       
            private ListNode finish = null;     
            private int capacity = 10;         

            public List()      
            {

            }
            public List(T value, int capacity)   
            {
                SetCapacity(value, capacity);
            }

            public void SetCapacity(T value, int capacity)  
            {
                for (int i = 1; i <= capacity; i++)
                {
                    Add(value);
                }
            }

            public int GetCapacity      
            {
                get
                {
                    var temp = head;
                    int i;

                    for (i = 0; temp != null; i++, temp = temp.next) ;
                    return i;
                }
            }

            private class ListNode                      
            {
                public T value { get; set; }
                public ListNode next { get; set; }

                public ListNode(T value)  
                {
                    this.value = value;
                }

                public ListNode(T value, ListNode next) : this(value)      
                {
                    this.next = next;
                }
            }

            public void Add(T value)                       
            {
                var node = new ListNode(value, null);
                if (head == null)
                {
                    head = finish = node;
                }    
                else
                {
                    finish.next = node;
                    finish = node;
                }
            }

            public void Delete(T value)            
            {
                try
                {
                    if (value == null)
                    {
                        throw new ArgumentNullException(nameof(value));
                    }

                    ListNode currnode = head, prevnode = null;

                    while (currnode != null)
                    {
                        if (currnode.value.Equals(value))
                        {
                            if (prevnode != null)
                            {
                                prevnode.next = currnode.next;

                                if (currnode.next == null)
                                {
                                    finish = prevnode;
                                }
                            }
                            else
                            {
                                head = head.next;
                                if (head == null)
                                {
                                    finish = null;
                                }
                            }

                        }

                        prevnode = currnode;
                        currnode = currnode.next;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            public void TrimExcess()     
            {
                for (ListNode r = head; r != null; r = r.next)
                {
                    if (r.value.Equals("0"))
                    {
                        Delete(r.value);
                    }
                }
            }

            public IEnumerator<T> GetEnumerator()       
            {
                for (var temp = head; temp != null; temp = temp.next)
                    yield return temp.value;
            }

            IEnumerator IEnumerable.GetEnumerator()    
            {
                return GetEnumerator();
            }

            public void PrintList(List<T> list)                     
            {
                for (ListNode r = head; r != null; r = r.next)
                {
                    Console.Write("{0} ", r.value);
                }
            }
        }

        static void Main(string[] args)
        {
            List<string> list = new List<string>();        
            list.SetCapacity("0", 10);                     
            list.PrintList(list);                          

            for (int i = 1; i <= 5; i++)
            {
                list.Add(i.ToString());                     
            }

            Console.Write("\n");
            list.PrintList(list);
            Console.Write("\n");
            list.TrimExcess();                              
            list.PrintList(list);
        }
    }
}
