using FairShare.API.DTOs.ExpenseSplit;
using FairShare.API.Mappers.Interfaces;
using FairShare.Data.DTOs.ExpenseSplit;

namespace FairShare.API.Mappers.Classes
{
    public class ExpenseSplitMapper : IExpenseSplitMapper
    {
        public ExpenseSplitDTO ToDetailDTO(ExpenseSplitDetailDTO entity)
        {
            return new ExpenseSplitDTO
            {
                Id = entity.Id,
                ExpenseId = entity.ExpenseId,
                ParticipantId = entity.ParticipantId,
                ParticipantName = entity.ParticipantName,
                OwedAmount = entity.OwedAmount
            };
        }
    }
}
