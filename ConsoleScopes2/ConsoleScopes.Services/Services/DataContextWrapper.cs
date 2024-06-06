namespace ConsoleScopes.Services;

public class DataContextWrapper : IDisposable
{

    public DataContextWrapper(IDataContextService dataContextService, string name)
    {
        Context = dataContextService;
        Context.UserName = name;
    }

    public IDataContextService Context { get; private set; }

    public void Dispose()
    {
        Context.Revert();
    }
}