using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IContractRepository
    {
        Task<Contract?> GetContractById(Guid id);
        Task<Contract?> GetContractByPatientId(Guid patientId);
        Task<IEnumerable<Contract>> GetContractsByPatientId(Guid patientId);
        Task<Contract> CreateContract(Contract contract);
        Task<Contract> UpdateContract(Contract contract);
        Task DeleteContract(Guid id);
    }
}
