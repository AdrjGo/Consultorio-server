namespace Application.Responses
{
    public class MonitoringResponse
    {
        public Guid Id { get; set; }
        public string Nomenclature { get; set; }
        public string Treatment { get; set; }
        public string Files { get; set; }
        public string Date { get; set; }
    }

    public class MonitoringCreatedUpdateResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}