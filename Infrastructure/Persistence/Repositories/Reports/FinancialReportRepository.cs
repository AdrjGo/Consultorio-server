using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class FinancialReportRepository : IFinancialReportRepository
    {
        private readonly DBContext _context;

        public FinancialReportRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Patient?> GetPatientWithPersonAsync(Guid patientId)
        {
            return await _context.Patients
                .Include(p => p.Person)
                .FirstOrDefaultAsync(p => p.Id == patientId);
        }

        public async Task<Clinic?> GetClinicAsync()
        {
            return await _context.Clinics
                .FirstOrDefaultAsync();
        }

        public async Task<List<PaymentTreatment>> GetPaymentsByPatientIdAsync(Guid patientId, DateOnly? startDate, DateOnly? endDate)
        {
            var query = _context.PaymentTreatments
                .Include(pt => pt.Contract)
                .ThenInclude(c => c.Patient)
                .ThenInclude(p => p.Person)
                .Where(pt => pt.Contract.PatientId == patientId);

            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(pt => DateOnly.FromDateTime(pt.CreatedAt) >= startDate.Value && DateOnly.FromDateTime(pt.CreatedAt) <= endDate.Value);
            }
            else if (startDate.HasValue)
            {
                query = query.Where(pt => DateOnly.FromDateTime(pt.CreatedAt) >= startDate.Value);
            }
            else if (endDate.HasValue)
            {
                query = query.Where(pt => DateOnly.FromDateTime(pt.CreatedAt) <= endDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Contract?> GetContractByIdAsync(Guid contractId)
        {
            return await _context.Contracts
                .Include(c => c.Patient)
                .ThenInclude(p => p.Person)
                .FirstOrDefaultAsync(c => c.Id == contractId);
        }

        public async Task<List<Contract>> GetContractsByPatientIdAsync(Guid patientId)
        {
            return await _context.Contracts
                .Include(c => c.Patient)
                .ThenInclude(p => p.Person)
                .Where(c => c.PatientId == patientId)
                .ToListAsync();
        }

        public async Task<List<PaymentTreatment>> GetPaymentsByContractIdAsync(Guid contractId)
        {
            return await _context.PaymentTreatments
                .Where(pt => pt.ContractId == contractId)
                .ToListAsync();
        }
    }
}