namespace Domain.Entities
{
    public class TreatmentProgress : BaseEntity
    {
        public required Guid ExamId { get; set; }
        public required int Payment { get; set; }
        public required int Debt { get; set; }

        public required PretreatmentExam PretreatmentExam { get; set; }
    }
}