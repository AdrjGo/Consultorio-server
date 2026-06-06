using Application.Dto.Reports;
using Domain.Interfaces;

namespace Application.Services
{
    public class ClinicalReportService
    {
        private readonly IClinicalReportRepository _repository;

        public ClinicalReportService(IClinicalReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<ClinicalReportDataDto?> GetClinicalReportDataAsync(Guid patientId)
        {
            var patient = await _repository.GetPatientWithPersonAsync(patientId);
            if (patient?.Person == null)
                return null;

            // Execute queries sequentially — DbContext is not thread-safe for concurrent operations
            var generalHistoryFormRes = await _repository.GetLatestFormResponseAsync(patientId, 1);
            var pretreatmentExams = await _repository.GetAllPretreatmentExamsByPatientIdAsync(patientId);
            var treatmentSummaryFormRes = await _repository.GetLatestFormResponseAsync(patientId, 3);
            var clinicHistoryFormRes = await _repository.GetLatestFormResponseAsync(patientId, 2);
            var monitorings = await _repository.GetAllMonitoringsByPatientIdAsync(patientId);

            var person = patient.Person;

            var patientInfo = new ClinicalReportPatientInfoDto(
                person.Name,
                person.LastName,
                person.BirthDate.ToString("dd-MM-yyyy"),
                person.Ci,
                person.Phone.ToString(),
                person.Email?.ToString() ?? ""
            );

            var pretreatmentExamDtos = pretreatmentExams.Select(pe =>
                new ClinicalReportPretreatmentExamDto(
                    pe.Id,
                    pe.Observations,
                    pe.Interconsultation,
                    pe.Piece,
                    pe.Caries,
                    pe.Treatment
                )).ToList();

            var monitoringDtos = monitorings.Select(m =>
                new ClinicalReportMonitoringRecordDto(
                    m.Id,
                    m.Appointment.StartDate.ToString("dd-MM-yyyy"),
                    m.Nomenclature,
                    m.Treatment,
                    m.Appointment?.Observations ?? "",
                    m.EvidenceFiles?.Select(ef => ef.Reference?.ToString() ?? "").ToList() ?? new List<string>()
                )).ToList();

            return new ClinicalReportDataDto(
                patientInfo,
                generalHistoryFormRes?.JsonResponse,
                pretreatmentExamDtos,
                treatmentSummaryFormRes?.JsonResponse,
                clinicHistoryFormRes?.JsonResponse,
                monitoringDtos,
                DateTime.UtcNow.ToString("o")
            );
        }
    }
}