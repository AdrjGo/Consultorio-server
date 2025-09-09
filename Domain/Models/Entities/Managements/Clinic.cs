using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Clinic : BaseEntity
    {
        public required string ClinicName { get; set; }
        public required string ClinicAddress { get; set; }
        public required PhoneNumber ClinicPhone { get; set; }
        public required PhoneNumber ClinicCellPhone { get; set; }
        public required EmailAddress ClinicEmail { get; set; }
        public string? LogoRef { get; set; }
        public string? LogoUrl { get; set; }
        public required Guid ManagerId { get; set; }

        public required Person Manager { get; set; }
    }
}