using System;
using System.IO;
using System.Runtime.Remoting.Messaging;

namespace List
{
    public class Queue<T> : List<T>
    {
        public Queue()
            : base()
        {}
        
        public Queue(T elt)
            : base(elt)
        {}
        
        public T front()
        {
            return head_.Data;
        }
        
        public void popFront()
        {
            delete(0);
        }

        public void pushBack(T elt)
        {
            insert(count, elt);
        }
    }
}