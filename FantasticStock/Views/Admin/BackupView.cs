using FantasticStock.Models;
using FantasticStock.Views.Admin;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FantasticStock.Models.Admin;

namespace FantasticStock.Views
{
    public partial class BackupView : UserControl, IBackupRestoreView
    {
        private readonly BindingSource _backupHistoryBindingSource = new BindingSource();
        private readonly BindingSource _scheduledBackupsBindingSource = new BindingSource();

        public event EventHandler BackupNowClicked;
        public event EventHandler VerifyBackupClicked;
        public event EventHandler RestoreBackupClicked;
        public event EventHandler DeleteBackupClicked;
        public event EventHandler ExecuteRestoreClicked;
        public event EventHandler BrowseBackupLocationClicked;
        public event EventHandler BrowseRestoreFileClicked;
        public event EventHandler AddScheduleClicked;
        public event EventHandler EditScheduleClicked;
        public event EventHandler DeleteScheduleClicked;
        public event EventHandler EnableScheduleClicked;
        public event EventHandler DisableScheduleClicked;
        public event EventHandler SaveScheduleClicked;
        public event EventHandler CancelScheduleEditClicked;

        // Properties for IBackupRestoreView
        public string BackupLocation
        {
            get => txtBackupLocation.Text;
            set => txtBackupLocation.Text = value;
        }

        public string BackupDescription
        {
            get => txtDescription.Text;
            set => txtDescription.Text = value;
        }

        public bool IncludeAttachments
        {
            get => chkIncludeAttachments.Checked;
            set => chkIncludeAttachments.Checked = value;
        }

        public int CompressionLevel
        {
            get
            {
                if (rbCompressionNone.Checked) return 0;
                if (rbCompressionNormal.Checked) return 1;
                if (rbCompressionHigh.Checked) return 2;
                return 0;
            }
        }

        public bool EncryptBackup
        {
            get => chkEncrypt.Checked;
            set => chkEncrypt.Checked = value;
        }

        public string EncryptionPassword
        {
            get => txtEncryptionPassword.Text;
            set => txtEncryptionPassword.Text = value;
        }

        public string RestoreFilePath
        {
            get => txtRestoreFilePath.Text;
            set => txtRestoreFilePath.Text = value;
        }

        public DateTime RestorePoint
        {
            get => dateRestorePoint.Value;
            set => dateRestorePoint.Value = value;
        }

        public bool OverwriteExistingData => rbOverwrite.Checked;

        public bool VerifyBeforeRestore
        {
            get => chkVerifyBeforeRestore.Checked;
            set => chkVerifyBeforeRestore.Checked = value;
        }

        public string SelectedScheduleType
        {
            get => (string)cmbScheduleType.SelectedValue;
            set => cmbScheduleType.SelectedValue = value;
        }

        public TimeSpan ScheduleTime
        {
            get => dtpScheduleTime.Value.TimeOfDay;
            set => dtpScheduleTime.Value = DateTime.Today.Add(value);
        }

        public BindingSource BackupHistoryBindingSource => _backupHistoryBindingSource;

        public BindingSource ScheduledBackupsBindingSource => _scheduledBackupsBindingSource;

        public BackupHistory SelectedBackupHistory
        {
            get
            {
                if (dgvBackupHistory.CurrentRow != null)
                {
                    return dgvBackupHistory.CurrentRow.DataBoundItem as BackupHistory;
                }
                return null;
            }
        }

        public ScheduledBackup SelectedScheduledBackup
        {
            get
            {
                if (dgvScheduledBackups.CurrentRow != null)
                {
                    return dgvScheduledBackups.CurrentRow.DataBoundItem as ScheduledBackup;
                }
                return null;
            }
        }

        public DayOfWeek SelectedDayOfWeek
        {
            get => (DayOfWeek)clbDaysofWeek.SelectedIndex;
            set => clbDaysofWeek.SelectedIndex = (int)value;
        }

        public BackupView()
        {
            InitializeComponent();

            dgvBackupHistory.DataSource = _backupHistoryBindingSource;
            dgvScheduledBackups.DataSource = _scheduledBackupsBindingSource;

            clbDaysofWeek.Items.Clear();
            clbDaysofWeek.Items.AddRange(new object[]
            {
            "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
            });
            clbDaysofWeek.SelectedIndex = 0;

            cmbScheduleType.Items.Clear();
            cmbScheduleType.Items.AddRange(new object[]
            {
            "Daily", "Weekly", "Monthly"
            });
            cmbScheduleType.SelectedIndex = 0;

            btnBackupNow.Click += (s, e) => BackupNowClicked?.Invoke(this, EventArgs.Empty);
            btnVerify.Click += (s, e) => VerifyBackupClicked?.Invoke(this, EventArgs.Empty);
            btnRestore.Click += (s, e) => RestoreBackupClicked?.Invoke(this, EventArgs.Empty);
            btnDelete.Click += (s, e) => DeleteBackupClicked?.Invoke(this, EventArgs.Empty);
            btnRestoreExecute.Click += (s, e) => ExecuteRestoreClicked?.Invoke(this, EventArgs.Empty);
            btnBrowseLocation.Click += (s, e) => BrowseBackupLocationClicked?.Invoke(this, EventArgs.Empty);
            btnBrowseRestoreFile.Click += (s, e) => BrowseRestoreFileClicked?.Invoke(this, EventArgs.Empty);
            btnAddSchedule.Click += (s, e) => AddScheduleClicked?.Invoke(this, EventArgs.Empty);
            btnEditSchedule.Click += (s, e) => EditScheduleClicked?.Invoke(this, EventArgs.Empty);
            btnDeleteSchedule.Click += (s, e) => DeleteScheduleClicked?.Invoke(this, EventArgs.Empty);
            btnEnableSchedule.Click += (s, e) => EnableScheduleClicked?.Invoke(this, EventArgs.Empty);
            btnDisableSchedule.Click += (s, e) => DisableScheduleClicked?.Invoke(this, EventArgs.Empty);
            btnSaveSchedule.Click += (s, e) => SaveScheduleClicked?.Invoke(this, EventArgs.Empty);
            btnCancelEdit.Click += (s, e) => CancelScheduleEditClicked?.Invoke(this, EventArgs.Empty);

            chkEncrypt.CheckedChanged += (s, e) =>
            {
                txtEncryptionPassword.Enabled = chkEncrypt.Checked;
            };

            cmbScheduleType.SelectedIndexChanged += (s, e) =>
            {
                Console.WriteLine(cmbScheduleType.SelectedValue);
                clbDaysofWeek.Enabled = (string)cmbScheduleType.SelectedValue == "Weekly";
            };

            EnableScheduleEditingControls(false);
        }

        public void ShowMessage(string message, string title, MessageType type)
        {
            MessageBoxIcon icon = MessageBoxIcon.Information;

            switch (type)
            {
                case MessageType.Warning:
                    icon = MessageBoxIcon.Warning;
                    break;
                case MessageType.Error:
                    icon = MessageBoxIcon.Error;
                    break;
            }

            MessageBox.Show(message, title, MessageBoxButtons.OK, icon);
        }

        public void ShowProgressDialog(string operation, int progress)
        {
            // In a real implementation, this would show a progress dialog
            // For this example, we'll just leave it as a stub
        }

        public void UpdateBackupHistoryList(List<BackupHistory> backupHistories)
        {
            _backupHistoryBindingSource.DataSource = backupHistories;
            _backupHistoryBindingSource.ResetBindings(false);
        }

        public void UpdateScheduledBackupsList(List<ScheduledBackup> scheduledBackups)
        {
            _scheduledBackupsBindingSource.DataSource = scheduledBackups;
            _scheduledBackupsBindingSource.ResetBindings(false);
        }

        public void EnableScheduleEditingControls(bool enabled)
        {
            // Enable/disable schedule editing controls
            cmbScheduleType.Enabled = enabled;
            clbDaysofWeek.Enabled = enabled && (string)cmbScheduleType.SelectedValue == "Weekly";
            dtpScheduleTime.Enabled = enabled;
            btnSaveSchedule.Enabled = enabled;
            btnCancelEdit.Enabled = enabled;

            // Enable/disable other controls based on editing mode
            btnAddSchedule.Enabled = !enabled;
            btnEditSchedule.Enabled = !enabled;
            btnDeleteSchedule.Enabled = !enabled;
            btnEnableSchedule.Enabled = !enabled;
            btnDisableSchedule.Enabled = !enabled;
            dgvScheduledBackups.Enabled = !enabled;
        }

        public void PopulateScheduleEditingControls(ScheduledBackup schedule)
        {
            cmbScheduleType.SelectedValue = schedule.ScheduleType;
            SelectedDayOfWeek = schedule.DayOfWeek.HasValue ? (DayOfWeek)schedule.DayOfWeek.Value : DayOfWeek.Sunday;
            dtpScheduleTime.Value = DateTime.Today.Add(schedule.ScheduleTime);
            BackupLocation = schedule.BackupPath;
            BackupDescription = schedule.Description;
            IncludeAttachments = schedule.IncludeAttachments;

            if (schedule.CompressionLevel == 0)
                rbCompressionNone.Checked = true;
            else if (schedule.CompressionLevel == 1)
                rbCompressionNormal.Checked = true;
            else
                rbCompressionHigh.Checked = true;

            EncryptBackup = schedule.IsEncrypted;
            EncryptionPassword = schedule.EncryptionPassword;
        }

        public void ClearScheduleEditingControls()
        {
            cmbScheduleType.SelectedIndex = 0;
            clbDaysofWeek.SelectedIndex = 0; // Set to Sunday by default
            dtpScheduleTime.Value = DateTime.Today;
            BackupLocation = string.Empty;
            BackupDescription = string.Empty;
            IncludeAttachments = false;
            rbCompressionNormal.Checked = true;
            EncryptBackup = false;
            EncryptionPassword = string.Empty;
        }

        public void SelectBackupHistoryItem(int backupId)
        {
            for (int i = 0; i < dgvBackupHistory.Rows.Count; i++)
            {
                var backup = dgvBackupHistory.Rows[i].DataBoundItem as BackupHistory;
                if (backup != null && backup.BackupID == backupId)
                {
                    dgvBackupHistory.CurrentCell = dgvBackupHistory.Rows[i].Cells[0];
                    return;
                }
            }
        }

        public void SelectScheduledBackupItem(int scheduleId)
        {
            for (int i = 0; i < dgvScheduledBackups.Rows.Count; i++)
            {
                var schedule = dgvScheduledBackups.Rows[i].DataBoundItem as ScheduledBackup;
                if (schedule != null && schedule.ScheduleID == scheduleId)
                {
                    dgvScheduledBackups.CurrentCell = dgvScheduledBackups.Rows[i].Cells[0];
                    return;
                }
            }
        }

        bool IBackupRestoreView.ConfirmAction(string message, string title)
        {
            throw new NotImplementedException();
        }
    }
}
