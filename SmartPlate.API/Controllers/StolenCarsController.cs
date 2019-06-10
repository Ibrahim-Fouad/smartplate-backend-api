using System;
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

        /// <summary>
        /// Report a car is stolen
        /// </summary>
        /// <param name="stolenCarForCreationDto">Report object that has information</param>
        /// <returns></returns>
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

        /// <summary>
        /// Report trying to steal the car plate
        /// </summary>
        /// <param name="carId">Car id</param>
        /// <returns></returns>
        [HttpPost("plate/{carId}")]
        public async Task<IActionResult> PlateHasStolen(int carId)
        {

            var model = new StolenCarForCreationDto
            {
                CarId = carId,
                CarOrPlateIsStoled = 1,
                DateStoled = DateTime.Now,
                LastLocation = "Your Last Parking",
                Latitude = 0,
                Longitude = 0
            };

            var result = await _stolenCarsRepository.AddNewStolenCar(model);

            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }

        /// <summary>
        /// Check if a car has reported stolen or not
        /// </summary>
        /// <param name="carId">Car id</param>
        /// <returns></returns>
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