using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClinicHistoryRespository : IClinicHistoryRepository
    {
        private readonly DBContext _context;
        public ClinicHistoryRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<ClinicHistory?> GetClinicHistoryById(Guid id)
        {
            return await _context.ClinicHistories.FindAsync(id);
        }

        public async Task<IEnumerable<ClinicHistory>> GetAllClinicHistoryByPatientId(Guid id)
        {
            return await _context.ClinicHistories.Where(x => x.PatientId == id).ToListAsync();
        }

        public async Task<ClinicHistory> CreateClinicHistory(ClinicHistory clinicHistory)
        {
            _context.ClinicHistories.Add(clinicHistory);
            await _context.SaveChangesAsync();
            return clinicHistory;
        }

        public async Task<ClinicHistory> UpdateClinicHistory(ClinicHistory clinicHistory)
        {
            _context.ClinicHistories.Update(clinicHistory);
            await _context.SaveChangesAsync();
            return clinicHistory;
        }

        public async Task DeleteClinicHistory(Guid id)
        {
            var clinicHistory = await _context.ClinicHistories.FindAsync(id);
            if (clinicHistory == null) return;
            _context.ClinicHistories.Remove(clinicHistory);
            await _context.SaveChangesAsync();
        }

    }
}