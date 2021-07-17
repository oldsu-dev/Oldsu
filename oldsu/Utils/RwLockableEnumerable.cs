using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace Oldsu.Utils
{
    public class RwLockableEnumerable<TValue> : IEnumerable<TValue>, IDisposable
    {
        private readonly ReaderWriterLockSlim _rwLock;
        private readonly IEnumerable<TValue>  _enumerable;

        public RwLockableEnumerable(ReaderWriterLockSlim rwLock, IEnumerable<TValue> enumerable)
        {
            _rwLock = rwLock;
            _rwLock.EnterReadLock();

            _enumerable = enumerable;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            return _enumerable.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private bool _disposed = false;
        
        public void Dispose()
        {
            if (_disposed) return;
            
            _rwLock.ExitReadLock();
            _disposed = true;
            
            GC.SuppressFinalize(this);
        }
    }
}