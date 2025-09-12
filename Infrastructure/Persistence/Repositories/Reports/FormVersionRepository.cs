
using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FormVersionRespository : IFormVersionRepository
    {
        private readonly DBContext _context;
        public FormVersionRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<FormVersion?> GetFormVersionById(Guid id)
        {
            return await _context.FormVersions.FindAsync(id);
        }

        public async Task<FormVersion?> GetAllFormVersionsByFormName(string formName)
        {
            return await _context.FormVersions.Include(fv => fv.Form).Where(fv => fv.Form.Name == formName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FormVersion>> GetBySubmoduleId(int submoduleId)
        {
            return await _context.FormVersions.Include(fv => fv.SubmodID).Where(fv => fv.SubmodID == submoduleId).ToListAsync();
        }

        public async Task<FormVersion> CreateFormVersion(FormVersion formVersion)
        {
            _context.FormVersions.Add(formVersion);
            await _context.SaveChangesAsync();
            return formVersion;
        }

        public async Task<FormVersion> UpdateFormVersion(FormVersion formVersion)
        {
            _context.FormVersions.Update(formVersion);
            await _context.SaveChangesAsync();
            return formVersion;
        }

        public async Task DeleteFormVersion(Guid id)
        {
            var formVersion = await _context.FormVersions.FindAsync(id);
            if (formVersion == null) return;
            _context.FormVersions.Remove(formVersion);
            await _context.SaveChangesAsync();
        }
    }
}