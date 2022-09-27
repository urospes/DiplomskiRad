using DefectsServiceAPI.IServices;
using DefectsServiceAPI.Protos;
using Grpc.Core;

namespace DefectsServiceAPI.Services
{
    public class GrpcService : DefectsRpcService.DefectsRpcServiceBase
    {
        private readonly IDefectsService _DefectsService;
        public GrpcService(IDefectsService defectsService)
        {
            _DefectsService = defectsService;
        }
        public override async Task<DefectsRpcResponse> GetDefectsForCar(DefectsRpcRequest request, ServerCallContext context)
        {
            var defects = await _DefectsService.GetDefectsForCar(request.CarId.ToString());
            var response = new DefectsRpcResponse();

            if (defects != null)
            {
                foreach (var defect in defects)
                {
                    var defectModel = new DefectRpcModel
                    {
                        DefectType = defect.DefectType,
                        Speed = defect.Speed,
                        IntakeAirFlowSpeed = defect.IntakeAirFlowSpeed,
                        BatteryPercentage = defect.BatteryPercentage,
                        EngineVibrationAmplitude = defect.EngineVibrationAmplitude,
                        Accelerometer11Value = defect.Accelerometer11Value,
                        Accelerometer12Value = defect.Accelerometer12Value,
                        Accelerometer21Value = defect.Accelerometer21Value,
                        Accelerometer22Value = defect.Accelerometer22Value,
                        TirePressure11 = defect.TirePressure11,
                        TirePressure12 = defect.TirePressure12,
                        TirePressure21 = defect.TirePressure21,
                        TirePressure22 = defect.TirePressure22,
                        CoolantTemp = defect.CoolantTemp,
                        BatteryVoltage = defect.BatteryVoltage,
                        IntakeAirTemp = defect.IntakeAirTemp,
                        CurrentDraw = defect.CurrentDraw
                    };

                    response.Defects.Add(defectModel);
                }
            }

            return response;
        }
    }
}
