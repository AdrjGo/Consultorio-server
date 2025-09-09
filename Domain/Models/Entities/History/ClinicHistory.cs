namespace Domain.Entities
{
    public class ClinicHistory : BaseEntity
    {
        public required Guid PatientId { get; set; }
        public required int SubmodId { get; set; }

        public required Patient Patient { get; set; }
        public required Submodule Submodule { get; set; }
    }
}