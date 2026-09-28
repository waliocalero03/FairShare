using FairShare.API.DTOs.ExpenseSplit;
using FairShare.API.Mappers.Interfaces;
using FairShare.Core;

namespace FairShare.API.Mappers.Classes
{
    public class ExpenseSplitMapper : IExpenseSplitMapper
    {
        public ExpenseSplit ToEntity(CreateExpenseSplitRequest request)
        {
            return new ExpenseSplit
            {
                ExpenseId = request.ExpenseId,
                ParticipantId = request.ParticipantId,
                OwedAmount = request.OwedAmount
            };
        }

        public ExpenseSplit ToEntity(UpdateExpenseSplitRequest request)
        {
            return new ExpenseSplit
            {
                Id = request.Id,
                ExpenseId = request.ExpenseId,
                ParticipantId = request.ParticipantId,
                OwedAmount = request.OwedAmount
            };
        }

        public ExpenseSplitResponse ToResponse(ExpenseSplit entity)
        {
            return new ExpenseSplitResponse
            {
                Id = entity.Id,
                ExpenseId = entity.ExpenseId,
                ParticipantId = entity.ParticipantId,
                OwedAmount = entity.OwedAmount
            };
        }
    }
}
