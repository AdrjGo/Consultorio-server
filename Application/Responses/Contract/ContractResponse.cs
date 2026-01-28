namespace Application.Responses
{
    public class ContractResponse
    {
        public Guid ContractId { get; set; }
        public Guid PatientId { get; set; }
        public decimal TotalCost { get; set; }
        public int MonthsDuration { get; set; }
        public string Date { get; set; }
        public int SubmodID { get; set; }
    }

    public class FullContractResponse
    {
        public ContractResponse Contract { get; set; }
        public string PaymentManagerName { get; set; }
        public string PaymentManagerId { get; set; }

    }

    public class ContractMessageResponse
    {
        public string Message { get; set; }
    }
}