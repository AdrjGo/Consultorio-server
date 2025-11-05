using FluentValidation;

namespace Application.Dto
{
    public class RoleDto
    {
        // public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
    }

    public class RoleDtoValidator : AbstractValidator<RoleDto>
    {
        public RoleDtoValidator()
        {
            RuleFor(role => role.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(20).WithMessage("El nombre no puede superar los 20 caracteres.");

            RuleFor(role => role.Description)
                .MaximumLength(100).WithMessage("La descripción no puede superar los 100 caracteres.");
        }
    }
}