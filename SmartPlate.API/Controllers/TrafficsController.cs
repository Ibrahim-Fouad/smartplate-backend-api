using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Dto.Traffics;

namespace SmartPlate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TrafficsController : ControllerBase
    {
        private readonly ITrafficsRepository _trafficsRepository;

        public TrafficsController(ITrafficsRepository trafficsRepository)
        {
            _trafficsRepository = trafficsRepository;
        }

        /// <summary>
        /// Create new traffic.
        /// </summary>
        /// <param name="trafficForCreateDto">Data of new traffic.</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddNewTraffic(TrafficForCreateDto trafficForCreateDto)
        {
            var result = await _trafficsRepository.AddTrafficAsync(trafficForCreateDto);

            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }

        /// <summary>
        /// Get a specific traffic's data.
        /// </summary>
        /// <param name="trafficId">Id of the traffic</param>
        /// <returns></returns>
        [HttpGet("{trafficId}")]
        public async Task<IActionResult> GetTrafficMapped(int trafficId)
        {
            var result = await _trafficsRepository.GetTrafficMappedAsync(trafficId);

            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }

        /// <summary>
        /// Update a specific traffic
        /// </summary>
        /// <param name="trafficId">Id of the traffic</param>
        /// <param name="trafficForUpdateDto">New info of the traffic.</param>
        /// <returns></returns>
        [HttpPut("{trafficId}")]
        public async Task<IActionResult> UpdateTraffic(int trafficId, TrafficForUpdateDto trafficForUpdateDto)
        {
            var result = await _trafficsRepository.EditTrafficAsync(trafficId, trafficForUpdateDto);

            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }
    }
}