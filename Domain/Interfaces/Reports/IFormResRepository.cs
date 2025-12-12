using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFormResRepository
    {
        Task<FormRes> GetFormResById(Guid id);
        // Task<IEnumerable<FormRes>> GetAllFormResByPatientId(Guid id);
        Task<FormRes> CreateFormRes(FormRes formRes);
        Task<FormRes> UpdateFormRes(FormRes formRes);
        Task DeleteFormRes(Guid id);
    }
}