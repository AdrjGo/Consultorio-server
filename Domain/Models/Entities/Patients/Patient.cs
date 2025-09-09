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
        public string? Sender { get; set; }

        public required Person Person { get; set; }
        public PatientResponsible? PatientResponsible { get; set; }
        public List<Appointment>? Appointments { get; set; }
        public required TreatmentSummary TreatmentSummaryDetail { get; set; }
        public required Contract Contract { get; set; }
        public required List<FormRes> FormResponse { get; set; } = new();
        public required ClinicHistory ClinicHistory { get; set; }
        public required GeneralHistory GeneralHistory { get; set; }
    }
}