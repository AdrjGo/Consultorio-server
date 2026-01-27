using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Patient : BaseEntity
    {
        public required Guid PersonId { get; set; }
        public Guid? ResponsibleId { get; set; }
        public required string Address { get; set; }
        public required string Zone { get; set; }
        public required string City { get; set; }
        public PhoneNumber? HomePhone { get; set; }
        public required string Occupation { get; set; }
        public required string PlaceOccupation { get; set; }
        public string? Nit { get; set; }
        public string? Sender { get; set; }

        public required Person Person { get; set; }
        public PatientResponsible? PatientResponsible { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public TreatmentSummary? TreatmentSummaryDetail { get; set; }
        public List<Contract>? Contract { get; set; }
        public List<FormRes>? FormResponse { get; set; } = new();
        public ICollection<ClinicHistory>? ClinicHistories { get; set; } = new List<ClinicHistory>();
        public ICollection<GeneralHistory>? GeneralHistories { get; set; } = new List<GeneralHistory>();
        public List<PretreatmentExam>? PretreatmentExams { get; set; }
        public List<TreatmentProgress>? TreatmentProgresses { get; set; }

        public List<PaymentTreatment>? PaymentTreatments { get; set; }

    }
}