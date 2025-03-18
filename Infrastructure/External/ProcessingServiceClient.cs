using Application.Interfaces;

namespace Infrastructure.External;

public class ProcessingServiceClient: IProcessingServiceClient
{
    private readonly HttpClient _httpClient;

    public ProcessingServiceClient(IHttpClientFactory clientFactory)
    {
        _httpClient = clientFactory.CreateClient("ProcessingService");
    }
    
    
    public async Task CancelOrder(Guid guid)
    {
        //var response = await _httpClient.PutAsync();
    }
}