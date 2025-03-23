using System;
using System.Collections.Generic;
using FantasticStock.Models;

namespace FantasticStock.Services.Admin
{
    public interface IMonitoringService
    {
        List<ErrorLogEntry> GetErrorLogs(DateTime? startDate = null, DateTime? endDate = null, int? severityLevel = null, string module = null);
        void LogError(string module, string message, string stackTrace, int severityLevel);
        bool ClearErrorLogs();
        
        SystemResources GetCurrentResources();
        List<SystemResources> GetResourceHistory(string resourceType, DateTime startDate, DateTime endDate);
        void LogSystemResources();
        
        bool OptimizeDatabase();
        bool RebuildIndexes();
        bool ClearCache();
    }
}