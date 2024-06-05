namespace ConsoleAsyncLocal.Services;

public interface IWorkerService : IDisposable
{
    Task<string> DoWorkAsync(string storeIt);
    string GetData();
}