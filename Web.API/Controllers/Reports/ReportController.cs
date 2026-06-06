using Application.Dto.Reports;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : ControllerBase
    {
        private readonly OrthodonticsContractReportService _orthodonticsService;
        private readonly ClinicalReportService _clinicalReportService;

        public ReportController(
            OrthodonticsContractReportService orthodonticsService,
            ClinicalReportService clinicalReportService)
        {
            _orthodonticsService = orthodonticsService;
            _clinicalReportService = clinicalReportService;
        }

        [Authorize(policy: Permissions.Contract.Read)]
        [HttpGet("contract-data/{contractId:guid}")]
        public async Task<ActionResult> GetContractData(Guid contractId)
        {
            try
            {
                var data = await _orthodonticsService.GetContractReportDataAsync(contractId);
                if (data == null)
                    return NotFound(new { message = "Contrato no encontrado" });

                return Ok(data);
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

        [Authorize(policy: Permissions.ClinicalReport.Read)]
        [HttpGet("clinical-data/{patientId:guid}")]
        public async Task<ActionResult> GetClinicalData(Guid patientId)
        {
            try
            {
                var data = await _clinicalReportService.GetClinicalReportDataAsync(patientId);
                if (data == null)
                    return NotFound(new { message = "Paciente no encontrado" });

                return Ok(data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
