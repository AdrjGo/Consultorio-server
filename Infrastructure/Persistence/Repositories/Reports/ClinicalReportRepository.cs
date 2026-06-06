using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ClinicalReportRepository : IClinicalReportRepository
    {
        private readonly DBContext _context;

        public ClinicalReportRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetPatientWithPersonAsync(Guid patientId)
        {
            return await _context.Patients
                .Include(p => p.Person)
                .FirstOrDefaultAsync(p => p.Id == patientId);
        }

        public async Task<FormRes?> GetLatestFormResponseAsync(Guid patientId, int submoduleId)
        {
            return await _context.FormRes
                .Include(fr => fr.FormVersion)
                .Where(fr => fr.PatientId == patientId && fr.FormVersion.SubmodID == submoduleId)
                .OrderByDescending(fr => fr.CreatedAt)
                .FirstOrDefaultAsync();
        }

        public async Task<List<PretreatmentExam>> GetAllPretreatmentExamsByPatientIdAsync(Guid patientId)
        {
            return await _context.PretreatmentExams
                .Where(pe => pe.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<List<Monitoring>> GetAllMonitoringsByPatientIdAsync(Guid patientId)
        {
            return await _context.Monitorings
                .Include(m => m.Appointment)
                .Include(m => m.EvidenceFiles)
                .Where(m => m.Appointment != null && m.Appointment.PatientId == patientId)
                .ToListAsync();
        }
    }
}