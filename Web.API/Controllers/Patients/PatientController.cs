using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly PatientsService _patientsService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PatientController(PatientsService patientsService, IHttpContextAccessor httpContextAccessor)
        {
            _patientsService = patientsService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Policy = Permissions.Patient.Read)]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllPatients()
        {
            try
            {
                var patients = await _patientsService.GetAllPatients();
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Patient.Read)]
        [HttpGet]
        public async Task<IActionResult> GetPagedPatients([FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null,
            [FromQuery] string? state = null)
        {
            try
            {
                var patients = await _patientsService.GetPagedPatients(pageNumber, pageSize, search, state);
                return Ok(patients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.Patient.Read)]
        [HttpGet("{id}/data")]
        public async Task<IActionResult> GetPatientById(Guid id)
        {
            try
            {
                var patient = await _patientsService.GetPatientById(id);
                return Ok(patient);
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

        [Authorize(Policy = Permissions.Patient.Read)]
        [HttpGet("{name}")]
        public async Task<IActionResult> GetPatientByName(string name)
        {
            try
            {
                var patient = await _patientsService.GetPatientByName(name);
                return Ok(patient);
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

        [Authorize(Policy = Permissions.Patient.Create)]
        [HttpPost]
        public async Task<IActionResult> CreatePatient([FromBody] PatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                var patient = await _patientsService.CreatePatient(dto, creatorName);
                return Ok(patient);
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

        [Authorize(Policy = Permissions.Patient.Update)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatient(Guid id, [FromBody] PatientDto dto)
        {
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var patient = await _patientsService.UpdatePatient(id, dto, creatorName);
                return Ok(patient);
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

        [Authorize(Policy = Permissions.Patient.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient(Guid id)
        {
            try
            {
                await _patientsService.DeletePatient(id);
                return Ok(new { message = "Paciente eliminado" });
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