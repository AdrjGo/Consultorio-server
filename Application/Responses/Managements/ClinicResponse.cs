namespace Application.Responses
{
    public class ClinicResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string LogoRef { get; set; }
        public string LogoUrl { get; set; }
        public Guid ManagerId { get; set; }
    }
}