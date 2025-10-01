using Application.Dto;
using Application.Security;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UserController(UserService userService, IHttpContextAccessor httpContextAccessor)
        {
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Policy = Permissions.User.Create)]
        [HttpPost("create")]
        public async Task<ActionResult<User>> CreateUser([FromBody] UserDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            return Ok(await _userService.CreateUser(dto, creatorName));
        }

        [Authorize(Policy = Permissions.User.Read)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                var users = await _userService.GetAllUsers();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.User.Read)]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetUserByName(string name)
        {
            try
            {
                var user = await _userService.GetUserByName(name);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.User.Read)]
        [HttpGet("{id}/data")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            try
            {
                var user = await _userService.GetUserById(id);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.User.Update)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] PersonDto dto)
        {
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var user = await _userService.UpdateUser(id, dto, creatorName);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.User.Update)]
        [HttpPatch("{id}/state")]
        public async Task<IActionResult> ChangeState(Guid id, [FromBody] UserChangeStateDto dto)
        {
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var user = await _userService.ChangeState(id, dto, creatorName);
                return Ok(user);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.User.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            try
            {
                await _userService.DeleteUser(id);
                return Ok(new { message = "Usuario eliminado" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}