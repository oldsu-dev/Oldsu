using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using Nito.AsyncEx;
using Oldsu.Utils.Threading;

namespace Oldsu.Utils.Cache
{
    // note not thread safe. 
    public class AsyncDictionaryWithExpiration
    {
        private AsyncRwLockWrapper<Dictionary<object, CacheEntry>> _cacheEntriesLocked = new();

        public AsyncDictionaryWithExpiration(CancellationToken expirationCt = default)
        {
            Task.Factory.StartNew(async () => await ExpiredEntriesWatchDog(expirationCt));
        }

        public async Task<(bool, object?)> TryGetValue(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            CacheEntry cacheEntry = default;
            bool isFound;

            isFound = await _cacheEntriesLocked.ReadAsync(cache =>
                cache.TryGetValue(key, out cacheEntry));

            return (isFound, cacheEntry.Value);
        }
        
        public async Task<bool> TryAdd(object key, object value, DateTime expirationAt)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));

            var cacheEntry = CreateCacheEntry(value, expirationAt);
            
            var isAlreadyInDictionary = await _cacheEntriesLocked.WriteAsync(cache =>
                cache.TryAdd(key, cacheEntry));

            return isAlreadyInDictionary;
        }

        public async Task<bool> TryRemove(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            var wasRemoved = await _cacheEntriesLocked.WriteAsync(cache =>
                cache.Remove(key));

            return wasRemoved;
        }

        private static CacheEntry CreateCacheEntry(object value, DateTime expirationTime)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return new CacheEntry(value, expirationTime);
        }

        private async Task ExpiredEntriesWatchDog(CancellationToken ct)
        {
            for (;;)
            {
                ct.ThrowIfCancellationRequested();

                await _cacheEntriesLocked.WriteAsync(cache =>
                {
                    foreach (var entry in cache)
                        if (entry.Value.IsExpired)
                            cache.Remove(entry.Key);
                });

                await Task.Delay(1000, ct);
            }
        }
    }
}