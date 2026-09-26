using FairShare.API.DTOs.Participant;
using FluentValidation;

namespace FairShare.API.Validators
{
    public class CreateParticipantRequestValidator : AbstractValidator<CreateParticipantRequest>
    {
        public CreateParticipantRequestValidator()
        {
            RuleFor(x => x.GroupId)
                .GreaterThan(0).WithMessage("El ID del grupo debe ser mayor a 0.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("El nombre del participante es obligatorio.")
                .MaximumLength(255).WithMessage("El nombre no puede superar los 255 caracteres.");
        }
    }
}
