namespace Domain.Entities
{
    public class Contract : BaseEntity
    {
        public required int SubmodID { get; set; }
        public required Guid PatientId { get; set; }
        public required DateTime ContractDate { get; set; }

        public required Submodule Submodule { get; set; }
        public required Patient Patient { get; set; }
        public ICollection<PaymentManager> PaymentManagers { get; set; } = new List<PaymentManager>();
    }
}