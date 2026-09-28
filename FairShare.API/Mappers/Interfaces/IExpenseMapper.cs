using FairShare.API.DTOs.Expense;
using FairShare.Core;

namespace FairShare.API.Mappers.Interfaces
{
    public interface IExpenseMapper
    {
        Expense ToEntity(CreateExpenseRequest request);
        Expense ToEntity(UpdateExpenseRequest request);
        ExpenseResponse ToResponse(Expense entity);
    }
}
