using Utils;
using System.Text;
using Newtonsoft.Json;
using DataClasses;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string SENSOR_DATA_MQTT_TOPIC = "sensor_data_topic";
const string KAFKA_DATA_TOPIC = "mqtt_data";

await KafkaHelper.CreateTopic(KAFKA_DATA_TOPIC, 1, 1);
await MqttHelper.SubscribeToTopic("mosquitto-service", 1883, SENSOR_DATA_MQTT_TOPIC, (e) => {
    var recordAsString = Encoding.ASCII.GetString(e.ApplicationMessage.Payload);
    var record = JsonConvert.DeserializeObject<DataRecord>(recordAsString);
    if(record != null)
    {
        Console.WriteLine(record.Speed);
        KafkaHelper.Produce(KAFKA_DATA_TOPIC, "Speed", record.Speed.ToString());
    }
    return Task.CompletedTask;
});

app.Run();
