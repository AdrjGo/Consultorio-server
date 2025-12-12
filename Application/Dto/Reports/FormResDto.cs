namespace Application.Dto
{
    public class FormResDto
    {
        public Guid FormVersionId { get; set; }
        public Guid PatientId { get; set; }
        public object JsonResponse { get; set; }
    }
}