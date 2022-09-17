using MQTTnet;
using MQTTnet.Client;

public static class MqttHelper
{
    public static async Task PublishToTopic(string server, int port, string topic, string payload)
        {
            var mqttFactory = new MqttFactory();
            using (var mqttClient = mqttFactory.CreateMqttClient())
            {
                var mqttClientOptions = new MqttClientOptionsBuilder()
                    .WithTcpServer(server, port)
                    .Build();

                ConfigureOnConnectionStatusChangedListeners(mqttClient);

                await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

                var applicationMessage = new MqttApplicationMessageBuilder()
                    .WithTopic(topic)
                    .WithPayload(payload)
                    .Build();

                await mqttClient.PublishAsync(applicationMessage, CancellationToken.None);
            }
        }
    
    public static async Task SubscribeToTopic(string server, int port, string topic, Func<MqttApplicationMessageReceivedEventArgs, Task> handler)
        {
            var mqttFactory = new MqttFactory();
            var mqttClient = mqttFactory.CreateMqttClient();
            var mqttClientOptions = new MqttClientOptionsBuilder()
                .WithTcpServer(server, port)
                .Build();

            ConfigureOnConnectionStatusChangedListeners(mqttClient);
            mqttClient.ApplicationMessageReceivedAsync += handler;

            await mqttClient.ConnectAsync(mqttClientOptions, CancellationToken.None);

            var mqttSubscribeOptions = mqttFactory.CreateSubscribeOptionsBuilder()
                .WithTopicFilter(f => { f.WithTopic(topic); })
                .Build();

            await mqttClient.SubscribeAsync(mqttSubscribeOptions, CancellationToken.None);
        }


    #region Private Methods

        private static void ConfigureOnConnectionStatusChangedListeners(IMqttClient mqttClient)
        {
             mqttClient.ConnectedAsync += (e) =>
                {
                    if (e.ConnectResult.ResultCode == MqttClientConnectResultCode.Success)
                    {
                        Console.WriteLine("Connected to client...");
                    }
                    return Task.CompletedTask;
                };
                mqttClient.DisconnectedAsync += async (e) => {
                    if(e.ClientWasConnected)
                    {
                        Console.WriteLine("Disconnected from client. Trying to reconnect...");
                        await mqttClient.ConnectAsync(mqttClient.Options);
                    }
                };
        }

    #endregion
}