using System;

namespace FantasticStock.Models
{
    public class BackupHistory
    {
        public int BackupID { get; set; }
        public string BackupName { get; set; }
        public string BackupPath { get; set; }
        public string Description { get; set; }
        public long BackupSize { get; set; }
        public int Duration { get; set; }
        public bool IncludeAttachments { get; set; }
        public int CompressionLevel { get; set; }
        public bool IsEncrypted { get; set; }
        public int? CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        
        public string FormattedSize
        {
            get
            {
                string[] sizes = { "B", "KB", "MB", "GB" };
                double len = BackupSize;
                int order = 0;
                while (len >= 1024 && order < sizes.Length - 1)
                {
                    order++;
                    len = len / 1024;
                }
                return $"{len:0.##} {sizes[order]}";
            }
        }
        
        public string DurationDisplay
        {
            get
            {
                TimeSpan time = TimeSpan.FromSeconds(Duration);
                return time.TotalMinutes < 1 ? 
                    $"{time.Seconds} sec" : 
                    $"{(int)time.TotalMinutes} min {time.Seconds} sec";
            }
        }
        
        public string CompressionLevelDisplay
        {
            get
            {
                return CompressionLevel switch
                {
                    0 => "None",
                    1 => "Normal",
                    2 => "High",
                    _ => "Unknown"
                };
            }
        }
        
        public string CreatedDateDisplay => CreatedDate.ToString("yyyy-MM-dd HH:mm:ss");
    }
}