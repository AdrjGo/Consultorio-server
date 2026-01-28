using Application.Dto;
using Application.Responses;
using Application.Utils;
using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;

namespace Application.Services
{
    public class ContractService
    {
        private readonly IFormResRepository _formResRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IFormVersionRepository _formVersionRepository;
        private readonly IPaymentManagerRepository _paymentManagerRepository;

        public ContractService(IFormResRepository formResRepository, IContractRepository contractRepository, IFormVersionRepository formVersionRepository, IPaymentManagerRepository paymentManagerRepository)
        {
            _formResRepository = formResRepository;
            _contractRepository = contractRepository;
            _formVersionRepository = formVersionRepository;
            _paymentManagerRepository = paymentManagerRepository;
        }

        public async Task<FullContractResponse> GetContractByPatientId(Guid patientId)
        {
            var contract = await _contractRepository.GetContractByPatientId(patientId);
            if (contract == null)
                throw new KeyNotFoundException($"No se encontró el contrato para el paciente");

            if (contract.PaymentManagers == null || !contract.PaymentManagers.Any())
                throw new InvalidOperationException("El contrato no tiene asignado un responsable de pago");

            var manager = contract.PaymentManagers.First();

            return new FullContractResponse
            {
                Contract = new ContractResponse
                {
                    ContractId = contract.Id,
                    PatientId = contract.PatientId,
                    TotalCost = contract.TotalCost,
                    MonthsDuration = contract.MonthsDuration,
                    Date = contract.CreatedAt.ToString("dd-MM-yyyy"),
                    SubmodID = contract.SubmodID

                },
                PaymentManagerName = manager.Person.Name + " " + manager.Person.LastName,
                PaymentManagerId = manager.Person.Id.ToString()
            };
        }

        public async Task<ContractMessageResponse> CreateContract(FormResDto FormResDto, ContractDto ContractDto, PaymentManagerDto PaymentManagerDto, string creatorName)
        {

            var existForm = await _formResRepository.GetFormResById(FormResDto.FormVersionId);
            if (existForm != null)
                throw new KeyNotFoundException($"Este formulario ya está respondido");

            var formRes = new FormRes
            {
                Id = Guid.CreateVersion7(),
                FormVersionId = FormResDto.FormVersionId,
                PatientId = FormResDto.PatientId,
                JsonResponse = FormResDto.JsonResponse,
                State = States.ACTIVE,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                CreatedBy = creatorName,
            };

            var existContract = await _contractRepository.GetContractByPatientId(FormResDto.PatientId);
            if (existContract != null)
                throw new KeyNotFoundException($"El paciente ya tiene un contrato");

            var submodForm = await _formVersionRepository.GetFormVersionById(FormResDto.FormVersionId);

            var contract = new Contract
            {
                Id = Guid.CreateVersion7(),
                SubmodID = submodForm.SubmodID,
                PatientId = FormResDto.PatientId,
                TotalCost = ContractDto.TotalCost,
                MonthsDuration = ContractDto.MonthsDuration,
                State = States.ACTIVE,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                CreatedBy = creatorName,
            };

            if (contract.Id == Guid.Empty)
            {
                throw new Exception("Error al crear el contrato");
            }

            var payer = new PaymentManager
            {
                Id = Guid.CreateVersion7(),
                ContractId = contract.Id,
                PersonId = PaymentManagerDto.PersonId,
                Parentage = PaymentManagerDto.Parentage,
                State = States.ACTIVE,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                CreatedBy = creatorName,
            };

            await _formResRepository.CreateFormRes(formRes);
            await _contractRepository.CreateContract(contract);
            await _paymentManagerRepository.CreatePaymentManager(payer);

            return new ContractMessageResponse
            {
                Message = "Contrato creado correctamente",
            };
        }
    }
}