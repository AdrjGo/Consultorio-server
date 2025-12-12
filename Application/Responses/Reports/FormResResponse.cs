namespace Application.Responses
{
    public class FormResResponse
    {
        public Guid Id { get; set; }
        public Guid FormversionId { get; set; }
        public Guid PatientId { get; set; }
        public object JsonResponse { get; set; }
    }

    public class FormResMessageResponse
    {
        public string Message { get; set; }
    }
}