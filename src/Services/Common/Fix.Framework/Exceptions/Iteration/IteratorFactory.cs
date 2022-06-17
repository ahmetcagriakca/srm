namespace Fix.Exceptions.Iteration
{
    public class IteratorFactory : IIteratorFactory
    {
        public IExceptionIterator Create(string iterator)
        {
            switch (iterator)
            {
                case "Parallel": return new ParallelIterator();
                case "Sequential": return new SequentialIterator();
                default: return new HandleOneIterator();
            }
        }
    }
}
