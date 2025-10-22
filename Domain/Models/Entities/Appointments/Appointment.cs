using Domain.Enum;

namespace Domain.Entities
{
    public class Appointment : BaseEntity
    {
        public required Guid PatientId { get; set; }
        public required Guid ProfessionalId { get; set; }
        public required DateTime StartDate { get; set; }
        public required DateTime EndDate { get; set; }
        public required AppointmentType Type { get; set; }
        public required AppointmentStatus Status { get; set; }
        public required string Reason { get; set; }
        public string? Observations { get; set; }
        public User? Professional { get; set; }
        public Patient? Patient { get; set; }
        public Monitoring? AppointmentMonitorings { get; set; }

    }
}