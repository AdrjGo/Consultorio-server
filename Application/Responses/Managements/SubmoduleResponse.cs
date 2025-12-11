namespace Application.Responses
{
    public class SubmoduleResponse
    {
        public int Id { get; set; }
        public string SubmoduleName { get; set; }
        public string State { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class SubmoduleMessageResponse
    {
        public string Message { get; set; }
    }
}