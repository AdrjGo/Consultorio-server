using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FormResRespository : IFormResRepository
    {
        private readonly DBContext _context;
        public FormResRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<FormRes?> GetFormResById(Guid id)
        {
            return await _context.FormRes.FindAsync(id);
        }

        public async Task<IEnumerable<FormRes>> GetAllFormResByPatientId(Guid id)
        {
            return await _context.FormRes.Include(fr => fr.Patient).Where(fr => fr.Patient.Id == id).ToListAsync();
        }

        public async Task<FormRes> CreateFormRes(FormRes formRes)
        {
            _context.FormRes.Add(formRes);
            await _context.SaveChangesAsync();
            return formRes;
        }

        public async Task<FormRes> UpdateFormRes(FormRes formRes)
        {
            _context.FormRes.Update(formRes);
            await _context.SaveChangesAsync();
            return formRes;
        }

        public async Task DeleteFormRes(Guid id)
        {
            var formRes = await _context.FormRes.FindAsync(id);
            if (formRes == null) return;
            _context.FormRes.Remove(formRes);
            await _context.SaveChangesAsync();
        }
    }
}