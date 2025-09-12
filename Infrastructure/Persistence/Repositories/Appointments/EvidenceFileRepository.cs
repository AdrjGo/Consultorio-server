using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class EvidenceFileRepository : IEvidenceFileRepository
    {
        private readonly DBContext _context;
        public EvidenceFileRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<EvidenceFile?> GetEvidenceFileById(Guid id)
        {
            return await _context.EvidenceFiles.FindAsync(id);
        }

        public async Task<IEnumerable<EvidenceFile>> GetEvidenceFilesByAppointmentId(Guid id)
        {
            return await _context.EvidenceFiles.Include(ef => ef.Monitoring).Where(ef => ef.Monitoring.AppointmentId == id).ToListAsync();
        }

        public async Task<IEnumerable<EvidenceFile?>> GetEvidenceFileByPatient(Guid id)
        {
            return await _context.EvidenceFiles.Include(ef => ef.Monitoring).Where(ef => ef.Monitoring.Appointment.PatientId == id).ToListAsync();
        }

        public async Task<EvidenceFile> CreateEvidenceFile(EvidenceFile evidenceFile)
        {
            _context.EvidenceFiles.Add(evidenceFile);
            await _context.SaveChangesAsync();
            return evidenceFile;
        }

        public async Task<EvidenceFile> UpdateEvidenceFile(EvidenceFile evidenceFile)
        {
            _context.EvidenceFiles.Update(evidenceFile);
            await _context.SaveChangesAsync();
            return evidenceFile;
        }

        public async Task DeleteEvidenceFile(Guid id)
        {
            var evidenceFile = await _context.EvidenceFiles.FindAsync(id);
            if (evidenceFile == null) return;
            _context.EvidenceFiles.Remove(evidenceFile);
            await _context.SaveChangesAsync();
        }
    }
}