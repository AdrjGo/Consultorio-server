using FluentValidation;

namespace Application.Dto
{
    public class UserRoleDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }

    public class UserRoleDtoValidator : AbstractValidator<UserRoleDto>
    {
        public UserRoleDtoValidator()
        {
            RuleFor(userRole => userRole.UserId)
                .NotEmpty().WithMessage("El usuario es obligatorio.");

            RuleFor(userRole => userRole.RoleId)
                .NotEmpty().WithMessage("El rol es obligatorio.");
        }
    }
}