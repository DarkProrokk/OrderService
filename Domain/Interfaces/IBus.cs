namespace Domain.Interfaces;

public interface IBus
{
    Task PublishAsync<T>(string topic, string key, T message, CancellationToken cancellationToken = default);
}