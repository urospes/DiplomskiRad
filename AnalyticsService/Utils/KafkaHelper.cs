using Confluent.Kafka;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;

namespace Utils
{
    public static class KafkaHelper
    {
        private static readonly StreamConfig<StringSerDes, StringSerDes> _streamConsumerConfig = LoadConsumerConfig();

        public static StreamConfig<StringSerDes, StringSerDes> LoadConsumerConfig()
        {
            return new StreamConfig<StringSerDes, StringSerDes>
            {
                BootstrapServers = "my-cluster-kafka-bootstrap.kafka.svc.cluster.local:9092",
                ApplicationId = "kafka-app",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                ClientId = "stream-consumer"
            };
        }

        public static void Consume(string topic)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true;
                cts.Cancel();
            };

            using (var consumer = new ConsumerBuilder<string, string>(_streamConsumerConfig.ToConsumerConfig("stream-consumer")).Build())
            {
                consumer.Subscribe(topic);
                try
                {
                    while (true)
                    {
                        var consumedEvent = consumer.Consume(cts.Token);
                        Console.WriteLine($"Event with key {consumedEvent.Message.Value} consumed...");
                        //... handle consume

                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Operation cancelled. Closing consumer...");
                }
                finally
                {
                    consumer.Close();
                }
            }
        }
    }
}
