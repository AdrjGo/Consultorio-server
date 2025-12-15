using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PretreatmentExamRespository : IPretreatmentExamRepository
    {
        private readonly DBContext _context;
        public PretreatmentExamRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<PretreatmentExam> GetPretreatmentExamById(Guid id)
        {
            return await _context.PretreatmentExams.FindAsync(id);
        }

        public async Task<IEnumerable<PretreatmentExam>> GetAllPretreatmentExamsByPatientId(Guid id)
        {
            return await _context.PretreatmentExams.Include(x => x.Patient).Where(x => x.Patient.Id == id).ToListAsync();
        }

        public async Task<PretreatmentExam> CreatePretreatmentExam(PretreatmentExam pretreatmentExam)
        {
            _context.PretreatmentExams.Add(pretreatmentExam);
            await _context.SaveChangesAsync();
            return pretreatmentExam;
        }

        public async Task<PretreatmentExam> UpdatePretreatmentExam(PretreatmentExam pretreatmentExam)
        {
            _context.PretreatmentExams.Update(pretreatmentExam);
            await _context.SaveChangesAsync();
            return pretreatmentExam;
        }

        public async Task DeletePretreatmentExam(Guid id)
        {
            var pretreatmentExam = await _context.PretreatmentExams.FindAsync(id);
            if (pretreatmentExam == null) return;
            _context.PretreatmentExams.Remove(pretreatmentExam);
            await _context.SaveChangesAsync();
        }
    }
}