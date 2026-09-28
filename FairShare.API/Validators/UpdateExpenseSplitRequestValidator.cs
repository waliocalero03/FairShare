using FairShare.API.DTOs.ExpenseSplit;
using FluentValidation;

namespace FairShare.API.Validators
{
    public class UpdateExpenseSplitRequestValidator : AbstractValidator<UpdateExpenseSplitRequest>
    {
        public UpdateExpenseSplitRequestValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("El ID es obligatorio y debe ser mayor a 0.");

            RuleFor(x => x.ExpenseId)
                .GreaterThan(0).WithMessage("El ID del gasto es obligatorio y debe ser mayor a 0.");

            RuleFor(x => x.ParticipantId)
                .GreaterThan(0).WithMessage("El ID del participante es obligatorio y debe ser mayor a 0.");

            RuleFor(x => x.OwedAmount)
                .GreaterThan(0).WithMessage("El monto adeudado debe ser mayor a 0.")
                .LessThanOrEqualTo(999999.99m).WithMessage("El monto adeudado no puede superar 999999.99.");
        }
    }
}
