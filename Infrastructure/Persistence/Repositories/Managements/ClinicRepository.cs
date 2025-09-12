using Domain.Entities;
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

        public async Task<Clinic> UpdateClinic(Clinic id)
        {
            _context.Clinics.Update(id);
            await _context.SaveChangesAsync();
            return id;
        }
    }
}