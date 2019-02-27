﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ADT
{
    public class MyLinkedList<T> : IEnumerable where T : IComparable<T>
    {
        // Insert code from MyLinkedList here ... 

        private class Node
        {
            public T Data { get; set; }
            public Node Next { get; set; }

            public Node(T data)
            {
                Data = data;
            }
        }

        private Node head;

        public int Count { get; private set; }
        public T First
        {
            get { return ItemAt(0); }
        }
        public T Last
        {
            get { return ItemAt(Count - 1); }
        }

        /// <summary>
        /// The Insert(Object data, int index = 0) method inserts data as a node in the list
        /// at the position indicated by index. 
        /// The list is 0-indexed. 
        /// Default value of index is 0.
        /// If index is 0 or less, the data is inserted at the start of the list.
        /// If index is equal to Count or higher, the data is inserted at the end of the list.
        /// </summary>
        public void Insert(T data, int index = 0)
        {
            Node n = new Node(data);

            // Adjust index, if necessary
            if (index > Count)
                index = Count;

            if (Count == 0 || index < 1)
            {
                n.Next = head;
                head = n;
            }
            else
            {
                Node position = head;
                for (int i = 0; i < index - 1; i++)
                {
                    position = position.Next;
                }
                n.Next = position.Next;
                position.Next = n;
            }

            Count++;
        }

        /// <summary>
        /// The Append(Object data) method appends data at the end of the list.
        /// </summary>
        public void Append(T data)
        {
            Insert(data, Count);
        }

        /// <summary>
        /// The Delete(int index = 0) method deletes the node in the list at the position indicated by index. 
        /// The list is 0-indexed. 
        /// Default value of index is 0.
        /// </summary>
        public void Delete(int index = 0)
        {
            if (Count > 0)
            {
                // Adjust index, if necessary
                if (index > Count)
                    index = Count;

                if (index < 1)
                    head = head.Next;
                else
                {
                    Node position = head;
                    for (int i = 0; i < index - 1; i++)
                    {
                        position = position.Next;
                    }
                    position.Next = position.Next.Next;
                }

                Count--;
            }
        }

        /// <summary>
        /// The ItemAt(int index) method returns the data from the list at the position indicated by index. 
        /// The list is 0-indexed. 
        /// </summary>
        public T ItemAt(int index)
        {
            T result = default(T);
            if (index < Count && index >= 0)
            {
                Node position = head;
                for (int i = 0; i < index; i++)
                {
                    position = position.Next;
                }
                result = position.Data;
            }
            return result;
        }

        /// <summary>
        /// The ToString() method returns a string representation of the whole list by concatenating 
        /// all the ToString()-values of each data object in the list.
        /// </summary>
        public override string ToString()
        {
            string result = "";
            Node pointernode = head;
            while (pointernode != null)
            {
                result = result + pointernode.Data.ToString() + "\n";

                pointernode = pointernode.Next;
            }
            return result;
        }

        public IEnumerator GetEnumerator()
        {
            return new MyLinkedListEnumerator(head);
        }

        public void Sort()
        {
            Node actualNode = head;
            T temp;
            int n = this.Count;
            for (int i = 0; i< n-1;i++)
            {
                actualNode = head;
                for(int j = 0; j < n-i-1; j++)
                {
                    if(actualNode != null)
                    {
                        if ((actualNode.Next.Data).CompareTo(actualNode.Data) == -1)
                        {
                            temp = actualNode.Data;
                            actualNode.Data = actualNode.Next.Data;
                            actualNode.Next.Data = temp;
                        }
                        actualNode = actualNode.Next;
                    }
                } 
            }
        }
        public void Sort(IComparer<T> ic)
        {
            Node actualNode = head;
            T temp;
            int n = this.Count;
            for (int i = 0; i < n - 1; i++)
            {
                actualNode = head;
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (actualNode != null)
                    {
                        
                        if (ic.Compare(actualNode.Data, actualNode.Next.Data) == 1)
                        {
                            temp = actualNode.Data;
                            actualNode.Data = actualNode.Next.Data;
                            actualNode.Next.Data = temp;
                        }
                        actualNode = actualNode.Next;
                    }
                }
            }
        }
        private class MyLinkedListEnumerator : IEnumerator
        {
            private Node _head;
            private Node temp;
            private Node reset;

            public MyLinkedListEnumerator(Node head)
            {
                temp = null;
                _head = head;
                reset = head;
            }
            public T Current
            {
                get
                {
                    if (temp == null)
                    {
                        return default(T);
                    }
                    else
                    {
                        return temp.Data;
                    }
                }
            }

            object IEnumerator.Current => Current;

            public bool MoveNext()
            {
                if (temp == null)
                {
                    temp = _head;
                }
                else
                {
                    temp = temp.Next;
                }
                if (temp == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            public void Reset()
            {
                _head = reset;
            }
        }
    }
}