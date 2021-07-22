using System;
using System.Threading;
using System.Threading.Tasks;
using Nito.AsyncEx;

namespace Oldsu.Utils.Threading
{
    public class AsyncRwLockWrapper<T>
    {
        private readonly AsyncReaderWriterLock _rwLock;
        private readonly T _value;

        public AsyncRwLockWrapper(T value)
        {
            _value = value;
            _rwLock = new AsyncReaderWriterLock();
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