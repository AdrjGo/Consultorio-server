using Domain.Entities;

namespace Domain.Interfaces
{
    public interface ISubmoduleRepository
    {
        Task<Submodule> GetSubmoduleById(int id);
        Task<IEnumerable<Submodule>> GetAllSubmodules();
        Task<Submodule> CreateSubmodule(Submodule submodule);
        Task<Submodule> UpdateSubmodule(Submodule submodule);
        Task DeleteSubmodule(int id);
    }
}