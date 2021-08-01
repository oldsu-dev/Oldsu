using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading;

namespace Oldsu.Utils
{

    
    public class MultiKeyDictionary<TKey1, TKey2, TValue>
        where TKey1 : notnull
        where TKey2 : notnull
        where TValue : notnull
    {
        public MultiKeyDictionary()
        {
            _dict1 = new Dictionary<TKey1, TValue>();
            _dict2 = new Dictionary<TKey2, TValue>();

            _rwLock = new ReaderWriterLockSlim();
        }

        public IEnumerable<TValue> Values => _dict1.Values;

        private readonly ReaderWriterLockSlim _rwLock;
        
        private readonly Dictionary<TKey1, TValue> _dict1;
        private readonly Dictionary<TKey2, TValue> _dict2;

        public bool TryGetValue(TKey1 key1, out TValue value)
        {
            _rwLock.EnterReadLock();

            try
            {
                return _dict1.TryGetValue(key1, out value);
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
        }

        public bool TryGetValue(TKey2 key2, out TValue value)
        {
            _rwLock.EnterReadLock();

            try
            {
                return _dict2.TryGetValue(key2, out value);
            }
            finally
            {
                _rwLock.ExitReadLock();
            }
        }

        public bool TryAdd(TKey1 key1, TKey2 key2, TValue value)
        {
            _rwLock.EnterWriteLock();

            try
            {
                return _dict1.TryAdd(key1, value) && _dict2.TryAdd(key2, value);
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }

        public bool TryRemove(TKey1 key1, TKey2 key2, out TValue value)
        {
            _rwLock.EnterWriteLock();

            try
            {
                return _dict1.Remove(key1, out value) && _dict2.Remove(key2, out _);
            }
            finally
            {
                _rwLock.ExitWriteLock();
            }
        }
    }
}