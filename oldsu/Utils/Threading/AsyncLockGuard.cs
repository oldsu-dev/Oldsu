using System;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Oldsu.Utils.Threading
{
    public class AsyncLockGuard<T> : IDisposable
    {
        private IDisposable _lock;
        public T Value { get; }

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

    public abstract class AsyncRwLockGuard<T> : IDisposable
    {
        internal IDisposable Lock;
        internal AsyncRwLockWrapper<T> Locker;
        public T Value { get; }

        internal AsyncRwLockGuard(IDisposable @lock, AsyncRwLockWrapper<T> locker, T value)
        {
            Lock = @lock;
            Value = value;
            Locker = locker;
        }

        public void Dispose()
        {
            Lock.Dispose();
        }
    }

    public class AsyncRwLockGuardRead<T> : AsyncRwLockGuard<T>
    {
        public AsyncRwLockGuardRead(IDisposable @lock, AsyncRwLockWrapper<T> locker, T value) 
            : base(@lock, locker, value)
        {
        }
    }
    
    public class AsyncRwLockGuardWrite<T> : AsyncRwLockGuard<T>
    {
        public AsyncRwLockGuardWrite(IDisposable @lock, AsyncRwLockWrapper<T> locker, T value) 
            : base(@lock, locker, value)
        {
        }
    }
}