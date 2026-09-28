using Dapper;
using FairShare.Core;
using FairShare.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FairShare.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        #region Variables
        private readonly IDbConnection _dbConnection;
        private const int RowsNotAffected = 0;

        #endregion

        #region Constructor

        public ExpenseRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        #endregion

        #region Public Methods

        public Expense? GetExpenseById(int id)
        {
            string sql = "SELECT * FROM Expenses WHERE Id = @Id;";

            return _dbConnection.QueryFirstOrDefault<Expense>(sql, new { Id = id });
        }

        public IEnumerable<Expense> GetExpensesByGroupId(int groupId)
        {
            string sql = "SELECT * FROM Expenses WHERE GroupId = @GroupId ORDER BY Date DESC;";

            return _dbConnection.Query<Expense>(sql, new { GroupId = groupId });
        }

        public int CreateExpense(Expense expense)
        {
            string sql = @"
                INSERT INTO Expenses (GroupId, PayerId, Description, TotalAmount, Date) 
                VALUES (@GroupId, @PayerId, @Description, @TotalAmount, @Date)
                RETURNING Id;";

            int newId = _dbConnection.QuerySingleOrDefault<int>(sql, expense);

            return newId;
        }

        public bool UpdateExpense(Expense expense)
        {
            string sql = @"
                UPDATE Expenses 
                SET GroupId = @GroupId, PayerId = @PayerId, Description = @Description, 
                    TotalAmount = @TotalAmount, Date = @Date 
                WHERE Id = @Id;";

            return ExecuteQuery(sql, expense);
        }

        public bool DeleteExpense(int id)
        {
            string sql = "DELETE FROM Expenses WHERE Id = @Id;";

            return ExecuteQuery(sql, new { Id = id });
        }

        #endregion

        #region Private Methods

        private bool ExecuteQuery(string sql, object parameters)
        {
            int rowsAffected = _dbConnection.Execute(sql, parameters);
            return rowsAffected > RowsNotAffected;
        }

        #endregion
    }
}
