namespace Application.Utils
{
    public static class LocalDateTime
    {
        public static DateTime ParseBoliviaTime(string dateTimeString)
        {
            // Parsea directamente y especifica Unspecified
            var parsed = DateTime.Parse(dateTimeString);
            return DateTime.SpecifyKind(parsed, DateTimeKind.Unspecified);
        }
    }
}