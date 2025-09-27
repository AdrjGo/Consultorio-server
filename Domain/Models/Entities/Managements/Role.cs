namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        public List<UserRole>? UserRoles { get; set; }
        public List<RolePermission>? RolePermissions { get; set; }
    }
}