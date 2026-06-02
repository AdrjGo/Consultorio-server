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

        public ReportController(OrthodonticsContractReportService orthodonticsService)
        {
            _orthodonticsService = orthodonticsService;
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
    }
}
