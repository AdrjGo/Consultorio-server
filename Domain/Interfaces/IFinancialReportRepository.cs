using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IFinancialReportRepository
    {
        Task<Patient?> GetPatientWithPersonAsync(Guid patientId);
        Task<Clinic?> GetClinicAsync();
        Task<List<PaymentTreatment>> GetPaymentsByPatientIdAsync(Guid patientId, DateOnly? startDate, DateOnly? endDate);
        Task<Contract?> GetContractByIdAsync(Guid contractId);
        Task<List<Contract>> GetContractsByPatientIdAsync(Guid patientId);
        Task<List<PaymentTreatment>> GetPaymentsByContractIdAsync(Guid contractId);
    }
}