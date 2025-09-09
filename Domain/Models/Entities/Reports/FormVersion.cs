using System.Net.Http.Json;

namespace Domain.Entities
{
    public class FormVersion : BaseEntity
    {
        public required int SubmodID { get; set; }
        public required Guid FormId { get; set; }
        public required int NumberVersion { get; set; }
        public required string JsonSchema { get; set; }

        public required Form Form { get; set; }
        public required Submodule Submodule { get; set; }
        public required List<FormRes> FormResponse { get; set; } = new();
    }
}