namespace Application.Interfaces;

public interface IProcessingServiceClient
{
    Task CancelOrder(Guid guid);
}