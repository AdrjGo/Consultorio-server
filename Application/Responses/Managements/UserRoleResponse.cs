namespace Application.Responses
{
    public class UserRoleResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class UserRoleMessageResponse
    {
        public Guid? Id { get; set; }
        public string Message { get; set; }
    }
}