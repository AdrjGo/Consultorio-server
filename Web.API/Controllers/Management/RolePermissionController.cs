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
    public class RolePermissionController : ControllerBase
    {
        private readonly RolePermissionService _rolePermissionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RolePermissionController(RolePermissionService rolePermissionService, IHttpContextAccessor httpContextAccessor)
        {
            _rolePermissionService = rolePermissionService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Policy = Permissions.RolePermission.Create)]
        [HttpPost]
        public async Task<IActionResult> AssignPermissionsToRole([FromBody] IEnumerable<RolePermissionDto> rolePermissions)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creatorName = _httpContextAccessor?.HttpContext?.User.FindFirst("name")?.Value;

            try
            {
                await _rolePermissionService.AssignPermissionsToRole(rolePermissions, creatorName);
                return Ok(new { message = "Permisos asignados" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.RolePermission.Delete)]
        [HttpDelete]
        public async Task<IActionResult> RemovePermissionsFromRole([FromBody] IEnumerable<Guid> rolePermissionIds)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _rolePermissionService.RemovePermissionsFromRole(rolePermissionIds);
                return Ok(new { message = "Permisos del rol eliminados" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

    }
}