namespace Application.Responses
{
    public class PatientResponse
    {
        public Guid Id { get; set; }
        public PersonResponse Responsible { get; set; }
        public string Address { get; set; }
        public string Zone { get; set; }
        public string City { get; set; }
        public string HomePhone { get; set; }
        public string Occupation { get; set; }
        public string PlaceOccupation { get; set; }
        public string Sender { get; set; }

        public PersonResponse Patient { get; set; }

    }
}