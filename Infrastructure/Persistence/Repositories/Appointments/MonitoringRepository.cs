using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MonitoringRespository : IMonitoringRepository
    {
        private readonly DBContext _context;
        public MonitoringRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<Monitoring?> GetMonitoringByAppointmentId(Guid id)
        {
            return await _context.Monitorings.Include(m => m.Appointment).Where(m => m.AppointmentId == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Monitoring?>> GetAllMonitoringsByPatientId(Guid id)
        {
            return await _context.Monitorings.Include(m => m.Appointment).ThenInclude(a => a.Patient).Include(m => m.EvidenceFiles).Where(m => m.Appointment.PatientId == id).ToListAsync();
        }

        public async Task<Monitoring> CreateMonitoring(Monitoring monitoring)
        {
            _context.Monitorings.Add(monitoring);
            await _context.SaveChangesAsync();
            return monitoring;
        }

        public async Task<Monitoring> UpdateMonitoring(Monitoring monitoring)
        {
            _context.Monitorings.Update(monitoring);
            await _context.SaveChangesAsync();
            return monitoring;
        }

        public async Task DeleteMonitoring(Guid id)
        {
            var monitoring = await _context.Monitorings.FindAsync(id);
            if (monitoring == null) return;
            _context.Monitorings.Remove(monitoring);
            await _context.SaveChangesAsync();
        }
    }
}