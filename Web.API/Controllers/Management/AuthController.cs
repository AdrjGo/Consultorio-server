using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginUser(dto.Email, dto.Password);

            if (!result.Success)
                return BadRequest(new { message = result.ErrorMessage });

            return Ok(new { token = result.Token });
        }
    }
}