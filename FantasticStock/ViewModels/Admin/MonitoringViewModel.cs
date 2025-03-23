using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using FantasticStock.Common;
using FantasticStock.Models;
using FantasticStock.Services.Admin;

namespace FantasticStock.ViewModels
{
    public class MonitoringViewModel : ViewModelBase
    {
        private readonly IMonitoringService _monitoringService;
        private readonly IAuditService _auditService;

        private BindingList<ErrorLogEntry> _errorLogs;
        private BindingList<AuditLogEntry> _auditLogs;
        private SystemResources _currentResources;
        private DateTime _startDate;
        private DateTime _endDate;
        private string _errorModuleFilter;
        private int? _errorSeverityFilter;
        private string _auditTypeFilter;
        private int? _auditUserFilter;
        private string _auditTableFilter;

        public MonitoringViewModel()
        {
            _monitoringService = ServiceLocator.GetService<IMonitoringService>();
            _auditService = ServiceLocator.GetService<IAuditService>();
            
            // Initialize commands
            RefreshResourcesCommand = new RelayCommand(RefreshResources);
            OptimizeDatabaseCommand = new RelayCommand(OptimizeDatabase);
            RebuildIndexesCommand = new RelayCommand(RebuildIndexes);
            ClearCacheCommand = new RelayCommand(ClearCache);
            FilterErrorLogsCommand = new RelayCommand(FilterErrorLogs);
            ClearErrorLogsCommand = new RelayCommand(ClearErrorLogs);
            FilterAuditLogsCommand = new RelayCommand(FilterAuditLogs);
            ClearAuditLogsCommand = new RelayCommand(ClearAuditLogs);
            ExportLogsCommand = new RelayCommand(ExportLogs);
            
            // Initialize filter dates to last 7 days
            _startDate = DateTime.Parse("2025-03-02 16:14:03").AddDays(-7);
            _endDate = DateTime.Parse("2025-03-02 16:14:03");
            
            // Load initial data
            LoadData();
        }

        #region Properties

        public BindingList<ErrorLogEntry> ErrorLogs
        {
            get => _errorLogs;
            set => SetProperty(ref _errorLogs, value);
        }

        public BindingList<AuditLogEntry> AuditLogs
        {
            get => _auditLogs;
            set => SetProperty(ref _auditLogs, value);
        }

        public SystemResources CurrentResources
        {
            get => _currentResources;
            set => SetProperty(ref _currentResources, value);
        }

        public DateTime StartDate
        {
            get => _startDate;
            set => SetProperty(ref _startDate, value);
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => SetProperty(ref _endDate, value);
        }

        public string ErrorModuleFilter
        {
            get => _errorModuleFilter;
            set => SetProperty(ref _errorModuleFilter, value);
        }

        public int? ErrorSeverityFilter
        {
            get => _errorSeverityFilter;
            set => SetProperty(ref _errorSeverityFilter, value);
        }

        public string AuditTypeFilter
        {
            get => _auditTypeFilter;
            set => SetProperty(ref _auditTypeFilter, value);
        }

        public int? AuditUserFilter
        {
            get => _auditUserFilter;
            set => SetProperty(ref _auditUserFilter, value);
        }

        public string AuditTableFilter
        {
            get => _auditTableFilter;
            set => SetProperty(ref _auditTableFilter, value);
        }

        #endregion

        #region Commands

        public ICommand RefreshResourcesCommand { get; }
        public ICommand OptimizeDatabaseCommand { get; }
        public ICommand RebuildIndexesCommand { get; }
        public ICommand ClearCacheCommand { get; }
        public ICommand FilterErrorLogsCommand { get; }
        public ICommand ClearErrorLogsCommand { get; }
        public ICommand FilterAuditLogsCommand { get; }
        public ICommand ClearAuditLogsCommand { get; }
        public ICommand ExportLogsCommand { get; }

        #endregion

        #region Command Implementations

        private void RefreshResources(object parameter)
        {
            try
            {
                CurrentResources = _monitoringService.GetCurrentResources();
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to refresh system resources: {ex.Message}", "Error");
            }
        }

        private void OptimizeDatabase(object parameter)
        {
            if (MessageService.ShowConfirmation(
                "Are you sure you want to optimize the database? This might take some time and could affect system performance temporarily.",
                "Confirm Optimization"))
            {
                try
                {
                    bool success = _monitoringService.OptimizeDatabase();
                    
                    if (success)
                    {
                        MessageService.ShowInformation("Database optimization completed successfully.", "Success");
                    }
                    else
                    {
                        MessageService.ShowError("Database optimization failed. Check the error logs for details.", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowError($"Database optimization failed: {ex.Message}", "Error");
                }
            }
        }

        private void RebuildIndexes(object parameter)
        {
            if (MessageService.ShowConfirmation(
                "Are you sure you want to rebuild all database indexes? This might take some time and could affect system performance temporarily.",
                "Confirm Index Rebuild"))
            {
                try
                {
                    bool success = _monitoringService.RebuildIndexes();
                    
                    if (success)
                    {
                        MessageService.ShowInformation("Index rebuild completed successfully.", "Success");
                    }
                    else
                    {
                        MessageService.ShowError("Index rebuild failed. Check the error logs for details.", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowError($"Index rebuild failed: {ex.Message}", "Error");
                }
            }
        }

        private void ClearCache(object parameter)
        {
            if (MessageService.ShowConfirmation(
                "Are you sure you want to clear the system cache? This might temporarily affect system performance.",
                "Confirm Clear Cache"))
            {
                try
                {
                    bool success = _monitoringService.ClearCache();
                    
                    if (success)
                    {
                        MessageService.ShowInformation("Cache cleared successfully.", "Success");
                    }
                    else
                    {
                        MessageService.ShowError("Failed to clear cache. Check the error logs for details.", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowError($"Clear cache failed: {ex.Message}", "Error");
                }
            }
        }

        private void FilterErrorLogs(object parameter)
        {
            LoadErrorLogs();
        }

        private void ClearErrorLogs(object parameter)
        {
            if (MessageService.ShowConfirmation(
                "Are you sure you want to clear all error logs? This action cannot be undone.",
                "Confirm Clear Error Logs"))
            {
                try
                {
                    bool success = _monitoringService.ClearErrorLogs();
                    
                    if (success)
                    {
                        MessageService.ShowInformation("Error logs cleared successfully.", "Success");
                        LoadErrorLogs();
                    }
                    else
                    {
                        MessageService.ShowError("Failed to clear error logs.", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowError($"Clear error logs failed: {ex.Message}", "Error");
                }
            }
        }

        private void FilterAuditLogs(object parameter)
        {
            LoadAuditLogs();
        }

        private void ClearAuditLogs(object parameter)
        {
            if (MessageService.ShowConfirmation(
                "Are you sure you want to clear all audit logs? This action cannot be undone.",
                "Confirm Clear Audit Logs"))
            {
                try
                {
                    bool success = _auditService.ClearAuditLogs();
                    
                    if (success)
                    {
                        MessageService.ShowInformation("Audit logs cleared successfully.", "Success");
                        LoadAuditLogs();
                    }
                    else
                    {
                        MessageService.ShowError("Failed to clear audit logs.", "Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowError($"Clear audit logs failed: {ex.Message}", "Error");
                }
            }
        }

        private void ExportLogs(object parameter)
        {
            // In a real application, this would save logs to a file
            MessageService.ShowInformation("Export functionality would be implemented here.", "Export");
        }

        #endregion

        #region Helper Methods

        private void LoadData()
        {
            try
            {
                // Load system resources
                CurrentResources = _monitoringService.GetCurrentResources();
                
                // Load logs
                LoadErrorLogs();
                LoadAuditLogs();
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to load monitoring data: {ex.Message}", "Error");
            }
        }

        private void LoadErrorLogs()
        {
            try
            {
                var logs = _monitoringService.GetErrorLogs(
                    StartDate,
                    EndDate,
                    ErrorSeverityFilter,
                    !string.IsNullOrWhiteSpace(ErrorModuleFilter) ? ErrorModuleFilter : null);
                    
                ErrorLogs = new BindingList<ErrorLogEntry>(logs);
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to load error logs: {ex.Message}", "Error");
            }
        }

        private void LoadAuditLogs()
        {
            try
            {
                var logs = _auditService.GetAuditLogs(
                    StartDate,
                    EndDate,
                    !string.IsNullOrWhiteSpace(AuditTypeFilter) ? AuditTypeFilter : null,
                    AuditUserFilter,
                    !string.IsNullOrWhiteSpace(AuditTableFilter) ? AuditTableFilter : null);
                    
                AuditLogs = new BindingList<AuditLogEntry>(logs);
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to load audit logs: {ex.Message}", "Error");
            }
        }

        #endregion
    }
}