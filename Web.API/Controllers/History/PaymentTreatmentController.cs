using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentTreatmentController : ControllerBase
    {
        private readonly PaymentTreatmentService _paymentTreatmentService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PaymentTreatmentController(PaymentTreatmentService paymentTreatmentService, IHttpContextAccessor httpContextAccessor)
        {
            _paymentTreatmentService = paymentTreatmentService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(Policy = Permissions.PaymentTreatment.Read)]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentTreatmentById(Guid id)
        {
            try
            {
                var paymentTreatment = await _paymentTreatmentService.GetPaymentTreatmentById(id);
                return Ok(paymentTreatment);
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

        [Authorize(Policy = Permissions.PaymentTreatment.Read)]
        [HttpGet("patient/{id}")]
        public async Task<IActionResult> GetAllPaymentTreatmentsByPatientId(Guid id)
        {
            try
            {
                var paymentTreatments = await _paymentTreatmentService.GetAllPaymentTreatmentsByPatientId(id);
                return Ok(paymentTreatments);
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

        [Authorize(Policy = Permissions.PaymentTreatment.Create)]
        [HttpPost("create")]
        public async Task<IActionResult> CreatePaymentTreatment([FromBody] PaymentTreatmentDto dto)
        {
            try
            {
                var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
                var paymentTreatment = await _paymentTreatmentService.CreatePaymentTreatment(dto, creatorName);
                return Ok(paymentTreatment);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [Authorize(Policy = Permissions.PaymentTreatment.Update)]
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePaymentTreatment(Guid id, [FromBody] PaymentTreatmentDto dto)
        {
            try
            {
                var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
                var paymentTreatment = await _paymentTreatmentService.UpdatePaymentTreatment(id, dto, creatorName);
                return Ok(paymentTreatment);
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

        [Authorize(Policy = Permissions.PaymentTreatment.Delete)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentTreatment(Guid id)
        {
            try
            {
                await _paymentTreatmentService.DeletePaymentTreatment(id);
                return Ok(new { message = "Pago eliminado" });
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