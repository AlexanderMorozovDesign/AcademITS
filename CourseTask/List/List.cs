using System;

namespace List
{
    class SimpleLinkedList<T>
    {
        private class ListNode
        {
            public T data;
            public ListNode next;

            public ListNode(T data, ListNode next)
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

        public string GetFirstValue
        {
            get { return Head.data.ToString(); }
        }

        public void AddToBack(T data)
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

        public void AddToFront(T data)
        {
            if (Head == null)
            {
                Head = new ListNode(data, null);
                Finish = Head;
            }
            else
            {
                ListNode node = new ListNode(data, Head);
                Head = node;
            }

            ++ListCount;
        }

        public void InsertOfIndex(int index, T data)
        {
            if (index < 0 || index >= ListCount)
            {
                throw new ArgumentOutOfRangeException();
            }

            if (index == 0)
            {
                AddToFront(data);
            }
            else
            {
                ListNode r = Head;

                for (int i = 1; i < index; ++i)
                {
                    r = r.next;
                }

                ListNode node = new ListNode(data, r.next);
                r.next = node;

                ++ListCount;
            }
        }

        public void PrintList()
        {
            for (ListNode r = Head; r != null; r = r.next)
            {
                Console.Write("{0} ", r.data);
            }
        }

        public void ClearList()
        {
            Head = null;
            Finish = null;
            ListCount = 0;
        }

        public bool ContainsOfValue(T data)
        {
            ListNode currnode = Head;

            while (currnode != null)
            {
                if (currnode.data.Equals(data))
                {
                    return true;
                }

                currnode = currnode.next;
            }

            return false;
        }

        public void CopyToArray(T[] array, int arrayIndex)
        {
            ListNode currnode = Head;

            while (currnode != null)
            {
                array[arrayIndex++] = currnode.data;
                currnode = currnode.next;
            }
        }

        public void ReverseList()
        {
            if (Head == null)
            {
                return;
            }

            ListNode prev = null, current = Head, next = null;

            while (current.next != null)
            {
                next = current.next;
                current.next = prev;
                prev = current;
                current = next;
            }

            current.next = prev;
            Head = current;
        }

        public bool DeleteOfValue(T data)
        {
            if (data == null)
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

                    return true;
                }

                prevnode = currnode;
                currnode = currnode.next;
            }

            return false;
        }
    }
}
