using Domain.Enum;
using FluentValidation;

namespace Application.Dto
{
    public class AppointmentDto
    {
        public Guid PatientId { get; set; }
        public Guid ProfessionalId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public AppointmentType Type { get; set; }
        public AppointmentStatus Status { get; set; }
        public string Reason { get; set; }
        public string Observations { get; set; }
    }

    public class AppointmentDtoValidator : AbstractValidator<AppointmentDto>
    {
        public AppointmentDtoValidator()
        {
            RuleFor(x => x.StartDate).NotNull().WithMessage("La fecha de inicio es obligatoria");
            RuleFor(x => x.EndDate).NotNull().WithMessage("La fecha de fin es obligatoria");
            RuleFor(x => x.StartDate).NotEqual(DateTime.MinValue);
            RuleFor(x => x.EndDate).NotEqual(DateTime.MinValue);
            RuleFor(x => x.Type).NotNull().WithMessage("El tipo de cita es obligatorio");
            RuleFor(x => x.Status).NotNull().WithMessage("El estado de cita es obligatorio");
            RuleFor(x => x.Reason).NotNull().WithMessage("La razón de cita es obligatoria");
            RuleFor(x => x.Observations).NotNull().WithMessage("Las observaciones son obligatorias");
        }
    }


    public class AppointmentUpdateDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public AppointmentType? Type { get; set; }
        public AppointmentStatus? Status { get; set; }
        public string? Reason { get; set; }
        public string? Observations { get; set; }
    }

}