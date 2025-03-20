namespace Domain.Interfaces;

public interface IOrderService
{
    Task Decline(Guid orderGuid);
}