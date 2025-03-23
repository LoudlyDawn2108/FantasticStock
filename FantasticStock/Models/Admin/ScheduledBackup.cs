using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasticStock.Models.Admin
{
    public class ScheduledBackup
    {
        public int ScheduleID { get; set; }
        public string BackupPath { get; set; }
        public string Description { get; set; }
        public bool IncludeAttachments { get; set; }
        public int CompressionLevel { get; set; }
        public bool IsEncrypted { get; set; }
        public string EncryptionPassword { get; set; }
        public string ScheduleType { get; set; } // "Daily", "Weekly", "Monthly"
        public TimeSpan ScheduleTime { get; set; }
        public int? DayOfWeek { get; set; } // 0-6 for Sunday-Saturday
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

    }
}
