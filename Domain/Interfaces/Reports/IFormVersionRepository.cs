using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFormVersionRepository
    {
        Task<FormVersion> GetFormVersionById(Guid id);
        Task<FormVersion> GetAllFormVersionsByFormName(string formName);
        Task<IEnumerable<FormVersion>> GetBySubmoduleId(int submoduleId);
        Task<FormVersion> CreateFormVersion(FormVersion formVersion);
        Task<FormVersion> UpdateFormVersion(FormVersion formVersion);
        Task DeleteFormVersion(Guid id);
    }
}