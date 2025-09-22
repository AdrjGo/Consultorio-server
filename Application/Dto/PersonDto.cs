using FluentValidation;

namespace Application.Dto
{
    public class PersonDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string BirthDate { get; set; }
        public string Sex { get; set; }
        public string Ci { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class PersonDtoValidator : AbstractValidator<PersonDto>
    {
        public PersonDtoValidator()
        {
            RuleFor(person => person.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.");

            RuleFor(person => person.LastName)
                .NotEmpty().WithMessage("El apellido es obligatorio.");

            RuleFor(person => person.Ci)
                .NotEmpty().WithMessage("El CI es obligatorio.")
                .MaximumLength(9).WithMessage("El CI no puede tener más de 9 caracteres.");

            RuleFor(person => person.Phone)
                .Matches(@"^\d{8,10}$")
                .WithMessage("El número de teléfono debe tener entre 8 y 10 dígitos.")
                .NotEmpty().WithMessage("El teléfono es obligatorio");

            RuleFor(person => person.Email)
                .EmailAddress().WithMessage("El correo electrónico es inválido.");
        }
    }
}