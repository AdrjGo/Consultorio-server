using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Reports
{
    public record PatientInfoDto(
        string FirstName, string LastName, string BirthDate,
        string Address, string Phone, int Age);

    public record BudgetRowDto(
        string Concepto, decimal Costo, int Cantidad,
        decimal Total, string? Observacion);

    public record PaymentManagerInfoDto(
        string Nombre, string Parentesco, string Email,
        string Telefono, string Celular);

    public record DoctorInfoDto(
        string DoctorName, string DoctorCI,
        string ClinicName, string ClinicAddress);

    public record OrthodonticsContractDataDto(
        PatientInfoDto Patient,
        object FormResponse,
        string ContractDate,
        decimal TotalCost,
        int MonthsDuration,
        PaymentManagerInfoDto PaymentManager,
        DoctorInfoDto Doctor);
}