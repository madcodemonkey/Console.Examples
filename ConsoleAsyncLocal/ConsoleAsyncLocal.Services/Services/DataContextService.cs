namespace ConsoleAsyncLocal.Services;

public class DataContextService : IDataContextService
{
    private readonly AsyncLocal<Stack<string>> _cache;
  
    public DataContextService()
    {
        _cache = new AsyncLocal<Stack<string>>
        {
            Value = new Stack<string>()
        };
    }
 
    public void Push(string data)
    {
      

        if (_cache.Value != null)
        {
            _cache.Value.Push(data);
        }
    }

    public string Pop()
    {
        if (_cache.Value != null)
        {
           return _cache.Value.Pop();
        }

        return "Nothing";
    }

    public string Peek()
    {
        if (_cache.Value != null && _cache.Value.Count > 0)
        {
            return _cache.Value.Peek();
        }

        return "Nothing";

    }
}