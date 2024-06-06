namespace ConsoleScopes.Services;

public class DataContextService : IDataContextService
{
    private readonly AsyncLocal<string> _userNameAsyncLocal;
    private string _userName = "Nobody";

    public DataContextService()
    {
        _userNameAsyncLocal = new AsyncLocal<string>
        {
            Value = "None"
        };
    }

    //public string UserName
    //{
    //    get => _userNameAsyncLocal.Value;
    //    set => _userNameAsyncLocal.Value = value;
    //}

    public string UserName
    {
        get => _userName;
        set => _userName = value;
    }
}