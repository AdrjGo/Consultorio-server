using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClinicRepository
    {
        Task<Clinic> GetClinicById(Guid id);
        Task<Clinic> GetClinic();
        Task<Clinic> CreateClinic(Clinic clinic);
        Task<Clinic> UpdateClinic(Clinic id);
        Task<User> GetManagerById(Guid id);
    }
}