using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<Appointment> GetAppointmentById(Guid id);
        Task<IEnumerable<Appointment>> GetAllAppointments();
        Task<Appointment> GetAppointmentInCourseByPatientId(Guid patientId);
        Task<IEnumerable<Appointment>> GetAppointmentsByDate(DateTime? initialDate, DateTime? finalDate);
        Task<IEnumerable<Appointment>> GetAppointmentsByPatientId(Guid patientId);
        Task<IEnumerable<Appointment>> GetOverlappingAppointments(Guid professionalId, DateTime startDate, DateTime endDate);
        Task<Appointment> CreateAppointment(Appointment appointment);
        Task<Appointment> UpdateAppointment(Appointment appointment);
        Task<Appointment> ChangeAppointmentLifeStatus(Appointment appointment);
        Task DeleteAppointment(Guid id);
    }
}
