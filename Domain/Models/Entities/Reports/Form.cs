namespace Domain.Entities
{
    public class Form : BaseEntity
    {
        public required string Name { get; set; }
        public required string? Description { get; set; }

        public List<FormVersion>? FormVersions { get; set; }
    }
}