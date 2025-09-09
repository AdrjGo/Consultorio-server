namespace Domain.Entities
{
    public class TreatmentSummary : BaseEntity
    {
        public required Guid PatientId { get; set; }
        public required int SubmodID { get; set; }
        public required DateTime SummaryDate { get; set; }

        public required Patient Patient { get; set; }
        public required Submodule Submodule { get; set; }
    }
}