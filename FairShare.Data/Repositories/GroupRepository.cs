using Dapper;
using FairShare.Core;
using FairShare.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace FairShare.Data.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        #region Variables
        private readonly IDbConnection _dbConnection;
        private const int RowsNotAffected = 0;

        #endregion

        #region Constructor

        public GroupRepository(IDbConnection dbConnection)
        {
            // La conexión de PostgreSQL se inyecta automáticamente aquí
            _dbConnection = dbConnection;
        }

        #endregion

        #region Public Methods

        public Group? GetGroupById(int idGroup)
        {
            string sql = "SELECT * FROM Groups WHERE Id = @Id;";

            // QueryFirstOrDefault devuelve el primer resultado o 'null' si no existe
            return _dbConnection.QueryFirstOrDefault<Group>(sql, new { Id = idGroup });
        }

        public bool CreateGroup(Group group)
        {
            // Ejecutamos el INSERT. Execute devuelve el número de filas afectadas.
            string sql = @"
                INSERT INTO Groups (Name, Code) 
                VALUES (@Name, @Code);";

            return ExecuteQuery(sql, group);
        }

        public bool UpdateGroup(Group group)
        {
            string sql = @"
                UPDATE Groups 
                SET Name = @Name, Code = @Code 
                WHERE Id = @Id;";

            return ExecuteQuery(sql, group);
        }

        public bool DeleteGroup(int idGroup)
        {
            string sql = "DELETE FROM Groups WHERE Id = @Id;";

            return ExecuteQuery(sql, new { Id = idGroup });
        }

        public Group? GetGroupByCode(string code)
        {
            string sql = "SELECT * FROM Groups WHERE Code = @Code;";

            return _dbConnection.QueryFirstOrDefault<Group>(sql, new { Code = code });
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
