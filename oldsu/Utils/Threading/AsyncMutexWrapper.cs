using System;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Oldsu.Utils.Threading
{
    public class AsyncMutexWrapper<T>
    {
        private readonly AsyncLock _lock;
        private T _value;

        // 5 seconds max lock to prevent deadlocks or performance bottlenecks.
        private const int MaxLockDelay = 5000;

        private static CancellationToken GetTimeoutCancellationToken() =>
            new CancellationTokenSource(MaxLockDelay).Token;

        public AsyncMutexWrapper()
        {
            _lock = new AsyncLock();
            _value = default!;
        }
        
        public AsyncMutexWrapper(T value) : this()
        {
            _value = value;
        }

        public async Task<AsyncLockGuard<T>> AcquireLockGuard() => 
            new(await _lock.LockAsync(GetTimeoutCancellationToken()), _value);
        
        public async Task SetValueAsync(T value)
        {
            using (await _lock.LockAsync(GetTimeoutCancellationToken()))
                _value = value;
        }
        
        public async Task<TResult> LockAsync<TResult>(Func<T, TResult> fn)
        {
            using (await _lock.LockAsync(GetTimeoutCancellationToken()))
                return fn(_value);
        }

        public async Task LockAsync(Action<T> action)
        {
            using (await _lock.LockAsync(GetTimeoutCancellationToken()))
                action(_value);
        }
        
        public async Task LockAsync(Func<T, Task> action)
        {
            using (await _lock.LockAsync(GetTimeoutCancellationToken()))
                await action(_value);
        }
        
        public async Task<TResult> LockAsync<TResult>(Func<T, Task<TResult>> action)
        {
            using (await _lock.LockAsync(GetTimeoutCancellationToken()))
                return await action(_value);
        }
    }
}