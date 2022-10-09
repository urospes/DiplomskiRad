using CarsServiceAPI.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson.IO;

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
            Console.WriteLine(cars.Count);
            if(cars != null)
                return Ok(cars);
            return NotFound();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCar([FromRoute] int id)
        {
            var car = await _CarsService.GetCar(id);
            if (car != null)
                return Ok(car);
            return NotFound();
        }

        [HttpGet]
        [Route("Defects/{id}")]
        public async Task<IActionResult> GetCarWithDefects([FromRoute] int id)
        {
            var car = await _CarsService.GetCarWithDefects(id);
            if (car != null)
                return Ok(car);
            return NotFound();
        }
    }
}
