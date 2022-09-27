using MongoDB.Bson;

namespace CarsServiceAPI.DTOs
{
    public class CarDTO
    {
        public string? CarId { get; set; }

        public string? Manufacturer { get; set; }

        public string? Model { get; set; }

        public int Year { get; set; }

        public virtual List<CarData> Data { get; set; }

        public static CarDTO ToDTO(BsonDocument car)
        {
            return new CarDTO
            {
                CarId = car.GetElement("carId").Value.ToString(),
                Manufacturer = car.GetElement("manufacturer").Value.ToString(),
                Model = car.GetElement("model").Value.ToString(),
                Year = car.GetElement("year").Value.ToInt32(),
                Data = new List<CarData>()
            };
        }
    }
}
