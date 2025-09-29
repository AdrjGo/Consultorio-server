namespace Application.Security
{
    public class Permissions
    {
        public static class User
        {
            public const string Create = "Create.User";
            public const string Read = "Read.User";
            public const string Update = "Update.User";
            public const string Delete = "Delete.User";
        }

        public static class UserRole
        {
            public const string Create = "Create.RolePermission";
            public const string Read = "Read.RolePermission";
            public const string Update = "Update.RolePermission";
            public const string Delete = "Delete.RolePermission";
        }

        public static class Role
        {
            public const string Create = "Create.Role";
            public const string Read = "Read.Role";
            public const string Update = "Update.Role";
            public const string Delete = "Delete.Role";
        }

        public static class RolePermission
        {
            public const string Create = "Create.RolePermission";
            public const string Read = "Read.RolePermission";
            public const string Update = "Update.RolePermission";
            public const string Delete = "Delete.RolePermission";
        }

        public static class Permission
        {
            public const string Create = "Create.Permission";
            public const string Read = "Read.Permission";
            public const string Update = "Update.Permission";
            public const string Delete = "Delete.Permission";
        }

        public static class Clinic
        {
            public const string Create = "Create.Clinic";
            public const string Read = "Read.Clinic";
            public const string Update = "Update.Clinic";
            public const string Delete = "Delete.Clinic";
        }
    }
}