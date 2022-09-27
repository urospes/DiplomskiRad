using Confluent.Kafka;
using MongoDB.Bson;
using MongoDB.Driver;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;

namespace Utils
{
    public static class KafkaHelper
    {
        private static readonly string KAFKA_URL = "my-cluster-kafka-bootstrap.kafka.svc.cluster.local:9092";

        private static readonly StreamConfig<StringSerDes, StringSerDes> _streamConsumerConfig = LoadConsumerConfig();

        public static StreamConfig<StringSerDes, StringSerDes> LoadConsumerConfig()
        {
            return new StreamConfig<StringSerDes, StringSerDes>
            {
                BootstrapServers = KAFKA_URL,
                ApplicationId = "kafka-app",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                ClientId = "stream-consumer"
            };
        }

        public static async Task Consume(string[] topics, IMongoDatabase db)
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            Console.CancelKeyPress += (_, e) => {
                e.Cancel = true;
                cts.Cancel();
            };

            using (var consumer = new ConsumerBuilder<string, string>(_streamConsumerConfig.ToConsumerConfig("stream-consumer")).Build())
            {
                consumer.Subscribe(topics);
                try
                {
                    while (true)
                    {
                        var consumedEvent = consumer.Consume(cts.Token);
                        //... handle consume
                        var document = BsonDocument.Parse(consumedEvent.Message.Value);
                        document.Add("defect", consumedEvent.Topic.Split('_')[0]);
                        Console.WriteLine(document.ToString());

                        await db.GetCollection<BsonDocument>("defects").InsertOneAsync(document);
                        Console.WriteLine("Wrote to mongo...");
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
