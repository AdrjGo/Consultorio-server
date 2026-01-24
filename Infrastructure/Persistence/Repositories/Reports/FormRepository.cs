using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class FormRepository : IFormRepository
    {
        private readonly DBContext _context;
        public FormRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<FormVersion> GetFormVersionById(Guid id)
        {
            return await _context.FormVersions.Include(fv => fv.Form).FirstOrDefaultAsync(fv => fv.Id == id);
        }

        public async Task<FormVersion> GetFormByName(string name)
        {
            return await _context.FormVersions.Include(fv => fv.Form).Where(fv => EF.Functions.ILike(fv.Form.Name, $"%{name}%")).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FormVersion>> GetAllFormVersionsByFormName(string formName)
        {
            return await _context.FormVersions.Include(fv => fv.Form).Where(fv => fv.Form.Name == formName).GroupBy(fv => fv.FormId)
        .Select(g => g.OrderByDescending(fv => fv.NumberVersion).First())
        .ToListAsync();
        }

        public async Task<FormVersion> GetFormBySubmodId(int submodId)
        {
            return await _context.FormVersions.Include(fv => fv.Form).Include(f => f.FormResponse).Where(fv => fv.SubmodID == submodId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<FormVersion>> GetAllFormVersionsByVersion()
        {
            var query = from fv in _context.FormVersions
                        join latest in (
                            from v in _context.FormVersions
                            group v by v.FormId into g
                            select new
                            {
                                FormId = g.Key,
                                MaxVersion = g.Max(x => x.NumberVersion)
                            }
                        )
                        on new { fv.FormId, fv.NumberVersion } equals new { latest.FormId, NumberVersion = latest.MaxVersion }
                        select fv;

            return await query
                .Include(fv => fv.Form)
                .ToListAsync();
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