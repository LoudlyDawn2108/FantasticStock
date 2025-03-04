using System;
using System.Collections.Generic;
using FantasticStock.Models;

namespace FantasticStock.Services
{
    public interface IAuditService
    {
        List<AuditLogEntry> GetAuditLogs(DateTime? startDate = null, DateTime? endDate = null, 
                                        string eventType = null, int? userId = null, 
                                        string tableName = null, int? severityLevel = null);
        void LogEvent(int? userId, string eventType, string tableName, string recordId, 
                      string oldValues, string newValues, int severityLevel = 1);
        bool ClearAuditLogs(DateTime? olderThan = null);
    }
}