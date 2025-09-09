using Domain.Enum;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Person : BaseEntity
    {
        public required string Name { get; set; }
        public required string LastName { get; set; }
        public required DateTime BirthDate { get; set; }
        public required Gender Sex { get; set; }
        public required string Ci { get; set; }
        public EmailAddress? Email { get; set; }
        public required PhoneNumber Phone { get; set; }
        public string? Profession { get; set; }

        public required Clinic Clinic { get; set; }
        public required User User { get; set; }
        public required Patient Patient { get; set; }
        public required PatientResponsible PatientResponsible { get; set; }
        public required PaymentManager PaymentManager { get; set; }
    }
}