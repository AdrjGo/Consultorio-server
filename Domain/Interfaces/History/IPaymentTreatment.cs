using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPaymentTreatmentRepository
    {
        Task<PaymentTreatment> GetPaymentTreatmentById(Guid id);
        Task<IEnumerable<PaymentTreatment>> GetAllPaymentTreatmentsByPatientId(Guid id);
        Task<PaymentTreatment> CreatePaymentTreatment(PaymentTreatment paymentTreatment);
        Task<PaymentTreatment> UpdatePaymentTreatment(PaymentTreatment paymentTreatment);
        Task DeletePaymentTreatment(Guid id);
    }
}