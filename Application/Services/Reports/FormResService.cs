using Application.Dto;
using Application.Responses;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class FormResService
    {
        private readonly IFormResRepository _formResRepository;

        public FormResService(IFormResRepository formResRepository)
        {
            _formResRepository = formResRepository;
        }

        public async Task<FormResResponse> GetFormResById(Guid id)
        {
            var formRes = await _formResRepository.GetFormResById(id);
            if (formRes == null)
                throw new KeyNotFoundException($"No se encontró el formulario con id {id}");
            return new FormResResponse
            {
                Id = formRes.Id,
                FormversionId = formRes.FormVersionId,
                PatientId = formRes.PatientId,
                JsonResponse = formRes.JsonResponse,
            };
        }

        public async Task<FormResMessageResponse> CreateFormRes(FormResDto dto, string creatorName)
        {
            var existForm = await _formResRepository.GetFormResById(dto.FormVersionId);
            if (existForm != null)
                throw new KeyNotFoundException($"Ya existe un formulario en este submodulo");

            var formRes = new FormRes
            {
                Id = Guid.CreateVersion7(),
                FormVersionId = dto.FormVersionId,
                PatientId = dto.PatientId,
                JsonResponse = dto.JsonResponse,
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName,
            };

            await _formResRepository.CreateFormRes(formRes);
            return new FormResMessageResponse
            {
                Message = "Formulario creado correctamente",
            };

        }
    }
}
