using CsvHelper;
using System.Globalization;
using Newtonsoft.Json;

public static class DataGen 
{
    private static int INTERVAL_MS = 1000;
    private static string SENSOR_DATA_TOPIC = "sensor_data_topic";
    private static string MQTT_URL = "app-mosquitto.com";

    public static async Task StartDataGen()
    {
        await ReadCsv("car_test_data.csv");
        Console.WriteLine("Reading done...");
    }

    private static async Task ReadCsv(string fileName)
    {
        using(var reader = new StreamReader(fileName))
        {
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<DataRecordClassMap>();
                var records = csv.GetRecords<DataRecord>();
                foreach(var record in records)
                {
                    await Task.Delay(INTERVAL_MS);
                    if(record != null)
                    {
                        var recordAsString = JsonConvert.SerializeObject(record);
                        await MqttHelper.PublishToTopic(MQTT_URL, 1883, SENSOR_DATA_TOPIC, recordAsString);
                    }
                }
            }
        }
    }
}