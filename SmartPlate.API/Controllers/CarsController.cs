using Microsoft.AspNetCore.Mvc;
using System;

namespace SmartPlate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        [HttpGet("id/1")]
        [Obsolete("For testing only.")]
        public IActionResult GetCar()
        {
            return Ok(new
            {
                id = 1,
                plateNumber = "ABC 123",
                shaseehNumber = 1,
                motorNumber = 2,
                color = "red",
                model = 2019,
                marka = "KIA",
                type = "SERATO",
                vechileType = "prive",
                fuel = 95,
                capacity = 200,
                passengers = 4,
                endDate = DateTime.Now.AddYears(10),
                startDate = DateTime.Now
            });
        }
    }
}