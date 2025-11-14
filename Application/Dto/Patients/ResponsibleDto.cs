using Domain.Enum;

namespace Application.Dto
{
    public class ResponsibleDto
    {
        public string Parentage { get; set; }

        public PersonDto Person { get; set; }
    }
}