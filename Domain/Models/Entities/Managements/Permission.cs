namespace Domain.Entities
{
    public class Permission : BaseEntity
    {
        public required string Name { get; set; }
        public required string Description { get; set; }

        public List<RolePermission>? RolePermissions { get; set; }
    }
}