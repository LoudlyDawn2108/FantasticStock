using AdminDomain.Common;
using AdminDomain.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminDomain.Views
{
    public partial class MainForm : Form
    {
        private readonly IDatabaseService _databaseService;
        private readonly IAuditService _auditService;

        // User controls for each tab
        private UserManagementView _userManagementView;
        private SystemConfigurationView _systemConfigView;
        private BackupView _backupView;
        private MonitoringView _monitoringView;

        public MainForm()
        {
            InitializeComponent();

            _databaseService = ServiceLocator.GetService<IDatabaseService>();
            _auditService = ServiceLocator.GetService<IAuditService>();

            // Initialize views
            InitializeViews();

            // Update status bar with current user info
            UpdateStatusBar();
        }

        private void InitializeViews()
        {
            // Create user management view
            _userManagementView = new UserManagementView
            {
                Dock = DockStyle.Fill
            };
            tabUserManagement.Controls.Add(_userManagementView);

            // Create system configuration view
            _systemConfigView = new SystemConfigurationView
            {
                Dock = DockStyle.Fill
            };
            tabSystemConfig.Controls.Add(_systemConfigView);

            // Create backup view
            _backupView = new BackupView
            {
                Dock = DockStyle.Fill
            };
            tabBackup.Controls.Add(_backupView);

            // Create monitoring view
            _monitoringView = new MonitoringView
            {
                Dock = DockStyle.Fill
            };
            tabMonitoring.Controls.Add(_monitoringView);
        }

        private void UpdateStatusBar()
        {
            // Display user info in status bar
            lblUserStatus.Text = $"Logged in as: {CurrentUser.DisplayName} ({CurrentUser.RoleName})";
            lblDateTime.Text = DateTime.Parse("2025-03-02 16:16:14").ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Log the logout event
            try
            {
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "UserLogout",
                    "Users",
                    CurrentUser.UserID.ToString(),
                    null,
                    $"User '{CurrentUser.Username}' logged out at {DateTime.Parse("2025-03-02 16:16:14"):yyyy-MM-dd HH:mm:ss}"
                );
            }
            catch
            {
                // Ignore errors during shutdown
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Admin Domain Management System\nVersion 1.0\n\n© 2025 Your Company",
                "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var changePasswordForm = new ChangePasswordForm())
            {
                changePasswordForm.ShowDialog(this);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update the date/time display
            lblDateTime.Text = DateTime.Parse("2025-03-02 16:19:04").ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
