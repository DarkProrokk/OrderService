using Confluent.Kafka;
using Domain.Interfaces;

namespace Infrastructure.Messages;

public class Bus(ProducerConfig config): IBus
{
    public async Task PublishAsync<T>(string topic, string key, T message, CancellationToken cancellationToken = default)
    {
        IProducer<string, T> producer = new ProducerBuilder<string, T>(config).Build();
        await producer.ProduceAsync(topic, new Message<string, T> { Value = message });
    }
}