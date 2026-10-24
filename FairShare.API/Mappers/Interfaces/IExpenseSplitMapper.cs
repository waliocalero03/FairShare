using FairShare.API.DTOs.ExpenseSplit;
using FairShare.Data.DTOs.ExpenseSplit;

namespace FairShare.API.Mappers.Interfaces
{
    public interface IExpenseSplitMapper
    {
        public ExpenseSplitDTO ToDetailDTO(ExpenseSplitDetailDTO entity);
    }
}
