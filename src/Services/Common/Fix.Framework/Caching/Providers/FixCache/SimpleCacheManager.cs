using System;

namespace Fix.Caching.Providers.FixCache
{
    public class SimpleCacheManager : ICacheManager
    {
        private static WeakCacheHolder<string, TTLWrapper<object>> weak = new WeakCacheHolder<string, TTLWrapper<object>>();

        public T Get<T>(string key)
        {
            if (weak.TryGetValue(key, out TTLWrapper<object> item))
            {
                if (!item.IsExpired)
                {
                    return (T)item.Data;
                }
                else
                {
                    Remove(key);
                }
            }
            return default;
        }

        public T Get<T>(string key, int cacheTime, Func<T> acquire)
        {
            if (IsSet(key))
            {
                return Get<T>(key);
            }
            else
            {
                var result = acquire();
                Set(key, result, cacheTime);
                return result;
            }
        }

        public bool IsSet(string key)
        {
            return weak.ContainsKey(key);
        }

        public void Remove(string key)
        {
            weak.TryRemove(key, out TTLWrapper<object> value);
        }


        public void Set<T>(string key, T data, int cacheTime)
        {
            Set(key, data, TimeSpan.FromTicks(DateTime.Now.AddSeconds(cacheTime).Ticks));
        }
        public void Set<T>(string key, T data, TimeSpan expiredOn)
        {
            weak.Add(key, new TTLWrapper<object>(data, expiredOn));
        }
    }
}
