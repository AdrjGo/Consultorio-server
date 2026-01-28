namespace Application.Dto
{
    public class FormContractRequest
    {
        public FormResDto FormRes { get; set; }
        public ContractDto Contract { get; set; }
        public PaymentManagerDto PaymentManager { get; set; }
    }
}