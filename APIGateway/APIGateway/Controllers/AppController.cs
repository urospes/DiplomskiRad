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
        public async Task<IActionResult> GetCarsBasic()
        {
            var result = await _gatewayService.GetCars();
            return Ok(result);
        }
    }
}
