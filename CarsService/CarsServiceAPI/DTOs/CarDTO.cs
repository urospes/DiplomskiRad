namespace CarsServiceAPI.DTOs
{
    public class CarDTO
    {
        public string? CarId { get; set; }

        public string? Manufacturer { get; set; }

        public string? Model { get; set; }

        public int Year { get; set; }

        public CarData? Data { get; set; }
    }
}
