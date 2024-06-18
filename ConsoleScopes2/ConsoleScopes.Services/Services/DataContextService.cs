namespace ConsoleScopes.Services;

public class DataContextService : IDataContextService
{
    private static readonly AsyncLocal<string> _userNameAsyncLocal = new();
    private string _userName = "Nobody";

    public DataContextService()
    {
        _userNameAsyncLocal.Value = "None";
    }

    public string UserName
    {
        get
        {
            return _userNameAsyncLocal.Value;
        }
        set
        {
            _userNameAsyncLocal.Value = value;
        }
    }
}