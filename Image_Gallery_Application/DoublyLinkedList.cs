using System.Diagnostics;

namespace Image_Gallery_Application
{
    public class Node
    {

        public string? elem;
        public Node? next;
        public Node? prev;

        Node()
        {
            elem = null;
            next = null;
            prev = null;
        }
        public Node(string elem)
        {
            this.elem = elem;
            this.next = null;
            this.prev = null;
        }
    }
    public class DoublyLinkedList
    {

        public Node? head;
        public Node? tail;

        public int size;

        public DoublyLinkedList()
        {
            head = null;
            tail = null;
            size = 0;
        }

        public void insertlast(string value)
        {
            Node temp = new Node(value);
            if (tail == null)
            {
                head = temp;
                tail = temp;
                size++;
            }
            else
            {
                tail.next = temp;
                temp.prev = tail;
                tail = temp;
                size++;

            }
        }

        void deletelast()
        {
            if (head != null)
            {

                Node temp = tail;
                if (size == 1)
                {
                    temp = null;
                    head = null;
                    tail = null;
                    size--;
                }
                else
                {
                    tail = tail.prev;
                    tail.next = null;
                    temp = null;
                    size--;

                }
            }
        }

        void deletefirst()
        {
            if (head != null)
            {
                Node? temp = head;
                if (size == 1)
                {
                    temp = null;
                    head = null;
                    tail = null;
                    size = 0;
                }
                else
                {
                    head = head.next;
                    head.prev = null;
                    temp = null;
                    size--;
                }
            }
        }
        public void deleteAt(int pos)
        {
            if (pos < 0 || pos >= size)
            {
            }
            else if (pos == 0)
            {
                deletefirst();
            }
            else if (pos == size - 1)
            {
                deletelast();
            }
            else
            {
                Node current = head;
                for (int i = 0; i < pos; i++)
                {
                    current = current.next;
                }
                Node? temp = current;
                (current.prev).next = temp.next;
                (current.next).prev = temp.prev;
                temp = null;
                size--;
            }
        }



        public void printAllNodes()
        {
            Node current = head;
            while (current != null)
            {
                current = current.next;
            }
        }

        public string GetLast()
        {
            if (tail != null)
            {
                return tail.elem;
            }
            return default(string);
        }
    }
}
