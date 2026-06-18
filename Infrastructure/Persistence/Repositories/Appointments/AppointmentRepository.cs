using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly DBContext _context;
        public AppointmentRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Appointment?> GetAppointmentById(Guid id)
        {
            return await _context.Appointments.Include(a => a.Patient).ThenInclude(a => a.Person).Include(a => a.Professional).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            return await _context.Appointments.Include(a => a.Patient).ThenInclude(a => a.Person).Include(a => a.Professional).ToListAsync();
        }

        public async Task<Appointment> GetAppointmentInCourseByPatientId(Guid patientId)
        {
            return await _context.Appointments.Include(a => a.Patient).ThenInclude(a => a.Person).Include(a => a.Professional).FirstOrDefaultAsync(a => a.PatientId == patientId && a.LifeStatus == Domain.Enum.AppointmentLifeStatus.EnCurso);
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDate(DateTime? initialDate, DateTime? finalDate)
        {
            var query = _context.Appointments
                .Include(a => a.Patient).ThenInclude(a => a.Person)
                .Include(a => a.Professional)
                .AsQueryable();

            if (initialDate.HasValue)
                query = query.Where(a => a.StartDate >= initialDate.Value);

            if (finalDate.HasValue)
                query = query.Where(a => a.EndDate <= finalDate.Value);

            return await query.ToListAsync();
        }


        public async Task<IEnumerable<Appointment>> GetAppointmentsByPatientId(Guid patientId)
        {
            return await _context.Appointments.Include(a => a.Patient).ThenInclude(a => a.Person).Include(a => a.Professional).Where(x => x.PatientId == patientId).ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetOverlappingAppointments(Guid professionalId, DateTime startDate, DateTime endDate)
        {
            return await _context.Appointments
                .Where(a => a.ProfessionalId == professionalId
                    && a.StartDate < endDate
                    && a.EndDate > startDate
                    && a.Status != Domain.Enum.AppointmentStatus.Cancelado)
                .ToListAsync();
        }

        public async Task<Appointment> CreateAppointment(Appointment appointment)
        {
            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> UpdateAppointment(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task<Appointment> ChangeAppointmentLifeStatus(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            await _context.SaveChangesAsync();
            return appointment;
        }

        public async Task DeleteAppointment(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return;
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}
