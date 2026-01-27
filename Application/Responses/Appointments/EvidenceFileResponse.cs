namespace Application.Responses
{
    public class EvidenceFileResponse
    {
        public Guid Id { get; set; }
        public Guid MonitoringId { get; set; }
        public string Format { get; set; }
        public string? ExternalReference { get; set; }
        public string? Reference { get; set; }
        public string? Description { get; set; }
        public string? MonitoringNomenclature { get; set; }
        public string? CreatedAt { get; set; }
    }

    public class EvidenceFileCreatedResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }

    public class EvidenceFileUpdatedResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}