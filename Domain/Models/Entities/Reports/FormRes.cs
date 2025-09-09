using System.Net.Http.Json;

namespace Domain.Entities
{
    public class FormRes : BaseEntity
    {
        public required Guid FormVersionId { get; set; }
        public required Guid PatientId { get; set; }
        public required string JsonResponse { get; set; }

        public required FormVersion FormVersion { get; set; }
        public required Patient Patient { get; set; }

    }
}