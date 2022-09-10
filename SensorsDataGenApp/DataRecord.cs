using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;

public class DataRecord
{
    [Name("speed")]
    public double Speed { get; set; }
    
    [Name("intake_air_flow_speed")]
    public double IntakeAirFlowSpeed { get; set; }

    [Name("battery_percentage")]
    public double BatteryPercentage { get; set; }

    [Name("engine_vibration_amplitude")]
    public double EngineVibrationAmplitude { get; set; }

    [Name("accelerometer11_value")]
    public double Accelerometer11Value { get; set; }

    [Name("accelerometer12_value")]
    public double Accelerometer12Value { get; set; }

    [Name("accelerometer21_value")]
    public double Accelerometer21Value { get; set; }

    [Name("accelerometer_22_value")]
    public double Accelerometer22Value { get; set; }

    [Name("control_unit_firmware")]
    public int ControlUnitFirmware { get; set; }

    [Name("tire_pressure11")]
    public int TirePressure11 { get; set; }

    [Name("tire_pressure12")]
    public int TirePressure12 { get; set; }

    [Name("tire_pressure21")]
    public int TirePressure21 { get; set; }

    [Name("tire_pressure22")]
    public int TirePressure22 { get; set; }

    [Name("coolant_temp")]
    public double CoolantTemp { get; set; }

    [Name("battery_voltage")]
    public double BatteryVoltage { get; set; }

    [Name("intake_air_temp")]
    public double IntakeAirTemp { get; set; }

    [Name("current_draw")]
    public double CurrentDraw { get; set; }

    [Name("throttle_pos")]
    public double ThrottlePos { get; set; }

    [Name("failure_occurred")]
    public bool FailureOccurred { get; set; }
}

public class DataRecordClassMap : ClassMap<DataRecord>
{
    public DataRecordClassMap()
    {
        Map(m => m.Speed).Name("speed");
        Map(m => m.IntakeAirFlowSpeed).Name("intake_air_flow_speed");
        Map(m => m.BatteryPercentage).Name("battery_percentage");
        Map(m => m.EngineVibrationAmplitude).Name("engine_vibration_amplitude");
        Map(m => m.Accelerometer11Value).Name("accelerometer11_value");
        Map(m => m.Accelerometer12Value).Name("accelerometer12_value");
        Map(m => m.Accelerometer21Value).Name("accelerometer21_value");
        Map(m => m.Accelerometer22Value).Name("accelerometer22_value");
        Map(m => m.ControlUnitFirmware).Name("control_unit_firmware");
        Map(m => m.TirePressure11).Name("tire_pressure11");
        Map(m => m.TirePressure12).Name("tire_pressure12");
        Map(m => m.TirePressure21).Name("tire_pressure21");
        Map(m => m.TirePressure22).Name("tire_pressure22");
        Map(m => m.CoolantTemp).Name("coolant_temp");
        Map(m => m.BatteryVoltage).Name("battery_voltage");
        Map(m => m.IntakeAirTemp).Name("intake_air_temp");
        Map(m => m.CurrentDraw).Name("current_draw");
        Map(m => m.ThrottlePos).Name("throttle_pos");
        Map(m => m.FailureOccurred).Name("failure_occurred");
    }
}