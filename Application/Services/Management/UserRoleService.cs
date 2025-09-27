using Application.Dto;
using Application.Interfaces;
using Domain.Entities;
using Domain.Enum;

namespace Application.Services
{
    public class UserRoleService
    {
        private readonly IUserRoleRepository _userRoleRepository;

        public UserRoleService(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        public async Task AssignRolesToUser(IEnumerable<UserRoleDto> roleIds, string creatorName)
        {
            var userRoles = roleIds.Select(roleId => new UserRole
            {
                Id = Guid.CreateVersion7(),
                UserId = roleId.UserId,
                RoleId = roleId.RoleId,
                State = States.ACTIVE,
                CreatedAt = DateTime.UtcNow,
                CreatedBy = creatorName,
            }).ToList();

            await _userRoleRepository.AssignRoleToUser(userRoles);
        }

        public async Task RemoveRolesFromUser(IEnumerable<Guid> userRoleIds)
        {
            var userRoles = await _userRoleRepository.GetUserRolesByIds(userRoleIds);

            if (!userRoles.Any())
                throw new KeyNotFoundException("No se encontraron roles para eliminar.");

            await _userRoleRepository.RemoveUserRoles(userRoles);
        }
    }
}