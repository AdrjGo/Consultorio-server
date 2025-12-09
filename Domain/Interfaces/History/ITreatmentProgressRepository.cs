using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITreatmentProgressRepository
    {
        Task<TreatmentProgress> GetTreatmentProgressById(Guid id);
        Task<IEnumerable<TreatmentProgress>> GetAllTreatmentProgressByPatientId(Guid id);
        Task<TreatmentProgress> CreateTreatmentProgress(TreatmentProgress treatmentProgress);
        Task<TreatmentProgress> UpdateTreatmentProgress(TreatmentProgress treatmentProgress);
        Task DeleteTreatmentProgress(Guid id);
    }
}