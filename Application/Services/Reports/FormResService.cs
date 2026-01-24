using Application.Dto;
using Application.Responses;
using Application.Utils;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class FormResService
    {
        private readonly IFormResRepository _formResRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IFormVersionRepository _formVersionRepository;

        public FormResService(IFormResRepository formResRepository, IContractRepository contractRepository, IFormVersionRepository formVersionRepository)
        {
            _formResRepository = formResRepository;
            _contractRepository = contractRepository;
            _formVersionRepository = formVersionRepository;
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
                throw new KeyNotFoundException($"Este formulario ya está respondido");

            var formRes = new FormRes
            {
                Id = Guid.CreateVersion7(),
                FormVersionId = dto.FormVersionId,
                PatientId = dto.PatientId,
                JsonResponse = dto.JsonResponse,
                State = States.ACTIVE,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                CreatedBy = creatorName,
            };

            var existContract = await _contractRepository.GetContractByPatientId(dto.PatientId);
            if (existContract != null)
                throw new KeyNotFoundException($"El paciente ya tiene un contrato");

            var submodForm = await _formVersionRepository.GetFormVersionById(dto.FormVersionId);

            var contract = new Contract
            {
                Id = Guid.CreateVersion7(),
                SubmodID = submodForm.SubmodID,
                PatientId = dto.PatientId,
                ContractDate = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                State = States.ACTIVE,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                CreatedBy = creatorName,
            };

            await _formResRepository.CreateFormRes(formRes);
            await _contractRepository.CreateContract(contract);


            return new FormResMessageResponse
            {
                Message = "Contrato creado correctamente",
            };

        }
    }
}
