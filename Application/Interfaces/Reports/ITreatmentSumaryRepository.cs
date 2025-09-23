using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ITreatmentSumaryRepository
    {
        Task<TreatmentSummary> GetTreatmentSumaryById(Guid id);
        Task<IEnumerable<TreatmentSummary>> GetAllTreatmentSumariesByPatientId(Guid id);
        Task<TreatmentSummary> CreateTreatmentSumary(TreatmentSummary treatmentSumary);
        Task<TreatmentSummary> UpdateTreatmentSumary(TreatmentSummary treatmentSumary);
        Task DeleteTreatmentSumary(Guid id);
    }
}