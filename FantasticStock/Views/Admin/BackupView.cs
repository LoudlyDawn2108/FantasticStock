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
    public partial class BackupView : UserControl
    {
        private BackupViewModel _viewModel;

        public BackupView()
        {
            InitializeComponent();

            // Initialize view model
            _viewModel = new BackupViewModel();

            // Set up data bindings
            SetupBindings();
        }

        private void SetupBindings()
        {
            // Backup history data grid
            dgvBackupHistory.DataSource = _viewModel.BackupHistory;

            // Backup settings
            txtBackupLocation.DataBindings.Add("Text", _viewModel, "BackupLocation", true, DataSourceUpdateMode.OnPropertyChanged);
            txtDescription.DataBindings.Add("Text", _viewModel, "BackupDescription", true, DataSourceUpdateMode.OnPropertyChanged);
            chkIncludeAttachments.DataBindings.Add("Checked", _viewModel, "IncludeAttachments", true, DataSourceUpdateMode.OnPropertyChanged);
            rbCompressionNone.DataBindings.Add("Checked", _viewModel, "CompressionLevel", true, DataSourceUpdateMode.OnPropertyChanged, 0);
            rbCompressionNormal.DataBindings.Add("Checked", _viewModel, "CompressionLevel", true, DataSourceUpdateMode.OnPropertyChanged, 1);
            rbCompressionHigh.DataBindings.Add("Checked", _viewModel, "CompressionLevel", true, DataSourceUpdateMode.OnPropertyChanged, 2);
            chkEncrypt.DataBindings.Add("Checked", _viewModel, "EncryptBackup", true, DataSourceUpdateMode.OnPropertyChanged);
            txtEncryptionPassword.DataBindings.Add("Text", _viewModel, "EncryptionPassword", true, DataSourceUpdateMode.OnPropertyChanged);

            // Scheduled backups data grid
            dgvScheduledBackups.DataSource = _viewModel.ScheduledBackups;

            // Commands
            btnBackupNow.Click += (s, e) => _viewModel.BackupNowCommand.Execute(null);
            btnRestore.Click += (s, e) => _viewModel.RestoreBackupCommand.Execute(null);
            btnVerify.Click += (s, e) => _viewModel.VerifyBackupCommand.Execute(null);
            btnDelete.Click += (s, e) => _viewModel.DeleteBackupCommand.Execute(null);
            btnBrowseLocation.Click += (s, e) => _viewModel.BrowseLocationCommand.Execute(null);

            btnAddSchedule.Click += (s, e) => _viewModel.AddScheduleCommand.Execute(null);
            btnEditSchedule.Click += (s, e) => _viewModel.EditScheduleCommand.Execute(null);
            btnDeleteSchedule.Click += (s, e) => _viewModel.DeleteScheduleCommand.Execute(null);
            btnEnableSchedule.Click += (s, e) => _viewModel.EnableScheduleCommand.Execute(null);
            btnDisableSchedule.Click += (s, e) => _viewModel.DisableScheduleCommand.Execute(null);
            btnSaveSchedule.Click += (s, e) => _viewModel.SaveScheduleCommand.Execute(null);
            btnCancelEdit.Click += (s, e) => _viewModel.CancelEditScheduleCommand.Execute(null);

            // Selection change events
            dgvBackupHistory.SelectionChanged += (s, e) =>
            {
                if (dgvBackupHistory.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedBackup = dgvBackupHistory.SelectedRows[0].DataBoundItem as BackupHistory;
                }
            };

            dgvScheduledBackups.SelectionChanged += (s, e) =>
            {
                if (dgvScheduledBackups.SelectedRows.Count > 0)
                {
                    _viewModel.SelectedSchedule = dgvScheduledBackups.SelectedRows[0].DataBoundItem as ScheduledBackup;
                }
            };
        }
    }
}
