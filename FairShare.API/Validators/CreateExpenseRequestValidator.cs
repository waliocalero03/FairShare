using FairShare.API.DTOs.Expense;
using FluentValidation;

namespace FairShare.API.Validators
{
    public class CreateExpenseRequestValidator : AbstractValidator<CreateExpenseRequest>
    {
        public CreateExpenseRequestValidator()
        {
            RuleFor(x => x.GroupId)
                .GreaterThan(0).WithMessage("El ID del grupo es obligatorio y debe ser mayor a 0.");

            RuleFor(x => x.PayerId)
                .GreaterThan(0).WithMessage("El ID del pagador es obligatorio y debe ser mayor a 0.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("La descripción del gasto es obligatoria.")
                .MaximumLength(500).WithMessage("La descripción no puede superar los 500 caracteres.");

            RuleFor(x => x.TotalAmount)
                .GreaterThan(0).WithMessage("El monto total debe ser mayor a 0.")
                .LessThanOrEqualTo(999999.99m).WithMessage("El monto total no puede superar 999999.99.");

            RuleFor(x => x.Date)
                .NotEmpty().WithMessage("La fecha es obligatoria.")
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La fecha no puede ser en el futuro.");
        }
    }
}
