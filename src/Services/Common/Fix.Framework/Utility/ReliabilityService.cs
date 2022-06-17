using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Fix.Utility
{
    public class ReliabilityService : IReliabilityService
    {
        public void RetryOnException<TException>(int maxAttempts, TimeSpan delay, Action operation) where TException : Exception
        {
            throw new NotImplementedException();
        }

        public void RetryOnException<TException, TResult>(int maxAttempts, TimeSpan delay, Func<TResult> operation) where TException : Exception
        {
            throw new NotImplementedException();
        }

        public async Task RetryOnExceptionAsync<TException>(int maxAttempts, TimeSpan delay, Func<Task> operation) where TException : Exception
        {
            if (maxAttempts <= 0)
                throw new ArgumentOutOfRangeException(nameof(maxAttempts));

            var attempts = 0;
            while (maxAttempts > attempts)
            {
                try
                {
                    attempts++;
                    await operation();
                    break;
                }
                catch (TException ex)
                {
                    Debug.Write(ex);
                    if (attempts == maxAttempts)
                        throw;

                    await Task.Delay(delay);
                }
            }
        }
    }
}


