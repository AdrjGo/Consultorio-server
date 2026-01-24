using Application.Dto;
using Application.Responses;
using Application.Utils;
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

        public async Task<FormVersionResponse> GetFormBySubmodId(int submoduleId, Guid patientId)
        {
            var form = await _formRepository.GetFormBySubmodId(submoduleId);
            if (form == null)
                throw new KeyNotFoundException("No se encontró ningún formulario");

            var respuestaPaciente = form.FormResponse.FirstOrDefault(r => r.PatientId == patientId);

            return new FormVersionResponse
            {
                Id = form.Id,
                SubmodId = form.SubmodID.ToString(),
                NumberVersion = form.NumberVersion,
                JsonSchema = form.JsonSchema,
                Form = new FormResponses
                {
                    Id = form.Form.Id,
                    Name = form.Form.Name,
                    Description = form.Form.Description,
                },
                Response = respuestaPaciente != null
                    ? new FormResResponse
                    {
                        Id = respuestaPaciente.Id,
                        FormversionId = form.Id,
                        PatientId = respuestaPaciente.PatientId,
                        JsonResponse = respuestaPaciente.JsonResponse
                    }
                    : null
            };
        }

        public async Task<FormVersionMessageResponse> CreateFormVersion(FormVersionDto dto, string creatorName)
        {
            var existingForm = await _formRepository.GetFormBySubmodId(dto.SubmodId);

            Guid formId;
            Form? formHeader = null;

            if (existingForm != null)
            {
                formId = existingForm.Form.Id;
            }
            else
            {
                formId = Guid.CreateVersion7();
                formHeader = new Form
                {
                    Id = formId,
                    Name = dto.Form.Name,
                    Description = dto.Form.Description ?? "",
                    State = States.ACTIVE,
                    CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                    CreatedBy = creatorName
                };
            }

            var formVersion = new FormVersion
            {
                Id = Guid.CreateVersion7(),
                SubmodID = dto.SubmodId,
                FormId = formId,
                NumberVersion = dto.NumberVersion,
                JsonSchema = dto.JsonSchema,
                State = States.ACTIVE,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                CreatedBy = creatorName
            };

            if (formHeader != null)
            {
                formVersion.Form = formHeader;
            }

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