namespace Module_2_2;

class Program
{
    private static async Task Main(string[] args)
    {
        using CancellationTokenSource cts = new CancellationTokenSource();

        Console.CancelKeyPress += (sender, eventArgs) =>
        {
            Console.WriteLine("down");
            eventArgs.Cancel = true;
            cts.Cancel();
        };

        try
        {
            if (args[0] == "consumer")
            {
                var consumer = new Consumer();
                
                consumer.StartConsuming(cts.Token);
            }
            else if (args[0] == "producer")
            {
                var producer = new Producer();

                await producer.StartProducing(cts.Token);
            }
        }
        catch (OperationCanceledException) when (cts.IsCancellationRequested)
        {
            Console.WriteLine("Operation canceled by user.");
        }
        finally
        {
            Console.WriteLine("App stopped.");
        }
    }
}