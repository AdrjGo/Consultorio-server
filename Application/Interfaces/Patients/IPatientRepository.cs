using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> GetPatientById(Guid id);
        Task<Patient> GetPatientByName(string name);
        Task<Patient> GetPatientByCi(string ci);
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<Patient> CreatePatient(Patient patient);
        Task<Patient> UpdatePatient(Patient patient);
        Task DeletePatient(Guid id);
    }
}