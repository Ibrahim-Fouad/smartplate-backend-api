using Microsoft.AspNetCore.Mvc;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Dto.Users;
using SmartPlate.API.Models.Users;
using System.Threading.Tasks;

namespace SmartPlate.API.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        /// <summary>
        /// Login for ordinary user.
        /// </summary>
        /// <param name="userForLoginDto"></param>
        /// <returns>User access token</returns>
        /// <response code="200">User logged in and his token generated successfully</response>
        /// <response code="400">UserId or Password is incorrect.</response>
        [HttpPost("user/login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> LoginUser(UserForLoginDto userForLoginDto)
        {
            var userAccessToken = await _authRepository.Login<User>(userForLoginDto.Id, userForLoginDto.Password);
            if (!userAccessToken.Success)
                return BadRequest(new {errorMessage = userAccessToken.ErrorMessage});

            return Ok(userAccessToken);
        }

        /// <summary>
        /// Login for officer user.
        /// </summary>
        /// <param name="userForLoginDto"></param>
        /// <returns>User access token</returns>
        /// <response code="200">User logged in and his token generated successfully</response>
        /// <response code="400">UserId or Password is incorrect.</response>
        [HttpPost("officer/login")]
        public async Task<IActionResult> LoginOfficer(UserForLoginDto userForLoginDto)
        {
            var userAccessToken = await _authRepository.Login<Officer>(userForLoginDto.Id, userForLoginDto.Password);
            if (!userAccessToken.Success)
                return BadRequest(new {errorMessage = userAccessToken.ErrorMessage});

            return Ok(userAccessToken);
        }

        /// <summary>
        /// Login for traffic employee.
        /// </summary>
        /// <param name="userForLoginDto"></param>
        /// <returns>User access token</returns>
        /// <response code="200">User logged in and his token generated successfully</response>
        /// <response code="400">UserId or Password is incorrect.</response>
        [HttpPost("traffic/login")]
        public async Task<IActionResult> LoginTrafficUser(UserForLoginDto userForLoginDto)
        {
            var userAccessToken =
                await _authRepository.Login<TrafficUser>(userForLoginDto.Id, userForLoginDto.Password);
            if (!userAccessToken.Success)
                return BadRequest(new {errorMessage = userAccessToken.ErrorMessage});

            return Ok(userAccessToken);
        }
    }
}