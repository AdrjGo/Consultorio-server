namespace Domain.Entities
{
    public class Contract : BaseEntity
    {
        public required int SubmodID { get; set; }
        public required Guid PatientId { get; set; }
        public required DateTime ContractDate { get; set; }

        public Submodule? Submodule { get; set; }
        public Patient? Patient { get; set; }
        public ICollection<PaymentManager> PaymentManagers { get; set; } = new List<PaymentManager>();

        public List<PaymentTreatment>? PaymentTreatments { get; set; }
    }
}