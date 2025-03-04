using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace AdminDomain.Services
{
    public class SqlDatabaseService : IDatabaseService
    {
        private readonly string _connectionString;
        private readonly string _masterConnectionString;
        
        public SqlDatabaseService()
        {
            _connectionString = "Data Source=.;Initial Catalog=AdminDomain;Integrated Security=True;";
            _masterConnectionString = "Data Source=.;Initial Catalog=master;Integrated Security=True;";
        }

        public string ConnectionString => _connectionString;

        public void EnsureDatabaseExists()
        {
            // Check if database exists
            bool dbExists = false;
            
            using (var connection = new SqlConnection(_masterConnectionString))
            {
                connection.Open();
                using (var command = new SqlCommand("SELECT COUNT(*) FROM sys.databases WHERE name = 'AdminDomain'", connection))
                {
                    dbExists = (int)command.ExecuteScalar() > 0;
                }
            }

            if (!dbExists)
            {
                // Create database and schema
                using (var connection = new SqlConnection(_masterConnectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand("CREATE DATABASE AdminDomain", connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                // Execute database schema script
                ExecuteSchemaScript();
            }
        }

        private void ExecuteSchemaScript()
        {
            // In a real application, you would load this from a file
            // For this example, we'll use a hardcoded simplified schema
            string schemaScript = @"
USE AdminDomain;

-- User Management Tables
CREATE TABLE Roles (
    RoleID INT PRIMARY KEY IDENTITY(1,1),
    RoleName NVARCHAR(50) NOT NULL UNIQUE,
    Description NVARCHAR(255),
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE()
);

CREATE TABLE Users (
    UserID INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL UNIQUE,
    PasswordHash NVARCHAR(256) NOT NULL,
    Salt NVARCHAR(128) NOT NULL,
    DisplayName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL,
    Phone NVARCHAR(20),
    RoleID INT NOT NULL,
    Status NVARCHAR(20) NOT NULL DEFAULT 'Active',
    TwoFactorEnabled BIT NOT NULL DEFAULT 0,
    AccountExpiry DATETIME NULL,
    LastLogin DATETIME NULL,
    CreatedDate DATETIME DEFAULT GETDATE(),
    ModifiedDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Users_Role FOREIGN KEY (RoleID) REFERENCES Roles(RoleID)
);

-- Insert default admin role and user
INSERT INTO Roles (RoleName, Description)
VALUES ('Admin', 'System Administrator with full access');

INSERT INTO Users (Username, PasswordHash, Salt, DisplayName, Email, RoleID)
VALUES ('admin', 'f07a147a4e14f6027c9d248a379c2212d7cd4fb5e34908de6c732978de4e239c', 'adminSalt123', 'System Administrator', 'admin@example.com', 1);
";

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (var command = new SqlCommand(schemaScript, connection))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

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