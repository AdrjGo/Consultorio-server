using Application.Responses;

namespace Application.Response
{
    public class ResponsibleResponse
    {
        public Guid Id { get; set; }
        public string Parentage { get; set; }
        public PersonResponse Person { get; set; }
    }
}