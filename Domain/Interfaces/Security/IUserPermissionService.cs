namespace Application.Interfaces
{
    public interface IUserPermissionService
    {
        Task<bool> UserHasPermissionAsync(Guid userId, string permissionId);
    }
}