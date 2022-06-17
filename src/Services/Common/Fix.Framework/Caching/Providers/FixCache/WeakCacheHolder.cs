using System;
using System.Collections.Concurrent;

namespace Fix.Caching.Providers.FixCache
{
    public class WeakCacheHolder<TKey, TVal> where TVal : class
    {
        static ConcurrentDictionary<TKey, WeakReference<TVal>> dictionary = new ConcurrentDictionary<TKey, WeakReference<TVal>>();

        public void Add(TKey key, TVal value)
        {
            dictionary.AddOrUpdate(key, new WeakReference<TVal>(value), (k, v) => new WeakReference<TVal>(value));
        }

        public bool ContainsKey(TKey key)
        {
            return dictionary.ContainsKey(key);
        }

        public bool TryGetValue(TKey key, out TVal value)
        {
            bool isAlive = false;
            value = null;
            WeakReference<TVal> entry;

            if (dictionary.TryGetValue(key, out entry))
            {
                isAlive = entry.TryGetTarget(out value);
                if (!isAlive)
                {
                    dictionary.TryRemove(key, out entry);
                }
            }

            return isAlive;
        }

        public bool TryRemove(TKey key, out TVal value)
        {
            WeakReference<TVal> entry;
            value = null;
            return dictionary.TryRemove(key, out entry);
        }
    }
}
