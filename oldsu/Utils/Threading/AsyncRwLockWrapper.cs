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
    
    public class AsyncRwLockWrapper<T> where T: new()
    {
        private readonly AsyncReaderWriterLock _rwLock;
        private T _value;

        // 5 seconds max lock to prevent deadlocks or performance bottlenecks.
        private const int MaxLockDelay = 5000;

        private static CancellationToken GetTimeoutCancellationToken() =>
            new CancellationTokenSource(MaxLockDelay).Token;

        public AsyncRwLockWrapper()
        {
            _value = new T();
        }

        public async Task<AsyncRwLockGuard<T>> AcquireWriteLockGuard() => 
            new(await _rwLock.WriterLockAsync(GetTimeoutCancellationToken()), _value);
        
        public async Task<AsyncRwLockGuard<T>> AcquireReadLockGuard() => 
            new(await _rwLock.ReaderLockAsync(GetTimeoutCancellationToken()), _value);

        public async Task SetValueAsync(T value)
        {
            using (await _rwLock.WriterLockAsync(GetTimeoutCancellationToken()))
                _value = value;
        }
        
        public async Task<TResult> ReadAsync<TResult>(Func<T, TResult> fn)
        {
            using (await _rwLock.ReaderLockAsync(GetTimeoutCancellationToken()))
                return fn(_value);
        }

        public async Task ReadAsync(Action<T> action)
        {
            using (await _rwLock.ReaderLockAsync(GetTimeoutCancellationToken()))
                action(_value);
        }
        
        public async Task ReadAsync(Func<T, Task> action)
        {
            using (await _rwLock.ReaderLockAsync(GetTimeoutCancellationToken()))
                await action(_value);
        }
        
        public async Task<TResult> ReadAsync<TResult>(Func<T, Task<TResult>> action)
        {
            using (await _rwLock.ReaderLockAsync(GetTimeoutCancellationToken()))
                return await action(_value);
        }
        
        public async Task<TResult> WriteAsync<TResult>(Func<T, TResult> fn)
        {
            using (await _rwLock.WriterLockAsync(GetTimeoutCancellationToken()))
                return fn(_value);
        }

        public async Task WriteAsync(Action<T> action)
        {
            using (await _rwLock.WriterLockAsync(GetTimeoutCancellationToken()))
                action(_value);
        }
        
        public async Task WriteAsync(Func<T, Task> action)
        {
            using (await _rwLock.WriterLockAsync(GetTimeoutCancellationToken()))
                await action(_value);
        }
        
        public async Task<TResult> WriteAsync<TResult>(Func<T, Task<TResult>> action)
        {
            using (await _rwLock.WriterLockAsync(GetTimeoutCancellationToken()))
                return await action(_value);
        }
    }
}