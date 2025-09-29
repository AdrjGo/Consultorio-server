using Microsoft.AspNetCore.Authorization;

namespace Application.Security.Authorization
{
    // 🔹 Este requisito es solo un "contrato" que después será manejado por un AuthorizationHandler.
    // 🔹 Todavía no consulta DB ni valida nada, solo define qué permiso se está pidiendo.

    // Requisito genérico para validar un permiso.
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}