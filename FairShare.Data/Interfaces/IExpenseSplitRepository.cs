using FairShare.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FairShare.Data.Interfaces
{
    public interface IExpenseSplitRepository
    {
        ExpenseSplit? GetExpenseSplitById(int id);

        // Búsquedas clave para la lógica de negocio:
        IEnumerable<ExpenseSplit> GetSplitsByExpenseId(int expenseId);
        IEnumerable<ExpenseSplit> GetSplitsByParticipantId(int participantId);

        bool CreateExpenseSplit(ExpenseSplit expenseSplit);
        bool UpdateExpenseSplit(ExpenseSplit expenseSplit);
        bool DeleteExpenseSplit(int id);
    }
}
