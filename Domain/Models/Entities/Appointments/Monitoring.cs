namespace Domain.Entities
{
    public class Monitoring : BaseEntity
    {
        public required Guid AppointmentId { get; set; }
        public required string Nomenclature { get; set; }
        public required string Treatment { get; set; }

        public Appointment Appointment { get; set; }
        public List<EvidenceFile> EvidenceFiles { get; set; }
    }
}