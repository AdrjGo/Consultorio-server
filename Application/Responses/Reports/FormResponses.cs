namespace Application.Responses
{
    public class FormResponses
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class FormMessageResponse
    {
        public string Message { get; set; }
    }
}