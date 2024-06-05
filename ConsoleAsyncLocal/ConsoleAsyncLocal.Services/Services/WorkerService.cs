namespace ConsoleAsyncLocal.Services;

public class WorkerService : IWorkerService
{
    private readonly IDataContextService _contextService;
    private readonly IHttpClientFactory _clientFactory;

    /// <summary>Constructor</summary>
    public WorkerService(IDataContextService contextService, IHttpClientFactory clientFactory)
    {
        _contextService = contextService;
        _clientFactory = clientFactory;
    }
 
    public async Task<string> DoWorkAsync(string storeIt)
    {
        _contextService.Push(storeIt);
        var data =  await DoExternalAsyncCall();
        return storeIt;
    }

    public string GetData()
    {
        return _contextService.Peek();
    }
 
    public void Dispose()
    {
        _contextService.Pop();
    }

    public async Task<string> DoExternalAsyncCall()
    {
        // Content from BBC One: Dr. Who website (©BBC)
        var request = new HttpRequestMessage(HttpMethod.Get,
            "https://www.bbc.co.uk/programmes/b006q2x0");
        var client = _clientFactory.CreateClient();
        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadAsStringAsync();
        }
        else
        {
            return $"StatusCode: {response.StatusCode}";
        }
    }
}


