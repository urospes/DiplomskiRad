using Confluent.Kafka;
using Confluent.Kafka.Admin;
using DataClasses;

namespace Utils;

public static class KafkaHelper
{
    private static readonly ProducerConfig _producerConfig = LoadProducerConfig();

    public static ProducerConfig LoadProducerConfig()
    {
        return new ProducerConfig
        {
            BootstrapServers = "my-cluster-kafka-bootstrap.kafka.svc.cluster.local:9092",
            MessageSendMaxRetries = 10,
            RetryBackoffMs = 100,
            LingerMs = 10
        };
    }

    public static void Produce(string topic, string key, string data)
    {
        using(var producer = new ProducerBuilder<string, string>(LoadProducerConfig()).Build())
        {
            Console.WriteLine("Producing a message...");
            producer.Produce(topic, new Message<string, string> {Key = key, Value = data}, handler);
            producer.Flush();
        }
    }

    private static void handler(DeliveryReport<string, string> report)
    {
        if(report.Error.IsError)
        {
            Console.WriteLine("An error occured in producer...");
        }
        else
        {
            Console.WriteLine("Message produced...");
        }
    }

    public static async Task CreateTopic(string name, int numPartitions, short replicationFactor)
    {
        using (var adminClient = new AdminClientBuilder(LoadProducerConfig()).Build())
        {
            try
            {
                await adminClient.CreateTopicsAsync(new List<TopicSpecification> {
                    new TopicSpecification { Name = name, NumPartitions = numPartitions, ReplicationFactor = replicationFactor } });
            }
            catch (CreateTopicsException e)
            {
                if (e.Results[0].Error.Code != ErrorCode.TopicAlreadyExists)
                    Console.WriteLine($"An error occured creating topic {name}: {e.Results[0].Error.Reason}");
                else
                    Console.WriteLine("Topic already exists...");
            }
        }
    }
}