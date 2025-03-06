using FantasticStock.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views
{
    public partial class MonitoringView : UserControl
    {
        private readonly MonitoringViewModel _viewModel;

        public MonitoringView()
        {
            InitializeComponent();

            // Initialize view model
            _viewModel = new MonitoringViewModel();

            // Set up data bindings
            SetupBindings();

            // Start timer to update resources display
            tmrRefresh.Interval = 5000; // 5 seconds
            tmrRefresh.Enabled = true;
            tmrRefresh.Start();
        }

        private void SetupBindings()
        {
            // Bind system resources
            progressCPU.DataBindings.Add("Value", _viewModel, "CurrentResources.CpuUsage", true);
            progressMemory.DataBindings.Add("Value", _viewModel, "CurrentResources.MemoryUsage", true);
            progressDisk.DataBindings.Add("Value", _viewModel, "CurrentResources.DiskUsage", true);
            lblCPUValue.DataBindings.Add("Text", _viewModel, "CurrentResources.CpuUsage", true);
            lblMemoryValue.DataBindings.Add("Text", _viewModel, "CurrentResources.MemoryUsage", true);
            lblDiskValue.DataBindings.Add("Text", _viewModel, "CurrentResources.DiskUsage", true);
            lblDatabaseSize.DataBindings.Add("Text", _viewModel, "CurrentResources.DatabaseSize", true);
            lblActiveUsers.DataBindings.Add("Text", _viewModel, "CurrentResources.ActiveUsers", true);
            lblLastBackup.DataBindings.Add("Text", _viewModel, "CurrentResources.LastBackupDate", true);
            lblUptime.DataBindings.Add("Text", _viewModel, "CurrentResources.Uptime", true);

            // Bind error logs
            dgvErrorLogs.DataSource = _viewModel.ErrorLogs;

            // Bind audit logs
            dgvAuditLogs.DataSource = _viewModel.AuditLogs;

            // Bind date filters
            dtpStartDate.DataBindings.Add("Value", _viewModel, "StartDate", true, DataSourceUpdateMode.OnPropertyChanged);
            dtpEndDate.DataBindings.Add("Value", _viewModel, "EndDate", true, DataSourceUpdateMode.OnPropertyChanged);

            // Bind other filters
            txtErrorModuleFilter.DataBindings.Add("Text", _viewModel, "ErrorModuleFilter", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbErrorSeverity.DataBindings.Add("SelectedValue", _viewModel, "ErrorSeverityFilter", true, DataSourceUpdateMode.OnPropertyChanged);
            txtAuditTableFilter.DataBindings.Add("Text", _viewModel, "AuditTableFilter", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbAuditType.DataBindings.Add("Text", _viewModel, "AuditTypeFilter", true, DataSourceUpdateMode.OnPropertyChanged);
            cmbAuditUser.DataBindings.Add("SelectedValue", _viewModel, "AuditUserFilter", true, DataSourceUpdateMode.OnPropertyChanged);

            // Populate dropdowns
            // Severity levels
            cmbErrorSeverity.Items.Add(new { Text = "All", Value = (int?)null });
            cmbErrorSeverity.Items.Add(new { Text = "Low", Value = 1 });
            cmbErrorSeverity.Items.Add(new { Text = "Medium", Value = 2 });
            cmbErrorSeverity.Items.Add(new { Text = "High", Value = 3 });
            cmbErrorSeverity.Items.Add(new { Text = "Critical", Value = 4 });
            cmbErrorSeverity.DisplayMember = "Text";
            cmbErrorSeverity.ValueMember = "Value";
            cmbErrorSeverity.SelectedIndex = 0;

            // Audit types
            cmbAuditType.Items.Add("All");
            cmbAuditType.Items.Add("Login/Logout");
            cmbAuditType.Items.Add("Create");
            cmbAuditType.Items.Add("Update");
            cmbAuditType.Items.Add("Delete");
            cmbAuditType.Items.Add("Export");
            cmbAuditType.Items.Add("Import");
            cmbAuditType.Items.Add("Security");
            cmbAuditType.SelectedIndex = 0;

            // Bind commands to buttons
            btnRefreshResources.Click += (s, e) => _viewModel.RefreshResourcesCommand.Execute(null);
            btnOptimizeDB.Click += (s, e) => _viewModel.OptimizeDatabaseCommand.Execute(null);
            btnRebuildIndexes.Click += (s, e) => _viewModel.RebuildIndexesCommand.Execute(null);
            btnClearCache.Click += (s, e) => _viewModel.ClearCacheCommand.Execute(null);
            btnFilterErrorLogs.Click += (s, e) => _viewModel.FilterErrorLogsCommand.Execute(null);
            btnClearErrorLogs.Click += (s, e) => _viewModel.ClearErrorLogsCommand.Execute(null);
            btnFilterAuditLogs.Click += (s, e) => _viewModel.FilterAuditLogsCommand.Execute(null);
            btnClearAuditLogs.Click += (s, e) => _viewModel.ClearAuditLogsCommand.Execute(null);
            btnExportLogs.Click += (s, e) => _viewModel.ExportLogsCommand.Execute(null);
        }

        private void tmrRefresh_Tick(object sender, EventArgs e)
        {
            // Auto-refresh system resources every 5 seconds
            _viewModel.RefreshResourcesCommand.Execute(null);

            // Update current time label
            lblCurrentTime.Text = $"Last updated: {DateTime.Parse("2025-03-02 17:41:59"):yyyy-MM-dd HH:mm:ss} by {Environment.UserName}";
        }
    }
}
