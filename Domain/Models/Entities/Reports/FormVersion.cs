using System.Net.Http.Json;

namespace Domain.Entities
{
    public class FormVersion : BaseEntity
    {
        public required int SubmodID { get; set; }
        public required Guid FormId { get; set; }
        public required int NumberVersion { get; set; }
        public required object JsonSchema { get; set; }

        public Form? Form { get; set; }
        public Submodule? Submodule { get; set; }
        public List<FormRes>? FormResponse { get; set; } = new();
    }
}