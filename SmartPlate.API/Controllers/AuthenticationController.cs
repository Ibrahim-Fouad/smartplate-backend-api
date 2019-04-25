using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmartPlate.API.Core.Interfaces;
using SmartPlate.API.Dto.Users;
using System;
using System.Threading.Tasks;

namespace SmartPlate.API.Controllers
{
    [Route("api/auth/{userType}")]
    [Produces("application/json")]
    [ApiController]
    [Authorize]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;

        public AuthenticationController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="userType">Type of user [ User, Officer, Traffic ]</param>
        /// <param name="userForRegister">New user data</param>
        /// <returns></returns>
        /// <response code="200">User Created successfully</response>
        /// <response code="400">Some required information is missed.</response> 
        [HttpPost("register")]
        [AllowAnonymous]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterUser(string userType, UserForRegisterDto userForRegister)
        {
            var result = await _authRepository.Register(userType, userForRegister);
            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }

        /// <summary>
        /// Login for users.
        /// </summary>
        /// <param name="userType">Type of user [ User, Officer, Traffic ]</param>
        /// <param name="userForLoginDto">Login data.</param>
        /// <returns>User access token</returns>
        /// <response code="200">User logged in and his token generated successfully</response>
        /// <response code="400">UserId or Password is incorrect.</response>
        [HttpPost("login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(string userType, UserForLoginDto userForLoginDto)
        {
            try
            {
                var userAccessToken =
                    await _authRepository.Login(userType, userForLoginDto.Id, userForLoginDto.Password);
                if (!userAccessToken.Success)
                    return BadRequest(new {errorMessage = userAccessToken.ErrorMessage});

                return Ok(userAccessToken);
            }
            catch (Exception e)
            {
                return StatusCode(500, new {e.Message});
            }
        }

        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="userType">Type of user [ User, Officer, Traffic ]</param>
        /// <param name="changePasswordDto">Password to change.</param>
        /// <returns>New access token.</returns>
        /// <response code="200">User's password has changed and generated new access token.</response>
        /// <response code="400">Password is incorrect or new password not equals confirm password.</response>
        [HttpPut("password")]
        [ProducesResponseType(200)]
        public async Task<IActionResult> ChangePassword(string userType, UserChangePasswordDto changePasswordDto)
        {
            var userId = User.FindFirst("id").Value;
            var result = await _authRepository.ChangePassword(userType, userId, changePasswordDto);
            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }

        /// <summary>
        /// Update current user information
        /// </summary>
        /// <param name="userType">Type of user [ User, Officer, Traffic ]</param>
        /// <param name="userForUpdateDto">New user data for update.</param>
        /// <returns></returns>
        [HttpPut("info")]
        public async Task<IActionResult> UpdateUser(string userType, UserForUpdateDto userForUpdateDto)
        {
            var userId = User.FindFirst("id").Value;
            var result = await _authRepository.UpdateUser(userType, userId, userForUpdateDto);
            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }

        /// <summary>
        /// Get logged in user information.
        /// </summary>
        /// <param name="userType">Type of user [ User, Officer, Traffic ]</param>
        /// <returns></returns>
        [HttpGet("info")]
        public async Task<IActionResult> GetUserInfo(string userType)
        {
            var userId = User.FindFirst("id").Value;
            var result = await _authRepository.GetUserMapped(userType, userId);
            if (!result.Success)
                return BadRequest(new {result.ErrorMessage});

            return Ok(result);
        }


        [HttpPost("seed")]
        [Obsolete("Do not use it.")]
        public IActionResult Seed()
        {
            _authRepository.Seed();
            return Ok();
        }
    }
}