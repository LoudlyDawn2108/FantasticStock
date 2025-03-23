using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using System.Windows.Input;
using FantasticStock.Common;
using FantasticStock.Models;
using FantasticStock.Models.Admin;
using FantasticStock.Services.Admin;

namespace FantasticStock.ViewModels
{
    public class BackupViewModel : ViewModelBase
    {
        private readonly IBackupService _backupService;
        
        private BindingList<BackupHistory> _backupHistory;
        private BackupHistory _selectedBackup;
        private string _backupLocation;
        private string _backupDescription;
        private bool _includeAttachments;
        private int _compressionLevel;
        private bool _encryptBackup;
        private string _encryptionPassword;
        private BindingList<ScheduledBackup> _scheduledBackups;
        private ScheduledBackup _selectedSchedule;
        
        public BackupViewModel()
        {
            _backupService = ServiceLocator.GetService<IBackupService>();
            
            // Initialize commands
            BackupNowCommand = new RelayCommand(BackupNow, CanBackupNow);
            RestoreBackupCommand = new RelayCommand(RestoreBackup, CanRestoreBackup);
            VerifyBackupCommand = new RelayCommand(VerifyBackup, CanVerifyBackup);
            DeleteBackupCommand = new RelayCommand(DeleteBackup, CanDeleteBackup);
            BrowseLocationCommand = new RelayCommand(BrowseLocation);
            
            AddScheduleCommand = new RelayCommand(AddSchedule);
            EditScheduleCommand = new RelayCommand(EditSchedule, CanEditSchedule);
            DeleteScheduleCommand = new RelayCommand(DeleteSchedule, CanDeleteSchedule);
            EnableScheduleCommand = new RelayCommand(EnableSchedule, CanEnableSchedule);
            DisableScheduleCommand = new RelayCommand(DisableSchedule, CanDisableSchedule);
            SaveScheduleCommand = new RelayCommand(SaveSchedule, CanSaveSchedule);
            CancelEditScheduleCommand = new RelayCommand(CancelEditSchedule);
            
            // Initialize default values
            _backupLocation = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Backups");
            _compressionLevel = 1; // Default to normal compression
            
            // Load data
            LoadData();
        }

        #region Properties

        public BindingList<BackupHistory> BackupHistory
        {
            get => _backupHistory;
            set => SetProperty(ref _backupHistory, value);
        }

        public BackupHistory SelectedBackup
        {
            get => _selectedBackup;
            set
            {
                if (SetProperty(ref _selectedBackup, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        public string BackupLocation
        {
            get => _backupLocation;
            set => SetProperty(ref _backupLocation, value);
        }

        public string BackupDescription
        {
            get => _backupDescription;
            set => SetProperty(ref _backupDescription, value);
        }

        public bool IncludeAttachments
        {
            get => _includeAttachments;
            set => SetProperty(ref _includeAttachments, value);
        }

        public int CompressionLevel
        {
            get => _compressionLevel;
            set => SetProperty(ref _compressionLevel, value);
        }

        public bool EncryptBackup
        {
            get => _encryptBackup;
            set => SetProperty(ref _encryptBackup, value);
        }

        public string EncryptionPassword
        {
            get => _encryptionPassword;
            set => SetProperty(ref _encryptionPassword, value);
        }

        public BindingList<ScheduledBackup> ScheduledBackups
        {
            get => _scheduledBackups;
            set => SetProperty(ref _scheduledBackups, value);
        }

        public ScheduledBackup SelectedSchedule
        {
            get => _selectedSchedule;
            set
            {
                if (SetProperty(ref _selectedSchedule, value))
                {
                    CommandManager.InvalidateRequerySuggested();
                }
            }
        }

        private bool IsEditingSchedule { get; set; }
        private ScheduledBackup OriginalSchedule { get; set; }

        #endregion

        #region Commands

        public ICommand BackupNowCommand { get; }
        public ICommand RestoreBackupCommand { get; }
        public ICommand VerifyBackupCommand { get; }
        public ICommand DeleteBackupCommand { get; }
        public ICommand BrowseLocationCommand { get; }
        
        public ICommand AddScheduleCommand { get; }
        public ICommand EditScheduleCommand { get; }
        public ICommand DeleteScheduleCommand { get; }
        public ICommand EnableScheduleCommand { get; }
        public ICommand DisableScheduleCommand { get; }
        public ICommand SaveScheduleCommand { get; }
        public ICommand CancelEditScheduleCommand { get; }

        #endregion

        #region Command Implementations

        private void BackupNow(object parameter)
        {
            try
            {
                if (!Directory.Exists(BackupLocation))
                {
                    Directory.CreateDirectory(BackupLocation);
                }
                
                var password = EncryptBackup ? EncryptionPassword : null;
                
                _backupService.CreateBackup(
                    BackupLocation,
                    BackupDescription,
                    IncludeAttachments,
                    CompressionLevel,
                    EncryptBackup,
                    password);
                
                MessageService.ShowInformation("Backup completed successfully.", "Success");
                
                // Reload backup history
                LoadBackupHistory();
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Backup failed: {ex.Message}", "Error");
            }
        }

        private bool CanBackupNow(object parameter)
        {
            return !string.IsNullOrWhiteSpace(BackupLocation) && 
                   (!EncryptBackup || !string.IsNullOrWhiteSpace(EncryptionPassword));
        }

        private void RestoreBackup(object parameter)
        {
            if (SelectedBackup == null)
                return;
                
            if (MessageService.ShowConfirmation(
                "Are you sure you want to restore this backup? This will replace all current data with the backup data.\n\n" +
                "It is highly recommended to create a backup of your current database before proceeding.",
                "Confirm Restore"))
            {
                try
                {
                    bool success = _backupService.RestoreFromBackup(SelectedBackup.BackupID);
                    
                    if (success)
                    {
                        MessageService.ShowInformation("Database restored successfully. The application will now restart.", "Restore Complete");
                        Application.Restart();
                        Environment.Exit(0);
                    }
                    else
                    {
                        MessageService.ShowError("Failed to restore database.", "Restore Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowError($"Restore failed: {ex.Message}", "Error");
                }
            }
        }

        private bool CanRestoreBackup(object parameter)
        {
            return SelectedBackup != null;
        }

        private void VerifyBackup(object parameter)
        {
            if (SelectedBackup == null)
                return;
                
            try
            {
                bool isValid = _backupService.VerifyBackupIntegrity(SelectedBackup.BackupID);
                
                if (isValid)
                {
                    MessageService.ShowInformation("Backup verification completed successfully. The backup is valid.", "Verification Complete");
                }
                else
                {
                    MessageService.ShowError("Backup verification failed. The backup may be corrupted or incomplete.", "Verification Error");
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Verification failed: {ex.Message}", "Error");
            }
        }

        private bool CanVerifyBackup(object parameter)
        {
            return SelectedBackup != null;
        }

        private void DeleteBackup(object parameter)
        {
            if (SelectedBackup == null)
                return;
                
            if (MessageService.ShowConfirmation(
                $"Are you sure you want to delete the backup '{SelectedBackup.BackupName}'?\n\nThis action cannot be undone.",
                "Confirm Delete"))
            {
                try
                {
                    bool success = _backupService.DeleteBackup(SelectedBackup.BackupID);
                    
                    if (success)
                    {
                        MessageService.ShowInformation("Backup deleted successfully.", "Delete Complete");
                        LoadBackupHistory();
                    }
                    else
                    {
                        MessageService.ShowError("Failed to delete backup.", "Delete Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowError($"Delete failed: {ex.Message}", "Error");
                }
            }
        }

        private bool CanDeleteBackup(object parameter)
        {
            return SelectedBackup != null;
        }

        private void BrowseLocation(object parameter)
        {
            // In a real application, this would show a folder browser dialog
            // For now, we'll just simulate selecting a folder
            
            string simulatedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Backups", DateTime.Parse("2025-03-02 16:11:28").ToString("yyyy-MM-dd"));
            
            if (!Directory.Exists(simulatedPath))
            {
                Directory.CreateDirectory(simulatedPath);
            }
            
            BackupLocation = simulatedPath;
        }

        private void AddSchedule(object parameter)
        {
            // Create a new schedule with default values
            SelectedSchedule = new ScheduledBackup
            {
                BackupPath = BackupLocation,
                ScheduleType = "Daily",
                ScheduleTime = new TimeSpan(1, 0, 0), // 1:00 AM
                IsActive = true,
                CompressionLevel = 1,
                IncludeAttachments = false,
                IsEncrypted = false
            };
            
            IsEditingSchedule = true;
            CommandManager.InvalidateRequerySuggested();
        }

        private void EditSchedule(object parameter)
        {
            if (SelectedSchedule == null)
                return;
                
            // Save original for cancel operation
            OriginalSchedule = CloneSchedule(SelectedSchedule);
            
            IsEditingSchedule = true;
            CommandManager.InvalidateRequerySuggested();
        }

        private bool CanEditSchedule(object parameter)
        {
            return SelectedSchedule != null && !IsEditingSchedule;
        }

        private void DeleteSchedule(object parameter)
        {
            if (SelectedSchedule == null)
                return;
                
            if (MessageService.ShowConfirmation(
                $"Are you sure you want to delete this backup schedule?\n\nThis action cannot be undone.",
                "Confirm Delete"))
            {
                try
                {
                    bool success = _backupService.DeleteScheduledBackup(SelectedSchedule.ScheduleID);
                    
                    if (success)
                    {
                        MessageService.ShowInformation("Backup schedule deleted successfully.", "Delete Complete");
                        LoadScheduledBackups();
                    }
                    else
                    {
                        MessageService.ShowError("Failed to delete backup schedule.", "Delete Error");
                    }
                }
                catch (Exception ex)
                {
                    MessageService.ShowError($"Delete failed: {ex.Message}", "Error");
                }
            }
        }

        private bool CanDeleteSchedule(object parameter)
        {
            return SelectedSchedule != null && !IsEditingSchedule;
        }

        private void EnableSchedule(object parameter)
        {
            if (SelectedSchedule == null)
                return;
                
            try
            {
                bool success = _backupService.EnableScheduledBackup(SelectedSchedule.ScheduleID, true);
                
                if (success)
                {
                    MessageService.ShowInformation("Backup schedule enabled successfully.", "Success");
                    LoadScheduledBackups();
                }
                else
                {
                    MessageService.ShowError("Failed to enable backup schedule.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to enable schedule: {ex.Message}", "Error");
            }
        }

        private bool CanEnableSchedule(object parameter)
        {
            return SelectedSchedule != null && !SelectedSchedule.IsActive && !IsEditingSchedule;
        }

        private void DisableSchedule(object parameter)
        {
            if (SelectedSchedule == null)
                return;
                
            try
            {
                bool success = _backupService.EnableScheduledBackup(SelectedSchedule.ScheduleID, false);
                
                if (success)
                {
                    MessageService.ShowInformation("Backup schedule disabled successfully.", "Success");
                    LoadScheduledBackups();
                }
                else
                {
                    MessageService.ShowError("Failed to disable backup schedule.", "Error");
                }
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to disable schedule: {ex.Message}", "Error");
            }
        }

        private bool CanDisableSchedule(object parameter)
        {
            return SelectedSchedule != null && SelectedSchedule.IsActive && !IsEditingSchedule;
        }

        private void SaveSchedule(object parameter)
        {
            if (SelectedSchedule == null)
                return;
                
            try
            {
                // Validate schedule times based on type
                ValidateSchedule(SelectedSchedule);
                
                if (SelectedSchedule.ScheduleID == 0)
                {
                    // Add new schedule
                    int scheduleId = _backupService.AddScheduledBackup(SelectedSchedule);
                    MessageService.ShowInformation("Backup schedule created successfully.", "Success");
                }
                else
                {
                    // Update existing schedule
                    bool success = _backupService.UpdateScheduledBackup(SelectedSchedule);
                    
                    if (success)
                    {
                        MessageService.ShowInformation("Backup schedule updated successfully.", "Success");
                    }
                    else
                    {
                        MessageService.ShowError("Failed to update backup schedule.", "Error");
                    }
                }
                
                // Exit edit mode and reload
                IsEditingSchedule = false;
                OriginalSchedule = null;
                LoadScheduledBackups();
                CommandManager.InvalidateRequerySuggested();
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to save schedule: {ex.Message}", "Error");
            }
        }

        private bool CanSaveSchedule(object parameter)
        {
            return IsEditingSchedule && SelectedSchedule != null && 
                   !string.IsNullOrWhiteSpace(SelectedSchedule.BackupPath) &&
                   !string.IsNullOrWhiteSpace(SelectedSchedule.ScheduleType) &&
                   (!SelectedSchedule.IsEncrypted || !string.IsNullOrWhiteSpace(SelectedSchedule.EncryptionPassword));
        }

        private void CancelEditSchedule(object parameter)
        {
            if (SelectedSchedule.ScheduleID == 0)
            {
                // Was adding new schedule
                SelectedSchedule = null;
            }
            else if (OriginalSchedule != null)
            {
                // Restore original values
                SelectedSchedule = OriginalSchedule;
            }
            
            IsEditingSchedule = false;
            OriginalSchedule = null;
            CommandManager.InvalidateRequerySuggested();
        }

        #endregion

        #region Helper Methods

        private void LoadData()
        {
            LoadBackupHistory();
            LoadScheduledBackups();
        }

        private void LoadBackupHistory()
        {
            try
            {
                var backups = _backupService.GetBackupHistory();
                BackupHistory = new BindingList<BackupHistory>(backups);
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to load backup history: {ex.Message}", "Error");
            }
        }

        private void LoadScheduledBackups()
        {
            try
            {
                var schedules = _backupService.GetScheduledBackups();
                ScheduledBackups = new BindingList<ScheduledBackup>(schedules);
            }
            catch (Exception ex)
            {
                MessageService.ShowError($"Failed to load scheduled backups: {ex.Message}", "Error");
            }
        }

        private void ValidateSchedule(ScheduledBackup schedule)
        {
            // Make sure appropriate fields are set based on schedule type
            switch (schedule.ScheduleType)
            {
                case "Daily":
                    schedule.DayOfWeek = null;
                    break;
                    
                case "Weekly":
                    if (!schedule.DayOfWeek.HasValue || schedule.DayOfWeek.Value < 0 || schedule.DayOfWeek.Value > 6)
                    {
                        throw new ArgumentException("Please select a valid day of the week for the weekly schedule.");
                    }
                    break;
                    
                default:
                    throw new ArgumentException("Invalid schedule type selected.");
            }
        }

        private ScheduledBackup CloneSchedule(ScheduledBackup schedule)
        {
            return new ScheduledBackup
            {
                ScheduleID = schedule.ScheduleID,
                BackupPath = schedule.BackupPath,
                Description = schedule.Description,
                IncludeAttachments = schedule.IncludeAttachments,
                CompressionLevel = schedule.CompressionLevel,
                IsEncrypted = schedule.IsEncrypted,
                EncryptionPassword = schedule.EncryptionPassword,
                ScheduleType = schedule.ScheduleType,
                ScheduleTime = schedule.ScheduleTime,
                DayOfWeek = schedule.DayOfWeek,
                IsActive = schedule.IsActive,
                CreatedBy = schedule.CreatedBy,
                CreatedByName = schedule.CreatedByName,
                CreatedDate = schedule.CreatedDate,
                ModifiedDate = DateTime.Parse("2025-03-02 16:14:03")
            };
        }

        #endregion
    }
}