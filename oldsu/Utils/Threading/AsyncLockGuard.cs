using System;

namespace Oldsu.Utils.Threading
{
    public class AsyncLockGuard<T> : IDisposable
    {
        private IDisposable _lock;
        public T Value { get; }

        public static T operator -(AsyncLockGuard<T> guard) => guard.Value;
        
        internal AsyncLockGuard(IDisposable @lock, T value)
        {
            _lock = @lock;
            Value = value;
        }
        
        public void Dispose()
        {
            _lock.Dispose();
        }
    }

}