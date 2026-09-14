using FairShare.API.DTOs;
using FluentValidation;

namespace FairShare.API.Validators
{
    public class UpdateGroupRequestValidator : AbstractValidator<UpdateGroupRequest>
    {
        public UpdateGroupRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del grupo es obligatorio para actualizar.")
                .MaximumLength(255).WithMessage("El nombre no puede superar los 255 caracteres.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("El código del grupo es obligatorio.")
                .MaximumLength(50).WithMessage("El código no puede superar los 50 caracteres.");

            RuleFor(x => x.Id)
                .NotNull().WithMessage("El ID del grupo es obligatorio para actualizar.");
        }
    }
}
