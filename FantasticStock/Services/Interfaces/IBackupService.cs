using System;
using System.Collections.Generic;
using FantasticStock.Models;

namespace FantasticStock.Services
{
    public interface IBackupService
    {
        List<BackupHistory> GetBackupHistory();
        BackupHistory GetBackupById(int backupId);
        int CreateBackup(string backupPath, string description, bool includeAttachments, int compressionLevel, bool encrypt, string password = null);
        bool RestoreFromBackup(int backupId, bool verifyFirst = true);
        bool VerifyBackupIntegrity(int backupId);
        bool DeleteBackup(int backupId);
        List<ScheduledBackup> GetScheduledBackups();
        int AddScheduledBackup(ScheduledBackup schedule);
        bool UpdateScheduledBackup(ScheduledBackup schedule);
        bool DeleteScheduledBackup(int scheduleId);
        bool EnableScheduledBackup(int scheduleId, bool enable);
    }
}