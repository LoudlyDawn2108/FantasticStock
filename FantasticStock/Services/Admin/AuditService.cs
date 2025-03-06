using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using FantasticStock.Common;
using FantasticStock.Models;

namespace FantasticStock.Services
{
    public class AuditService : IAuditService
    {
        private readonly IDatabaseService _databaseService;

        public AuditService()
        {
            _databaseService = ServiceLocator.GetService<IDatabaseService>();
        }

        public List<AuditLogEntry> GetAuditLogs(DateTime? startDate = null, DateTime? endDate = null, 
                                               string eventType = null, int? userId = null, 
                                               string tableName = null, int? severityLevel = null)
        {
            var logs = new List<AuditLogEntry>();
            
            string query = @"
                SELECT a.*, u.Username
                FROM AuditLog a
                LEFT JOIN Users u ON a.UserID = u.UserID
                WHERE 1=1";
                
            var parameters = new List<SqlParameter>();
            
            if (startDate.HasValue)
            {
                query += " AND a.Timestamp >= @StartDate";
                parameters.Add(new SqlParameter("@StartDate", startDate.Value));
            }
            
            if (endDate.HasValue)
            {
                query += " AND a.Timestamp <= @EndDate";
                parameters.Add(new SqlParameter("@EndDate", endDate.Value));
            }
            
            if (!string.IsNullOrEmpty(eventType))
            {
                query += " AND a.EventType = @EventType";
                parameters.Add(new SqlParameter("@EventType", eventType));
            }
            
            if (userId.HasValue)
            {
                query += " AND a.UserID = @UserID";
                parameters.Add(new SqlParameter("@UserID", userId.Value));
            }
            
            if (!string.IsNullOrEmpty(tableName))
            {
                query += " AND a.TableName = @TableName";
                parameters.Add(new SqlParameter("@TableName", tableName));
            }
            
            if (severityLevel.HasValue)
            {
                query += " AND a.SeverityLevel = @SeverityLevel";
                parameters.Add(new SqlParameter("@SeverityLevel", severityLevel.Value));
            }
            
            query += " ORDER BY a.Timestamp DESC";
            
            DataTable dataTable = _databaseService.ExecuteQuery(query, parameters.ToArray());
            
            foreach (DataRow row in dataTable.Rows)
            {
                logs.Add(MapAuditLogEntry(row));
            }
            
            return logs;
        }

        public void LogEvent(int? userId, string eventType, string tableName, string recordId, 
                           string oldValues, string newValues, int severityLevel = 1)
        {
            try
            {
                string query = @"
                    INSERT INTO AuditLog (UserID, EventType, TableName, RecordID, OldValues, 
                                         NewValues, IPAddress, SeverityLevel, Timestamp)
                    VALUES (@UserID, @EventType, @TableName, @RecordID, @OldValues, 
                           @NewValues, @IPAddress, @SeverityLevel, @Timestamp)";

                                var parameters = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userId ?? (object)DBNull.Value),
                    new SqlParameter("@EventType", eventType),
                    new SqlParameter("@TableName", tableName),
                    new SqlParameter("@RecordID", recordId ?? (object)DBNull.Value),
                    new SqlParameter("@OldValues", oldValues ?? (object)DBNull.Value),
                    new SqlParameter("@NewValues", newValues ?? (object)DBNull.Value),
                    new SqlParameter("@IPAddress", GetClientIPAddress() ?? (object)DBNull.Value),
                    new SqlParameter("@SeverityLevel", severityLevel),
                    new SqlParameter("@Timestamp", DateTime.Parse("2025-03-02 16:05:52"))
                };

                _databaseService.ExecuteNonQuery(query, parameters);
            }
            catch (Exception ex)
            {
                // Log error but don't throw (to avoid circular dependency issues)
                Console.WriteLine($"Error logging audit event: {ex.Message}");
            }
        }

        public bool ClearAuditLogs(DateTime? olderThan = null)
        {
            try
            {
                string query;
                SqlParameter[] parameters = null;
                
                if (olderThan.HasValue)
                {
                    query = "DELETE FROM AuditLog WHERE Timestamp < @OlderThan";
                    parameters = new[] { new SqlParameter("@OlderThan", olderThan.Value) };
                }
                else
                {
                    query = "DELETE FROM AuditLog";
                }

                _databaseService.ExecuteNonQuery(query, parameters);
                
                // Log the action (directly to database to avoid recursion)
                string auditQuery = @"
                    INSERT INTO AuditLog (UserID, EventType, TableName, RecordID, NewValues, IPAddress, SeverityLevel, Timestamp)
                    VALUES (@UserID, 'ClearAuditLogs', 'AuditLog', 'All', @Message, @IPAddress, 2, @Timestamp)";

                string message = olderThan.HasValue
                    ? $"Audit logs older than {olderThan.Value.ToString("yyyy-MM-dd HH:mm:ss")} have been cleared"
                    : "All audit logs have been cleared";

                var auditParams = new SqlParameter[]
                {
                    new SqlParameter("@UserID", CurrentUser.UserID),
                    new SqlParameter("@Message", message),
                    new SqlParameter("@IPAddress", GetClientIPAddress() ?? (object)DBNull.Value),
                    new SqlParameter("@Timestamp", DateTime.Parse("2025-03-02 16:05:52"))
                };

                _databaseService.ExecuteNonQuery(auditQuery, auditParams);
                
                return true;
            }
            catch (Exception ex)
            {
                // Log to error log directly to avoid circular dependency
                string errorQuery = @"
                    INSERT INTO ErrorLog (ErrorModule, ErrorMessage, SeverityLevel, UserID, IPAddress, Timestamp)
                    VALUES ('Audit', @Message, 3, @UserID, @IPAddress, @Timestamp)";

                var errorParams = new SqlParameter[]
                {
                    new SqlParameter("@Message", $"Failed to clear audit logs: {ex.Message}"),
                    new SqlParameter("@UserID", CurrentUser.UserID),
                    new SqlParameter("@IPAddress", GetClientIPAddress() ?? (object)DBNull.Value),
                    new SqlParameter("@Timestamp", DateTime.Parse("2025-03-02 16:05:52"))
                };

                _databaseService.ExecuteNonQuery(errorQuery, errorParams);
                
                return false;
            }
        }

        private AuditLogEntry MapAuditLogEntry(DataRow row)
        {
            return new AuditLogEntry
            {
                AuditID = Convert.ToInt32(row["AuditID"]),
                UserID = row["UserID"] != DBNull.Value ? (int?)Convert.ToInt32(row["UserID"]) : null,
                Username = row["Username"] != DBNull.Value ? row["Username"].ToString() : null,
                EventType = row["EventType"].ToString(),
                TableName = row["TableName"].ToString(),
                RecordID = row["RecordID"] != DBNull.Value ? row["RecordID"].ToString() : null,
                OldValues = row["OldValues"] != DBNull.Value ? row["OldValues"].ToString() : null,
                NewValues = row["NewValues"] != DBNull.Value ? row["NewValues"].ToString() : null,
                IPAddress = row["IPAddress"] != DBNull.Value ? row["IPAddress"].ToString() : null,
                SeverityLevel = Convert.ToInt32(row["SeverityLevel"]),
                Timestamp = Convert.ToDateTime(row["Timestamp"])
            };
        }

        private string GetClientIPAddress()
        {
            try
            {
                string hostName = Dns.GetHostName();
                IPAddress[] addresses = Dns.GetHostAddresses(hostName);
                
                foreach (IPAddress address in addresses)
                {
                    if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    {
                        return address.ToString();
                    }
                }
                
                return "127.0.0.1";
            }
            catch
            {
                return null;
            }
        }
    }
}