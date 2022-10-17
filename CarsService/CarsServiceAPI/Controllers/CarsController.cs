using CarsServiceAPI.DTOs;
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
            try
            {
                var cars = await _CarsService.GetCars();
                Console.WriteLine(cars.Count);
                if (cars != null)
                    return Ok(cars);
                return NotFound();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCar([FromRoute] int id)
        {
            try
            {
                var car = await _CarsService.GetCar(id);
                if (car != null)
                    return Ok(car);
                return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("Defects/{id}")]
        public async Task<IActionResult> GetCarWithDefects([FromRoute] int id)
        {  
            try
            {
                var car = await _CarsService.GetCarWithDefects(id);
                if (car != null)
                    return Ok(car);
                return NotFound();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCar([FromBody] CarWriteDTO car)
        {  
            try
            {
                var result = await _CarsService.AddCar(car);
                if (result)
                    return Accepted();

                return BadRequest("Car has not been created...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }
    }
}
