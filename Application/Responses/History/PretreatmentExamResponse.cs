namespace Application.Responses
{
    public class PretreatmentExamResponse
    {
        public Guid Id { get; set; }
        public string Observations { get; set; }
        public string Interconsultation { get; set; }
        public string Piece { get; set; }
        public bool Caries { get; set; }
        public string? Treatment { get; set; }
        public int Cost { get; set; }
        public string Date { get; set; }
    }

    public class PretreatmentSummaryResponse
    {
        public int TotalCost { get; set; }
        public List<PretreatmentExamResponse> Exams { get; set; } = new();
    }

    public class PretreatmentExamMessageResponse
    {
        public string Message { get; set; }
    }
}