using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;

namespace Oldsu.Utils
{
    public class MultiKeyConcurrentDictionary<TKey1, TKey2, TValue>
        where TKey1 : notnull
        where TKey2 : notnull
        where TValue : notnull
    {
        private ConcurrentDictionary<TKey1, TValue> dict1 = new();
        private ConcurrentDictionary<TKey2, TValue> dict2 = new();

        public ICollection<TValue> Values => dict1.Values;

        public bool TryGetValue(TKey1 key1, out TValue value)
        {
            lock (this)
            {
                return dict1.TryGetValue(key1, out value);
            }
        }

        public bool TryGetValue(TKey2 key2, out TValue value)
        {
            lock (this)
            {
                return dict2.TryGetValue(key2, out value);
            }
        }

        public bool TryAdd(TKey1 key1, TKey2 key2, TValue value)
        {
            lock (this)
            {
                return dict1.TryAdd(key1, value) && dict2.TryAdd(key2, value);
            }
        }

        public bool TryRemove(TKey1 key1, TKey2 key2, TValue value)
        {
            lock (this)
            {
                return dict1.TryRemove(key1, out _) && dict2.TryRemove(key2, out _);
            }
        }
    }
}