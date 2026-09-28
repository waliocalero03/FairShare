using FairShare.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace FairShare.Data.Interfaces
{
    public interface IExpenseRepository
    {
        Expense? GetExpenseById(int id);
        IEnumerable<Expense> GetExpensesByGroupId(int groupId);

        // ¡Importante! Devuelve int (el nuevo ID) en lugar de bool
        int CreateExpense(Expense expense); 

        bool UpdateExpense(Expense expense);
        bool DeleteExpense(int id);
    }
}
