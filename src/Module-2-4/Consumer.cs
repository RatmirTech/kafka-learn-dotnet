namespace Module_2_4;

public class Consumer
{
    public void StartConsuming(CancellationToken token = default)
    {
        var config = new Confluent.Kafka.ConsumerConfig
        {
            BootstrapServers = "localhost:9092",
            GroupId = "some-group",
            AutoOffsetReset = Confluent.Kafka.AutoOffsetReset.Earliest
        };

        using var consumer = new Confluent.Kafka.ConsumerBuilder<string, string>(config)
            .SetKeyDeserializer(Confluent.Kafka.Deserializers.Utf8)
            .SetValueDeserializer(Confluent.Kafka.Deserializers.Utf8)
            .Build();

        consumer.Subscribe("some-topic");

        try
        {
            while (!token.IsCancellationRequested)
            {
                var cr = consumer.Consume(token);
                Console.WriteLine("Получено сообщение: key = {0}, value = {1}, offset = {2}", 
                    cr.Message.Key, cr.Message.Value, cr.Offset);
            }
        }
        catch (OperationCanceledException) when (token.IsCancellationRequested)
        {
            Console.WriteLine("Consumer stopped");
        }
        finally
        {
            consumer.Close();
        }
    }
}