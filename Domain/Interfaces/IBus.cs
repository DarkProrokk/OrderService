namespace Domain.Interfaces;

public interface IBus
{
    Task PublishAsync<T>(string topic, T message, CancellationToken cancellationToken = default);
}