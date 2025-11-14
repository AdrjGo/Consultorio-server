using Domain.Enum;

namespace Application.Responses
{
    public class UserResponse
    {
        public Guid Id { get; set; }
        public string State { get; set; }
        public PersonResponse Person { get; set; }
        public IEnumerable<RoleResponse> Roles { get; set; }
        public string CreatedAt { get; set; }
        public string? UpdatedAt { get; set; }
        public string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
    }

    public class UserChangeStateResponse
    {
        public Guid Id { get; set; }
        public States State { get; set; }
    }
}