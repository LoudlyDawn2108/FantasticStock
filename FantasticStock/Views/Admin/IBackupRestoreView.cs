using FantasticStock.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using FantasticStock.Models.Admin;

namespace FantasticStock.Views.Admin
{
    interface IBackupRestoreView
    {
        // Backup View Properties
        string BackupLocation { get; set; }
        string BackupDescription { get; set; }
        bool IncludeAttachments { get; set; }
        int CompressionLevel { get; }
        bool EncryptBackup { get; set; }
        string EncryptionPassword { get; set; }

        // Restore View Properties
        string RestoreFilePath { get; set; }
        DateTime RestorePoint { get; set; }
        bool OverwriteExistingData { get; }
        bool VerifyBeforeRestore { get; set; }

        // Schedule View Properties
        string SelectedScheduleType { get; set; }
        DayOfWeek SelectedDayOfWeek { get; set; }
        TimeSpan ScheduleTime { get; set; }

        // Data Binding Properties
        BindingSource BackupHistoryBindingSource { get; }
        BindingSource ScheduledBackupsBindingSource { get; }

        // Selected Items
        BackupHistory SelectedBackupHistory { get; }
        ScheduledBackup SelectedScheduledBackup { get; }

        // Events
        event EventHandler BackupNowClicked;
        event EventHandler VerifyBackupClicked;
        event EventHandler RestoreBackupClicked;
        event EventHandler DeleteBackupClicked;
        event EventHandler ExecuteRestoreClicked;
        event EventHandler BrowseBackupLocationClicked;
        event EventHandler BrowseRestoreFileClicked;
        event EventHandler AddScheduleClicked;
        event EventHandler EditScheduleClicked;
        event EventHandler DeleteScheduleClicked;
        event EventHandler EnableScheduleClicked;
        event EventHandler DisableScheduleClicked;
        event EventHandler SaveScheduleClicked;
        event EventHandler CancelScheduleEditClicked;

        // UI Methods
        void ShowMessage(string message, string title, MessageType type);
        bool ConfirmAction(string message, string title);
        void ShowProgressDialog(string operation, int progress);
        void UpdateBackupHistoryList(List<BackupHistory> backupHistories);
        void UpdateScheduledBackupsList(List<ScheduledBackup> scheduledBackups);
        void EnableScheduleEditingControls(bool enabled);
        void PopulateScheduleEditingControls(ScheduledBackup schedule);
        void ClearScheduleEditingControls();
        void SelectBackupHistoryItem(int backupId);
        void SelectScheduledBackupItem(int scheduleId);
    }

    public enum MessageType
    {
        Information,
        Warning,
        Error,
    }
}
