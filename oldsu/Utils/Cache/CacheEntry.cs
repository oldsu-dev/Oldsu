using System;

namespace Oldsu.Utils.Cache
{
    public struct CacheEntry
    {
        private DateTime ExpirationAt { get; }

        public bool IsExpired 
            => ExpirationAt < DateTime.Now; // is datetime.now slow?

        public object Value;

        public CacheEntry(object value, DateTime expirationAt)
        {
            Value = value;
            ExpirationAt = expirationAt;
        }
    }
}