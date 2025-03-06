using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Management;
using System.Net;
using FantasticStock.Common;
using FantasticStock.Models;

namespace FantasticStock.Services
{
    public class MonitoringService : IMonitoringService
    {
        private readonly IDatabaseService _databaseService;

        public MonitoringService()
        {
            _databaseService = ServiceLocator.GetService<IDatabaseService>();
        }

        public List<ErrorLogEntry> GetErrorLogs(DateTime? startDate = null, DateTime? endDate = null, int? severityLevel = null, string module = null)
        {
            var logs = new List<ErrorLogEntry>();
            
            string query = @"
                SELECT e.*, u.Username
                FROM ErrorLog e
                LEFT JOIN Users u ON e.UserID = u.UserID
                WHERE 1=1";
                
            var parameters = new List<SqlParameter>();
            
            if (startDate.HasValue)
            {
                query += " AND e.Timestamp >= @StartDate";
                parameters.Add(new SqlParameter("@StartDate", startDate.Value));
            }
            
            if (endDate.HasValue)
            {
                query += " AND e.Timestamp <= @EndDate";
                parameters.Add(new SqlParameter("@EndDate", endDate.Value));
            }
            
            if (severityLevel.HasValue)
            {
                query += " AND e.SeverityLevel = @SeverityLevel";
                parameters.Add(new SqlParameter("@SeverityLevel", severityLevel.Value));
            }
            
            if (!string.IsNullOrEmpty(module))
            {
                query += " AND e.ErrorModule = @Module";
                parameters.Add(new SqlParameter("@Module", module));
            }
            
            query += " ORDER BY e.Timestamp DESC";
            
            DataTable dataTable = _databaseService.ExecuteQuery(query, parameters.ToArray());
            
            foreach (DataRow row in dataTable.Rows)
            {
                logs.Add(new ErrorLogEntry
                {
                    ErrorID = Convert.ToInt32(row["ErrorID"]),
                    ErrorModule = row["ErrorModule"].ToString(),
                    ErrorMessage = row["ErrorMessage"].ToString(),
                    StackTrace = row["StackTrace"] != DBNull.Value ? row["StackTrace"].ToString() : null,
                    SeverityLevel = Convert.ToInt32(row["SeverityLevel"]),
                    UserID = row["UserID"] != DBNull.Value ? (int?)Convert.ToInt32(row["UserID"]) : null,
                    Username = row["Username"] != DBNull.Value ? row["Username"].ToString() : null,
                    IPAddress = row["IPAddress"] != DBNull.Value ? row["IPAddress"].ToString() : null,
                    Timestamp = Convert.ToDateTime(row["Timestamp"])
                });
            }
            
            return logs;
        }

                public void LogError(string module, string message, string stackTrace, int severityLevel)
        {
            string query = @"
                INSERT INTO ErrorLog (ErrorModule, ErrorMessage, StackTrace, SeverityLevel, UserID, IPAddress, Timestamp)
                VALUES (@ErrorModule, @ErrorMessage, @StackTrace, @SeverityLevel, @UserID, @IPAddress, @Timestamp)";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ErrorModule", module),
                new SqlParameter("@ErrorMessage", message),
                new SqlParameter("@StackTrace", stackTrace ?? (object)DBNull.Value),
                new SqlParameter("@SeverityLevel", severityLevel),
                new SqlParameter("@UserID", CurrentUser.UserID != 0 ? (object)CurrentUser.UserID : DBNull.Value),
                new SqlParameter("@IPAddress", GetClientIPAddress() ?? (object)DBNull.Value),
                new SqlParameter("@Timestamp", DateTime.Parse("2025-03-02 16:04:21"))
            };

            _databaseService.ExecuteNonQuery(query, parameters);
        }

        public bool ClearErrorLogs()
        {
            try
            {
                string query = "DELETE FROM ErrorLog";
                _databaseService.ExecuteNonQuery(query);
                
                // Log the action
                var auditService = ServiceLocator.GetService<IAuditService>();
                auditService.LogEvent(
                    CurrentUser.UserID,
                    "ClearErrorLogs",
                    "ErrorLog",
                    "All",
                    null,
                    "All error logs have been cleared"
                );
                
                return true;
            }
            catch (Exception ex)
            {
                LogError("System", $"Failed to clear error logs: {ex.Message}", ex.StackTrace, 3);
                return false;
            }
        }

        public SystemResources GetCurrentResources()
        {
            var resources = new SystemResources
            {
                Timestamp = DateTime.Parse("2025-03-02 16:04:21"),
                ServerName = Environment.MachineName
            };

            try
            {
                // CPU Usage
                using (var cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
                {
                    cpuCounter.NextValue(); // First call will return 0
                    System.Threading.Thread.Sleep(100); // Wait a bit
                    resources.CpuUsage = cpuCounter.NextValue();
                }

                // Memory Usage
                using (var memCounter = new PerformanceCounter("Memory", "Available MBytes"))
                {
                    float availableMB = memCounter.NextValue();
                    var computerInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
                    ulong totalMemoryBytes = computerInfo.TotalPhysicalMemory;
                    float totalMemoryMB = totalMemoryBytes / 1024f / 1024f;
                    resources.MemoryUsage = 100 * (1 - (availableMB / totalMemoryMB));
                }

                // Disk Usage
                using (var diskCounter = new PerformanceCounter("LogicalDisk", "% Free Space", "C:"))
                {
                    resources.DiskUsage = 100 - diskCounter.NextValue();
                }

                // Database Size
                string query = "SELECT SUM(size * 8 / 1024.0) AS SizeMB FROM sys.database_files";
                resources.DatabaseSize = Convert.ToDouble(_databaseService.ExecuteScalar(query));

                // Active Connections
                query = "SELECT COUNT(*) FROM sys.dm_exec_connections";
                resources.ActiveConnections = Convert.ToInt32(_databaseService.ExecuteScalar(query));

                // Save to database
                StoreSystemResources(resources);

                return resources;
            }
            catch (Exception ex)
            {
                LogError("System", $"Failed to get system resources: {ex.Message}", ex.StackTrace, 2);
                
                // Return partial data
                return resources;
            }
        }

        public List<SystemResources> GetResourceHistory(string resourceType, DateTime startDate, DateTime endDate)
        {
            var resources = new List<SystemResources>();
            
            string query = @"
                SELECT *
                FROM SystemResources
                WHERE ResourceType = @ResourceType
                AND Timestamp BETWEEN @StartDate AND @EndDate
                ORDER BY Timestamp";
                
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ResourceType", resourceType),
                new SqlParameter("@StartDate", startDate),
                new SqlParameter("@EndDate", endDate)
            };
            
            DataTable dataTable = _databaseService.ExecuteQuery(query, parameters);
            
            foreach (DataRow row in dataTable.Rows)
            {
                resources.Add(new SystemResources
                {
                    ResourceID = Convert.ToInt32(row["ResourceID"]),
                    ResourceType = row["ResourceType"].ToString(),
                    ResourceValue = Convert.ToDouble(row["ResourceValue"]),
                    ServerName = row["ServerName"].ToString(),
                    Timestamp = Convert.ToDateTime(row["Timestamp"])
                });
            }
            
            return resources;
        }

        public void LogSystemResources()
        {
            try
            {
                var resources = GetCurrentResources();
                
                // Already stored in GetCurrentResources method
            }
            catch (Exception ex)
            {
                LogError("System", $"Failed to log system resources: {ex.Message}", ex.StackTrace, 2);
            }
        }

        public bool OptimizeDatabase()
        {
            try
            {
                // DBCC CHECKDB
                string query = "DBCC CHECKDB WITH NO_INFOMSGS";
                _databaseService.ExecuteNonQuery(query);
                
                // Update Statistics
                query = "EXEC sp_updatestats";
                _databaseService.ExecuteNonQuery(query);
                
                // Log the action
                var auditService = ServiceLocator.GetService<IAuditService>();
                auditService.LogEvent(
                    CurrentUser.UserID,
                    "DatabaseOptimize",
                    "System",
                    "Database",
                    null,
                    "Database optimization completed"
                );
                
                return true;
            }
            catch (Exception ex)
            {
                LogError("System", $"Failed to optimize database: {ex.Message}", ex.StackTrace, 3);
                return false;
            }
        }

        public bool RebuildIndexes()
        {
            try
            {
                // Get list of user tables
                string query = @"
                    SELECT t.name AS TableName
                    FROM sys.tables t
                    INNER JOIN sys.schemas s ON t.schema_id = s.schema_id
                    WHERE s.name = 'dbo'";
                
                DataTable tables = _databaseService.ExecuteQuery(query);
                
                foreach (DataRow row in tables.Rows)
                {
                    string tableName = row["TableName"].ToString();
                    
                    // Rebuild indexes for this table
                    query = $"ALTER INDEX ALL ON {tableName} REBUILD WITH (ONLINE = OFF)";
                    _databaseService.ExecuteNonQuery(query);
                }
                
                // Log the action
                var auditService = ServiceLocator.GetService<IAuditService>();
                auditService.LogEvent(
                    CurrentUser.UserID,
                    "IndexRebuild",
                    "System",
                    "Indexes",
                    null,
                    "Index rebuild completed"
                );
                
                return true;
            }
            catch (Exception ex)
            {
                LogError("System", $"Failed to rebuild indexes: {ex.Message}", ex.StackTrace, 3);
                return false;
            }
        }

        public bool ClearCache()
        {
            try
            {
                string query = "DBCC FREEPROCCACHE; DBCC DROPCLEANBUFFERS;";
                _databaseService.ExecuteNonQuery(query);
                
                // Log the action
                var auditService = ServiceLocator.GetService<IAuditService>();
                auditService.LogEvent(
                    CurrentUser.UserID,
                    "ClearCache",
                    "System",
                    "Cache",
                    null,
                    "System cache cleared"
                );
                
                return true;
            }
            catch (Exception ex)
            {
                LogError("System", $"Failed to clear cache: {ex.Message}", ex.StackTrace, 3);
                return false;
            }
        }

        private void StoreSystemResources(SystemResources resources)
        {
            try
            {
                // Store CPU usage
                StoreResourceValue("CPU", resources.CpuUsage);
                
                // Store memory usage
                StoreResourceValue("Memory", resources.MemoryUsage);
                
                // Store disk usage
                StoreResourceValue("Disk", resources.DiskUsage);
                
                // Store database size
                StoreResourceValue("DatabaseSize", resources.DatabaseSize);
                
                // Store active connections
                StoreResourceValue("Connections", resources.ActiveConnections);
            }
            catch (Exception ex)
            {
                LogError("System", $"Failed to store system resources: {ex.Message}", ex.StackTrace, 2);
            }
        }

        private void StoreResourceValue(string resourceType, double value)
        {
            string query = @"
                INSERT INTO SystemResources (ResourceType, ResourceValue, ServerName, Timestamp)
                VALUES (@ResourceType, @ResourceValue, @ServerName, @Timestamp)";
                
            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ResourceType", resourceType),
                new SqlParameter("@ResourceValue", value),
                new SqlParameter("@ServerName", Environment.MachineName),
                new SqlParameter("@Timestamp", DateTime.Parse("2025-03-02 16:04:21"))
            };
            
            _databaseService.ExecuteNonQuery(query, parameters);
        }

        private string GetClientIPAddress()
        {
            try
            {
                // In a WinForms app, this won't be a web client, so we'll use the local machine IP
                string hostName = Dns.GetHostName();
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);
                
                // Get the first IPv4 address
                foreach (IPAddress address in addresses)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return address.ToString();
                    }
                }
                
                return "127.0.0.1"; // Localhost if no other address found
            }
            catch
            {
                return null;
            }
        }
    }

    public class SystemResources
    {
        public int ResourceID { get; set; }
        public string ResourceType { get; set; }
        public double ResourceValue { get; set; }
        public string ServerName { get; set; }
        public DateTime Timestamp { get; set; }
        
        // Extended properties not directly in database
        public double CpuUsage { get; set; }
        public double MemoryUsage { get; set; }
        public double DiskUsage { get; set; }
        public double DatabaseSize { get; set; }
        public int ActiveConnections { get; set; }
    }
}