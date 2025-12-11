using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class FormService
    {
        private readonly IFormRepository _formRepository;
        // private readonly IFormVersionRepository _formVersionRepository;

        public FormService(IFormRepository formRepository /* IFormVersionRepository formVersionRepository */)
        {
            _formRepository = formRepository;
            // _formVersionRepository = formVersionRepository;
        }

        public async Task<FormVersionResponse> GetFormVersionById(Guid id)
        {
            var formVersion = await _formRepository.GetFormVersionById(id);
            if (formVersion == null)
                throw new KeyNotFoundException($"No se encontró el formulario con id {id}");
            return new FormVersionResponse
            {
                Id = formVersion.Id,
                SubmodId = formVersion.SubmodID.ToString(),
                NumberVersion = formVersion.NumberVersion,
                JsonSchema = formVersion.JsonSchema,
                Form = new FormResponses
                {
                    Id = formVersion.Form.Id,
                    Name = formVersion.Form.Name,
                    Description = formVersion.Form.Description,
                },
            };
        }

        public async Task<FormVersionResponse> GetFormByName(string name)
        {
            var formVersion = await _formRepository.GetFormByName(name);
            if (formVersion == null)
                throw new KeyNotFoundException($"No se encontró el formulario con nombre {name}");

            return new FormVersionResponse
            {
                Id = formVersion.Id,
                SubmodId = formVersion.SubmodID.ToString(),
                NumberVersion = formVersion.NumberVersion,
                JsonSchema = formVersion.JsonSchema,
                Form = new FormResponses
                {
                    Id = formVersion.Form.Id,
                    Name = formVersion.Form.Name,
                    Description = formVersion.Form.Description,
                },
            };
        }

        public async Task<IEnumerable<FormVersionResponse>> GetAllFormVersionsByFormName(string formName)
        {
            var forms = await _formRepository.GetAllFormVersionsByFormName(formName);
            if (forms == null)
                throw new KeyNotFoundException($"No se encontró ningún formulario");
            return forms.Select(f => new FormVersionResponse
            {
                Id = f.Id,
                SubmodId = f.SubmodID.ToString(),
                NumberVersion = f.NumberVersion,
                JsonSchema = f.JsonSchema,
                Form = new FormResponses
                {
                    Id = f.Form.Id,
                    Name = f.Form.Name,
                    Description = f.Form.Description,
                },
            });
        }

        public async Task<IEnumerable<FormVersionResponse>> GetAllFormVersionsByVersion()
        {
            var forms = await _formRepository.GetAllFormVersionsByVersion();
            if (forms == null)
                throw new KeyNotFoundException($"No se encontró ningún formulario");
            return forms.Select(f => new FormVersionResponse
            {
                Id = f.Id,
                SubmodId = f.SubmodID.ToString(),
                NumberVersion = f.NumberVersion,
                JsonSchema = f.JsonSchema,
                Form = new FormResponses
                {
                    Id = f.Form.Id,
                    Name = f.Form.Name,
                    Description = f.Form.Description,
                },
            });
        }

        public async Task<FormVersionMessageResponse> CreateFormVersion(FormVersionDto dto, string creatorName)
        {
            var existForm = await _formRepository.GetFormBySubmodId(dto.SubmodId);
            if (existForm != null)
                throw new KeyNotFoundException($"Ya existe un formulario en este submodulo");

            var formHeader = new Form
            {
                Id = Guid.CreateVersion7(),
                Name = dto.Form.Name,
                Description = dto.Form.Description ?? "",
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName,
            };

            var formVersion = new FormVersion
            {
                Id = Guid.CreateVersion7(),
                SubmodID = dto.SubmodId,
                FormId = formHeader.Id,
                NumberVersion = dto.NumberVersion,
                JsonSchema = dto.JsonSchema,
                Form = formHeader,
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName,
            };

            await _formRepository.CreateFormVersion(formVersion);
            return new FormVersionMessageResponse
            {
                Message = "Formulario creado correctamente",
            };
        }

        public async Task<FormVersionMessageResponse> UpdateFormVersion(Guid id, FormVersionDto dto, string creatorName)
        {
            var formVersion = await _formRepository.GetFormVersionById(id);

            if (formVersion == null)
            {
                throw new KeyNotFoundException($"Formulario {id} no encontrado");
            }

            formVersion.SubmodID = dto.SubmodId;
            formVersion.NumberVersion = dto.NumberVersion;
            formVersion.Form.Name = dto.Form.Name;
            formVersion.Form.Description = dto.Form.Description;

            if (formVersion.JsonSchema == dto.JsonSchema && formVersion.NumberVersion == dto.NumberVersion)
            {
                throw new KeyNotFoundException($"Esta versión ya existe");
            }
            formVersion.JsonSchema = dto.JsonSchema;
            formVersion.Form.UpdatedAt = DateTime.UtcNow;
            formVersion.Form.UpdatedBy = creatorName;

            await _formRepository.UpdateFormVersion(formVersion);
            return new FormVersionMessageResponse
            {
                Message = "Formulario actualizado correctamente",
            };

        }

    }
}