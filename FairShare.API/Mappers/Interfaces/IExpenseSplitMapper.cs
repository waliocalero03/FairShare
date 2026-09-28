using FairShare.API.DTOs.ExpenseSplit;
using FairShare.Core;

namespace FairShare.API.Mappers.Interfaces
{
    public interface IExpenseSplitMapper
    {
        ExpenseSplit ToEntity(CreateExpenseSplitRequest request);
        ExpenseSplit ToEntity(UpdateExpenseSplitRequest request);
        ExpenseSplitResponse ToResponse(ExpenseSplit entity);
    }
}
