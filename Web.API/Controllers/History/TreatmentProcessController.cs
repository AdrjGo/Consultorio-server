using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/treatmentProcess")]
    public class TreatmentProcess : ControllerBase
    {
        private readonly TreatmentProgressService _treatmentProgressService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TreatmentProcess(TreatmentProgressService treatmentProgressService, IHttpContextAccessor httpContextAccessor)
        {
            _treatmentProgressService = treatmentProgressService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(policy: Permissions.TreatmentProgress.Read)]
        [HttpGet("treatment/{id}")]
        public async Task<IActionResult> GetTreatmentProgressById(Guid id)
        {
            try
            {
                var treatmentProgress = await _treatmentProgressService.GetTreatmentProgressById(id);
                return Ok(treatmentProgress);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(policy: Permissions.TreatmentProgress.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllTreatmentProgressByPatientId(Guid id)
        {
            try
            {
                var treatmentProgress = await _treatmentProgressService.GetAllTreatmentProgressByPatientId(id);
                return Ok(treatmentProgress);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(policy: Permissions.TreatmentProgress.Create)]
        [HttpPost]
        public async Task<IActionResult> CreateTreatmentProgress(TreatmentProgressDto treatmentProgressDto)
        {
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var treatmentProgress = await _treatmentProgressService.CreateTreatmentProgress(treatmentProgressDto.PatientId, treatmentProgressDto.Payment, creatorName);
                return Ok(treatmentProgress);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(policy: Permissions.TreatmentProgress.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTreatmentProgress(Guid id)
        {
            try
            {
                await _treatmentProgressService.DeleteTreatmentProgress(id);
                return Ok(new { message = "Se eliminó correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}