using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISubmoduleRepository
    {
        Task<Submodule> GetSubmoduleById(Guid id);
        Task<IEnumerable<Submodule>> GetAllSubmodules();
        Task<Submodule> CreateSubmodule(Submodule submodule);
        Task<Submodule> UpdateSubmodule(Submodule submodule);
        Task DeleteSubmodule(Guid id);
    }
}