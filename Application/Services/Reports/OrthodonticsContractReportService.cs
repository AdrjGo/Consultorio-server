using Application.Dto.Reports;
using Domain.Interfaces;

namespace Application.Services
{
    public class OrthodonticsContractReportService
    {
        private readonly IOrthodonticsContractReportRepository _repository;

        public OrthodonticsContractReportService(IOrthodonticsContractReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<OrthodonticsContractDataDto?> GetContractReportDataAsync(Guid contractId)
        {
            var contract = await _repository.GetContractWithDetailsAsync(contractId);
            if (contract?.Patient?.Person == null)
                return null;

            var patient = contract.Patient;

            // Calculate age
            var age = DateTime.Now.Year - patient.Person.BirthDate.Year;
            if (patient.Person.BirthDate > DateOnly.FromDateTime(DateTime.Now.AddYears(-age)))
                age--;

            // Get clinic and form response
            var clinic = await _repository.GetClinicWithManagerAsync();
            var formResponse = await _repository.GetLatestFormResponseAsync(patient.Id, 4);

            // Build DTOs
            var patientInfo = new PatientInfoDto(
                patient.Person.Name,
                patient.Person.LastName,
                patient.Person.BirthDate.ToString("dd-MM-yyyy"),
                patient.Address ?? "",
                patient.HomePhone?.ToString() ?? patient.Person.Phone.ToString(),
                age
            );

            var paymentManagerEntity = contract.PaymentManagers.FirstOrDefault();
            var paymentManagerInfo = paymentManagerEntity != null
                ? new PaymentManagerInfoDto(
                    $"{paymentManagerEntity.Person.Name} {paymentManagerEntity.Person.LastName}",
                    paymentManagerEntity.Parentage ?? "",
                    paymentManagerEntity.Person.Email?.ToString() ?? "",
                    paymentManagerEntity.Person.Phone.ToString(),
                    ""
                )
                : new PaymentManagerInfoDto("", "", "", "", "");

            var doctorInfo = clinic?.Manager?.Person != null
                ? new DoctorInfoDto(
                    $"{clinic.Manager.Person.Name} {clinic.Manager.Person.LastName}",
                    clinic.Manager.Person.Ci,
                    clinic.ClinicName,
                    clinic.ClinicAddress
                )
                : new DoctorInfoDto("", "", "", "");

            return new OrthodonticsContractDataDto(
                patientInfo,
                formResponse?.JsonResponse ?? new object(),
                contract.CreatedAt.ToString("dd-MM-yyyy"),
                contract.TotalCost,
                contract.MonthsDuration,
                paymentManagerInfo,
                doctorInfo
            );
        }
    }
}
