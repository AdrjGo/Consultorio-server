using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.Data;

namespace Infrastructure.Repositories
{
    public class ClinicRespository : IClinicRepository
    {
        private readonly DBContext _context;
        public ClinicRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<Clinic> CraeteClinic(Clinic Clinic)
        {
            _context.Clinics.Add(Clinic);
            await _context.SaveChangesAsync();
            return Clinic;
        }

        public async Task<Clinic> UpdateClinic(Clinic id)
        {
            _context.Clinics.Update(id);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}