using FantasticStock.Services.Admin;
using FantasticStock.Views.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FantasticStock.Models.Admin;

namespace FantasticStock.Presenters.Admin
{
    class BackupRestorePresenter
    {
        private readonly IBackupRestoreView _view;
        private readonly IBackupService _backupService;
        private ScheduledBackup _currentEditingSchedule;
        private bool _isEditingSchedule;

        public BackupRestorePresenter(IBackupRestoreView view, IBackupService backupService)
        {
            _view = view;
            _backupService = backupService;

            // Subscribe to view events
            _view.BackupNowClicked += OnBackupNowClicked;
            _view.VerifyBackupClicked += OnVerifyBackupClicked;
            _view.RestoreBackupClicked += OnRestoreBackupClicked;
            _view.DeleteBackupClicked += OnDeleteBackupClicked;
            _view.ExecuteRestoreClicked += OnExecuteRestoreClicked;
            _view.BrowseBackupLocationClicked += OnBrowseBackupLocationClicked;
            _view.BrowseRestoreFileClicked += OnBrowseRestoreFileClicked;
            _view.AddScheduleClicked += OnAddScheduleClicked;
            _view.EditScheduleClicked += OnEditScheduleClicked;
            _view.DeleteScheduleClicked += OnDeleteScheduleClicked;
            _view.EnableScheduleClicked += OnEnableScheduleClicked;
            _view.DisableScheduleClicked += OnDisableScheduleClicked;
            _view.SaveScheduleClicked += OnSaveScheduleClicked;
            _view.CancelScheduleEditClicked += OnCancelScheduleEditClicked;

            // Initialize view
            LoadBackupHistory();
            LoadScheduledBackups();
        }

        public void LoadBackupHistory()
        {
            try
            {
                var backupHistory = _backupService.GetBackupHistory();
                _view.UpdateBackupHistoryList(backupHistory);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error loading backup history: {ex.Message}", "Error", MessageType.Error);
            }
        }

        public void LoadScheduledBackups()
        {
            try
            {
                var scheduledBackups = _backupService.GetScheduledBackups();
                _view.UpdateScheduledBackupsList(scheduledBackups);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error loading scheduled backups: {ex.Message}", "Error", MessageType.Error);
            }
        }

        private void OnBackupNowClicked(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(_view.BackupLocation))
                {
                    _view.ShowMessage("Please specify a backup location.", "Validation Error", MessageType.Warning);
                    return;
                }

                // Create backup
                int backupId = _backupService.CreateBackup(
                    _view.BackupLocation,
                    _view.BackupDescription,
                    _view.IncludeAttachments,
                    _view.CompressionLevel,
                    _view.EncryptBackup,
                    _view.EncryptBackup ? _view.EncryptionPassword : null
                );

                if (backupId > 0)
                {
                    _view.ShowMessage("Backup created successfully.", "Success", MessageType.Information);
                    LoadBackupHistory();
                    _view.SelectBackupHistoryItem(backupId);
                }
                else
                {
                    _view.ShowMessage("Failed to create backup.", "Error", MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error creating backup: {ex.Message}", "Error", MessageType.Error);
            }
        }

        private void OnVerifyBackupClicked(object sender, EventArgs e)
        {
            var selectedBackup = _view.SelectedBackupHistory;
            if (selectedBackup == null)
            {
                _view.ShowMessage("Please select a backup to verify.", "Validation Error", MessageType.Warning);
                return;
            }

            try
            {
                bool result = _backupService.VerifyBackupIntegrity(selectedBackup.BackupID);
                if (result)
                {
                    _view.ShowMessage("Backup verification successful. The backup is valid.", "Verification Result", MessageType.Information);
                }
                else
                {
                    _view.ShowMessage("Backup verification failed. The backup may be corrupted or incomplete.", "Verification Result", MessageType.Warning);
                }

                // Refresh backup history to show updated verification status
                LoadBackupHistory();
                _view.SelectBackupHistoryItem(selectedBackup.BackupID);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error verifying backup: {ex.Message}", "Error", MessageType.Error);
            }
        }

        private void OnRestoreBackupClicked(object sender, EventArgs e)
        {
            var selectedBackup = _view.SelectedBackupHistory;
            if (selectedBackup == null)
            {
                _view.ShowMessage("Please select a backup to restore.", "Validation Error", MessageType.Warning);
                return;
            }

            // Set the restore file path in the restore tab
            _view.RestoreFilePath = selectedBackup.BackupPath;
        }

        private void OnDeleteBackupClicked(object sender, EventArgs e)
        {
            var selectedBackup = _view.SelectedBackupHistory;
            if (selectedBackup == null)
            {
                _view.ShowMessage("Please select a backup to delete.", "Validation Error", MessageType.Warning);
                return;
            }

            var result = _view.ConfirmAction(
                $"Are you sure you want to delete the backup '{selectedBackup.Description}' created on {selectedBackup.CreatedDate}?",
                "Confirm Deletion"
            );

            // Assuming ShowMessage returns true for "Yes" in Question mode
            if (result)
            {
                try
                {
                    bool deleteResult = _backupService.DeleteBackup(selectedBackup.BackupID);
                    if (deleteResult)
                    {
                        _view.ShowMessage("Backup deleted successfully.", "Success", MessageType.Information);
                        LoadBackupHistory();
                    }
                    else
                    {
                        _view.ShowMessage("Failed to delete backup.", "Error", MessageType.Error);
                    }
                }
                catch (Exception ex)
                {
                    _view.ShowMessage($"Error deleting backup: {ex.Message}", "Error", MessageType.Error);
                }
            }
        }

        private void OnExecuteRestoreClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_view.RestoreFilePath))
            {
                _view.ShowMessage("Please select a backup file to restore.", "Validation Error", MessageType.Warning);
                return;
            }

            var result = _view.ConfirmAction(
                "Are you sure you want to restore the database? This operation cannot be undone.",
                "Confirm Restoration"
            );

            // Assuming ShowMessage returns true for "Yes" in Question mode
            if (result)
            {
                try
                {
                    // Find the backup ID from path
                    var backupHistory = _backupService.GetBackupHistory();
                    var backupToRestore = backupHistory.FirstOrDefault(b => b.BackupPath == _view.RestoreFilePath);

                    if (backupToRestore == null)
                    {
                        _view.ShowMessage("The selected backup file was not found in the backup history.", "Error", MessageType.Error);
                        return;
                    }

                    bool restoreResult = _backupService.RestoreFromBackup(
                        backupToRestore.BackupID,
                        _view.VerifyBeforeRestore
                    );

                    if (restoreResult)
                    {
                        _view.ShowMessage("Database restored successfully.", "Success", MessageType.Information);
                    }
                    else
                    {
                        _view.ShowMessage("Failed to restore database.", "Error", MessageType.Error);
                    }
                }
                catch (Exception ex)
                {
                    _view.ShowMessage($"Error restoring database: {ex.Message}", "Error", MessageType.Error);
                }
            }
        }

        private void OnBrowseBackupLocationClicked(object sender, EventArgs e)
        {
            // This would typically show a SaveFileDialog
            // For this example, we'll assume the view handles this and sets the BackupLocation property
        }

        private void OnBrowseRestoreFileClicked(object sender, EventArgs e)
        {
            // This would typically show an OpenFileDialog
            // For this example, we'll assume the view handles this and sets the RestoreFilePath property
        }

        private void OnAddScheduleClicked(object sender, EventArgs e)
        {
            _isEditingSchedule = false;
            _currentEditingSchedule = null;
            _view.ClearScheduleEditingControls();
            _view.EnableScheduleEditingControls(true);
        }

        private void OnEditScheduleClicked(object sender, EventArgs e)
        {
            var selectedSchedule = _view.SelectedScheduledBackup;
            if (selectedSchedule == null)
            {
                _view.ShowMessage("Please select a schedule to edit.", "Validation Error", MessageType.Warning);
                return;
            }

            _isEditingSchedule = true;
            _currentEditingSchedule = selectedSchedule;
            _view.PopulateScheduleEditingControls(selectedSchedule);
            _view.EnableScheduleEditingControls(true);
        }

        private void OnDeleteScheduleClicked(object sender, EventArgs e)
        {
            var selectedSchedule = _view.SelectedScheduledBackup;
            if (selectedSchedule == null)
            {
                _view.ShowMessage("Please select a schedule to delete.", "Validation Error", MessageType.Warning);
                return;
            }

            var result = _view.ConfirmAction(
                $"Are you sure you want to delete the scheduled backup '{selectedSchedule.Description}'?",
                "Confirm Deletion"
            );

            // Assuming ShowMessage returns true for "Yes" in Question mode
            if (result)
            {
                try
                {
                    bool deleteResult = _backupService.DeleteScheduledBackup(selectedSchedule.ScheduleID);
                    if (deleteResult)
                    {
                        _view.ShowMessage("Scheduled backup deleted successfully.", "Success", MessageType.Information);
                        LoadScheduledBackups();
                    }
                    else
                    {
                        _view.ShowMessage("Failed to delete scheduled backup.", "Error", MessageType.Error);
                    }
                }
                catch (Exception ex)
                {
                    _view.ShowMessage($"Error deleting scheduled backup: {ex.Message}", "Error", MessageType.Error);
                }
            }
        }

        private void OnEnableScheduleClicked(object sender, EventArgs e)
        {
            UpdateScheduleStatus(true);
        }

        private void OnDisableScheduleClicked(object sender, EventArgs e)
        {
            UpdateScheduleStatus(false);
        }

        private void UpdateScheduleStatus(bool enable)
        {
            var selectedSchedule = _view.SelectedScheduledBackup;
            if (selectedSchedule == null)
            {
                _view.ShowMessage("Please select a schedule to update.", "Validation Error", MessageType.Warning);
                return;
            }

            try
            {
                bool result = _backupService.EnableScheduledBackup(selectedSchedule.ScheduleID, enable);
                if (result)
                {
                    _view.ShowMessage($"Schedule {(enable ? "enabled" : "disabled")} successfully.", "Success", MessageType.Information);
                    LoadScheduledBackups();
                    _view.SelectScheduledBackupItem(selectedSchedule.ScheduleID);
                }
                else
                {
                    _view.ShowMessage($"Failed to {(enable ? "enable" : "disable")} schedule.", "Error", MessageType.Error);
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error updating schedule status: {ex.Message}", "Error", MessageType.Error);
            }
        }

        private void OnSaveScheduleClicked(object sender, EventArgs e)
        {
            try
            {
                // Validate input
                if (_view.SelectedScheduleType == "Weekly")
                {
                    // No need to check if it's empty since we're always selecting one day
                }

                var schedule = new ScheduledBackup
                {
                    ScheduleType = _view.SelectedScheduleType,
                    DayOfWeek = _view.SelectedScheduleType == "Weekly" ? (int)_view.SelectedDayOfWeek : -1,
                    ScheduleTime = _view.ScheduleTime,
                    BackupPath = _view.BackupLocation,
                    Description = _view.BackupDescription,
                    IncludeAttachments = _view.IncludeAttachments,
                    CompressionLevel = _view.CompressionLevel,
                    IsEncrypted = _view.EncryptBackup,
                    EncryptionPassword = _view.EncryptBackup ? _view.EncryptionPassword : null,
                    IsActive = true
                };

                if (_isEditingSchedule && _currentEditingSchedule != null)
                {
                    // Update existing schedule
                    schedule.ScheduleID = _currentEditingSchedule.ScheduleID;
                    bool updateResult = _backupService.UpdateScheduledBackup(schedule);

                    if (updateResult)
                    {
                        _view.ShowMessage("Schedule updated successfully.", "Success", MessageType.Information);
                        LoadScheduledBackups();
                        _view.SelectScheduledBackupItem(schedule.ScheduleID);
                        _view.EnableScheduleEditingControls(false);
                    }
                    else
                    {
                        _view.ShowMessage("Failed to update schedule.", "Error", MessageType.Error);
                    }
                }
                else
                {
                    // Create new schedule
                    int scheduleId = _backupService.AddScheduledBackup(schedule);

                    if (scheduleId > 0)
                    {
                        _view.ShowMessage("Schedule created successfully.", "Success", MessageType.Information);
                        LoadScheduledBackups();
                        _view.SelectScheduledBackupItem(scheduleId);
                        _view.EnableScheduleEditingControls(false);
                    }
                    else
                    {
                        _view.ShowMessage("Failed to create schedule.", "Error", MessageType.Error);
                    }
                }

                _isEditingSchedule = false;
                _currentEditingSchedule = null;
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Error saving schedule: {ex.Message}", "Error", MessageType.Error);
            }
        }

        private void OnCancelScheduleEditClicked(object sender, EventArgs e)
        {
            _isEditingSchedule = false;
            _currentEditingSchedule = null;
            _view.ClearScheduleEditingControls();
            _view.EnableScheduleEditingControls(false);
        }
    }
}
