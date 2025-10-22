using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MonitoringController : ControllerBase
    {
        private readonly MonitoringService _monitoringService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MonitoringController(MonitoringService monitoringService, IHttpContextAccessor httpContextAccessor)
        {
            _monitoringService = monitoringService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Policy = Permissions.Monitoring.Read)]
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetMonitoringsByPatientId(Guid patientId)
        {
            try
            {
                var monitorings = await _monitoringService.GetMonitoringsByPatientId(patientId);
                return Ok(monitorings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Monitoring.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMonitoringById(Guid id)
        {
            try
            {
                var monitoring = await _monitoringService.GetMonitoringById(id);
                return Ok(monitoring);
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

        [Authorize(Policy = Permissions.Monitoring.Create)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateMonitoring([FromBody] MonitoringDto dto)
        {
            try
            {
                var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
                var monitoring = await _monitoringService.CreateMonitoring(dto, creatorName);
                return Ok(monitoring);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Monitoring.Update)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateMonitoring(Guid id, [FromBody] MonitoringDto dto)
        {
            try
            {
                var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
                var monitoring = await _monitoringService.UpdateMonitoring(id, dto, creatorName);
                return Ok(monitoring);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Monitoring.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMonitoring(Guid id)
        {
            try
            {
                await _monitoringService.DeleteMonitoring(id);
                return Ok(new { message = "Seguimiento eliminado" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}