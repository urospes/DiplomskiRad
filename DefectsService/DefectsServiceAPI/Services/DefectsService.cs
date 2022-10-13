using DefectsServiceAPI.DTOs;
using DefectsServiceAPI.IServices;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DefectsServiceAPI.Services
{
    public class DefectsService : IDefectsService
    {
        private static string MONGODB_URL = "mongodb://mongodb-data-0.mongodb-data-headless.default.svc.cluster.local:27017";
        private static readonly MongoClient _MongoClient = new MongoClient(MONGODB_URL);
        private static readonly IMongoDatabase _Database = _MongoClient.GetDatabase("defectsDatabase");
        private static readonly IMongoCollection<BsonDocument> _DefectsCollection = _Database.GetCollection<BsonDocument>("defects");

        public async Task<List<DefectDTO>> GetDefectsForCar(string carId)
        {
            try
            {
                var filter = Builders<BsonDocument>.Filter.Eq("CarId", carId);
                var defects = await _DefectsCollection.Find(filter).Limit(10).ToListAsync();

                var defectDTOs = new List<DefectDTO>();
                foreach(var defect in defects)
                {
                    defectDTOs.Add(DefectDTO.ToDTO(defect));
                }

                return defectDTOs;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
