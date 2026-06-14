using System;
using System.Collections.Generic;

namespace Application.Dto.Reports
{
    public record ContractBalanceDto(int TotalCost, int TotalPaid, int Debt, int RunningBalance);
    
    public record AccountStatementDataDto(
        PaymentInfoDto Patient,
        ClinicInfoDto Clinic,
        List<ContractBalanceDto> Contracts,
        int TotalDebt,
        string GeneratedAt);
}