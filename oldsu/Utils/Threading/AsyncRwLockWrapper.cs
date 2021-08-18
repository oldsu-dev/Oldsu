using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Nito.AsyncEx;

namespace Oldsu.Utils.Threading
{
    public class AsyncRwLockWrapper<T>
    {
        private readonly AsyncReaderWriterLock _rwLock;
        private T _value;

        private readonly AsyncLock _upgradeMutex;
        
        // 5 seconds max lock to prevent deadlocks or performance bottlenecks.
        private const int MaxLockDelay = 5000;

        private static CancellationToken GetTimeoutCancellationToken() =>
            new CancellationTokenSource(MaxLockDelay).Token;

        public AsyncRwLockWrapper()
        {
            _rwLock = new AsyncReaderWriterLock();
            _upgradeMutex = new AsyncLock();
            _value = default!;
            _upgradeMutex = new AsyncLock();
        }
        
        public AsyncRwLockWrapper(T value) : this()
        {
            _value = value;
        }

        public async Task<AsyncRwLockGuardWrite<T>> AcquireWriteLockGuard() => 
            new(await _rwLock.WriterLockAsync(GetTimeoutCancellationToken()), this, _value);
        
        public async Task<AsyncRwLockGuardRead<T>> AcquireReadLockGuard() => 
            new(await _rwLock.ReaderLockAsync(GetTimeoutCancellationToken()), this, _value);

        public async Task Upgrade(AsyncRwLockGuardRead<T> guard)
        {
            if (guard.Locker != this)
                throw new InvalidOperationException("The lock is not owned by this ReadWriterLock");

            using (await _upgradeMutex.LockAsync())
            {
                guard.Lock.Dispose();
                guard.Lock = await _rwLock.WriterLockAsync(GetTimeoutCancellationToken());
            }
        }
        
        public async Task Downgrade(AsyncRwLockGuardWrite<T> guard)
        {
            if (guard.Locker != this)
                throw new InvalidOperationException("The lock is not owned by this ReadWriterLock");

            using (await _upgradeMutex.LockAsync())
            {
                guard.Lock.Dispose();
                guard.Lock = await _rwLock.ReaderLockAsync(GetTimeoutCancellationToken());
            }
        }
        
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