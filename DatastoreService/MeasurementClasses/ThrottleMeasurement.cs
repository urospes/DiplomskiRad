using InfluxDB.Client.Core;

namespace MeasurementClasses;

[Measurement("throttle")]
public class ThrottleMeasurement
{
    [Column("car_id", IsTag = true)] public string? CarId { get; set; }

    [Column("value")] public double Value { get; set; }
}