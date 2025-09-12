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
            return await _context.Appointments.FindAsync(id);
        }

        public async Task<IEnumerable<Appointment>> GetAllAppointments()
        {
            return await _context.Appointments.ToListAsync();
        }

        public async Task<IEnumerable<Appointment>> GetAppointmentsByDate(DateTime? initialDate, DateTime? finalDate)
        {
            return await _context.Appointments.Where(x => x.StartDate >= initialDate && x.EndDate <= finalDate || x.StartDate >= initialDate && x.EndDate == null || x.StartDate == null && x.EndDate <= finalDate).ToListAsync();
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

        public async Task DeleteAppointment(Guid id)
        {
            var appointment = await _context.Appointments.FindAsync(id);
            if (appointment == null) return;
            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();
        }
    }
}