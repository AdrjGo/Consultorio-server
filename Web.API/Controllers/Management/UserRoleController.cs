using Application.Dto;
using Application.Security;
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

        // [Authorize(Policy = Permissions.UserRole.Read)]
        // [HttpGet]
        // public async Task<IActionResult> GetUserRolesByIds([FromBody] IEnumerable<Guid> ids)
        // {
        //     if (!ModelState.IsValid)
        //         return BadRequest(ModelState);

        //     try
        //     {
        //         var userRoles = await _userRoleService.GetUserRolesByIds(ids);
        //         return Ok(userRoles);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, new { message = ex.Message });
        //     }
        // }

        [Authorize(Policy = Permissions.UserRole.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserRolesByUserId(Guid id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var userRoles = await _userRoleService.GetUserRolesByUserId(id);
                return Ok(userRoles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.UserRole.Update)]
        [HttpPatch]
        public async Task<IActionResult> UpdateUserRoles(
        [FromBody] IEnumerable<UserRoleDto> rolesDto)
        {
            if (rolesDto == null || !rolesDto.Any())
                return BadRequest("Debe enviar al menos un rol.");

            var creatorName = User.Identity?.Name ?? "System";

            var result = await _userRoleService.UpdateUserRoles(rolesDto, creatorName);

            return Ok(result);
        }


        [Authorize(Policy = Permissions.UserRole.Create)]
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

        [Authorize(Policy = Permissions.UserRole.Delete)]
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