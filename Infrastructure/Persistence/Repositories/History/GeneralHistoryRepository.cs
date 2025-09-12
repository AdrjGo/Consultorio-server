using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GeneralHistoryRespository : IGeneralHistoryRepository
    {
        private readonly DBContext _context;
        public GeneralHistoryRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<GeneralHistory?> GetGeneralHistoryById(Guid id)
        {
            return await _context.GeneralHistories.FindAsync(id);
        }

        public async Task<IEnumerable<GeneralHistory>> GetAllGeneralHistoryByPatientId(Guid id)
        {
            return await _context.GeneralHistories.Where(x => x.PatientId == id).ToListAsync();
        }

        public async Task<GeneralHistory> CreateGeneralHistory(GeneralHistory generalHistory)
        {
            _context.GeneralHistories.Add(generalHistory);
            await _context.SaveChangesAsync();
            return generalHistory;
        }

        public async Task<GeneralHistory> UpdateGeneralHistory(GeneralHistory generalHistory)
        {
            _context.GeneralHistories.Update(generalHistory);
            await _context.SaveChangesAsync();
            return generalHistory;
        }

        public async Task DeleteGeneralHistory(Guid id)
        {
            var generalHistory = await _context.GeneralHistories.FindAsync(id);
            if (generalHistory == null) return;
            _context.GeneralHistories.Remove(generalHistory);
            await _context.SaveChangesAsync();
        }
    }
}