using System;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Oldsu.Utils.Threading
{
    public class AsyncRwLockGuard<T> : IDisposable
    {
        private IDisposable _rwLock;
        public T Value { get; }

        public static T operator ~(AsyncRwLockGuard<T> guard) => guard.Value;
        
        internal AsyncRwLockGuard(IDisposable rwLock, T value)
        {
            _rwLock = rwLock;
            Value = value;
        }
        
        public void Dispose()
        {
            _rwLock.Dispose();
        }
    }
    
    public class AsyncRwLockWrapper<T>
    {
        private readonly AsyncReaderWriterLock _rwLock;
        private T _value;

        public AsyncRwLockWrapper(T value)
        {
            _value = value;
            _rwLock = new AsyncReaderWriterLock();
        }

        public async Task<AsyncRwLockGuard<T>> AcquireWriteLockGuard() => new(await _rwLock.WriterLockAsync(), _value);
        public async Task<AsyncRwLockGuard<T>> AcquireReadLockGuard() => new(await _rwLock.ReaderLockAsync(), _value);

        public async Task SetValueAsync(T value)
        {
            using (await _rwLock.WriterLockAsync())
                _value = value;
        }
        
        public async Task<TResult> ReadAsync<TResult>(Func<T, TResult> fn)
        {
            using (await _rwLock.ReaderLockAsync())
                return fn(_value);
        }

        public async Task ReadAsync(Action<T> action)
        {
            using (await _rwLock.ReaderLockAsync())
                action(_value);
        }
        
        public async Task ReadAsync(Func<T, Task> action)
        {
            using (await _rwLock.ReaderLockAsync())
                await action(_value);
        }
        
        public async Task<TResult> ReadAsync<TResult>(Func<T, Task<TResult>> action)
        {
            using (await _rwLock.ReaderLockAsync())
                return await action(_value);
        }
        
        public async Task<TResult> WriteAsync<TResult>(Func<T, TResult> fn)
        {
            using (await _rwLock.WriterLockAsync())
                return fn(_value);
        }

        public async Task WriteAsync(Action<T> action)
        {
            using (await _rwLock.WriterLockAsync())
                action(_value);
        }
        
        public async Task WriteAsync(Func<T, Task> action)
        {
            using (await _rwLock.WriterLockAsync())
                await action(_value);
        }
        
        public async Task<TResult> WriteAsync<TResult>(Func<T, Task<TResult>> action)
        {
            using (await _rwLock.WriterLockAsync())
                return await action(_value);
        }
    }
}