namespace Domain.Entities
{
    public class UserRole : BaseEntity
    {
        public required Guid UserId { get; set; }
        public required Guid RoleId { get; set; }

        public User? User { get; set; }
        public Role? Role { get; set; }
    }
}