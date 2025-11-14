using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PatientRespository : IPatientRepository
    {
        private readonly DBContext _context;
        public PatientRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetPatientById(Guid id)
        {
            return await _context.Patients.Include(pa => pa.Person).Include(pa => pa.PatientResponsible).ThenInclude(pr => pr.Person).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Patient?>> GetPatientsByName(string name)
        {
            return await _context.Patients
                .Include(u => u.Person)
                .Include(pa => pa.PatientResponsible)
                .ThenInclude(pr => pr.Person)
                .Where(u => EF.Functions.ILike(u.Person.Name, $"%{name}%")
                            || EF.Functions.ILike(u.Person.LastName, $"%{name}%"))
                .ToListAsync();
        }

        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _context.Patients.Include(pa => pa.Person).Include(pa => pa.PatientResponsible).ThenInclude(pr => pr.Person).ToListAsync();
        }

        public async Task<Patient?> GetPatientByCi(string ci)
        {
            return await _context.Patients.Include(pa => pa.Person).Include(pa => pa.PatientResponsible).ThenInclude(pr => pr.Person).FirstOrDefaultAsync(p => p.Person.Ci == ci);
        }

        public async Task<(IEnumerable<Patient> Patients, int TotalCount)> GetPatientsPagedAsync(int pageNumber, int pageSize, string? search = null, string? state = null)
        {
            var query = _context.Patients
                .Include(p => p.Person)
                .Include(p => p.PatientResponsible)
                    .ThenInclude(r => r.Person)
                .AsQueryable();

            if (!string.IsNullOrEmpty(state))
                query = query.Where(p => p.State.ToString().ToLower() == state.ToLower());

            if (!string.IsNullOrEmpty(search))
                query = query.Where(p =>
                    EF.Functions.ILike(p.Person.Name, $"%{search}%") ||
                    EF.Functions.ILike(p.Person.LastName, $"%{search}%"));

            var totalCount = await query.CountAsync();

            var patients = await query
                .OrderBy(p => p.Person.LastName)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (patients, totalCount);
        }


        public async Task<Patient> CreatePatient(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task<Patient> UpdatePatient(Patient patient)
        {
            _context.Patients.Update(patient);
            await _context.SaveChangesAsync();
            return patient;
        }

        public async Task DeletePatient(Guid id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null) return;
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
        }
    }
}