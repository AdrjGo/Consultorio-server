namespace Domain.Entities
{
    public class PretreatmentExam : BaseEntity
    {
        public required Guid HistoryId { get; set; }
        public required string Observations { get; set; }
        public required string Interconsultation { get; set; }
        public required string Piece { get; set; }
        public string? Caries { get; set; }
        public string? Treatment { get; set; }
        public int Cost { get; set; }

        public required GeneralHistory GeneralHistory { get; set; }
        public required List<TreatmentProgress> TreatmentProgress { get; set; }
    }
}