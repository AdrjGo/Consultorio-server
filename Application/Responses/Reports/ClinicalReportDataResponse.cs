namespace Application.Responses
{
    public class ClinicalReportDataResponse
    {
        public Guid PatientId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string DateOfBirth { get; set; } = string.Empty;
        public string Gender { get; set; } = string.Empty;

        public string ClinicName { get; set; } = string.Empty;
        public string ClinicAddress { get; set; } = string.Empty;
        public string ClinicPhone { get; set; } = string.Empty;
        public string ClinicLogoUrl { get; set; } = string.Empty;

        public string ContractNumber { get; set; } = "N/A";
        public string ContractStartDate { get; set; } = "N/A";
        public string ContractStatus { get; set; } = "N/A";

        public string ResponsiblePartyName { get; set; } = "N/A";
        public string ResponsiblePartyPhone { get; set; } = "N/A";
        public string ResponsiblePartyRelationship { get; set; } = "N/A";

        public List<PaymentEntryResponse> Payments { get; set; } = new();
    }

    public class PaymentEntryResponse
    {
        public string Date { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public string ReceiptNumber { get; set; } = string.Empty;
    }
}
