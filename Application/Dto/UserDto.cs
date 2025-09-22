using FluentValidation;

namespace Application.Dto
{
    public class UserDto
    {
        public string Password { get; set; }

        public PersonDto Person { get; set; }
    }

    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
        {
            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("La contraseña es obligatoria.")
                .MinimumLength(6).WithMessage("La contraseña debe tener al menos 6 caracteres.");

            RuleFor(user => user.Person)
                .NotNull().WithMessage("La persona asociada es obligatoria.")
                .SetValidator(new PersonDtoValidator());
        }
    }
}