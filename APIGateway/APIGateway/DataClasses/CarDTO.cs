namespace APIGateway.DataClasses
{
    public class CarDTO
    {
        public string? CarId { get; set; }

        public string? Manufacturer { get; set; }

        public string? Model { get; set; }

        public int Year { get; set; }

        public virtual List<CarDefect> Defects { get; set; }
    }
}
