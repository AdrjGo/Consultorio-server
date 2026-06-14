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
        private readonly FinancialReportService _financialReportService;

        public ReportController(
            OrthodonticsContractReportService orthodonticsService,
            ClinicalReportService clinicalReportService,
            FinancialReportService financialReportService)
        {
            _orthodonticsService = orthodonticsService;
            _clinicalReportService = clinicalReportService;
            _financialReportService = financialReportService;
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

        [Authorize(policy: Permissions.FinancialReport.Read)]
        [HttpGet("payment-data/{patientId:guid}")]
        public async Task<ActionResult> GetPaymentData(Guid patientId, [FromQuery] DateOnly? startDate, [FromQuery] DateOnly? endDate)
        {
            try
            {
                var data = await _financialReportService.GetPaymentReportDataAsync(patientId, startDate, endDate);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(policy: Permissions.FinancialReport.Read)]
        [HttpGet("quota-data/{contractId:guid}")]
        public async Task<ActionResult> GetQuotaData(Guid contractId)
        {
            try
            {
                var data = await _financialReportService.GetQuotaReportDataAsync(contractId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [Authorize(policy: Permissions.FinancialReport.Read)]
        [HttpGet("account-statement/{patientId:guid}")]
        public async Task<ActionResult> GetAccountStatement(Guid patientId)
        {
            try
            {
                var data = await _financialReportService.GetAccountStatementDataAsync(patientId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
