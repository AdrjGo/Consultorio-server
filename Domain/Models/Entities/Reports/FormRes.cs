namespace Domain.Entities
{
    public class FormRes : BaseEntity
    {
        public required Guid FormVersionId { get; set; }
        public required Guid PatientId { get; set; }
        public required object JsonResponse { get; set; }

        public FormVersion? FormVersion { get; set; }
        public Patient? Patient { get; set; }

    }
}