namespace Application.Dto
{
    public class FormVersionDto
    {
        public int SubmodId { get; set; }
        public int NumberVersion { get; set; }
        public object JsonSchema { get; set; }

        public FormDto Form { get; set; }
    }
}