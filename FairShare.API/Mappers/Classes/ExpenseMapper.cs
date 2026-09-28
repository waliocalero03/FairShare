using FairShare.API.DTOs.Expense;
using FairShare.Core;
using FairShare.API.Mappers.Interfaces;

namespace FairShare.API.Mappers.Classes
{
    public class ExpenseMapper : IExpenseMapper
    {
        public Expense ToEntity(CreateExpenseRequest request)
        {
            return new Expense
            {
                GroupId = request.GroupId,
                PayerId = request.PayerId,
                Description = request.Description,
                TotalAmount = request.TotalAmount,
                Date = request.Date
            };
        }

        public Expense ToEntity(UpdateExpenseRequest request)
        {
            return new Expense
            {
                Id = request.Id,
                GroupId = request.GroupId,
                PayerId = request.PayerId,
                Description = request.Description,
                TotalAmount = request.TotalAmount,
                Date = request.Date
            };
        }

        public ExpenseResponse ToResponse(Expense entity)
        {
            return new ExpenseResponse
            {
                Id = entity.Id,
                GroupId = entity.GroupId,
                PayerId = entity.PayerId,
                Description = entity.Description,
                TotalAmount = entity.TotalAmount,
                Date = entity.Date
            };
        }
    }
}
