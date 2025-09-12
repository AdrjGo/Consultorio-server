using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class TreatmentSumaryRespository : ITreatmentSumaryRepository
    {
        private readonly DBContext _context;
        public TreatmentSumaryRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<TreatmentSummary?> GetTreatmentSumaryById(Guid id)
        {
            return await _context.TreatmentSummaries.FindAsync(id);
        }

        public async Task<IEnumerable<TreatmentSummary>> GetAllTreatmentSumariesByPatientId(Guid id)
        {
            return await _context.TreatmentSummaries.Include(ts => ts.Patient).Where(ts => ts.Patient.Id == id).ToListAsync();
        }

        public async Task<TreatmentSummary> CreateTreatmentSumary(TreatmentSummary treatmentSumary)
        {
            _context.TreatmentSummaries.Add(treatmentSumary);
            await _context.SaveChangesAsync();
            return treatmentSumary;
        }

        public async Task<TreatmentSummary> UpdateTreatmentSumary(TreatmentSummary treatmentSumary)
        {
            _context.TreatmentSummaries.Update(treatmentSumary);
            await _context.SaveChangesAsync();
            return treatmentSumary;
        }

        public async Task DeleteTreatmentSumary(Guid id)
        {
            var treatmentSumary = await _context.TreatmentSummaries.FindAsync(id);
            if (treatmentSumary == null) return;
            _context.TreatmentSummaries.Remove(treatmentSumary);
            await _context.SaveChangesAsync();
        }
    }
}