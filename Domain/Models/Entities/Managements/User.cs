namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public required Guid PersonId { get; set; }
        public required string Password { get; set; }

        public required Person Person { get; set; }
        public required List<UserRole> UserRoles { get; set; }
        public required List<Appointment> Appointments { get; set; }
    }
}