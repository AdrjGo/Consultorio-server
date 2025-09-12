using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IMonitoringRepository
    {
        Task<Monitoring> GetMonitoringByAppointmentId(Guid id);
        Task<IEnumerable<Monitoring>> GetAllMonitoringsByPatientId(Guid id);
        Task<Monitoring> CreateMonitoring(Monitoring monitoring);
        Task<Monitoring> UpdateMonitoring(Monitoring monitoring);
        Task DeleteMonitoring(Guid id);
    }
}