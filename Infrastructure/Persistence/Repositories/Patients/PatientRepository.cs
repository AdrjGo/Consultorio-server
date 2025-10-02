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

        public async Task<Patient?> GetPatientByName(string name)
        {
            return await _context.Patients.Include(pa => pa.Person).Include(pa => pa.PatientResponsible).ThenInclude(pr => pr.Person).FirstOrDefaultAsync(p => EF.Functions.ILike(p.Person.Name, $"%{name}%"));
        }

        public async Task<Patient?> GetPatientByCi(string ci)
        {
            return await _context.Patients.Include(pa => pa.Person).Include(pa => pa.PatientResponsible).ThenInclude(pr => pr.Person).FirstOrDefaultAsync(p => p.Person.Ci == ci);
        }


        public async Task<IEnumerable<Patient>> GetAllPatients()
        {
            return await _context.Patients.Include(pa => pa.Person).Include(pa => pa.PatientResponsible).ThenInclude(pr => pr.Person).ToListAsync();
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