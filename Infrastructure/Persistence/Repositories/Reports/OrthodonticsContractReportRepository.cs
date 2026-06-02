using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class OrthodonticsContractReportRepository : IOrthodonticsContractReportRepository
    {
        private readonly DBContext _context;

        public OrthodonticsContractReportRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Contract?> GetContractWithDetailsAsync(Guid contractId)
        {
            return await _context.Contracts
                .Include(c => c.Patient)
                    .ThenInclude(p => p.Person)
                .Include(c => c.PaymentManagers)
                    .ThenInclude(pm => pm.Person)
                .FirstOrDefaultAsync(c => c.Id == contractId);
        }

        public async Task<Clinic?> GetClinicWithManagerAsync()
        {
            return await _context.Clinics
                .Include(c => c.Manager)
                    .ThenInclude(m => m.Person)
                .FirstOrDefaultAsync();
        }

        public async Task<FormRes?> GetLatestFormResponseAsync(Guid patientId, int submoduleId)
        {
            return await _context.FormRes
                .Include(fr => fr.FormVersion)
                .Where(fr => fr.PatientId == patientId && fr.FormVersion.SubmodID == submoduleId)
                .OrderByDescending(fr => fr.CreatedAt)
                .FirstOrDefaultAsync();
        }
    }
}
