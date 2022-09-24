using Utils;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string SENSOR_DATA_MQTT_TOPIC = "sensor_data_topic";
const string KAFKA_DATA_TOPIC = "kafka_sensor_data";

await KafkaHelper.CreateTopic(KAFKA_DATA_TOPIC, 1, 1);

await MqttHelper.SubscribeToTopic("mosquitto-service", 1883, SENSOR_DATA_MQTT_TOPIC, (e) => {
    var recordAsString = Encoding.ASCII.GetString(e.ApplicationMessage.Payload);
    if(recordAsString != null && recordAsString.Length > 0)
    {
        KafkaHelper.Produce(KAFKA_DATA_TOPIC, "Record emitted", recordAsString);
    }
    return Task.CompletedTask;
});

await KafkaHelper.ConfigureKafkaStreams(KAFKA_DATA_TOPIC);

app.Run();
