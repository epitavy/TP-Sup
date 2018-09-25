using System;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace List
{
    public class Dequeue<T> : List<T>
    {
        public Dequeue()
            : base()
        {}
        
        public Dequeue(T elt)
            : base(elt)
        {}
        
        public T front()
        {
            return head_.Data;
        }
        
        public T back()
        {
            return tail_.Data;
        }
        
        public virtual void popBack()
        {
            delete(count - 1);
        }
        
        public void popFront()
        {
            delete(0);
        }

        public void pushFront(T elt)
        {
            insert(0, elt);
        }
        
        public void pushBack(T elt)
        {
            insert(count, elt);
        }
    }
}