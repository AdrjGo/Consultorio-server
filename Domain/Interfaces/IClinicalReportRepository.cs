using Domain.Entities;

namespace Domain.Interfaces;

public interface IClinicalReportRepository
{
    Task<Patient?> GetPatientWithPersonAsync(Guid patientId);
    Task<FormRes?> GetLatestFormResponseAsync(Guid patientId, int submoduleId);
    Task<List<PretreatmentExam>> GetAllPretreatmentExamsByPatientIdAsync(Guid patientId);
    Task<List<Monitoring>> GetAllMonitoringsByPatientIdAsync(Guid patientId);
}