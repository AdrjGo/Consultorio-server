using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppointmentController(AppointmentService appointmentService, IHttpContextAccessor httpContextAccessor)
        {
            _appointmentService = appointmentService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Policy = Permissions.Appointment.Read)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllAppointments()
        {
            try
            {
                var appointments = await _appointmentService.GetAllAppointments();
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Appointment.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAppointmentById(Guid id)
        {
            try
            {
                var appointment = await _appointmentService.GetAppointmentById(id);
                return Ok(appointment);
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

        [Authorize(Policy = Permissions.Appointment.Read)]
        [HttpGet("date")]
        public async Task<IActionResult> GetAppointmentsByDate([FromQuery] string? initialDate, [FromQuery] string? finalDate)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByDate(initialDate, finalDate);
                return Ok(appointments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Appointment.Read)]
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetAppointmentsByPatientId(Guid patientId)
        {
            try
            {
                var appointments = await _appointmentService.GetAppointmentsByPatientId(patientId);
                return Ok(appointments);
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

        [Authorize(Policy = Permissions.Appointment.Create)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentDto dto)
        {
            try
            {
                var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
                var appointment = await _appointmentService.CreateAppointment(dto, creatorName);
                return Ok(appointment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Appointment.Update)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAppointment(Guid id, [FromBody] AppointmentUpdateDto dto)
        {
            try
            {
                var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
                var appointment = await _appointmentService.UpdateAppointment(id, dto, creatorName);
                return Ok(appointment);
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

        [Authorize(Policy = Permissions.Appointment.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAppointment(Guid id)
        {
            try
            {
                await _appointmentService.DeleteAppointment(id);
                return Ok(new { message = "Cita eliminada" });
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