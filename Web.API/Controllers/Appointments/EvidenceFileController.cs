using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EvidenceFileController : ControllerBase
    {
        private readonly EvidenceFileService _evidenceFileService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EvidenceFileController(EvidenceFileService evidenceFileService, IHttpContextAccessor httpContextAccessor)
        {
            _evidenceFileService = evidenceFileService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Policy = Permissions.EvidenceFile.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEvidenceFileById(Guid id)
        {
            try
            {
                var evidenceFile = await _evidenceFileService.GetEvidenceFileById(id);
                return Ok(evidenceFile);
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

        [Authorize(Policy = Permissions.EvidenceFile.Read)]
        [HttpGet("patient/{patientId}")]
        public async Task<IActionResult> GetEvidenceFilesByPatientId(Guid patientId)
        {
            try
            {
                var evidenceFiles = await _evidenceFileService.GetEvidenceFileByPatient(patientId);
                return Ok(evidenceFiles);
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

        [Authorize(Policy = Permissions.EvidenceFile.Create)]
        [HttpPost("create")]
        public async Task<IActionResult> CreateEvidenceFile([FromBody] EvidenceFileDto dto)
        {
            try
            {
                var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
                var evidenceFile = await _evidenceFileService.CreateEvidenceFile(dto, creatorName);
                return Ok(evidenceFile);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.EvidenceFile.Update)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateEvidenceFile(Guid id, [FromBody] EvidenceFileUpdateDto dto)
        {
            try
            {
                var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
                var evidenceFile = await _evidenceFileService.UpdateEvidenceFile(id, dto, creatorName);
                return Ok(evidenceFile);
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

        [Authorize(Policy = Permissions.EvidenceFile.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvidenceFile(Guid id)
        {
            try
            {
                await _evidenceFileService.DeleteEvidenceFile(id);
                return Ok(new { message = "Archivo eliminado" });
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