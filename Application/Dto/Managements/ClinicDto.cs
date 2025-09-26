using FluentValidation;

namespace Application.Dto
{
    public class ClinicDto
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string CellPhone { get; set; }
        public string Email { get; set; }
        public string LogoRef { get; set; }
        public string LogoUrl { get; set; }
        public Guid ManagerId { get; set; }

    }

    public class ClinicDtoValidator : AbstractValidator<ClinicDto>
    {
        public ClinicDtoValidator()
        {
            RuleFor(clinic => clinic.Name)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres.");

            RuleFor(clinic => clinic.Address)
                .NotEmpty().WithMessage("La dirección es obligatoria.")
                .MaximumLength(100).WithMessage("La dirección no puede superar los 100 caracteres.");

            RuleFor(clinic => clinic.Phone)
                .NotEmpty().WithMessage("El teléfono es obligatorio.")
                .MaximumLength(10).WithMessage("El teléfono no puede superar los 10 caracteres.");

            RuleFor(clinic => clinic.CellPhone)
                .NotEmpty().WithMessage("El celular es obligatorio.")
                .MaximumLength(10).WithMessage("El celular no puede superar los 10 caracteres.");

            RuleFor(clinic => clinic.Email)
                .NotEmpty().WithMessage("El email es obligatorio.")
                .MaximumLength(20).WithMessage("El email no puede superar los 20 caracteres.");

            // RuleFor(clinic => clinic.LogoRef)
            //     .NotEmpty().WithMessage("El logo es obligatorio.");

            // RuleFor(clinic => clinic.LogoUrl)
            //     .NotEmpty().WithMessage("La url del logo es obligatoria.");
        }
    }
}