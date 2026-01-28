using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class ContractRespository : IContractRepository
    {
        private readonly DBContext _context;
        public ContractRespository(DBContext context)
        {
            _context = context;
        }

        public async Task<Contract?> GetContractById(Guid id)
        {
            return await _context.Contracts.FindAsync(id);
        }

        public async Task<Contract?> GetContractByPatientId(Guid patientId)
        {
            return await _context.Contracts.Include(c => c.Patient).Include(c => c.PaymentManagers).ThenInclude(pm => pm.Person).Where(c => c.PatientId == patientId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Contract>> GetContractsByPatientId(Guid patientId)
        {
            return await _context.Contracts.Include(c => c.Patient).Where(c => c.PatientId == patientId).ToListAsync();
        }

        public async Task<Contract> CreateContract(Contract contract)
        {
            _context.Contracts.Add(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task<Contract> UpdateContract(Contract contract)
        {
            _context.Contracts.Update(contract);
            await _context.SaveChangesAsync();
            return contract;
        }

        public async Task DeleteContract(Guid id)
        {
            var contract = await _context.Contracts.FindAsync(id);
            if (contract == null) return;
            _context.Contracts.Remove(contract);
            await _context.SaveChangesAsync();
        }
    }
}