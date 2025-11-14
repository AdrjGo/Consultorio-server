using Application.Response;

namespace Application.Responses
{
    public class PatientResponse
    {
        public Guid Id { get; set; }
        public ResponsibleResponse? Responsible { get; set; }
        public string Address { get; set; }
        public string Zone { get; set; }
        public string City { get; set; }
        public string HomePhone { get; set; }
        public string Occupation { get; set; }
        public string PlaceOccupation { get; set; }
        public string? Nit { get; set; }
        public string Sender { get; set; }
        public string? State { get; set; }
        public string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }

        public PersonResponse PatientPerson { get; set; }

    }

    public class PatientMessageResponse
    {
        public Guid Id { get; set; }
        public string Message { get; set; }
    }
}