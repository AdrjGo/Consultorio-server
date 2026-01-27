namespace Application.Dto
{
    public class EvidenceFileDto
    {
        public Guid MonitoringId { get; set; }
        public string Format { get; set; }
        public string? ExternalReference { get; set; }
        // public string? Reference { get; set; }
        public string? Description { get; set; }
    }

    public class EvidenceFileUpdateDto
    {
        public Guid? MonitoringId { get; set; }
        public string? Format { get; set; }
        public string? ExternalReference { get; set; }
        public string? Reference { get; set; }
        public string? Description { get; set; }
    }
}