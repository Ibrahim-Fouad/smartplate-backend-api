using Microsoft.AspNetCore.Mvc;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Dto;
using SmartPlate.API.Dto.Cars;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace SmartPlate.API.Controllers
{
    [Route("api/cars")]
    [ApiController]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private readonly ICarsRepository _carsRepository;

        public CarsController(ICarsRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        //[HttpGet("id/1")]
        //[Obsolete("For testing only.")]
        //public IActionResult GetCarAsync()
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
        /// [Web] Create new car.
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
        /// [All] Get car by plate number
        /// </summary>
        /// <param name="plateNumber">Number of plate</param>
        /// <returns>Car object</returns>
        [HttpGet("{plateNumber}")]
        public async Task<IActionResult> GetCarByPlateNumber(string plateNumber)
        {
            var car = await _carsRepository.GetCarMappedAsync(plateNumber);
            if (!car.Success)
                return BadRequest(new {car.ErrorMessage});

            return Ok(car);
        }

        /// <summary>
        /// [All] Get car by id
        /// </summary>
        /// <param name="id">Id of car</param>
        /// <returns>Car object</returns>
        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetCarById(int id)
        {
            var car = await _carsRepository.GetCarMappedAsync(id);
            if (!car.Success)
                return BadRequest(new {car.ErrorMessage});

            return Ok(car);
        }

        /// <summary>
        /// [Web] Update car info (change owner of the car)
        /// </summary>
        /// <param name="carId">Id of car</param>
        /// <param name="carForUpdate">Information of new owner or new traffic.</param>
        /// <returns>Car object</returns>
        [HttpPut("{carId}")]
        public async Task<IActionResult> UpdateCar(int carId, CarForUpdateDto carForUpdate)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updateResult = await _carsRepository.UpdateCarDetailsAsync(carId, carForUpdate);
            if (!updateResult.Success)
                return BadRequest(new {updateResult.ErrorMessage});

            return Ok(updateResult);
        }

        /// <summary>
        /// [Web, Android] Get List of all cars of current logged in user with ability to sort them.
        /// </summary>
        /// <param name="userId">User id </param>
        /// <param name="sortBy">Column name to sort with [ id, plateNumber, fuel, vechileType, carModel, model ]</param>
        /// <param name="orderBy">ASC or DESC</param>
        /// <param name="pageSize">The number of records in the page</param>
        /// <param name="pageNumber">The number of page you want to view.</param>
        /// <returns></returns>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetUserCars(string userId = "me", string sortBy = "id", string orderBy = "asc",
            int pageSize = 10,
            int pageNumber = 1)
        {
            userId = userId == "me" ? User.FindFirst("id").Value : userId;
            var sortObj = new SortDto(sortBy, orderBy, pageSize, pageNumber);
            return Ok(await _carsRepository.GetUsersCarsAsync(userId, sortObj));
        }

        /// <summary>
        /// [Web] Get List of all cars with ability to sort them.
        /// </summary>
        /// <param name="sortBy">Column name to sort with [ id, plateNumber, fuel, vechileType, carModel, model ]</param>
        /// <param name="orderBy">ASC or DESC</param>
        /// <param name="pageSize">The number of records in the page</param>
        /// <param name="pageNumber">The number of page you want to view.</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllCars(string sortBy = "id", string orderBy = "asc", int pageSize = 10,
            int pageNumber = 1)
        {
            //var userId = User.FindFirst("id").Value;
            var sortObj = new SortDto(sortBy, orderBy, pageSize, pageNumber);
            return Ok(await _carsRepository.GetAllCarsAsync(sortObj));
        }
    }
}