using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class PaymentTreatmentRespository : IPaymentTreatmentRepository
    {
        private readonly DBContext _context;
        public PaymentTreatmentRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<PaymentTreatment> GetPaymentTreatmentById(Guid id)
        {
            return await _context.PaymentTreatments.FindAsync(id);
        }

        public async Task<IEnumerable<PaymentTreatment>> GetAllPaymentTreatmentsByPatientId(Guid id)
        {
            return await _context.PaymentTreatments.Include(pt => pt.Patient).ThenInclude(p => p.Person).Include(pt => pt.Contract).Where(pt => pt.PatientId == id).ToListAsync();
        }

        public async Task<PaymentTreatment> CreatePaymentTreatment(PaymentTreatment paymentTreatment)
        {
            _context.PaymentTreatments.Add(paymentTreatment);
            await _context.SaveChangesAsync();
            return paymentTreatment;
        }

        public async Task<PaymentTreatment> UpdatePaymentTreatment(PaymentTreatment paymentTreatment)
        {
            _context.PaymentTreatments.Update(paymentTreatment);
            await _context.SaveChangesAsync();
            return paymentTreatment;
        }

        public async Task DeletePaymentTreatment(Guid id)
        {
            var paymentTreatment = await _context.PaymentTreatments.FindAsync(id);
            if (paymentTreatment == null) return;
            _context.PaymentTreatments.Remove(paymentTreatment);
            await _context.SaveChangesAsync();
        }
    }
}