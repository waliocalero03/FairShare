using FairShare.Data.DTOs.ExpenseSplit;
using System;
using System.Collections.Generic;
using System.Text;

namespace FairShare.Data.Interfaces
{
    public interface IExpenseSplitRepository
    {
        public IEnumerable<ExpenseSplitDetailDTO> GetExpenseSplitByIdGroup(int groupId);
    }
}
