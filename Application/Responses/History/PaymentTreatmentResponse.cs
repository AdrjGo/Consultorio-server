using Domain.Enum;

namespace Application.Responses
{
    public class PaymentTreatmentResponse
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public int payment { get; set; }
        public int Debt { get; set; }
        public PaymentMethod Method { get; set; }
        public string RecivedBy { get; set; }
        public string? Observations { get; set; }
        public string? CreatedAt { get; set; }
    }

    public class PaymentTreatmentCreatedResponse
    {
        public Guid Id { get; set; }
        public required string Message { get; set; }
    }
}