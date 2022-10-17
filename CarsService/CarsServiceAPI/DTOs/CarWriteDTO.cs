namespace CarsServiceAPI.DTOs
{
    public class CarWriteDTO
    {
        public int carId { get; set; }

        public string? manufacturer { get; set; }

        public string? model { get; set; }

        public int year { get; set; }
    }
}
