using Confluent.Kafka;

namespace Module_2_2;

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
        try
        {
            var dr = await producer.ProduceAsync("some-topic",
                new Message<string, string> { Key = "UserId", Value = "Ivan_1234" }, token);
            Console.WriteLine($"Delivered '{dr.Value}' to '{dr.TopicPartitionOffset}'");
        }
        catch (ProduceException<string, string> e)
        {
            Console.WriteLine($"Delivery failed: {e.Error.Reason}");
        }
        finally
        {
            producer.Flush(token);
        }
    }
}