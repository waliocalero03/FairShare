using Dapper;
using FairShare.Core;
using FairShare.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FairShare.Data.Repositories
{
    public class ExpenseSplitRepository : IExpenseSplitRepository
    {
        #region Variables
        private readonly IDbConnection _dbConnection;
        private const int RowsNotAffected = 0;

        #endregion

        #region Constructor

        public ExpenseSplitRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        #endregion

        #region Public Methods

        public ExpenseSplit? GetExpenseSplitById(int id)
        {
            string sql = "SELECT * FROM ExpenseSplits WHERE Id = @Id;";

            return _dbConnection.QueryFirstOrDefault<ExpenseSplit>(sql, new { Id = id });
        }

        public IEnumerable<ExpenseSplit> GetSplitsByExpenseId(int expenseId)
        {
            string sql = "SELECT * FROM ExpenseSplits WHERE ExpenseId = @ExpenseId ORDER BY ParticipantId;";

            return _dbConnection.Query<ExpenseSplit>(sql, new { ExpenseId = expenseId });
        }

        public IEnumerable<ExpenseSplit> GetSplitsByParticipantId(int participantId)
        {
            string sql = "SELECT * FROM ExpenseSplits WHERE ParticipantId = @ParticipantId ORDER BY ExpenseId;";

            return _dbConnection.Query<ExpenseSplit>(sql, new { ParticipantId = participantId });
        }

        public bool CreateExpenseSplit(ExpenseSplit expenseSplit)
        {
            string sql = @"
                INSERT INTO ExpenseSplits (ExpenseId, ParticipantId, OwedAmount) 
                VALUES (@ExpenseId, @ParticipantId, @OwedAmount);";

            int rowsAffected = _dbConnection.Execute(sql, new
            {
                expenseSplit.ExpenseId,
                expenseSplit.ParticipantId,
                expenseSplit.OwedAmount
            });

            return rowsAffected != RowsNotAffected;
        }

        public bool UpdateExpenseSplit(ExpenseSplit expenseSplit)
        {
            string sql = @"
                UPDATE ExpenseSplits 
                SET ExpenseId = @ExpenseId, 
                    ParticipantId = @ParticipantId, 
                    OwedAmount = @OwedAmount 
                WHERE Id = @Id;";

            int rowsAffected = _dbConnection.Execute(sql, new
            {
                expenseSplit.Id,
                expenseSplit.ExpenseId,
                expenseSplit.ParticipantId,
                expenseSplit.OwedAmount
            });

            return rowsAffected != RowsNotAffected;
        }

        public bool DeleteExpenseSplit(int id)
        {
            string sql = "DELETE FROM ExpenseSplits WHERE Id = @Id;";

            int rowsAffected = _dbConnection.Execute(sql, new { Id = id });

            return rowsAffected != RowsNotAffected;
        }

        #endregion
    }
}
