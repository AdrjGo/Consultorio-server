using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IGeneralHistoryRepository
    {
        Task<GeneralHistory> GetGeneralHistoryById(Guid id);
        Task<IEnumerable<GeneralHistory>> GetAllGeneralHistoryByPatientId(Guid id);
        Task<GeneralHistory> CreateGeneralHistory(GeneralHistory generalHistory);
        Task<GeneralHistory> UpdateGeneralHistory(GeneralHistory generalHistory);
        Task DeleteGeneralHistory(Guid id);
    }
}