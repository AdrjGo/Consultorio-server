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
        public required string Reason { get; set; }
        public required string Observations { get; set; }

        public required User Professional { get; set; }
        public required Patient Patient { get; set; }
        public required Monitoring AppointmentMonitorings { get; set; }

    }
}