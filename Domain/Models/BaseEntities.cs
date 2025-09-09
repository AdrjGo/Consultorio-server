using Domain.Enum;

namespace Domain.Entities
{
    public class BaseEntity
    {
        public required Guid Id { get; set; } = Guid.CreateVersion7();
        public required States State { get; set; } = States.ACTIVE;
        public required string CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public required DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}