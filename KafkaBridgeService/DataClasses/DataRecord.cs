namespace DataClasses;

public class DataRecord
{
    public string CarId { get; set; }
    
    public double Speed { get; set; }
    
    public double IntakeAirFlowSpeed { get; set; }

    public double BatteryPercentage { get; set; }

    public double EngineVibrationAmplitude { get; set; }

    public double Accelerometer11Value { get; set; }

    public double Accelerometer12Value { get; set; }

    public double Accelerometer21Value { get; set; }

    public double Accelerometer22Value { get; set; }

    public int ControlUnitFirmware { get; set; }

    public int TirePressure11 { get; set; }

    public int TirePressure12 { get; set; }

    public int TirePressure21 { get; set; }

    public int TirePressure22 { get; set; }

    public double CoolantTemp { get; set; }

    public double BatteryVoltage { get; set; }

    public double IntakeAirTemp { get; set; }

    public double CurrentDraw { get; set; }

    public double ThrottlePos { get; set; }

    public bool FailureOccurred { get; set; }
}