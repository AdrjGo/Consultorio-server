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

        public required TreatmentSummary TreatmentSummary { get; set; }
        public required Contract Contract { get; set; }
        public required List<FormVersion> FormVersions { get; set; } = new();
        public required ClinicHistory ClinicHistory { get; set; }
        public required GeneralHistory GeneralHistory { get; set; }
    }
}