using Confluent.Kafka;

namespace Utils;

public static class KafkaHelper
{
    private static readonly ProducerConfig _producerConfig = LoadProducerConfig();
    private static readonly ConsumerConfig _consumerConfig = LoadConsumerConfig();

    public static ConsumerConfig LoadConsumerConfig()
    {
        return new ConsumerConfig
        {

        };
    }

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
            Console.WriteLine("Flushing");
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
}