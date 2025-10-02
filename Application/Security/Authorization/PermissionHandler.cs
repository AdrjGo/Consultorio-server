using System.Security.Claims;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Application.Security.Authorization
{
    // Valida si el usuario tiene el permiso solicitado en el requisito.

    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUserPermissionService _userPermissionService;

        public PermissionHandler(IUserPermissionService userPermissionService)
        {
            _userPermissionService = userPermissionService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            var userId = context.User.FindFirstValue("userId");
            if (string.IsNullOrEmpty(userId)) return;

            var hasPermission = await _userPermissionService.UserHasPermissionAsync(Guid.Parse(userId), requirement.Permission);
            if (hasPermission) context.Succeed(requirement);
        }
    }
}