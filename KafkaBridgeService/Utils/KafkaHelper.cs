using Confluent.Kafka;
using Confluent.Kafka.Admin;
using DataClasses;
using Newtonsoft.Json;
using Streamiz.Kafka.Net;
using Streamiz.Kafka.Net.SerDes;
using Streamiz.Kafka.Net.Stream;
using System.Reflection.Metadata.Ecma335;

namespace Utils;

public static class KafkaHelper
{
    private static readonly ProducerConfig _producerConfig = LoadProducerConfig();
    private static readonly StreamConfig<StringSerDes, StringSerDes> _streamConfig = LoadStreamConfig();

    private static StreamConfig<StringSerDes, StringSerDes> LoadStreamConfig()
    {
        return new StreamConfig<StringSerDes, StringSerDes>()
        {
            BootstrapServers = "my-cluster-kafka-bootstrap.kafka.svc.cluster.local:9092",
            MessageSendMaxRetries = 10,
            RetryBackoffMs = 100,
            LingerMs = 10,
            ApplicationId = "kafka-app",
            ClientId = "kafka-client"
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
        using(var producer = new ProducerBuilder<string, string>(_producerConfig).Build())
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
        

        using (var adminClient = new AdminClientBuilder(_streamConfig.ToAdminConfig("kafka-client")).Build())
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

    public static async Task ConfigureKafkaStreams(string inputTopic)
    {
        var streamBuilder = new StreamBuilder();

        var inputStream = streamBuilder.Stream<string, string>(inputTopic);


        foreach (var filterField in (FilterFieldEnum[])Enum.GetValues(typeof(FilterFieldEnum)))
        {
            string outputTopic = $"{filterField}_topic";
            await CreateTopic(outputTopic, 1, 1);

            inputStream.Filter((key, value) => FilterStream(key, value, filterField))
                       .To(outputTopic);
        }

        Topology t = streamBuilder.Build();
        KafkaStream stream = new KafkaStream(t, _streamConfig);
        await stream.StartAsync();
    }


    public static bool FilterStream(string key, string value, FilterFieldEnum filterField)
    {
        var record = JsonConvert.DeserializeObject<DataRecord>(value);
        if (record == null)
            return false;
        Console.WriteLine("u funkciji" + filterField);
        switch (filterField)
        {
            case FilterFieldEnum.batteryPercentage:
                return record.BatteryPercentage < 90;
            case FilterFieldEnum.overheating:
                return record.CoolantTemp > 370.0;
            case FilterFieldEnum.engineVibrations:
                return record.EngineVibrationAmplitude > 1400.0;
            case FilterFieldEnum.tireFailure:
                return !(record.TirePressure11 <= 35 && record.TirePressure11 >= 31 &&
                         record.TirePressure12 <= 35 && record.TirePressure12 >= 31 &&
                         record.TirePressure21 <= 35 && record.TirePressure21 >= 31 &&
                         record.TirePressure22 <= 35 && record.TirePressure22 >= 31);
            case FilterFieldEnum.chassis:
                return !(record.Accelerometer11Value < 3.8 &&
                         record.Accelerometer12Value < 3.8 &&
                         record.Accelerometer21Value < 3.8 &&
                         record.Accelerometer22Value < 3.8);
            default:
                return false;
        }
    }
}