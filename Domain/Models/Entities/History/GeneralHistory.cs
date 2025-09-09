namespace Domain.Entities
{
    public class GeneralHistory : BaseEntity
    {
        public required Guid PatientId { get; set; }
        public required int SubmodId { get; set; }
        public required string FilledBy { get; set; }

        public required Patient Patient { get; set; }
        public required Submodule Submodule { get; set; }
        public required PretreatmentExam PretreatmentExam { get; set; }
    }
}