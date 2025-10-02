using FluentValidation;

namespace Application.Dto
{
    public class PatientDto
    {
        public required string Address { get; set; }
        public required string Zone { get; set; }
        public required string City { get; set; }
        public string? HomePhone { get; set; }
        public required string Occupation { get; set; }
        public required string PlaceOccupation { get; set; }
        public string? Sender { get; set; }

        public required PersonDto Person { get; set; }
        public ResponsibleDto? Responsible { get; set; }
    }

    public class PatientDtoValidator : AbstractValidator<PatientDto>
    {
        public PatientDtoValidator()
        {
            RuleFor(patient => patient.Person)
                .NotNull().WithMessage("La persona asociada es obligatoria.")
                .SetValidator(new PersonDtoValidator());

            RuleFor(patient => patient.Address)
                .NotEmpty().WithMessage("La dirección es obligatoria.");

            RuleFor(patient => patient.Zone)
                .NotEmpty().WithMessage("La zona es obligatoria.");

            RuleFor(patient => patient.City)
                .NotEmpty().WithMessage("La ciudad es obligatoria.");

            RuleFor(patient => patient.Occupation)
                .NotEmpty().WithMessage("La ocupación es obligatoria.");

            RuleFor(patient => patient.PlaceOccupation)
                .NotEmpty().WithMessage("El lugar de ocupación es obligatorio.");
        }
    }
}