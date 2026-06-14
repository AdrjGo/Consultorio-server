using System;
using System.Collections.Generic;

namespace Application.Dto.Reports
{
    public record PaymentInfoDto(string Name, string LastName, string Ci, string Phone);
    
    public record ClinicInfoDto(string Name, string Address, string Phone);
    
    public record PaymentRowDto(string Date, string Description, int Amount, string Method, string ReceivedBy);
    
    public record PaymentReportDataDto(
        PaymentInfoDto Patient,
        ClinicInfoDto Clinic,
        string? StartDate, 
        string? EndDate,
        List<PaymentRowDto> Payments,
        string GeneratedAt);
}