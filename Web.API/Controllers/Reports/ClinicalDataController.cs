using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers.Reports
{
    [ApiController]
    [Route("api/report/clinical-data")]
    public class ClinicalDataController : ControllerBase
    {
        private readonly ClinicalDataService _clinicalDataService;

        public ClinicalDataController(ClinicalDataService clinicalDataService)
        {
            _clinicalDataService = clinicalDataService;
        }

        [Authorize(Policy = Permissions.Patient.Read)]
        [HttpGet("{patientId:guid}")]
        public async Task<IActionResult> GetClinicalData(Guid patientId)
        {
            try
            {
                var data = await _clinicalDataService.GetClinicalReportData(patientId);
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
