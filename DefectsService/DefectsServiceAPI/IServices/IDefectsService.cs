using DefectsServiceAPI.DTOs;

namespace DefectsServiceAPI.IServices
{
    public interface IDefectsService
    {
        public Task<List<DefectDTO>> GetDefectsForCar(string carId);
    }
}
