using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SubmoduleController : ControllerBase
    {
        private readonly SubmoduleService _submoduleService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SubmoduleController(SubmoduleService submoduleService, IHttpContextAccessor httpContextAccessor)
        {
            _submoduleService = submoduleService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(policy: Permissions.Submodule.Read)]
        [HttpGet]
        public async Task<IActionResult> GetAllSubmodules()
        {
            var creatorName = _httpContextAccessor?.HttpContext?.User.FindFirst("name")?.Value;
            try
            {
                var submodules = await _submoduleService.GetAllSubmodules();
                return Ok(submodules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(policy: Permissions.Submodule.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubmoduleById(int id)
        {
            var creatorName = _httpContextAccessor?.HttpContext?.User.FindFirst("name")?.Value;
            try
            {
                var submodule = await _submoduleService.GetSubmoduleById(id);
                return Ok(submodule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(policy: Permissions.Submodule.Create)]
        [HttpPost]
        public async Task<IActionResult> CreateSubmodule([FromBody] SubmoduleDto submoduleDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creatorName = _httpContextAccessor?.HttpContext?.User.FindFirst("name")?.Value;

            try
            {
                var submodule = await _submoduleService.CreateSubmodule(submoduleDto, creatorName);
                return Ok(submodule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(policy: Permissions.Submodule.Update)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateSubmodule(int id, [FromBody] SubmoduleDto submoduleDto)
        {
            var creatorName = _httpContextAccessor?.HttpContext?.User.FindFirst("name")?.Value;
            try
            {
                var message = await _submoduleService.UpdateSubmodule(id, submoduleDto, creatorName);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(policy: Permissions.Submodule.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubmodule(int id)
        {
            try
            {
                await _submoduleService.DeleteSubmodule(id);
                return Ok(new { message = "Submódulo eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}