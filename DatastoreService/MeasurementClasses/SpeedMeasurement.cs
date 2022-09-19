using InfluxDB.Client.Core;

namespace MeasurementClasses;

[Measurement("speed")]
public class SpeedMeasurement
{
    [Column("car_id", IsTag = true)] public string? CarId { get; set; }

    [Column("value")] public double Value { get; set; }
}