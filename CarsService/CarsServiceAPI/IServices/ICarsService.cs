using CarsServiceAPI.DTOs;

namespace CarsServiceAPI.IServices
{
    public interface ICarsService
    {
        public Task<List<CarDTO>> GetCars();

        public Task<CarDTO> GetCar(int carId);
    }
}
