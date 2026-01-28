using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IPaymentManagerRepository
    {
        Task<PaymentManager> GetPaymentManagerByContractId(Guid contractId);
        Task<PaymentManager> CreatePaymentManager(PaymentManager paymentManagerDto);
    }
}