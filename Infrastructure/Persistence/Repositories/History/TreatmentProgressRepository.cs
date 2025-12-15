using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TreatmentProgressRespository : ITreatmentProgressRepository
    {
        private readonly DBContext _context;
        public TreatmentProgressRespository(DBContext context)
        {
            _context = context;
        }
        public async Task<TreatmentProgress?> GetTreatmentProgressById(Guid id)
        {
            return await _context.TreatmentProgress.FindAsync(id);
        }

        public async Task<IEnumerable<TreatmentProgress>> GetAllTreatmentProgressByPatientId(Guid id)
        {
            return await _context.TreatmentProgress.Include(x => x.PretreatmentExam).Where(x => x.PretreatmentExam.Patient.Id == id).ToListAsync();
        }

        public async Task<TreatmentProgress> CreateTreatmentProgress(TreatmentProgress treatmentProgress)
        {
            _context.TreatmentProgress.Add(treatmentProgress);
            await _context.SaveChangesAsync();
            return treatmentProgress;
        }

        public async Task<TreatmentProgress> UpdateTreatmentProgress(TreatmentProgress treatmentProgress)
        {
            _context.TreatmentProgress.Update(treatmentProgress);
            await _context.SaveChangesAsync();
            return treatmentProgress;
        }

        public async Task DeleteTreatmentProgress(Guid id)
        {
            var treatmentProgress = await _context.TreatmentProgress.FindAsync(id);
            if (treatmentProgress == null) return;
            _context.TreatmentProgress.Remove(treatmentProgress);
            await _context.SaveChangesAsync();
        }
    }
}