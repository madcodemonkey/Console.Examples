namespace ConsoleScopes.Services;

public class DataContextService : IDataContextService
{
    private readonly AsyncLocal<string> _priorUserNameAsyncLocal;
    private readonly AsyncLocal<string> _userNameAsyncLocal;
    private string _userName = "Nobody";

    public DataContextService()
    {
        _priorUserNameAsyncLocal= new AsyncLocal<string>
        {
            Value = "None"
        };
        _userNameAsyncLocal = new AsyncLocal<string>
        {
            Value = "None"
        };
    }

    public string UserName
    {
        get
        {
            return _userNameAsyncLocal.Value;
        }
        set
        {
            //_priorUserNameAsyncLocal.Value = _userNameAsyncLocal.Value;
            _userNameAsyncLocal.Value = value;
        }
    }

    public void Revert()
    {
        //_priorUserNameAsyncLocal.Value = "Nothing";
        //_userNameAsyncLocal.Value = _priorUserNameAsyncLocal.Value;

    }

    //public string UserName
    //{
    //    get => _userName;
    //    set => _userName = value;
    //}
}

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
