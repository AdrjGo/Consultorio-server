using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPatientRepository
    {
        Task<Patient> GetPatientById(Guid id);
        Task<IEnumerable<Patient>> GetPatientsByName(string name);
        Task<Patient> GetPatientByCi(string ci);
        Task<IEnumerable<Patient>> GetAllPatients();
        Task<(IEnumerable<Patient> Patients, int TotalCount)> GetPatientsPagedAsync(int pageNumber, int pageSize, string? search = null, string? state = null);
        Task<Patient> CreatePatient(Patient patient);
        Task<Patient> UpdatePatient(Patient patient);
        Task DeletePatient(Guid id);
    }
}