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

        public Clinic? Clinic { get; set; }
        public User? User { get; set; }
        public Patient? Patient { get; set; }
        public PatientResponsible? PatientResponsible { get; set; }
        public PaymentManager? PaymentManager { get; set; }
    }
}