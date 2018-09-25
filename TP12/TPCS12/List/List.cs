using System;
using System.Collections.Generic;
using System.IO;

namespace List
{
    public class List<T>
    {
        protected Node<T> head_;
        protected Node<T> tail_;
        protected int count;

        public List()
        {
            head_ = null;
            tail_ = null;
            count = 0;
        }

        public List(T data)
        {
            head_ = new Node<T>(data);
            tail_ = head_;
            count = 1;
        }

        public void print()
        {
            Node<T> current = head_;
            while (current != tail_)
            {
                Console.Write(current.Data + ", ");
                current = current.Next;
            }
            Console.WriteLine(current.Data);
        }
 
        public T this[int i]
        {
            
            get
            {
                if(i > count)
                    throw new IndexOutOfRangeException("Index out of bounds");
                Node<T> current;
                /*
                 * Si on est plus proche du debut de la liste,
                 * on la parcourt en commencant par le debut
                 */
                if(i < count / 2) 
                {
                    current = head_;
                    int j = 0;
                    while (j < i)
                    {
                        current = current.Next;
                        j++;
                    }
                }
                //Sinon on commence par la fin
                else
                {
                    current = tail_;
                    int j = count - 1;
                    while (j > i)
                    {
                        current = current.Prev;
                        j--;
                    }
                }
                return current.Data;
            }
            set
            {
                if(i > count)
                    throw new IndexOutOfRangeException("Index out of bounds");
                Node<T> current;
                /*
                 * Si on est plus proche du debut de la liste,
                 * on la parcourt en commencant par le debut
                 */
                if(i < count / 2)
                {
                    current = head_;
                    int j = 0;
                    while (j < i)
                    {
                        current = current.Next;
                        j++;
                    }
                }
                else
                {
                    current = tail_;
                    int j = count - 1;
                    while (j > i)
                    {
                        current = current.Prev;
                        j--;
                    }
                }
                current.Data = value;
            }
        }

        public void insert(int i, T value)
        {
            if(i > count  || i < 0)
                throw new IndexOutOfRangeException("Index out of bounds : " + i);
            
            //On creer un nouveau noeud avec la donnee a ajouter
            Node<T> insertNode = new Node<T>(value);
            //Si on insere dans une liste vide
            if (count == 0 && i == 0)
            {
                head_ = insertNode;
                tail_ = head_;
            }
                
            //Si on insere en queue
            else if (i == count)
            {
                insertNode.Prev = tail_;
                tail_.Next = insertNode;
                tail_ = insertNode;
                if (count == 1)
                {
                    tail_.Prev = head_;
                    head_.Next = tail_;
                }
                    
            }
            //Si on insere en tete
            else if (i == 0)
            {
                insertNode.Next = head_;
                head_.Prev = insertNode;
                head_ = insertNode;
                if (count == 1)
                {
                    head_.Next = tail_;
                    tail_.Prev = head_;
                }
                    
            }

            else
            {
                Node<T> current = head_;
                int j = 0;
                while (j < i-1)
                {
                    current = current.Next;
                    j++;
                }

                Node<T> currNext = current.Next;
                current.Next = insertNode;
                currNext.Prev = insertNode;
                insertNode.Next = currNext;
                insertNode.Prev = current;

            }

            count++;
        }

        public void delete(int i)
        {
            if(i >= count || i < 0)
                throw new IndexOutOfRangeException("Index out of bounds : " + i);
            if (count == 1)
            {
                head_ = null;
                tail_ = null;
            }
            //Si on supprime en queue
            else if (i == count - 1)
            {
                tail_ = tail_.Prev;
                tail_.Next = null;
            }
            //Si on supprime en tete
            else if (i == 0)
            {
                head_ = head_.Next;
                head_.Prev = null;
            }

            else
            {
                Node<T> current = head_;
                int j = 0;
                while (j < i)
                {
                    current = current.Next;
                    j++;
                }
            
                current.Prev.Next = current.Next;
                current.Next.Prev = current.Prev;
            }

            count--;
        }

        public int Count => count;
    }
}