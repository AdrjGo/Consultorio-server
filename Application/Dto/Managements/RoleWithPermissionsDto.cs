namespace Application.Dto
{
    public class RoleWithPermissionsDto
    {
        public RoleDto Role { get; set; }
        public IEnumerable<Guid> Permissions { get; set; }
    }
}