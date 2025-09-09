namespace Domain.Entities
{
    public class PaymentManager : BaseEntity
    {
        public required Guid PersonId { get; set; }
        public required Guid ContractId { get; set; }
        public string? Parentage { get; set; }

        public required Person Person { get; set; }
        public required Contract Contract { get; set; }
    }
}