using CarsServiceAPI.DTOs;

namespace CarsServiceAPI.IServices
{
    public interface ICarsService
    {
        public Task<List<CarDTO>> GetCars();

        public Task<CarDTO> GetCar(int carId);

        public Task<CarDTO> GetCarWithDefects(int carId);

        public Task<bool> AddCar(CarWriteDTO car);
    }
}
