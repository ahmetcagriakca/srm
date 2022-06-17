using System;
using System.Threading.Tasks;

namespace Fix.Utility
{
    public interface IReliabilityService : IDependency
    {
        Task RetryOnExceptionAsync<TException>(int maxAttempts, TimeSpan delay, Func<Task> operation) where TException : Exception;
        void RetryOnException<TException>(int maxAttempts, TimeSpan delay, Action operation) where TException : Exception;
        void RetryOnException<TException, TResult>(int maxAttempts, TimeSpan delay, Func<TResult> operation) where TException : Exception;
    }
}
