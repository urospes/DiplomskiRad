using CsvHelper;
using System.Globalization;
using Utils;
using Newtonsoft.Json;

public static class DataGen 
{
    private static int INTERVAL_MS = 1000;

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
                        await MqttHelper.PublishToTopic("my-mosquitto.com", 1883, "topic", recordAsString);
                    }
                }
            }
        }
    }
}