using Utils;
using System.Text;
using Newtonsoft.Json;
using DataClasses;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

await MqttHelper.SubscribeToTopic("mosquitto-service", 1883, "topic", (e) => {
    var recordAsString = Encoding.ASCII.GetString(e.ApplicationMessage.Payload);
    var record = JsonConvert.DeserializeObject<DataRecord>(recordAsString);
    if(record != null)
    {
        Console.WriteLine(record.Speed);
    }
    return Task.CompletedTask;
});

app.Run();