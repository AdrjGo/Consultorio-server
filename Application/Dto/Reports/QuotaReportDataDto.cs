using System;
using System.Collections.Generic;

namespace Application.Dto.Reports
{
    public record ContractSummaryDto(int TotalCost, int TotalPaid, int Debt);
    
    public record QuotaRowDto(string QuotaMonth, int Amount, bool Paid, string? PaidDate, string? Method);
    
    public record QuotaReportDataDto(
        PaymentInfoDto Patient,
        ClinicInfoDto Clinic,
        ContractSummaryDto Contract,
        List<QuotaRowDto> Quotas,
        string GeneratedAt);
}