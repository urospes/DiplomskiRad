using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using MeasurementClasses;

namespace Utils;

public static class InfluxDBHelper
{
    private static readonly string TOKEN = "token";
    private static readonly string BUCKET = "car_data";
    private static readonly string ORG = "up_diplomski";
    private static readonly string URL = "http://influxdb-0.influxdb:8086";

    public static void WriteToInflux(string data)
        {
            try
            {
                var influxDBClient = InfluxDBClientFactory.Create(URL, TOKEN);
                using (var writeApi = influxDBClient.GetWriteApi())
                {
                    var speedMeasurement = new SpeedMeasurement
                    {
                        CarId = "1",
                        Value = Convert.ToDouble(data)
                    };
                    writeApi.WriteMeasurement(speedMeasurement, WritePrecision.Ns, BUCKET, ORG);
                }
                influxDBClient.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
}