using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ControllerBase
    {
        private readonly PermissionService _permissionService;

        public PermissionController(PermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [Authorize(Policy = Permissions.Permission.Read)]
        [HttpGet]
        public async Task<IActionResult> GetAllPermissions()
        {
            try
            {
                var permissions = await _permissionService.GetAllPermissions();
                return Ok(permissions);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Permission.Read)]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetPermissionByName(string name)
        {
            try
            {
                var permission = await _permissionService.GetPermissionByName(name);
                return Ok(permission);
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

        [Authorize(Policy = Permissions.Permission.Create)]
        [HttpPost]
        public async Task<IActionResult> CreatePermission([FromBody] PermissionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creatorName = HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var permission = await _permissionService.CreatePermission(dto, creatorName);
                return Ok(permission);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Permission.Update)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePermission(Guid id, [FromBody] PermissionDto dto)
        {
            var creatorName = HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var permission = await _permissionService.UpdatePermission(id, dto, creatorName);
                return Ok(permission);
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

        [Authorize(Policy = Permissions.Permission.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePermission(Guid id)
        {
            try
            {
                await _permissionService.DeletePermission(id);
                return Ok(new { message = "Permiso eliminado" });
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