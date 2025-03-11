using FantasticStock.Models;
using FantasticStock.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views
{
    public partial class BackupView : UserControl
    {
        private readonly BackupViewModel _viewModel;

        public BackupView()
        {
            InitializeComponent();

            _viewModel = new BackupViewModel();

            // Initialize data bindings
            SetupBindings();

            // Initialize event handlers
            SetupEventHandlers();
        }

        private void SetupBindings()
        {
            // Backup panel bindings
            txtBackupLocation.DataBindings.Add("Text", _viewModel, "BackupLocation", false, DataSourceUpdateMode.OnPropertyChanged);
            txtDescription.DataBindings.Add("Text", _viewModel, "BackupDescription", false, DataSourceUpdateMode.OnPropertyChanged);
            chkIncludeAttachments.DataBindings.Add("Checked", _viewModel, "IncludeAttachments", false, DataSourceUpdateMode.OnPropertyChanged);
            chkEncrypt.DataBindings.Add("Checked", _viewModel, "EncryptBackup", false, DataSourceUpdateMode.OnPropertyChanged);
            txtEncryptionPassword.DataBindings.Add("Text", _viewModel, "EncryptionPassword", false, DataSourceUpdateMode.OnPropertyChanged);

            // Compression level radio buttons
            rbCompressionNone.Checked = _viewModel.CompressionLevel == 0;
            rbCompressionNormal.Checked = _viewModel.CompressionLevel == 1;
            rbCompressionHigh.Checked = _viewModel.CompressionLevel == 2;

            // History DataGrid binding
            dgvBackupHistory.DataSource = _viewModel.BackupHistory;

            // Schedule DataGrid binding
            dgvScheduledBackups.DataSource = _viewModel.ScheduledBackups;

            // Update UI based on encrypt checkbox state
            txtEncryptionPassword.Enabled = chkEncrypt.Checked;
        }

        private void SetupEventHandlers()
        {
            // Backup panel events
            btnBackupNow.Click += (s, e) => _viewModel.BackupNowCommand.Execute(null);
            btnRestore.Click += (s, e) => _viewModel.RestoreBackupCommand.Execute(null);
            btnVerify.Click += (s, e) => _viewModel.VerifyBackupCommand.Execute(null);
            btnDelete.Click += (s, e) => _viewModel.DeleteBackupCommand.Execute(null);
            btnBrowseLocation.Click += (s, e) => _viewModel.BrowseLocationCommand.Execute(null);

            // Selection change events
            dgvBackupHistory.SelectionChanged += (s, e) =>
            {
                if (dgvBackupHistory.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedBackup = dgvBackupHistory.SelectedRows[0].DataBoundItem as BackupHistory;
                }
            };

            // Encryption password field enabled state
            chkEncrypt.CheckedChanged += (s, e) =>
            {
                txtEncryptionPassword.Enabled = chkEncrypt.Checked;
            };

            // Compression level radio buttons
            rbCompressionNone.CheckedChanged += (s, e) =>
            {
                if (rbCompressionNone.Checked)
                    _viewModel.CompressionLevel = 0;
            };

            rbCompressionNormal.CheckedChanged += (s, e) =>
            {
                if (rbCompressionNormal.Checked)
                    _viewModel.CompressionLevel = 1;
            };

            rbCompressionHigh.CheckedChanged += (s, e) =>
            {
                if (rbCompressionHigh.Checked)
                    _viewModel.CompressionLevel = 2;
            };

            // Restore tab events
            btnBrowseRestoreFile.Click += (s, e) => BrowseRestoreFile();
            dateRestorePoint.ValueChanged += (s, e) => UpdateRestorePoint();
            btnRestoreExecute.Click += (s, e) => ExecuteRestore();

            // Schedule dialog button
            btnSchedule.Click += (s, e) => ShowScheduleDialog();
        }

        private void BrowseRestoreFile()
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Backup Files (*.bak)|*.bak|All files (*.*)|*.*";
                dialog.Title = "Select Backup File";
                dialog.InitialDirectory = _viewModel.BackupLocation;

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    txtRestoreFilePath.Text = dialog.FileName;
                    // Enable restore point calendar if this is an incremental backup
                    bool isIncremental = IsIncrementalBackup(dialog.FileName);
                    dateRestorePoint.Enabled = isIncremental;
                    lblRestorePoint.Enabled = isIncremental;

                    if (isIncremental)
                    {
                        // Load available restore points
                        LoadRestorePoints(dialog.FileName);
                    }
                }
            }
        }

        private bool IsIncrementalBackup(string fileName)
        {
            // This would check the backup file to determine if it's incremental
            // Simplified for example
            return Path.GetFileName(fileName).Contains("_inc_");
        }

        private void LoadRestorePoints(string fileName)
        {
            // In real implementation, this would parse the backup file for available restore points
            // For example, load timestamps from the backup file

            // For demo, just use the current date and two previous days
            dateRestorePoint.MinDate = DateTime.Now.AddDays(-2);
            dateRestorePoint.MaxDate = DateTime.Now;
            dateRestorePoint.Value = DateTime.Now;
        }

        private void UpdateRestorePoint()
        {
            // Update the selected restore point in the viewmodel or prepare for restore
            lblSelectedPoint.Text = $"Selected Point: {dateRestorePoint.Value.ToString("yyyy-MM-dd HH:mm:ss")}";
        }

        private void ExecuteRestore()
        {
            if (string.IsNullOrWhiteSpace(txtRestoreFilePath.Text))
            {
                MessageBox.Show("Please select a backup file.", "Missing Information",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool verifyFirst = chkVerifyBeforeRestore.Checked;
            bool overwrite = rbOverwrite.Checked; // Otherwise it's merge

            if (MessageBox.Show(
                "Are you sure you want to restore this backup? This will replace or merge with your current data.\n\n" +
                "It is highly recommended to create a backup of your current database before proceeding.",
                "Confirm Restore", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                try
                {
                    // In a real implementation, we would call the appropriate restore method
                    // For now, simulate success

                    MessageBox.Show("Database restored successfully. The application will now restart.",
                        "Restore Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // In a real application, you would restart the application here
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Restore failed: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowScheduleDialog()
        {
            // In a real application, this would open a dialog to manage scheduled backups
            // For demo purposes, we'll switch to the schedule tab
            tabControl1.SelectedTab = tabScheduledBackups;
        }
    }
}
