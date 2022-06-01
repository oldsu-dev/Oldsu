using System.Collections.Generic;

namespace Oldsu.Utils
{
    public class LimitedBag<T>
    {
        public int MaxItems { get; }

        private readonly Queue<T> _objects;

        public LimitedBag(int maxItems)
        {
            _objects = new Queue<T>();
            MaxItems = maxItems;
        }

        public IEnumerable<T> Objects => _objects;

        public void Push(T obj)
        {
            if (_objects.Count + 1 > MaxItems)
                _ = _objects.Dequeue();
            
            _objects.Enqueue(obj);
        }
    }
}