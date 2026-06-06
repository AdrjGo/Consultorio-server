namespace Application.Dto.Reports;

public record ClinicalReportPatientInfoDto(
    string Name,
    string LastName,
    string BirthDate,
    string Ci,
    string Phone,
    string Email);

public record ClinicalReportMonitoringRecordDto(
    Guid Id,
    string Date,
    string Nomenclature,
    string Treatment,
    string Observations,
    List<string> Files);

public record ClinicalReportPretreatmentExamDto(
    Guid Id,
    string Observations,
    string Interconsultation,
    string Piece,
    bool Caries,
    string? Treatment);

public record ClinicalReportClinicInfoDto(
    string ClinicAddress,
    string ClinicPhone,
    string ClinicEmail,
    string City);

public record ClinicalReportDataDto(
    ClinicalReportPatientInfoDto Patient,
    object? GeneralHistory,
    List<ClinicalReportPretreatmentExamDto> PretreatmentExams,
    object? TreatmentSummary,
    object? ClinicHistory,
    List<ClinicalReportMonitoringRecordDto> Monitoring,
    string GeneratedAt);