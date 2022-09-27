using DefectsServiceAPI.IServices;
using Microsoft.AspNetCore.Mvc;

namespace DefectsServiceAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class DefectsController : ControllerBase
    {
        private IDefectsService _DefectsService { get; set; }

        public DefectsController(IDefectsService defectsService)
        {
            _DefectsService = defectsService;
        }

        [HttpGet]
        [Route("{carId}")]

        public async Task<IActionResult> GetDefectsForCar([FromRoute]string carId)
        {
            var result = await _DefectsService.GetDefectsForCar(carId);

            if(result != null)
                return Ok(result);

            return NotFound();
        }
    }
}
