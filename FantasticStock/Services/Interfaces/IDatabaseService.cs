using System;
using System.Data;
using System.Data.SqlClient;

namespace FantasticStock.Services
{
    public interface IDatabaseService
    {
        string ConnectionString { get; }
        SqlConnection CreateConnection();
        DataTable ExecuteQuery(string query, params SqlParameter[] parameters);
        object ExecuteScalar(string query, params SqlParameter[] parameters);
        int ExecuteNonQuery(string query, params SqlParameter[] parameters);
        void ExecuteInTransaction(Action<SqlConnection, SqlTransaction> action);
    }
}