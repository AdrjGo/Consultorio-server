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
        }


        public static class Patient
        {
            public const string Create = "Create.Patient";
            public const string Read = "Read.Patient";
            public const string Update = "Update.Patient";
            public const string Delete = "Delete.Patient";
        }

        public static class Appointment
        {
            public const string Create = "Create.Appointment";
            public const string Read = "Read.Appointment";
            public const string Update = "Update.Appointment";
            public const string Delete = "Delete.Appointment";
        }

        public static class EvidenceFile
        {
            public const string Create = "Create.EvidenceFile";
            public const string Read = "Read.EvidenceFile";
            public const string Update = "Update.EvidenceFile";
            public const string Delete = "Delete.EvidenceFile";
        }

        public static class Monitoring
        {
            public const string Create = "Create.Monitoring";
            public const string Read = "Read.Monitoring";
            public const string Update = "Update.Monitoring";
            public const string Delete = "Delete.Monitoring";
        }
    }
}