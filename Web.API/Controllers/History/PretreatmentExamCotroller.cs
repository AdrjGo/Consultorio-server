using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/pretreatmentExam")]
    public class PretreatmentExamController : ControllerBase
    {
        private readonly PretreatmentExamService _pretreatmentExamService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PretreatmentExamController(PretreatmentExamService pretreatmentExamService, IHttpContextAccessor httpContextAccessor)
        {
            _pretreatmentExamService = pretreatmentExamService;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpGet("pretreatment/{id}")]
        [Authorize(policy: Permissions.PretreatmentExam.Read)]
        public async Task<IActionResult> GetPretreatmentExamById(Guid id)
        {
            try
            {
                var pretreatmentExam = await _pretreatmentExamService.GetPretreatmentExamById(id);
                return Ok(pretreatmentExam);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(policy: Permissions.PretreatmentExam.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllPretreatmentExamsByPatientId(Guid id)
        {
            try
            {
                var pretreatmentExams = await _pretreatmentExamService.GetAllPretreatmentExamsByPatientId(id);
                return Ok(pretreatmentExams);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(policy: Permissions.PretreatmentExam.Create)]
        [HttpPost]
        public async Task<IActionResult> CreatePretreatmentExam(PretreatmentExamDto pretreatmentExamDto)
        {
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var pretreatmentExam = await _pretreatmentExamService.CreatePretreatmentExam(pretreatmentExamDto, creatorName);
                return Ok(pretreatmentExam);
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

        [Authorize(policy: Permissions.PretreatmentExam.Update)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePretreatmentExam(Guid id, [FromBody] PretreatmentExamDto pretreatmentExamDto)
        {
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var pretreatmentExam = await _pretreatmentExamService.UpdatePretreatmentExam(id, pretreatmentExamDto, creatorName);
                return Ok(pretreatmentExam);
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

        [Authorize(policy: Permissions.PretreatmentExam.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePretreatmentExam(Guid id)
        {
            try
            {
                await _pretreatmentExamService.DeletePretreatmentExam(id);
                return Ok(new { message = "Se eliminó correctamente" });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (KeyNotFoundException ex)
            {
                throw new InvalidOperationException("Error al eliminar examen pretratamiento", ex);
            }
        }
    }
}