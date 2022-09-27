using CarsServiceAPI.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarsServiceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private ICarsService _CarsService { get; set; }

        public CarsController(ICarsService carsService)
        {
            _CarsService = carsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCars()
        {
            var cars = await _CarsService.GetCars();
            return Ok();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCar([FromRoute] int id)
        {
            var cars = await _CarsService.GetCar(id);
            return Ok();
        }
    }
}
