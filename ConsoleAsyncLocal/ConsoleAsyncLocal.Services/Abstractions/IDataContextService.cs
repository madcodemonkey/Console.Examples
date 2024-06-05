namespace ConsoleAsyncLocal.Services;

public interface IDataContextService 
{
    void Push(string data);

    string Pop();

    string Peek();
}