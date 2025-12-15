namespace Domain.Entities
{
    public class PretreatmentExam : BaseEntity
    {
        // public required Guid HistoryId { get; set; }
        public required Guid PatientId { get; set; }
        public required string Observations { get; set; }
        public required string Interconsultation { get; set; }
        public required string Piece { get; set; }
        public bool Caries { get; set; }
        public string? Treatment { get; set; }
        public int Cost { get; set; }

        // public GeneralHistory? GeneralHistory { get; set; }
        public Patient? Patient { get; set; }
        public List<TreatmentProgress>? TreatmentProgress { get; set; }
    }
}