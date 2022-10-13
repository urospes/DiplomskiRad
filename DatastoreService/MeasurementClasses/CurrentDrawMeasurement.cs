using InfluxDB.Client.Core;

namespace MeasurementClasses;

[Measurement("current_draw")]
public class CurrentDrawMeasurement
{
    [Column("car_id", IsTag = true)] public string? CarId { get; set; }

    [Column("value")] public double Value { get; set; }
}