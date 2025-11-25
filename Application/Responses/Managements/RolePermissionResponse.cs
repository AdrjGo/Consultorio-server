namespace Application.Responses
{
    public class RolePermissionResponse
    {
        public Guid Id { get; set; }
        public string? Key { get; set; }
        public List<PermissionResponse> Permissions { get; set; }
    }
}