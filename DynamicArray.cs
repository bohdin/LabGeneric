using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LabGeneric
{
    public class DynamicArray<T> : IEnumerable<T>,  IList<T>
    {
        class Node<T>
        {
            public Node(T data)
            {
                Data = data;
            }
            public T Data { get; set; }
            public Node<T> Next { get; set; }
        }
        public delegate void Handler(string message);
        public event Handler? Notify;
        Node<T> head; // перший елемент
        Node<T> tail; // останній елемент
        int count;  // кількість елементів
        // Конструктор
        public DynamicArray()
        {
            head = null;
            tail = head;
            count = 0;
        }
        public DynamicArray(IEnumerable<T> collection)
        {
            if (collection == null)
                throw new ArgumentNullException();
            foreach (T item in collection)
                Add(item);
        }
        // додавання елементу

        public void Add(T data)
        {
            Node<T> node = new Node<T>(data);

            if (head == null)
                head = node;
            else
                tail.Next = node;
            tail = node;
            Notify?.Invoke($"Елемент {data} додано");
            count++;
        }
        // вставка елементу
        public void Insert(int index, T data)
        {
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException();
            if (index == count)
            {
                Add(data);
                return;
            }
            Node<T> node = new Node<T>(data);
            Node<T> current = head;
            Node<T> previous = null;
            int key = 0;
            while(current != null)
            {
                if(key == index)
                {
                    if(previous != null)
                    {
                        previous.Next = node;
                        node.Next = current;

                    }
                    else
                    {
                        node.Next = head;
                        head = node;
                    }
                    count++;
                    Notify?.Invoke($"Елемент {data} додано, з індексом {index}");
                    return;
                }
                key++;
                previous = current;
                current = current.Next;
            }
        }
        // вставка списку
        public void InsertRange(int index, IEnumerable<T> collection)
        {
            if (index < 0 || index > count)
                throw new ArgumentOutOfRangeException();
            if (collection == null)
                throw new ArgumentNullException();
            if (index == count)
            {
                foreach (T item in collection)
                    Add(item);
                return;
            }
            foreach (T data in collection)
            {
                Insert(index++, data);
            }
        }
        // видалення елементу
        public bool Remove(T data)
        {
            Node<T> current = head;
            Node<T> previous = null;

            while (current != null)
            {
                if (current.Data.Equals(data))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        head = head.Next;
                        if (head == null)
                            tail = null;
                    }
                    count--;
                    Notify?.Invoke($"Елемент {data} видалено");
                    return true;
                }

                previous = current;
                current = current.Next;
            }
            Notify?.Invoke($"Елемент {data} не існує");
            return false;
        }
        // видалення елементу за індексом
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException();

            Node<T> current = head;
            Node<T> previous = null;
            int key = 0;
            while (current != null)
            {
                if (key == index)
                {
                    Notify?.Invoke($"Елемент {current.Data} з індексом {index} видалено");
                    if (previous != null)
                    {
                        previous.Next = current.Next;
                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        head = head.Next;
                        if (head == null)
                            tail = null;
                    }
                    count--;
                    
                    return;
                }
                key++;
                previous = current;
                current = current.Next;
            }
        }

        public int Count { get { return count; } }
        public bool IsEmpty { get { return count == 0; } }
        // очистить список
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
            Notify?.Invoke($"Колекція очищена");
        }
        // індекс елементу
        public int IndexOf(T data)
        {
            Node<T> current = head;
            int index = 0;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return index;
                index++;
                current = current.Next;
            }
            return -1;
        }
        // чи має елемент список
        public bool Contains(T data)
        {
            Node<T> current = head;
            while (current != null)
            {
                if (current.Data.Equals(data))
                    return true;
                current = current.Next;
            }
            return false;
        }

        // копіювання в масив
        public void CopyTo(T[] array, int index)
        {
            if (index < 0 || index >= count)
                throw new ArgumentOutOfRangeException();

            Node<T> current = head;
            while(current != null)
            {
                array[index++] = current.Data;
                current = current.Next;
            }
        }

        public T this[int index]
        {
            get
            {
                if (index >= 0 && index < count)
                {
                    int counter = 0;
                    Node<T> current = head;
                    while (counter++ < index)
                        current = current.Next;
                    return current.Data;
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
            set
            {
                if (index >= 0 && index < count)
                {
                    int counter = 0;
                    Node<T> current = head;
                    while (counter++ < index)
                        current = current.Next;
                    current.Data = value;
                }
                else
                    throw new ArgumentOutOfRangeException();
            }
        }


        // реалізація інтерфейсу IEnumerable
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)this).GetEnumerator();
        }
        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }
        public bool IsReadOnly
        {
            get { return false; }
        }
        public override string ToString()
        {
            StringBuilder output = new StringBuilder();
            Node<T> node = head;
            output.Append("[ ");
            while(node != null)
            {
                if(node.Next != null)
                    output.Append($"{node.Data}, ");
                else
                    output.Append($"{node.Data} ");
                node = node.Next;
            }
            output.Append("]");
            return output.ToString();
        }
        public void DisplayMessage(string message) => Console.WriteLine(message);
    }
}
