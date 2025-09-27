using Domain.Enum;

namespace Application.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public States State { get; set; }
        public PersonResponse Person { get; set; }
    }

    public class UserChangeStateResponse
    {
        public Guid Id { get; set; }
        public States State { get; set; }
    }
}