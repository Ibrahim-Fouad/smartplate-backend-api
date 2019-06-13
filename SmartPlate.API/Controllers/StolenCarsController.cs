using System;
using Microsoft.AspNetCore.Mvc;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Dto.StolenCars;
using System.Threading.Tasks;
using SmartPlate.API.Dto;

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

            var result = await _stolenCarsRepository.AddNewStolenCarAsync(stolenCarForCreationDto);

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
                LastLocation = "Your Last Parking location",
                Latitude = 0,
                Longitude = 0
            };

            var result = await _stolenCarsRepository.AddNewStolenCarAsync(model);

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
            var result = await _stolenCarsRepository.CheckForCarAsync(carId);
            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }

        //
        /// <summary>
        /// [Web] Get all stolen cars with ability to sort them
        /// </summary>
        /// <param name="sortBy">Column name to sort with [ id, car, reternedToOwner, stolenObject ]</param>
        /// <param name="orderBy">ASC or DESC</param>
        /// <param name="pageSize">The number of records in the page</param>
        /// <param name="pageNumber">The number of page you want to view.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FilterStolenCars(string sortBy = "id", string orderBy = "asc",
            int pageSize = 10,
            int pageNumber = 1)
        {
            var sortObj = new SortDto(sortBy, orderBy, pageSize, pageNumber);
            return Ok(await _stolenCarsRepository.FilterStolenCarsAsync(sortObj));
        }
    }
}