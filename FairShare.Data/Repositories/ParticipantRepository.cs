using Dapper;
using FairShare.Core;
using FairShare.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FairShare.Data.Repositories
{
    public class ParticipantRepository : IParticipantRepository
    {
        #region Variables
        private readonly IDbConnection _dbConnection;
        private const int RowsNotAffected = 0;
        #endregion

        #region Constructor
        public ParticipantRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        #endregion

        #region Public Methods

        public Participant? GetParticipantById(int id)
        {
            string sql = "SELECT * FROM Participants WHERE Id = @Id;";
            return _dbConnection.QueryFirstOrDefault<Participant>(sql, new { Id = id });
        }

        public IEnumerable<Participant> GetParticipantsByGroupId(int groupId)
        {
            string sql = "SELECT * FROM Participants WHERE GroupId = @GroupId;";

            // Query devuelve una colección (IEnumerable)
            return _dbConnection.Query<Participant>(sql, new { GroupId = groupId });
        }

        public bool CreateParticipant(Participant participant)
        {
            string sql = @"
                INSERT INTO Participants (GroupId, Name) 
                VALUES (@GroupId, @Name);";

            return ExecuteQuery(sql, participant);
        }

        public bool UpdateParticipant(Participant participant)
        {
            // Solo actualizamos el nombre. Cambiar a una persona de grupo no suele tener 
            // sentido en el dominio de esta app.
            string sql = @"
                UPDATE Participants 
                SET Name = @Name 
                WHERE Id = @Id;";

            return ExecuteQuery(sql, participant);
        }

        public bool DeleteParticipant(int id)
        {
            string sql = "DELETE FROM Participants WHERE Id = @Id;";
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
