using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IClinicRepository
    {
        Task<Clinic> UpdateClinic(Clinic id);
    }
}