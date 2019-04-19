using Microsoft.AspNetCore.Mvc;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Dto.Users;
using SmartPlate.API.Models.Users;
using System.Threading.Tasks;

namespace SmartPlate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("user/login")]
        public async Task<IActionResult> LoginUser(UserForLoginDto userForLoginDto)
        {
            var userAccessToken = await _authRepository.Login<User>(userForLoginDto.Id, userForLoginDto.Password);
            if (!userAccessToken.Success)
                return BadRequest(new {errorMessage = userAccessToken.ErrorMessage});

            return Ok(userAccessToken);
        }

        [HttpPost("officer/login")]
        public async Task<IActionResult> LoginOfficer(UserForLoginDto userForLoginDto)
        {
            var userAccessToken = await _authRepository.Login<Officer>(userForLoginDto.Id, userForLoginDto.Password);
            if (!userAccessToken.Success)
                return BadRequest(new {errorMessage = userAccessToken.ErrorMessage});

            return Ok(userAccessToken);
        }

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