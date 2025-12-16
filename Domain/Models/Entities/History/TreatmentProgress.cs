namespace Domain.Entities
{
    public class TreatmentProgress : BaseEntity
    {
        public required Guid PatientId { get; set; }
        public required int Payment { get; set; }
        public required int Debt { get; set; }

        public Patient? Patient { get; set; }
    }
}