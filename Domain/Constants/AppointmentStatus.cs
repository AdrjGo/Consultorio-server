namespace Domain.Constants
{
    public class AppointmentStatus
    {
        public static string SCHEDULED { get; } = "SCHEDULED";
        public static string CANCELLED { get; } = "CANCELLED";
        public static string RESCHEDULED { get; } = "RESCHEDULED";
        public static string COMPLETED { get; } = "COMPLETED";
        public static string NO_SHOW { get; } = "NO_SHOW";
    }

    public class AppointmentTypes
    {
        public static string VISIT { get; } = "VISIT";
        public static string APPOINTMENT { get; } = "APPOINTMENT";
        public static string EMERGENCY { get; } = "EMERGENCY";
        public static string TREATMENT { get; } = "TREATMENT";
        public static string PRESCRIPTION { get; } = "PRESCRIPTION";
        public static string CONSULTATION { get; } = "CONSULTATION";
        public static string RE_CONSULTATION { get; } = "RE_CONSULTATION";
    }
}