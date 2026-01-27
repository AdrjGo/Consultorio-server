using Domain.Enum;

namespace Domain.Entities
{
    public class PaymentTreatment : BaseEntity
    {
        public required Guid PatientId { get; set; }
        public Guid ContractId { get; set; }
        public required int Amount { get; set; }
        public required PaymentMethod Method { get; set; }
        public required string RecivedBy { get; set; }
        public required string? Observations { get; set; }

        public Patient? Patient { get; set; }
        public Contract? Contract { get; set; }
    }
}