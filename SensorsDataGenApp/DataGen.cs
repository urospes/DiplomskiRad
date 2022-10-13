using CsvHelper;
using System.Globalization;
using Newtonsoft.Json;

public static class DataGen 
{
    private static string SENSOR_DATA_TOPIC = "sensor_data_topic";
    private static string MQTT_URL = "mosquitto.cartracker.com";

    public static async Task StartDataGen(string carId, int interval)
    {
        await ReadCsv("./CSV_Data/car_test_data0.csv", carId, interval);
        Console.WriteLine("Reading done...");
    }

    private static async Task ReadCsv(string fileName, string carId, int interval)
    {
        using(var reader = new StreamReader(fileName))
        {
            using(var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Context.RegisterClassMap<DataRecordClassMap>();
                var records = csv.GetRecords<DataRecord>();
                foreach(var record in records)
                {
                    await Task.Delay(interval);
                    if(record != null)
                    {
                        record.CarId = carId;
                        var recordAsString = JsonConvert.SerializeObject(record);
                        await MqttHelper.PublishToTopic(MQTT_URL, 1883, SENSOR_DATA_TOPIC, recordAsString);
                    }
                }
            }
        }
    }
}