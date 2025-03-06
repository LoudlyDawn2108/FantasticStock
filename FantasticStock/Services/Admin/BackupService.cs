using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.IO.Compression;
using FantasticStock.Common;
using FantasticStock.Models;

namespace FantasticStock.Services
{
    public class BackupService : IBackupService
    {
        private readonly IDatabaseService _databaseService;
        private readonly IAuditService _auditService;
        private static readonly string ConnectionString = "Data Source=.;Initial Catalog=FantasticStock;Integrated Security=True;";

        public BackupService()
        {
            _databaseService = ServiceLocator.GetService<IDatabaseService>();
            _auditService = ServiceLocator.GetService<IAuditService>();
        }

        public List<BackupHistory> GetBackupHistory()
        {
            var backups = new List<BackupHistory>();
            
            string query = @"
                SELECT b.*, u.Username as CreatedByName
                FROM BackupHistory b
                LEFT JOIN Users u ON b.CreatedBy = u.UserID
                ORDER BY b.CreatedDate DESC";
                
            DataTable dataTable = _databaseService.ExecuteQuery(query);
            
            foreach (DataRow row in dataTable.Rows)
            {
                backups.Add(MapBackupHistory(row));
            }
            
            return backups;
        }

        public BackupHistory GetBackupById(int backupId)
        {
            string query = @"
                SELECT b.*, u.Username as CreatedByName
                FROM BackupHistory b
                LEFT JOIN Users u ON b.CreatedBy = u.UserID
                WHERE b.BackupID = @BackupID";
                
            var parameter = new SqlParameter("@BackupID", backupId);
            DataTable dataTable = _databaseService.ExecuteQuery(query, parameter);
            
            if (dataTable.Rows.Count == 0)
                return null;
                
            return MapBackupHistory(dataTable.Rows[0]);
        }

        public int CreateBackup(string backupPath, string description, bool includeAttachments, int compressionLevel, bool encrypt, string password = null)
        {
            DateTime startTime = DateTime.Parse("2025-03-02 15:58:15");
            string backupName = $"Backup_{startTime.ToString("yyyyMMdd_HHmmss")}";
            string backupFile = Path.Combine(backupPath, $"{backupName}.bak");
            
            try
            {
                // Create directory if it doesn't exist
                Directory.CreateDirectory(backupPath);
                
                // Perform the backup
                using (var connection = new SqlConnection(ConnectionString))
                {
                    connection.Open();
                    var command = new SqlCommand($"BACKUP DATABASE FantasticStock TO DISK = '{backupFile}'", connection);
                    command.ExecuteNonQuery();
                }

                // Compress and encrypt if needed
                long backupSize = new FileInfo(backupFile).Length;
                if (compressionLevel > 0 || encrypt)
                {
                    string processedFile = ProcessBackupFile(backupFile, compressionLevel, encrypt, password);
                    File.Delete(backupFile);
                    backupFile = processedFile;
                    backupSize = new FileInfo(backupFile).Length;
                }

                // Calculate duration
                DateTime endTime = DateTime.Now;
                int duration = (int)(endTime - startTime).TotalSeconds;

                // Save backup info to database
                string query = @"
                    INSERT INTO BackupHistory (BackupName, BackupPath, Description, BackupSize, 
                                             Duration, IncludeAttachments, CompressionLevel,
                                             IsEncrypted, CreatedBy, CreatedDate)
                    VALUES (@BackupName, @BackupPath, @Description, @BackupSize, 
                           @Duration, @IncludeAttachments, @CompressionLevel,
                           @IsEncrypted, @CreatedBy, @CreatedDate);
                    SELECT SCOPE_IDENTITY();";

                var parameters = new SqlParameter[]
                {
                                        new SqlParameter("@BackupName", backupName),
                    new SqlParameter("@BackupPath", backupFile),
                    new SqlParameter("@Description", description ?? (object)DBNull.Value),
                    new SqlParameter("@BackupSize", backupSize),
                    new SqlParameter("@Duration", duration),
                    new SqlParameter("@IncludeAttachments", includeAttachments),
                    new SqlParameter("@CompressionLevel", compressionLevel),
                    new SqlParameter("@IsEncrypted", encrypt),
                    new SqlParameter("@CreatedBy", CurrentUser.UserID),
                    new SqlParameter("@CreatedDate", DateTime.Parse("2025-03-02 16:00:28"))
                };

                int backupId = Convert.ToInt32(_databaseService.ExecuteScalar(query, parameters));
                
                // Log the backup creation
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "BackupCreate",
                    "BackupHistory",
                    backupId.ToString(),
                    null,
                    $"Backup created: {backupName}, Size: {backupSize} bytes"
                );
                
                return backupId;
            }
            catch (Exception ex)
            {
                // Log error
                var errorService = ServiceLocator.GetService<IMonitoringService>();
                errorService.LogError("Backup", ex.Message, ex.StackTrace, 3);
                
                // Clean up if backup file was created
                if (File.Exists(backupFile))
                {
                    try { File.Delete(backupFile); } catch { }
                }
                
                throw;
            }
        }

        public bool RestoreFromBackup(int backupId, bool verifyFirst = true)
        {
            BackupHistory backup = GetBackupById(backupId);
            if (backup == null)
                return false;

            try
            {
                string backupFile = backup.BackupPath;
                
                // Check if encrypted or compressed
                if (backup.IsEncrypted || backup.CompressionLevel > 0)
                {
                    // Prompt for password if encrypted
                    string password = null;
                    if (backup.IsEncrypted)
                    {
                        // In a real application, you would prompt the user for the password
                        password = "defaultPassword";
                    }
                    
                    // Decompress and decrypt
                    backupFile = ProcessBackupFileReverse(backup.BackupPath, backup.CompressionLevel, backup.IsEncrypted, password);
                }

                if (verifyFirst && !VerifyBackupIntegrity(backupId))
                {
                    throw new Exception("Backup integrity check failed");
                }

                // Close all existing connections
                using (var connection = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=True;"))
                {
                    connection.Open();
                    var command = new SqlCommand("ALTER DATABASE FantasticStock SET SINGLE_USER WITH ROLLBACK IMMEDIATE", connection);
                    command.ExecuteNonQuery();
                    
                    // Restore database
                    command = new SqlCommand($"RESTORE DATABASE FantasticStock FROM DISK = '{backupFile}' WITH REPLACE", connection);
                    command.ExecuteNonQuery();
                    
                    // Set back to multi user mode
                    command = new SqlCommand("ALTER DATABASE FantasticStock SET MULTI_USER", connection);
                    command.ExecuteNonQuery();
                }

                // Clean up temp file if created
                if (backupFile != backup.BackupPath && File.Exists(backupFile))
                {
                    File.Delete(backupFile);
                }

                // Log the restore
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "BackupRestore",
                    "BackupHistory",
                    backupId.ToString(),
                    null,
                    $"Database restored from backup: {backup.BackupName}"
                );

                return true;
            }
            catch (Exception ex)
            {
                // Log error
                var errorService = ServiceLocator.GetService<IMonitoringService>();
                errorService.LogError("Backup", $"Restore failed: {ex.Message}", ex.StackTrace, 4);
                return false;
            }
        }

        public bool VerifyBackupIntegrity(int backupId)
        {
            BackupHistory backup = GetBackupById(backupId);
            if (backup == null)
                return false;

            try
            {
                string backupFile = backup.BackupPath;
                
                // Check if encrypted or compressed
                if (backup.IsEncrypted || backup.CompressionLevel > 0)
                {
                    // Prompt for password if encrypted
                    string password = null;
                    if (backup.IsEncrypted)
                    {
                        // In a real application, you would prompt the user for the password
                        password = "defaultPassword";
                    }
                    
                    // Decompress and decrypt to temp file
                    backupFile = ProcessBackupFileReverse(backup.BackupPath, backup.CompressionLevel, backup.IsEncrypted, password);
                }

                // Verify database backup
                using (var connection = new SqlConnection("Data Source=.;Initial Catalog=master;Integrated Security=True;"))
                {
                    connection.Open();
                    var command = new SqlCommand($"RESTORE VERIFYONLY FROM DISK = '{backupFile}'", connection);
                    command.ExecuteNonQuery();
                }

                // Clean up temp file if created
                if (backupFile != backup.BackupPath && File.Exists(backupFile))
                {
                    File.Delete(backupFile);
                }

                return true;
            }
            catch (Exception ex)
            {
                // Log error
                var errorService = ServiceLocator.GetService<IMonitoringService>();
                errorService.LogError("Backup", $"Verification failed: {ex.Message}", ex.StackTrace, 3);
                return false;
            }
        }

        public bool DeleteBackup(int backupId)
        {
            // Get backup details first
            BackupHistory backup = GetBackupById(backupId);
            if (backup == null)
                return false;

            try
            {
                // Delete from database first
                string query = "DELETE FROM BackupHistory WHERE BackupID = @BackupID";
                var parameter = new SqlParameter("@BackupID", backupId);
                int rowsDeleted = _databaseService.ExecuteNonQuery(query, parameter);
                
                if (rowsDeleted > 0)
                {
                    // Try to delete the physical file
                    try
                    {
                        if (File.Exists(backup.BackupPath))
                        {
                            File.Delete(backup.BackupPath);
                        }
                    }
                    catch (Exception ex)
                    {
                        // Log but don't fail if file delete fails
                        var errorService = ServiceLocator.GetService<IMonitoringService>();
                        errorService.LogError("Backup", $"Could not delete backup file: {ex.Message}", ex.StackTrace, 2);
                    }
                    
                    // Log the backup deletion
                    _auditService.LogEvent(
                        CurrentUser.UserID,
                        "BackupDelete",
                        "BackupHistory",
                        backupId.ToString(),
                        Newtonsoft.Json.JsonConvert.SerializeObject(backup),
                        null
                    );
                    
                    return true;
                }
                
                return false;
            }
            catch (Exception ex)
            {
                // Log error
                var errorService = ServiceLocator.GetService<IMonitoringService>();
                errorService.LogError("Backup", $"Delete failed: {ex.Message}", ex.StackTrace, 3);
                return false;
            }
        }

        public List<ScheduledBackup> GetScheduledBackups()
        {
            var schedules = new List<ScheduledBackup>();
            
            string query = @"
                SELECT sb.*, u.Username as CreatedByName
                FROM ScheduledBackups sb
                LEFT JOIN Users u ON sb.CreatedBy = u.UserID
                ORDER BY sb.ScheduleType, sb.ScheduleTime";
                
            DataTable dataTable = _databaseService.ExecuteQuery(query);
            
            foreach (DataRow row in dataTable.Rows)
            {
                schedules.Add(new ScheduledBackup
                {
                    ScheduleID = Convert.ToInt32(row["ScheduleID"]),
                    BackupPath = row["BackupPath"].ToString(),
                    Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                    IncludeAttachments = Convert.ToBoolean(row["IncludeAttachments"]),
                    CompressionLevel = Convert.ToInt32(row["CompressionLevel"]),
                    IsEncrypted = Convert.ToBoolean(row["IsEncrypted"]),
                    EncryptionPassword = row["EncryptionPassword"] != DBNull.Value ? row["EncryptionPassword"].ToString() : null,
                    ScheduleType = row["ScheduleType"].ToString(),
                    ScheduleTime = TimeSpan.Parse(row["ScheduleTime"].ToString()),
                    DayOfWeek = row["DayOfWeek"] != DBNull.Value ? (int?)Convert.ToInt32(row["DayOfWeek"]) : null,
                    DayOfMonth = row["DayOfMonth"] != DBNull.Value ? (int?)Convert.ToInt32(row["DayOfMonth"]) : null,
                    IsActive = Convert.ToBoolean(row["IsActive"]),
                    CreatedBy = row["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(row["CreatedBy"]) : null,
                    CreatedByName = row["CreatedByName"] != DBNull.Value ? row["CreatedByName"].ToString() : null,
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    ModifiedDate = Convert.ToDateTime(row["ModifiedDate"])
                });
            }
            
            return schedules;
        }

        public int AddScheduledBackup(ScheduledBackup schedule)
        {
            string query = @"
                INSERT INTO ScheduledBackups (BackupPath, Description, IncludeAttachments,
                                            CompressionLevel, IsEncrypted, EncryptionPassword,
                                            ScheduleType, ScheduleTime, DayOfWeek, DayOfMonth,
                                            IsActive, CreatedBy, CreatedDate, ModifiedDate)
                VALUES (@BackupPath, @Description, @IncludeAttachments,
                        @CompressionLevel, @IsEncrypted, @EncryptionPassword,
                        @ScheduleType, @ScheduleTime, @DayOfWeek, @DayOfMonth,
                        @IsActive, @CreatedBy, @CreatedDate, @ModifiedDate);
                SELECT SCOPE_IDENTITY();";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@BackupPath", schedule.BackupPath),
                new SqlParameter("@Description", schedule.Description ?? (object)DBNull.Value),
                new SqlParameter("@IncludeAttachments", schedule.IncludeAttachments),
                new SqlParameter("@CompressionLevel", schedule.CompressionLevel),
                new SqlParameter("@IsEncrypted", schedule.IsEncrypted),
                new SqlParameter("@EncryptionPassword", schedule.EncryptionPassword ?? (object)DBNull.Value),
                new SqlParameter("@ScheduleType", schedule.ScheduleType),
                new SqlParameter("@ScheduleTime", schedule.ScheduleTime.ToString()),
                new SqlParameter("@DayOfWeek", schedule.DayOfWeek ?? (object)DBNull.Value),
                new SqlParameter("@DayOfMonth", schedule.DayOfMonth ?? (object)DBNull.Value),
                new SqlParameter("@IsActive", schedule.IsActive),
                new SqlParameter("@CreatedBy", CurrentUser.UserID),
                new SqlParameter("@CreatedDate", DateTime.Parse("2025-03-02 16:00:28")),
                new SqlParameter("@ModifiedDate", DateTime.Parse("2025-03-02 16:00:28"))
            };

            int scheduleId = Convert.ToInt32(_databaseService.ExecuteScalar(query, parameters));
            
            // Log the schedule creation
            _auditService.LogEvent(
                CurrentUser.UserID,
                "BackupScheduleCreate",
                "ScheduledBackups",
                scheduleId.ToString(),
                null,
                Newtonsoft.Json.JsonConvert.SerializeObject(new { 
                    Schedule = schedule.ScheduleType, 
                    Time = schedule.ScheduleTime.ToString(),
                    Path = schedule.BackupPath 
                })
            );
            
            return scheduleId;
        }

        public bool UpdateScheduledBackup(ScheduledBackup schedule)
        {
            // Get existing schedule for audit
            var existingSchedule = GetScheduledBackups().FirstOrDefault(s => s.ScheduleID == schedule.ScheduleID);
            if (existingSchedule == null)
                return false;

            string query = @"
                UPDATE ScheduledBackups
                SET BackupPath = @BackupPath,
                    Description = @Description,
                    IncludeAttachments = @IncludeAttachments,
                    CompressionLevel = @CompressionLevel,
                    IsEncrypted = @IsEncrypted,
                    EncryptionPassword = @EncryptionPassword,
                    ScheduleType = @ScheduleType,
                    ScheduleTime = @ScheduleTime,
                    DayOfWeek = @DayOfWeek,
                    DayOfMonth = @DayOfMonth,
                    IsActive = @IsActive,
                    ModifiedDate = @ModifiedDate
                WHERE ScheduleID = @ScheduleID";

                        var parameters = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID", schedule.ScheduleID),
                new SqlParameter("@BackupPath", schedule.BackupPath),
                new SqlParameter("@Description", schedule.Description ?? (object)DBNull.Value),
                new SqlParameter("@IncludeAttachments", schedule.IncludeAttachments),
                new SqlParameter("@CompressionLevel", schedule.CompressionLevel),
                new SqlParameter("@IsEncrypted", schedule.IsEncrypted),
                new SqlParameter("@EncryptionPassword", schedule.EncryptionPassword ?? (object)DBNull.Value),
                new SqlParameter("@ScheduleType", schedule.ScheduleType),
                new SqlParameter("@ScheduleTime", schedule.ScheduleTime.ToString()),
                new SqlParameter("@DayOfWeek", schedule.DayOfWeek ?? (object)DBNull.Value),
                new SqlParameter("@DayOfMonth", schedule.DayOfMonth ?? (object)DBNull.Value),
                new SqlParameter("@IsActive", schedule.IsActive),
                new SqlParameter("@ModifiedDate", DateTime.Parse("2025-03-02 16:02:12"))
            };

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);
            
            if (rowsAffected > 0)
            {
                // Log the schedule update
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "BackupScheduleUpdate",
                    "ScheduledBackups",
                    schedule.ScheduleID.ToString(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(existingSchedule),
                    Newtonsoft.Json.JsonConvert.SerializeObject(schedule)
                );
            }

            return rowsAffected > 0;
        }

        public bool DeleteScheduledBackup(int scheduleId)
        {
            // Get existing schedule for audit
            var existingSchedule = GetScheduledBackups().FirstOrDefault(s => s.ScheduleID == scheduleId);
            if (existingSchedule == null)
                return false;

            string query = "DELETE FROM ScheduledBackups WHERE ScheduleID = @ScheduleID";
            var parameter = new SqlParameter("@ScheduleID", scheduleId);
            
            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameter);
            
            if (rowsAffected > 0)
            {
                // Log the schedule deletion
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "BackupScheduleDelete",
                    "ScheduledBackups",
                    scheduleId.ToString(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(existingSchedule),
                    null
                );
            }

            return rowsAffected > 0;
        }

        public bool EnableScheduledBackup(int scheduleId, bool enable)
        {
            // Get existing schedule for audit
            var existingSchedule = GetScheduledBackups().FirstOrDefault(s => s.ScheduleID == scheduleId);
            if (existingSchedule == null)
                return false;

            string query = @"
                UPDATE ScheduledBackups
                SET IsActive = @IsActive,
                    ModifiedDate = @ModifiedDate
                WHERE ScheduleID = @ScheduleID";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@ScheduleID", scheduleId),
                new SqlParameter("@IsActive", enable),
                new SqlParameter("@ModifiedDate", DateTime.Parse("2025-03-02 16:02:12"))
            };

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);
            
            if (rowsAffected > 0)
            {
                // Log the status change
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    enable ? "BackupScheduleEnable" : "BackupScheduleDisable",
                    "ScheduledBackups",
                    scheduleId.ToString(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(new { existingSchedule.IsActive }),
                    Newtonsoft.Json.JsonConvert.SerializeObject(new { IsActive = enable })
                );
            }

            return rowsAffected > 0;
        }

        private string ProcessBackupFile(string inputFile, int compressionLevel, bool encrypt, string password)
        {
            string outputFile = inputFile;
            
            // Apply compression if needed
            if (compressionLevel > 0)
            {
                string compressedFile = inputFile + ".zip";
                using (var fileStream = new FileStream(compressedFile, FileMode.Create))
                using (var zipArchive = new ZipArchive(fileStream, ZipArchiveMode.Create))
                {
                    zipArchive.CreateEntryFromFile(inputFile, Path.GetFileName(inputFile), 
                        compressionLevel == 1 ? CompressionLevel.Fastest : 
                        compressionLevel == 2 ? CompressionLevel.Optimal : 
                        CompressionLevel.NoCompression);
                }
                outputFile = compressedFile;
            }
            
            // Apply encryption if needed
            if (encrypt && !string.IsNullOrEmpty(password))
            {
                string encryptedFile = outputFile + ".enc";
                EncryptFile(outputFile, encryptedFile, password);
                
                // If we created a compressed file, delete it now
                if (compressionLevel > 0)
                {
                    File.Delete(outputFile);
                }
                outputFile = encryptedFile;
            }
            
            return outputFile;
        }

        private string ProcessBackupFileReverse(string inputFile, int compressionLevel, bool encrypted, string password)
        {
            string outputFile = inputFile;
            string tempPath = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString());
            Directory.CreateDirectory(tempPath);
            
            try
            {
                // Decrypt first if needed
                if (encrypted && !string.IsNullOrEmpty(password))
                {
                    string decryptedFile = Path.Combine(tempPath, Path.GetFileNameWithoutExtension(inputFile));
                    DecryptFile(inputFile, decryptedFile, password);
                    outputFile = decryptedFile;
                }
                
                // Decompress if needed
                if (compressionLevel > 0)
                {
                    string extractedFile = Path.Combine(tempPath, "backup.bak");
                    using (var archive = ZipFile.OpenRead(outputFile))
                    {
                        var entry = archive.Entries.FirstOrDefault();
                        if (entry != null)
                        {
                            entry.ExtractToFile(extractedFile, true);
                            
                            // If we created a decrypted file, delete it now
                            if (encrypted && outputFile != inputFile)
                            {
                                File.Delete(outputFile);
                            }
                            
                            outputFile = extractedFile;
                        }
                    }
                }
                
                return outputFile;
            }
            catch
            {
                // Cleanup on failure
                try { Directory.Delete(tempPath, true); } catch { }
                throw;
            }
        }

        private void EncryptFile(string inputFile, string outputFile, string password)
        {
            byte[] salt = new byte[16];
            new Random().NextBytes(salt);
            
            using (var derivedBytes = new Rfc2898DeriveBytes(password, salt, 1000))
            using (var aes = Aes.Create())
            {
                aes.Key = derivedBytes.GetBytes(32);
                aes.IV = derivedBytes.GetBytes(16);
                
                using (var fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
                using (var fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                {
                    // Write salt
                    fsOutput.Write(salt, 0, salt.Length);
                    
                    using (var cryptoStream = new CryptoStream(fsOutput, aes.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        byte[] buffer = new byte[1024];
                        int read;
                        
                        while ((read = fsInput.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            cryptoStream.Write(buffer, 0, read);
                        }
                    }
                }
            }
        }

        private void DecryptFile(string inputFile, string outputFile, string password)
        {
            byte[] salt = new byte[16];
            
            using (var fsInput = new FileStream(inputFile, FileMode.Open, FileAccess.Read))
            {
                fsInput.Read(salt, 0, salt.Length);
                
                using (var derivedBytes = new Rfc2898DeriveBytes(password, salt, 1000))
                using (var aes = Aes.Create())
                {
                    aes.Key = derivedBytes.GetBytes(32);
                    aes.IV = derivedBytes.GetBytes(16);
                    
                    using (var cryptoStream = new CryptoStream(fsInput, aes.CreateDecryptor(), CryptoStreamMode.Read))
                    using (var fsOutput = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
                    {
                        byte[] buffer = new byte[1024];
                        int read;
                        
                        while ((read = cryptoStream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            fsOutput.Write(buffer, 0, read);
                        }
                    }
                }
            }
        }

        private BackupHistory MapBackupHistory(DataRow row)
        {
            return new BackupHistory
            {
                BackupID = Convert.ToInt32(row["BackupID"]),
                BackupName = row["BackupName"].ToString(),
                BackupPath = row["BackupPath"].ToString(),
                Description = row["Description"] != DBNull.Value ? row["Description"].ToString() : null,
                BackupSize = Convert.ToInt64(row["BackupSize"]),
                Duration = Convert.ToInt32(row["Duration"]),
                IncludeAttachments = Convert.ToBoolean(row["IncludeAttachments"]),
                CompressionLevel = Convert.ToInt32(row["CompressionLevel"]),
                IsEncrypted = Convert.ToBoolean(row["IsEncrypted"]),
                CreatedBy = row["CreatedBy"] != DBNull.Value ? (int?)Convert.ToInt32(row["CreatedBy"]) : null,
                CreatedByName = row["CreatedByName"] != DBNull.Value ? row["CreatedByName"].ToString() : null,
                CreatedDate = Convert.ToDateTime(row["CreatedDate"])
            };
        }
    }
}

public class ScheduledBackup
{
    public int ScheduleID { get; set; }
    public string BackupPath { get; set; }
    public string Description { get; set; }
    public bool IncludeAttachments { get; set; }
    public int CompressionLevel { get; set; }
    public bool IsEncrypted { get; set; }
    public string EncryptionPassword { get; set; }
    public string ScheduleType { get; set; }
    public TimeSpan ScheduleTime { get; set; }
    public int? DayOfWeek { get; set; }
    public int? DayOfMonth { get; set; }
    public bool IsActive { get; set; }
    public int? CreatedBy { get; set; }
    public string CreatedByName { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime ModifiedDate { get; set; }
}