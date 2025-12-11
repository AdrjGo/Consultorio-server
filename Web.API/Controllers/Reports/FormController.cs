using Application.Dto;
using Application.Security;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FormController : ControllerBase
    {
        private readonly FormService _formService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public FormController(FormService formService, IHttpContextAccessor httpContextAccessor)
        {
            _formService = formService;
            _httpContextAccessor = httpContextAccessor;
        }

        [Authorize(policy: Permissions.DynamicForm.Read)]
        [HttpGet("{id}/id")]
        public async Task<ActionResult> GetFormVersionById(Guid id)
        {
            try
            {
                var formVersion = await _formService.GetFormVersionById(id);
                return Ok(formVersion);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(policy: Permissions.DynamicForm.Read)]
        [HttpGet("{name}/name")]
        public async Task<ActionResult> GetFormByName(string name)
        {
            try
            {
                var formVersion = await _formService.GetFormByName(name);
                return Ok(formVersion);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(policy: Permissions.DynamicForm.Read)]
        [HttpGet("{formName}/all")]
        public async Task<ActionResult> GetAllFormVersionsByFormName(string formName)
        {
            try
            {
                var forms = await _formService.GetAllFormVersionsByFormName(formName);
                return Ok(forms);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(policy: Permissions.DynamicForm.Read)]
        [HttpGet("all")]
        public async Task<ActionResult> GetAllForms()
        {
            try
            {
                var forms = await _formService.GetAllFormVersionsByVersion();
                return Ok(forms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [Authorize(policy: Permissions.DynamicForm.Create)]
        [HttpPost]
        public async Task<ActionResult> CreateFormVersion([FromBody] FormVersionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;
            try
            {
                var message = await _formService.CreateFormVersion(dto, creatorName ?? "");
                return Ok(message);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateFormVersion(Guid id, [FromBody] FormVersionDto dto)
        {
            var creatorName = _httpContextAccessor.HttpContext.User.FindFirst("name")?.Value;

            try
            {
                var message = await _formService.UpdateFormVersion(id, dto, creatorName);
                return Ok(new { message = message.Message });
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



    }
}