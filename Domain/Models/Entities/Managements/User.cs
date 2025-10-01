namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public required Guid PersonId { get; set; }
        public required string Password { get; set; }

        public Person Person { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Appointment> Appointments { get; set; }
    }
}