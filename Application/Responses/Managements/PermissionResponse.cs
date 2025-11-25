using FluentValidation;

namespace Application.Responses
{
    public class PermissionResponse
    {
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class PermissionResponseValidator : AbstractValidator<PermissionResponse>
    {
        public PermissionResponseValidator()
        {
            RuleFor(x => x.Key).NotEmpty().WithMessage("El campo Clave es requerido");
            RuleFor(x => x.Name).NotEmpty().WithMessage("El campo Nombre es requerido");
            RuleFor(x => x.Description).MaximumLength(100).WithMessage("El campo Descripción no puede tener más de 100 caracteres");
        }
    }

    public class PermissionMessageResponse
    {
        public string Message { get; set; }
    }
}