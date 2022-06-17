using System;

namespace Fix.Caching
{
    public interface ICacheManager : IScoped
    {
        T Get<T>(string key);
        T Get<T>(string key, int cacheTime, Func<T> acquire);
        void Set<T>(string key, T data, int cacheTime);
        void Set<T>(string key, T data, TimeSpan expiredOn);
        bool IsSet(string key);
        void Remove(string key);

    }
}
