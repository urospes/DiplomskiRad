using APIGateway.DataClasses;
using APIGateway.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace APIGateway.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AppController : ControllerBase
    {
        private readonly IGatewayService _gatewayService;
        public AppController(IGatewayService gatewayService)
        {
            _gatewayService = gatewayService;
        }

        [HttpGet]
        [Route("cars")]
        public async Task<IActionResult> GetCarsBasic()
        {
            try
            {
                var result = await _gatewayService.GetCars();
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("cars/{id}")]
        public async Task<IActionResult> GetCarWithDefects([FromRoute] int id)
        {
            try
            {
                var result = await _gatewayService.GetCarWithDefects(id);
                if (result == null)
                    return NotFound();

                return Ok(result);
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
                var result = await _gatewayService.AddCar(car);
                if (result)
                    return Created("Car Added", car);

                return BadRequest("Car not inserted...");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(500);
            }
        }
    }
}
