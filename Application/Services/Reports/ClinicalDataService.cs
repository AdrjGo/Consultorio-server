using Application.Responses;
using Domain.Interfaces;

namespace Application.Services
{
    public class ClinicalDataService
    {
        private readonly IPatientRepository _patientRepository;
        private readonly IClinicRepository _clinicRepository;
        private readonly IContractRepository _contractRepository;
        private readonly IPaymentTreatmentRepository _paymentTreatmentRepository;

        public ClinicalDataService(
            IPatientRepository patientRepository,
            IClinicRepository clinicRepository,
            IContractRepository contractRepository,
            IPaymentTreatmentRepository paymentTreatmentRepository)
        {
            _patientRepository = patientRepository;
            _clinicRepository = clinicRepository;
            _contractRepository = contractRepository;
            _paymentTreatmentRepository = paymentTreatmentRepository;
        }

        public async Task<ClinicalReportDataResponse> GetClinicalReportData(Guid patientId)
        {
            var patient = await _patientRepository.GetPatientById(patientId);
            if (patient == null)
                throw new KeyNotFoundException("No se encontró al paciente");

            var clinic = await _clinicRepository.GetClinic();

            var contracts = await _contractRepository.GetContractsByPatientId(patientId);
            var activeContract = contracts?.FirstOrDefault(c => c.Submodule != null);

            var payments = await _paymentTreatmentRepository.GetAllPaymentTreatmentsByPatientId(patientId);

            var response = new ClinicalReportDataResponse
            {
                PatientId = patient.Id,
                FirstName = patient.Person.Name,
                LastName = patient.Person.LastName,
                DateOfBirth = patient.Person.BirthDate.ToString("dd/MM/yyyy"),
                Gender = patient.Person.Sex.ToString(),

                ClinicName = clinic?.ClinicName ?? string.Empty,
                ClinicAddress = clinic?.ClinicAddress ?? string.Empty,
                ClinicPhone = clinic?.ClinicPhone?.Value ?? string.Empty,
                ClinicLogoUrl = clinic?.LogoUrl ?? string.Empty,

                ContractNumber = activeContract?.Id.ToString() ?? "N/A",
                ContractStartDate = activeContract?.CreatedAt.ToString("dd/MM/yyyy") ?? "N/A",
                ContractStatus = activeContract != null ? "Activo" : "N/A",

                ResponsiblePartyName = patient.PatientResponsible != null
                    ? $"{patient.PatientResponsible.Person.Name} {patient.PatientResponsible.Person.LastName}"
                    : "N/A",
                ResponsiblePartyPhone = patient.PatientResponsible?.Person.Phone?.Value ?? "N/A",
                ResponsiblePartyRelationship = patient.PatientResponsible?.Parentage.ToString() ?? "N/A",

                Payments = payments?.Select(p => new PaymentEntryResponse
                {
                    Date = p.CreatedAt.ToString("dd/MM/yyyy"),
                    Description = p.Observations ?? "Sin descripción",
                    Amount = p.Amount,
                    ReceiptNumber = p.ContractId.ToString()
                }).ToList() ?? new List<PaymentEntryResponse>()
            };

            return response;
        }
    }
}
