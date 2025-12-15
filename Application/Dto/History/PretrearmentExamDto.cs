namespace Application.Dto
{
    public class PretreatmentExamDto
    {
        public Guid PatientId { get; set; }
        public string Observations { get; set; }
        public string Interconsultation { get; set; }
        public string Piece { get; set; }
        public bool Caries { get; set; }
        public string? Treatment { get; set; }
        public int Cost { get; set; }
    }
}