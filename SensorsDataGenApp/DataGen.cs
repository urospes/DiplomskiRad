using CsvHelper;
using System.Globalization;
using Utils;

public static class DataGen 
{
    private static int INTERVAL_MS = 500;

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
                        await MqttHelper.PublishToTopic("localhost", 53300, "topic", "123");
                    }
                }
            }
        }
    }
}