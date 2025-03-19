using System.Text;
using System.Text.Json;
using Application.Commands;
using Application.Interfaces;
using Microsoft.Extensions.Logging;
using Orderseer.Common.Models;
using Results;

namespace Infrastructure.External;

public class ProcessingServiceClient(ILogger<ProcessingServiceClient> logger): IProcessingServiceClient
{
    private readonly HttpClient _httpClient;

    public ProcessingServiceClient(IHttpClientFactory clientFactory, ILogger<ProcessingServiceClient> logger) : this(logger)
    {
        _httpClient = clientFactory.CreateClient("ProcessingService");
    }
    
    
    public async Task<OperationResult> CancelStatusAsync(OrderStatusChangeModel data)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
            IncludeFields = true
        };
        
        var json = JsonSerializer.Serialize(data);
        var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
        
        try
        {
            var response = await _httpClient.PutAsync("Order", httpContent);
            var responseBody = await response.Content.ReadAsStringAsync();
            var apiResponse = JsonSerializer.Deserialize<OperationResult>(responseBody, options);
            if (apiResponse is null) return OperationResult.Failure("Bad Operation Result");
            return apiResponse;
        }
        catch (HttpRequestException e)
        {
            return OperationResult.Failure("Exception has occurred while executing the request.");
        }
    }
}