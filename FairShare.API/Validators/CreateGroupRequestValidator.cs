using FairShare.API.DTOs;
using FluentValidation;

namespace FairShare.API.Validators
{
    public class CreateGroupRequestValidator : AbstractValidator<CreateGroupRequest>
    {
        public CreateGroupRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del grupo es obligatorio.")
                .MaximumLength(255).WithMessage("El nombre no puede superar los 255 caracteres.");

            RuleFor(x => x.Code)
                .NotEmpty().WithMessage("El código del grupo es obligatorio.")
                .MaximumLength(50).WithMessage("El código no puede superar los 50 caracteres.");
        }
    }
}
