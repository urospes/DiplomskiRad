using CarsServiceAPI.DTOs;
using CarsServiceAPI.IServices;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace CarsServiceAPI.Services
{
    public class CarsService : ICarsService
    {
        private static string MONGODB_URL = "mongodb://mongodb-cars-headless.default.svc.cluster.local:27017";
        private static readonly MongoClient _MongoClient = new MongoClient(MONGODB_URL);
        private static readonly IMongoDatabase _Database = _MongoClient.GetDatabase("carsDatabase");
        private static readonly IMongoCollection<BsonDocument> _CarsCollection = _Database.GetCollection<BsonDocument>("cars");

        public async Task<CarDTO> GetCar(int carId)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("carId", carId);
                var car = await (await _CarsCollection.FindAsync(filter)).FirstAsync();
                Console.WriteLine(JsonConvert.SerializeObject(car));

                if (car != null)
                    return CarDTO.ToDTO(car);
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<List<CarDTO>> GetCars()
        {
            try
            {
                var cars = await (await _CarsCollection.FindAsync(new BsonDocument())).ToListAsync();

                var carDTOs = new List<CarDTO>();

                if(cars != null)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(cars));
                    foreach (var car in cars)
                        carDTOs.Add(CarDTO.ToDTO(car));
                }

                return carDTOs;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public async Task<CarDTO> GetCarWithDefects(int carId)
        {
            var carDTO = await GetCar(carId);
            if (carDTO == null)
                return null;

            var defects = await GrpcClient.GetDefects(carId);
            if (defects == null)
                return null;

            carDTO.Data = new List<CarData>();
            foreach (var defect in defects.Defects)
            {
                carDTO.Data.Add(new CarData
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
                });
            }

            return carDTO;
        }

        public async Task<bool> AddCar(CarWriteDTO car)
        {
            try
            {
                Console.WriteLine("evo meee");
                var bsonCar = car.ToBsonDocument();
                Console.WriteLine(bsonCar);

                if (bsonCar == null)
                    return false;

                await _CarsCollection.InsertOneAsync(bsonCar);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
