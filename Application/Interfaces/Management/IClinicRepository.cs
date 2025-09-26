using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClinicRepository
    {
        Task<Clinic> CraeteClinic(Clinic clinic);
        Task<Clinic> UpdateClinic(Clinic id);
    }
}