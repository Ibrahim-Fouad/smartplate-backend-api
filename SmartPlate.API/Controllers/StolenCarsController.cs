using Microsoft.AspNetCore.Mvc;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Dto.StolenCars;
using System.Threading.Tasks;

namespace SmartPlate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StolenCarsController : ControllerBase
    {
        private readonly IStolenCarsRepository _stolenCarsRepository;

        public StolenCarsController(IStolenCarsRepository stolenCarsRepository)
        {
            _stolenCarsRepository = stolenCarsRepository;
           
        }

        [HttpPost]
        public async Task<IActionResult> AddNewStolenCar(StolenCarForCreationDto stolenCarForCreationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _stolenCarsRepository.AddNewStolenCar(stolenCarForCreationDto);

            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }

        [HttpGet("{carId}")]
        public async Task<IActionResult> CheckForStolenCar(int carId)
        {
            var result = await _stolenCarsRepository.CheckForCar(carId);
            if (!result.Success)
                return BadRequest(new { result.ErrorMessage });

            return Ok(result);

        }
    }
}