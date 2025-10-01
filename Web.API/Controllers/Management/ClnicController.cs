using Application.Dto;
using Application.Responses;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClnicController : ControllerBase
    {
        private readonly ClinicService _clinicService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ClnicController(ClinicService clinicService, IHttpContextAccessor httpContextAccessor)
        {
            _clinicService = clinicService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Policy = Permissions.Clinic.Create)]
        [HttpPost("create")]
        public async Task<ActionResult<ClinicResponse>> CreateClinic([FromBody] ClinicDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            return Ok(await _clinicService.CraeteClinic(dto, creatorName));
        }

        [Authorize(Policy = Permissions.Clinic.Read)]
        [HttpGet]
        public async Task<IActionResult> GetClinic()
        {
            try
            {
                var clinics = await _clinicService.GetClinic();
                return Ok(clinics);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Clinic.Update)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateClinic(Guid id, [FromBody] ClinicDto dto)
        {
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var clinic = await _clinicService.UpdateClinic(id, dto, creatorName);
                return Ok(clinic);
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