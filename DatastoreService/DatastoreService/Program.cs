using Utils;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string KAFKA_DATA_TOPIC = "mqtt_data";
KafkaHelper.Consume(KAFKA_DATA_TOPIC);

app.Run();
