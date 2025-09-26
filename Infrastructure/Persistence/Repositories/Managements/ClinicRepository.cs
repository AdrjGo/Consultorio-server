using Domain.Entities;
using Domain.Enum;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClinicRespository : IClinicRepository
    {
        private readonly DBContext _context;
        public ClinicRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<Clinic> GetClinicById(Guid id)
        {
            return await _context.Clinics.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<Clinic> GetClinic()
        {
            return await _context.Clinics.FirstAsync();
        }

        public async Task<Clinic> CreateClinic(Clinic Clinic)
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

        public async Task<User> GetManagerById(Guid id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}