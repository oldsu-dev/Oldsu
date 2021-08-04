using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
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

        public async Task<CacheResult> TryGetValue(object key)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            var cacheResult = new CacheResult();
            CacheEntry cacheEntry = default;

            cacheResult.IsFound = await _cacheEntriesLocked.ReadAsync(cache =>
                cache.TryGetValue(key, out cacheEntry));

            if (cacheEntry.IsExpired)
                cacheResult.IsFound = false;

            StartFindAndDestroyJob();
            
            return cacheResult;
        }

        public async Task TryAdd(object key, object value)
        {
            if (key == null)
                throw new ArgumentNullException(nameof(key));
            
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            StartFindAndDestroyJob();
        }

        public void TryRemove(object key)
        {
            StartFindAndDestroyJob();
        }

        private static CacheEntry CreateCacheEntry(object value, DateTime expirationTime)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            return new CacheEntry(value, expirationTime);
        }

        private void StartFindAndDestroyJob() =>
            Task.Run(async () => FindAndDestroyExpiredEntries());

        // prevent for 2 unnecessary checks to launch at the same time.
        private volatile bool _checkingForExpiredEntries = false;

        private async Task FindAndDestroyExpiredEntries()
        {
            if (_checkingForExpiredEntries)
                return;

            _checkingForExpiredEntries = true;

            await _cacheEntriesLocked.WriteAsync(cache =>
            {
                foreach (var entry in cache)
                    if (entry.Value.IsExpired)
                        cache.Remove(entry.Key);
            });

            _checkingForExpiredEntries = false;
        }
    }

    public struct CacheResult
    {
        public bool IsFound { get; set; }
        public object? Value { get; set; }
    }
}