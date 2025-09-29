using FluentValidation;

namespace Application.Dto
{
    public class PermissionDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class PermissionDtoValidator : AbstractValidator<PermissionDto>
    {
        public PermissionDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("El campo Nombre es requerido");
            RuleFor(x => x.Description).MaximumLength(100).WithMessage("El campo Descripción no puede tener más de 100 caracteres");
        }
    }
}