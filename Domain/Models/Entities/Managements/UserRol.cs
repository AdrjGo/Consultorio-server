namespace Domain.Entities
{
    public class UserRol : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required Guid RoleId { get; set; }

        public required User User { get; set; }
        public required Role Role { get; set; }
    }
}