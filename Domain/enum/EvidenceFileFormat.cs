namespace Domain.Enum
{
    public enum EvidenceFileFormat
    {
        Pdf,
        Png,
        Jpg,
        Doc,
        Xlsx,
        Txt,
        Csv,
        Zip,
        Html
    }

    public static class EvidenceFileFormatExtensions
    {
        public static string GetMimeType(this EvidenceFileFormat format)
        {
            return format switch
            {
                EvidenceFileFormat.Pdf => "application/pdf",
                EvidenceFileFormat.Png => "image/png",
                EvidenceFileFormat.Jpg => "image/jpeg",
                EvidenceFileFormat.Doc => "application/msword",
                EvidenceFileFormat.Xlsx => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                EvidenceFileFormat.Txt => "text/plain",
                EvidenceFileFormat.Csv => "text/csv",
                EvidenceFileFormat.Zip => "application/zip",
                EvidenceFileFormat.Html => "text/html",
                _ => "application/octet-stream"
            };
        }
    }


}