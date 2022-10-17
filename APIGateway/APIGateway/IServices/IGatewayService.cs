using APIGateway.DataClasses;

namespace APIGateway.IServices
{
    public interface IGatewayService
    {
        public Task<List<CarDTO>> GetCars();

        public Task<CarDTO> GetCarWithDefects(int id);

        public Task<bool> AddCar(CarWriteDTO car);
    }
}
