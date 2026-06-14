using Application.Dto.Reports;
using Domain.Interfaces;
using Domain.Entities;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Application.Services
{
    public class FinancialReportService
    {
        private readonly IFinancialReportRepository _repository;

        public FinancialReportService(IFinancialReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<PaymentReportDataDto> GetPaymentReportDataAsync(Guid patientId, DateOnly? startDate, DateOnly? endDate)
        {
            var patient = await _repository.GetPatientWithPersonAsync(patientId);
            if (patient == null)
                throw new Exception("Patient not found");

            var payments = await _repository.GetPaymentsByPatientIdAsync(patientId, startDate, endDate);
            var clinic = await _repository.GetClinicAsync();

            // Create DTOs based on the actual data
            var patientDto = new PaymentInfoDto(
                patient.Person.Name,
                patient.Person.LastName,
                patient.Person.Ci,
                patient.Person.Phone.ToString()
            );

            var clinicDto = new ClinicInfoDto(
                clinic?.ClinicName ?? "",
                clinic?.ClinicAddress ?? "",
                clinic?.ClinicPhone.ToString() ?? ""
            );

var paymentRows = new List<PaymentRowDto>();
            foreach (var payment in payments)
            {
                paymentRows.Add(new PaymentRowDto(
                    payment.CreatedAt.ToString("yyyy-MM-dd"),
                    $"Pago para contrato {payment.ContractId}",
                    payment.Amount,
                    payment.Method.ToString(),
                    payment.RecivedBy
                ));
            }

            return new PaymentReportDataDto(
                patientDto,
                clinicDto,
                startDate?.ToString() ?? "",
                endDate?.ToString() ?? "",
                paymentRows,
                DateTime.Now.ToString("dd/MM/yyyy")
            );
        }

        public async Task<QuotaReportDataDto> GetQuotaReportDataAsync(Guid contractId)
        {
            var contract = await _repository.GetContractByIdAsync(contractId);
            if (contract == null)
                throw new Exception("Contract or patient not found");

            var payments = await _repository.GetPaymentsByContractIdAsync(contractId);
            var clinic = await _repository.GetClinicAsync();

            var patientDto = new PaymentInfoDto(
                contract.Patient.Person.Name,
                contract.Patient.Person.LastName,
                contract.Patient.Person.Ci,
                contract.Patient.Person.Phone.ToString()
            );

            var clinicDto = new ClinicInfoDto(
                clinic?.ClinicName ?? "",
                clinic?.ClinicAddress ?? "",
                clinic?.ClinicPhone.ToString() ?? ""
            );

            // Calculate contract summary
            int totalCost = contract.TotalCost;
            int totalPaid = payments.Sum(p => p.Amount);
            int debt = totalCost - totalPaid;

            var contractSummary = new ContractSummaryDto(totalCost, totalPaid, debt);

            // Create quota rows
            var quotaRows = new List<QuotaRowDto>();
            // For simplicity, we're creating a basic implementation here
            // In a real implementation, this would need to be more sophisticated
            quotaRows.Add(new QuotaRowDto("Total", totalPaid, true, DateTime.Now.ToString(), "Total"));

            return new QuotaReportDataDto(
                patientDto,
                clinicDto,
                contractSummary,
                quotaRows,
                DateTime.Now.ToString("dd/MM/yyyy")
            );
        }

        public async Task<AccountStatementDataDto> GetAccountStatementDataAsync(Guid patientId)
        {
            var patient = await _repository.GetPatientWithPersonAsync(patientId);
            if (patient == null)
                throw new Exception("Patient not found");

            var contracts = await _repository.GetContractsByPatientIdAsync(patientId);
            var clinic = await _repository.GetClinicAsync();

            var patientDto = new PaymentInfoDto(
                patient.Person.Name,
                patient.Person.LastName,
                patient.Person.Ci,
                patient.Person.Phone.ToString()
            );

            var clinicDto = new ClinicInfoDto(
                clinic?.ClinicName ?? "",
                clinic?.ClinicAddress ?? "",
                clinic?.ClinicPhone.ToString() ?? ""
            );

            int totalDebt = 0;
            var contractBalances = new List<ContractBalanceDto>();

            foreach (var contract in contracts)
            {
                var payments = await _repository.GetPaymentsByContractIdAsync(contract.Id);
                int totalPaid = payments.Sum(p => p.Amount);
                int debt = contract.TotalCost - totalPaid;
                totalDebt += debt;
                contractBalances.Add(new ContractBalanceDto(
                    contract.TotalCost,
                    totalPaid,
                    debt,
                    contract.TotalCost - totalPaid
                ));
            }

            return new AccountStatementDataDto(
                patientDto,
                clinicDto,
                contractBalances,
                totalDebt,
                DateTime.Now.ToString("dd/MM/yyyy")
            );
        }
    }
}