using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetAppointmentById(Guid id);
        Task<IEnumerable<Appointment>> GetAllAppointments();
        Task<IEnumerable<Appointment>> GetAppointmentsByDate(DateTime? initialDate, DateTime? finalDate);
        Task<Appointment> CreateAppointment(Appointment appointment);
        Task<Appointment> UpdateAppointment(Appointment appointment);
        Task DeleteAppointment(Guid id);
    }
}