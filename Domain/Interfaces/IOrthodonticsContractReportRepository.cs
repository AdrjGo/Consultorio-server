using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IOrthodonticsContractReportRepository
    {
        Task<Contract?> GetContractWithDetailsAsync(Guid contractId);
        Task<Clinic?> GetClinicWithManagerAsync();
        Task<FormRes?> GetLatestFormResponseAsync(Guid patientId, int submoduleId);
    }
}
