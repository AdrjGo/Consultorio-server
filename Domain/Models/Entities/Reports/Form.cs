namespace Domain.Entities
{
    public class Form : BaseEntity
    {
        public required string Name { get; set; }
        public required string? Description { get; set; }

        public required List<FormVersion> FormVersions { get; set; }
    }
}