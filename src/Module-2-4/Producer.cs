using Confluent.Kafka;

namespace Module_2_4;

public class Producer
{
    public async Task StartProducing(CancellationToken token = default)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = "localhost:9092"
        };

        using var producer = new ProducerBuilder<string, string>(config)
            .SetKeySerializer(Serializers.Utf8)
            .SetValueSerializer(Serializers.Utf8)
            .Build();

        const int messageCount = 100;
        const int parallelism = 5;

        await Parallel.ForEachAsync(
            Enumerable.Range(0, messageCount),
            new ParallelOptions { MaxDegreeOfParallelism = parallelism, CancellationToken = token },
            async (i, ct) =>
            {
                var key = $"user-{i % 10}";
                var value = $"message-{i}";

                try
                {
                    var dr = await producer.ProduceAsync("some-topic", new Message<string, string>
                    {
                        Key = key,
                        Value = value
                    }, ct);

                    Console.WriteLine($"Sent to partition {dr.Partition}: key='{key}', value='{value}'");
                }
                catch (ProduceException<string, string> e)
                {
                    Console.WriteLine($"Delivery failed for key '{key}': {e.Error.Reason}");
                }
            });

        producer.Flush(token);
    }
}