namespace Fix.Tasks.Shell
{
    public interface IShellTask : IDependency
    {
        void Execute();
    }
}
