using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> GetPatientById(Guid id);
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> CreatePatient(Patient patient);
        Task<Patient> UpdatePatient(Patient patient);
        Task DeletePatient(Guid id);
    }
}