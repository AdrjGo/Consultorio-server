namespace Domain.Entities
{
    public class Permission : BaseEntity
    {
        public string Key { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }

        public List<RolePermission>? RolePermissions { get; set; }
    }
}