using Application.Dto;
using Application.Interfaces;
using Application.Responses;
using Application.Utils;
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

        public async Task<IEnumerable<UserRoleResponse>> GetUserRolesByIds(IEnumerable<Guid> ids)
        {
            var userRoles = await _userRoleRepository.GetUserRolesByIds(ids);
            return userRoles.Select(ur => new UserRoleResponse
            {
                Id = ur.Id,
                UserId = ur.UserId,
                RoleId = ur.RoleId,
            }).ToList();
        }

        public async Task<UserRoleMessageResponse> UpdateUserRoles(
            IEnumerable<UserRoleDto> newRoleDtos,
            string creatorName)
        {
            var userId = newRoleDtos.First().UserId;

            // 1. Roles actuales
            var existingRoles = await _userRoleRepository.GetUserRolesByUserId(userId);

            // 2. Eliminar roles previos
            await _userRoleRepository.RemoveUserRoles(existingRoles);

            // 3. Crear nuevos registros UserRole
            var newRoles = newRoleDtos.Select(dto => new UserRole
            {
                Id = Guid.CreateVersion7(),
                UserId = userId,
                RoleId = dto.RoleId,
                State = States.ACTIVE,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
                CreatedBy = creatorName,
            }).ToList();

            // 4. Insertar nuevos roles
            await _userRoleRepository.AssignRoleToUser(newRoles);

            return new UserRoleMessageResponse
            {
                Message = "Roles actualizados correctamente"
            };
        }

        public async Task<IEnumerable<UserRoleResponse>> GetUserRolesByUserId(Guid userId)
        {
            var userRoles = await _userRoleRepository.GetUserRolesByUserId(userId);
            return userRoles.Select(ur => new UserRoleResponse
            {
                Id = ur.Id,
                UserId = ur.UserId,
                RoleId = ur.RoleId,
            }).ToList();
        }


        public async Task AssignRolesToUser(IEnumerable<UserRoleDto> roleIds, string creatorName)
        {
            var userRoles = roleIds.Select(roleId => new UserRole
            {
                Id = Guid.CreateVersion7(),
                UserId = roleId.UserId,
                RoleId = roleId.RoleId,
                State = States.ACTIVE,
                CreatedAt = LocalDateTime.ParseBoliviaTime(DateTime.UtcNow.ToString("o")),
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