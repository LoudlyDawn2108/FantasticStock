using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace FantasticStock.Services.Admin
{
    public class SqlDatabaseService : IDatabaseService
    {
        private readonly string _connectionString;
        private readonly string _masterConnectionString;
        
        public SqlDatabaseService()
        {
            _connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=FantasticStock;Integrated Security=True;";
            _masterConnectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=master;Integrated Security=True;";

        }

        public string ConnectionString => _connectionString;

        public SqlConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();
            return connection;
        }

        public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }

                var dataTable = new DataTable();
                using (var adapter = new SqlDataAdapter(command))
                {
                    adapter.Fill(dataTable);
                }
                return dataTable;
            }
        }

        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteScalar();
            }
        }

        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (var connection = CreateConnection())
            using (var command = new SqlCommand(query, connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteNonQuery();
            }
        }

        public void ExecuteInTransaction(Action<SqlConnection, SqlTransaction> action)
        {
            using (var connection = CreateConnection())
            {
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        action(connection, transaction);
                        transaction.Commit();
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}