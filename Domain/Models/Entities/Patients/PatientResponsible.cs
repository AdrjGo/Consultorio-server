using Domain.Enum;

namespace Domain.Entities
{
    public class PatientResponsible : BaseEntity
    {
        public required Guid PersonId { get; set; }
        public required PatientParentage Parentage { get; set; }

        public required Person Person { get; set; }
        public Patient? Patient { get; set; }
    }
}