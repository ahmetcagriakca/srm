using System;

namespace Fix.Caching.Providers.FixCache
{
    public class TTLWrapper<T>
    {
        public TTLWrapper(T data, TimeSpan expiredOn)
        {
            Data = data;
            ExpiredOn = expiredOn;
        }
        public TimeSpan ExpiredOn { get; set; }

        public T Data { get; set; }

        public bool IsExpired
        {
            get
            {
                return (ExpiredOn < TimeSpan.FromTicks(DateTime.Now.Ticks));
            }
        }
    }
}
