using Utils;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

await MqttHelper.SubscribeToTopic("mqtt", 1883, "topic", (e) => {
    var message = Encoding.ASCII.GetString(e.ApplicationMessage.Payload);
    Console.WriteLine(message);
    return Task.CompletedTask;
});

app.Run();