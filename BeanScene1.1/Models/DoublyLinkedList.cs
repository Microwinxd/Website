using Microsoft.Identity.Client;

namespace BeanScene1._1.Models
{
    public class DoublyLinkedList
    {
        private Node head;

        public void AddLast(int data)
        {
            Node newNode = new Node(data);

            if (head == null)
            {
                head = newNode;
                return;
            }

            Node current = head;
            while (current.Next != null)
            {
                current = current.Next;
            }
            current.Next = newNode;
            current.Prev = current;
        }
        public void AddFirst(int data)
        {
            Node newNode = new Node(data);
            if (head == null)
            {
                head = newNode;
                return;
            }

            newNode.Next = head;
            head.Prev = newNode;
            head = newNode;
        }
        public void Delete(int data)
        {
            Node current = head;

            while (current != null)
            {
                if (current.Data == data)
                {
                    if (current.Prev != null)
                        current.Prev.Next = current.Next;
                    else
                        head = current.Next;
                    if (current.Next != null)
                        current.Next.Prev = current.Prev;
                    return;
                }
                current = current.Next;
            }
            Console.WriteLine("Value not found");

        }
        public void DisplayForward()
        {
            Node current = head;
            Console.Write("Forward: ");
            while (current != null)
            {
                Console.WriteLine(current.Data + " ");
                current = current.Next;
            }
            Console.WriteLine();
        }
        public void DisplayBackward()
        {
            if (head == null) return;

            Node current = head;
            while (current.Next != null)
                current = current.Next;
            Console.Write("Backward: ");
            while (current != null)
            {
                Console.Write(current.Data + " ");
                current = current.Prev;
            }
            Console.WriteLine();
        }

    }
}
