using Domain.Enum;

namespace Domain.Entities
{
    public class Submodule
    {
        public required int Id { get; set; }
        public required string Name { get; set; }

        public required States State { get; set; } = States.ACTIVE;
        public required string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public TreatmentSummary? TreatmentSummary { get; set; }
        public Contract? Contract { get; set; }
        public List<FormVersion>? FormVersions { get; set; } = new();
        public ClinicHistory? ClinicHistory { get; set; }
        public GeneralHistory? GeneralHistory { get; set; }
    }
}