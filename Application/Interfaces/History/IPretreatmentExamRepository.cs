using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPretreatmentExamRepository
    {
        Task<PretreatmentExam> GetPretreatmentExamById(Guid id);
        Task<IEnumerable<PretreatmentExam>> GetAllPretreatmentExamsByPatientId(Guid id);
        Task<PretreatmentExam> CreatePretreatmentExam(PretreatmentExam pretreatmentExam);
        Task<PretreatmentExam> UpdatePretreatmentExam(PretreatmentExam pretreatmentExam);
        Task DeletePretreatmentExam(Guid id);
    }
}