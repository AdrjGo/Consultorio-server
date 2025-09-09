using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class EvidenceFile : BaseEntity
    {
        public required Guid MonitoringId { get; set; }
        public required EvidenceFileFormat Format { get; set; }
        public Url? ExternalReference { get; set; }
        public FilePath? Reference { get; set; }
        public string? Description { get; set; }

        public required Monitoring Monitoring { get; set; }
    }
}