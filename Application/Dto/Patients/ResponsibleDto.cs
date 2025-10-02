using Domain.Enum;

namespace Application.Dto
{
    public class ResponsibleDto
    {
        public PatientParentage Parentage { get; set; }

        public PersonDto Person { get; set; }
    }
}