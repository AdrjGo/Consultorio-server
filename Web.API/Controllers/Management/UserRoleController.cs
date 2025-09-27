using Application.Dto;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserRoleController : ControllerBase
    {
        private readonly UserRoleService _userRoleService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserRoleController(UserRoleService userRoleService, IHttpContextAccessor httpContextAccessor)
        {
            _userRoleService = userRoleService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AssignRolesToUser([FromBody] IEnumerable<UserRoleDto> roleIds)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creatorName = _httpContextAccessor?.HttpContext?.User.FindFirst("name")?.Value;

            try
            {
                await _userRoleService.AssignRolesToUser(roleIds, creatorName);
                return Ok(new { message = "Roles asignados" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> RemoveRolesFromUser([FromBody] IEnumerable<Guid> roleIds)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creatorName = _httpContextAccessor?.HttpContext?.User.FindFirst("name")?.Value;

            try
            {
                await _userRoleService.RemoveRolesFromUser(roleIds);
                return Ok(new { message = "Roles del usuario eliminados" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}