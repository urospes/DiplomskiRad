using Confluent.Kafka;
using DataClasses;
using Newtonsoft.Json;

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
            e.Cancel = true;
            cts.Cancel();
        };

        using(var consumer = new ConsumerBuilder<string, string>(LoadConsumerConfig()).Build())
        {
            consumer.Subscribe(topic);
            try
            {
                while (true)
                {
                    var consumedEvent = consumer.Consume(cts.Token);
                    Console.WriteLine($"Event with key {consumedEvent.Message.Key} consumed...");
                    //... handle consume
                    Console.WriteLine("Trying to write to influx...");
                    var record = JsonConvert.DeserializeObject<DataRecord>(consumedEvent.Message.Value);
                    if(record != null)
                        InfluxDBHelper.WriteToInflux(record);
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