using Microsoft.AspNetCore.Mvc;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Dto;
using SmartPlate.API.Dto.Cars;
using System.Threading.Tasks;

namespace SmartPlate.API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly ICarsRepository _carsRepository;

        public CarsController(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        //[HttpGet("id/1")]
        //[Obsolete("For testing only.")]
        //public IActionResult GetCar()
        //{
        //    return Ok(new
        //    {
        //        id = 1,
        //        plateNumber = "ABC 123",
        //        shaseehNumber = 1,
        //        motorNumber = 2,
        //        color = "red",
        //        model = 2019,
        //        marka = "KIA",
        //        type = "SERATO",
        //        vechileType = "prive",
        //        fuel = 95,
        //        capacity = 200,
        //        passengers = 4,
        //        endDate = DateTime.Now.AddYears(10),
        //        startDate = DateTime.Now
        //    });
        //}

        /// <summary>
        /// Create new car.
        /// </summary>
        /// <param name="carForCreationDto">Object of cars details.</param>
        /// <returns>new car object</returns>
        ///<response code="200">Cars added successfully.</response>
        ///<response code="400">Input data error.</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> CreateCar(CarForCreationDto carForCreationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _carsRepository.AddCarAsync(carForCreationDto);
            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }

        /// <summary>
        /// Get car by plate number
        /// </summary>
        /// <param name="plateNumber">Number of plate</param>
        /// <returns>Car object</returns>
        [HttpGet("{plateNumber}")]
        public async Task<IActionResult> GetCarByPlateNumber(string plateNumber)
        {
            var car = await _carsRepository.GetCarMapped(plateNumber);
            if (!car.Success)
                return BadRequest(new {car.ErrorMessage});

            return Ok(car);
        }

        /// <summary>
        /// Get car by id
        /// </summary>
        /// <param name="id">Id of car</param>
        /// <returns>Car object</returns>
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car = await _carsRepository.GetCarMapped(id);
            if (!car.Success)
                return BadRequest(new {car.ErrorMessage});

            return Ok(car);
        }

        /// <summary>
        /// Update car info (change owner of the car)
        /// </summary>
        /// <param name="carId">Id of car</param>
        /// <param name="carForUpdate">Information of new owner or new traffic.</param>
        /// <returns>Car object</returns>
        [HttpPut("{carId}")]
        public async Task<IActionResult> UpdateCar(int carId, CarForUpdateDto carForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateResult = await _carsRepository.UpdateCarDetails(carId, carForUpdate);
            if (!updateResult.Success)
                return BadRequest(new {updateResult.ErrorMessage});

            return Ok(updateResult);
        }

        /// <summary>
        /// Get List of all cars of current logged in user with ability to sort them.
        /// </summary>
        /// <param name="sortBy">Column name to sort with [ id, plateNumber, fuel, vechileType, carModel, model ]</param>
        /// <param name="orderBy">ASC or DESC</param>
        /// <param name="pageSize">The number of records in the page</param>
        /// <param name="pageNumber">The number of page you want to view.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FilterTraffics(string sortBy = "id", string orderBy = "asc", int pageSize = 10,
            int pageNumber = 1)
        {
            var userId = User.FindFirst("id").Value;
            var sortObj = new SortDto(sortBy, orderBy, pageSize, pageNumber);
            return Ok(await _carsRepository.GetUsersCars(userId, sortObj));
        }
    }
}