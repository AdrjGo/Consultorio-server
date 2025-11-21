namespace Application.Responses
{
    public class RoleResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string UsersUsingRole { get; set; }
    }
}