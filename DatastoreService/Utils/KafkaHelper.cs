using Confluent.Kafka;

namespace Utils;

public static class KafkaHelper
{
    private static readonly ConsumerConfig _consumerConfig = LoadConsumerConfig();

    public static ConsumerConfig LoadConsumerConfig()
    {
        return new ConsumerConfig
        {
            BootstrapServers = "my-cluster-kafka-bootstrap.kafka.svc.cluster.local:9092",
            GroupId = "basic_sensor_data",
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true,
            EnableAutoOffsetStore = false
        };
    }

    public static void Consume(string topic)
    {
        CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true; // prevent the process from terminating.
                cts.Cancel();
            };

        using(var consumer = new ConsumerBuilder<string, string>(LoadConsumerConfig()).Build())
        {
            consumer.Subscribe(topic);
            try
            {
                while (true)
                {
                    var cr = consumer.Consume(cts.Token);
                    Console.WriteLine($"Consumed record with value {cr.Message.Value}");
                }
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("Closing consumer...");
            }
            finally
            {
                consumer.Close();
            }
        }
    }
}