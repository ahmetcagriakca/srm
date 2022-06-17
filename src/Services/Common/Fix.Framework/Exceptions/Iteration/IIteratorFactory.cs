namespace Fix.Exceptions.Iteration
{
    public interface IIteratorFactory : IScoped
    {
        IExceptionIterator Create(string iterator);
    }
}
