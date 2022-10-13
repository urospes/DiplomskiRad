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
            var result = await _gatewayService.GetCars();
            if(result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpGet]
        [Route("cars/{id}")]
        public async Task<IActionResult> GetCarWithDefects([FromRoute] int id)
        {
            var result = await _gatewayService.GetCarWithDefects(id);
            if (result == null)
                return NotFound();

            return Ok(result);
        }
    }
}
