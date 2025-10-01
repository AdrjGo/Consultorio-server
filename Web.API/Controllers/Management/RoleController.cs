using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoleController : ControllerBase
    {
        private readonly RoleService _roleService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RoleController(RoleService roleService, IHttpContextAccessor httpContextAccessor)
        {
            _roleService = roleService;
            _httpContextAccessor = httpContextAccessor;

        }

        [Authorize(Policy = Permissions.Role.Read)]
        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRoles();
                return Ok(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Role.Read)]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetRoleByName(string name)
        {
            try
            {
                var role = await _roleService.GetRoleByName(name);
                return Ok(role);
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

        [Authorize(Policy = Permissions.Role.Read)]
        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetRolesByUserId(Guid userId)
        {
            try
            {
                var roles = await _roleService.GetRolesByUserId(userId);
                return Ok(roles);
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

        [Authorize(Policy = Permissions.Role.Create)]
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] RoleDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creatorName = _httpContextAccessor?.HttpContext?.User.FindFirst("name")?.Value;

            try
            {
                var role = await _roleService.CreateRole(dto, creatorName);
                return Ok(role);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Role.Update)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateRole(Guid id, [FromBody] RoleDto dto)
        {
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var role = await _roleService.UpdateRole(id, dto, creatorName);
                return Ok(role);
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

        [Authorize(Policy = Permissions.Role.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            try
            {
                await _roleService.DeleteRole(id);
                return Ok(new { message = "Rol eliminado" });
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