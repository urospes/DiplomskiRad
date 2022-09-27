using CarsServiceAPI.DTOs;
using CarsServiceAPI.IServices;
using MongoDB.Bson;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace CarsServiceAPI.Services
{
    public class CarsService : ICarsService
    {
        private static string MONGODB_URL = "mongodb://mongodb-cars-0.mongodb-cars-headless.default.svc.cluster.local:27017";
        private static readonly MongoClient _MongoClient = new MongoClient(MONGODB_URL);
        private static readonly IMongoDatabase _Database = _MongoClient.GetDatabase("carsDatabase");
        private static readonly IMongoCollection<BsonDocument> _CarsCollection = _Database.GetCollection<BsonDocument>("cars");

        public async Task<CarDTO> GetCar(int carId)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("carId", carId);
                var car = await (await _CarsCollection.FindAsync(filter)).FirstAsync();
                Console.WriteLine(car);
                Console.WriteLine(JsonConvert.SerializeObject(car));

                return new CarDTO
                {
                    //CarId = car.
                };
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
                Console.WriteLine(cars);
                Console.WriteLine(JsonConvert.SerializeObject(cars));

                return new List<CarDTO>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
