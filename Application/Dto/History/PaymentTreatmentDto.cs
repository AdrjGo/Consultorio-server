using Domain.Enum;

namespace Application.Dto
{
    public class PaymentTreatmentDto
    {
        public required Guid PatientId { get; set; }
        public required int Amount { get; set; }
        public required PaymentMethod Method { get; set; }
        public required string RecivedBy { get; set; }
        public string? Observations { get; set; }
    }
}