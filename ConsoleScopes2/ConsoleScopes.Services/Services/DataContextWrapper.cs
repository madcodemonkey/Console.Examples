namespace ConsoleScopes.Services;

public class DataContextWrapper  
{

    public DataContextWrapper(IDataContextService dataContextService, string name)
    {
        Context = dataContextService;
        Context.UserName = name;
    }

    public IDataContextService Context { get; private set; }

  
}