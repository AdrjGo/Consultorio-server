using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClinicHistoryRepository
    {
        Task<ClinicHistory> GetClinicHistoryById(Guid id);
        Task<IEnumerable<ClinicHistory>> GetAllClinicHistoryByPatientId(Guid id);
        Task<ClinicHistory> CreateClinicHistory(ClinicHistory clinicHistory);
        Task<ClinicHistory> UpdateClinicHistory(ClinicHistory clinicHistory);
        Task DeleteClinicHistory(Guid id);
    }
}