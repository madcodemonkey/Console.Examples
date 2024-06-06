namespace ConsoleScopes.Services;

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
 
    public async Task DoWorkAsync()
    {
        // Content from BBC One: Dr. Who website (©BBC)
        var request = new HttpRequestMessage(HttpMethod.Get,
            "https://www.bbc.co.uk/programmes/b006q2x0");
        var client = _clientFactory.CreateClient();
        var response = await client.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            await response.Content.ReadAsStringAsync();
        }
        else
        {
            throw new Exception("Unable to do remote call!");
        }
    }
}


