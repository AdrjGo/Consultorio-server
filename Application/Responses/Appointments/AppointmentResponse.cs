using Domain.Enum;

namespace Application.Responses
{
    public class AppointmentResponse
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public Guid ProfessionalId { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public AppointmentType Type { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Reason { get; set; }
        public string Observations { get; set; }

        public UserResponse Professional { get; set; }
        public PatientResponse Patient { get; set; }
    }

    public class AppointmentCreatedResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }


    public class AppointmentUpdatedResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}