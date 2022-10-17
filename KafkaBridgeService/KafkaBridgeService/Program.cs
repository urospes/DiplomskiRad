using Utils;
using System.Text;
using System;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string SENSOR_DATA_MQTT_TOPIC = "sensor_data_topic";
const string KAFKA_DATA_TOPIC = "kafka_sensor_data";
//string MOSQUITTO = "mosquitto-service";

var pod = Environment.GetEnvironmentVariable("POD_NAME");
if (pod == null)
    return;
var index = pod.Split("-")[3];
string MOSQUITTO = $"mosquitto-deployment-{index}.mosquitto-headless.default.svc.cluster.local";

await KafkaHelper.CreateTopic(KAFKA_DATA_TOPIC, 1, 1);

await MqttHelper.SubscribeToTopic(MOSQUITTO, 1883, SENSOR_DATA_MQTT_TOPIC, (e) => {
    var recordAsString = Encoding.ASCII.GetString(e.ApplicationMessage.Payload);
    if(recordAsString != null && recordAsString.Length > 0)
    {
        KafkaHelper.Produce(KAFKA_DATA_TOPIC, "Record emitted", recordAsString);
    }
    return Task.CompletedTask;
});

await KafkaHelper.ConfigureKafkaStreams(KAFKA_DATA_TOPIC);

app.Run();
