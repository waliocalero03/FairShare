using FairShare.API.DTOs.Participant;
using FluentValidation;

namespace FairShare.API.Validators
{
    public class UpdateParticipantRequestValidator : AbstractValidator<UpdateParticipantRequest>
    {
        public UpdateParticipantRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del participante es obligatorio.")
                .MaximumLength(255).WithMessage("El nombre no puede superar los 255 caracteres.");

            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID del participante debe ser mayor a 0.");
        }
    }
}
