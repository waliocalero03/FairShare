using Dapper;
using FairShare.Data.DTOs.ExpenseSplit;
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

        #endregion

        #region Constructor

        public ExpenseSplitRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        #endregion

        #region Methods

        public IEnumerable<ExpenseSplitDetailDTO> GetExpenseSplitByIdGroup(int groupId)
        {
            string sql = @"
                SELECT e.Id, e.Description, e.TotalAmount, e.Date, p.Name AS PayerName 
                FROM Expenses e
                INNER JOIN Participants p ON e.PayerId = p.Id
                WHERE e.GroupId = @GroupId 
                ORDER BY e.Date DESC;";

            return _dbConnection.Query<ExpenseSplitDetailDTO>(sql, new { GroupId = groupId });
        }

        #endregion
    }
}
