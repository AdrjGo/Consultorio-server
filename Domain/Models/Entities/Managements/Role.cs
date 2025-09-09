namespace Domain.Entities
{
    public class Role : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        public required List<UserRol> UserRols { get; set; }
        public required List<RolePermission> RolePermissions { get; set; }
    }
}