using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IFormRepository
    {
        Task<FormVersion> GetFormVersionById(Guid id);
        Task<FormVersion> GetFormByName(string name);
        Task<FormVersion> GetFormBySubmodId(int submoduleId);
        Task<IEnumerable<FormVersion>> GetAllFormVersionsByFormName(string formName);
        Task<IEnumerable<FormVersion>> GetAllFormVersionsByVersion();
        Task<FormVersion> CreateFormVersion(FormVersion formVersion);
        Task<FormVersion> UpdateFormVersion(FormVersion formVersion);
        Task DeleteFormVersion(Guid id);
    }
}