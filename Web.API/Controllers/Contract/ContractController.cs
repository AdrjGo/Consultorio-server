using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContractController : ControllerBase
    {
        private readonly ContractService _contractService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ContractController(ContractService contractService, IHttpContextAccessor httpContextAccessor)
        {
            _contractService = contractService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(policy: Permissions.Contract.Read)]
        [HttpGet("{patientId}")]
        public async Task<ActionResult> GetContractByPatientId(Guid patientId)
        {
            try
            {
                var contract = await _contractService.GetContractByPatientId(patientId);
                return Ok(contract);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(policy: Permissions.Contract.Create)]
        [HttpPost]
        public async Task<ActionResult> CreateContract([FromBody] FormContractRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
            try
            {
                var message = await _contractService.CreateContract(request.FormRes, request.Contract, request.PaymentManager, creatorName ?? "");
                return Ok(message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}