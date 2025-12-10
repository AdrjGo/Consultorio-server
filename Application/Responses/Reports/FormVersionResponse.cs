namespace Application.Responses
{
    public class FormVersionResponse
    {
        public Guid Id { get; set; }
        public string SubmodId { get; set; }
        public int NumberVersion { get; set; }
        public object JsonSchema { get; set; }

        public FormResponses Form { get; set; }
    }

    public class FormVersionMessageResponse
    {
        public string Message { get; set; }
    }
}