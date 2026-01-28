using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentManagerRepository : IPaymentManagerRepository
    {
        private readonly DBContext _context;
        public PaymentManagerRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<PaymentManager> GetPaymentManagerByContractId(Guid contractId)
        {
            return await _context.PaymentManagers.Include(pm => pm.Contract).Where(pm => pm.ContractId == contractId).FirstOrDefaultAsync();
        }

        public async Task<PaymentManager> CreatePaymentManager(PaymentManager paymentManagerDto)
        {
            _context.PaymentManagers.Add(paymentManagerDto);
            await _context.SaveChangesAsync();
            return paymentManagerDto;
        }
    }
}