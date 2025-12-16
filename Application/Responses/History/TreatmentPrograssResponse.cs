namespace Application.Responses
{
    public class TreatmentProgressResponse
    {
        public Guid Id { get; set; }
        public Guid PatientId { get; set; }
        public int Payment { get; set; }
        public int Debt { get; set; }
        public string Date { get; set; }
    }

    public class TreatmentProgressSummaryResponse
    {
        public int TotalPayment { get; set; }
        public int TotalDebt { get; set; }
        public List<TreatmentProgressResponse> TreatmentProgresses { get; set; }
    }

    public class TreatmentProgressMessageResponse
    {
        public string Message { get; set; }
    }
}