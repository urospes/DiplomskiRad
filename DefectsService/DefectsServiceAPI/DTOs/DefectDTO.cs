using MongoDB.Bson;

namespace DefectsServiceAPI.DTOs
{
    public class DefectDTO
    {
        public string? DefectType { get; set; }

        public double Speed { get; set; }

        public double IntakeAirFlowSpeed { get; set; }

        public double BatteryPercentage { get; set; }

        public double EngineVibrationAmplitude { get; set; }

        public double Accelerometer11Value { get; set; }

        public double Accelerometer12Value { get; set; }

        public double Accelerometer21Value { get; set; }

        public double Accelerometer22Value { get; set; }

        public int TirePressure11 { get; set; }

        public int TirePressure12 { get; set; }

        public int TirePressure21 { get; set; }

        public int TirePressure22 { get; set; }

        public double CoolantTemp { get; set; }

        public double BatteryVoltage { get; set; }

        public double IntakeAirTemp { get; set; }

        public double CurrentDraw { get; set; }

        public static DefectDTO ToDTO(BsonDocument defect)
        {
            return new DefectDTO
            {
                DefectType = defect.GetElement("defect").Value.ToString(),
                Speed = defect.GetElement("Speed").Value.ToDouble(),
                IntakeAirFlowSpeed = defect.GetElement("IntakeAirFlowSpeed").Value.ToDouble(),
                BatteryPercentage = defect.GetElement("BatteryPercentage").Value.ToDouble(),
                EngineVibrationAmplitude = defect.GetElement("EngineVibrationAmplitude").Value.ToDouble(),
                Accelerometer11Value = defect.GetElement("Accelerometer11Value").Value.ToDouble(),
                Accelerometer12Value = defect.GetElement("Accelerometer12Value").Value.ToDouble(),
                Accelerometer21Value = defect.GetElement("Accelerometer21Value").Value.ToDouble(),
                Accelerometer22Value = defect.GetElement("Accelerometer22Value").Value.ToDouble(),
                TirePressure11 = defect.GetElement("TirePressure11").Value.ToInt32(),
                TirePressure12 = defect.GetElement("TirePressure12").Value.ToInt32(),
                TirePressure21 = defect.GetElement("TirePressure21").Value.ToInt32(),
                TirePressure22 = defect.GetElement("TirePressure22").Value.ToInt32(),
                CoolantTemp = defect.GetElement("CoolantTemp").Value.ToDouble(),
                BatteryVoltage = defect.GetElement("BatteryVoltage").Value.ToDouble(),
                IntakeAirTemp = defect.GetElement("IntakeAirTemp").Value.ToDouble(),
                CurrentDraw = defect.GetElement("CurrentDraw").Value.ToDouble()
            };
        }
    }
}
