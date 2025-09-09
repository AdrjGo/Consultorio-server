namespace Domain.Entities
{
    public class RolePermission : BaseEntity
    {
        public required Guid RoleId { get; set; }
        public required Guid PermissionId { get; set; }

        public required Role Role { get; set; }
        public required Permission Permission { get; set; }
    }
}