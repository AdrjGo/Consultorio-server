using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormResController : ControllerBase
    {

        private readonly FormResService _formResService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FormResController(FormResService formResService, IHttpContextAccessor httpContextAccessor)
        {
            _formResService = formResService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(policy: Permissions.DynamicForm.Read)]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFormResById(Guid id)
        {
            try
            {
                var formRes = await _formResService.GetFormResById(id);
                return Ok(formRes);
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

        [Authorize(policy: Permissions.DynamicForm.Create)]
        [HttpPost]
        public async Task<ActionResult> CreateFormRes([FromBody] FormResDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
            try
            {
                var message = await _formResService.CreateFormRes(dto, creatorName ?? "");
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