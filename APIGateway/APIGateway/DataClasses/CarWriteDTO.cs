namespace APIGateway.DataClasses
{
    public class CarWriteDTO
    {
        public int CarId { get; set; }

        public string? Manufacturer { get; set; }

        public string? Model { get; set; }

        public int Year { get; set; }
    }
}
