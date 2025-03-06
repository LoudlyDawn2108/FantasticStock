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
        public int? DayOfMonth { get; set; } // 1-31
        public bool IsActive { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public string CompressionLevelDisplay
        {
            get
            {
                switch (CompressionLevel)
                {
                    case 0:
                        return "None";
                    case 1:
                        return "Normal";
                    case 2:
                        return "High";
                    default:
                        return "Unknown";
                }
            }
        }

        public string ScheduleDisplay
        {
            get
            {
                string timeFormat = ScheduleTime.ToString(@"hh\:mm");

                switch (ScheduleType)
                {
                    case "Daily":
                        return $"Daily at {timeFormat}";
                    case "Weekly":
                        return $"Weekly on {GetDayOfWeekName()} at {timeFormat}";
                    case "Monthly":
                        return $"Monthly on day {DayOfMonth} at {timeFormat}";
                    default:
                        return "Invalid schedule";
                }
            }
        }

        private string GetDayOfWeekName()
        {
            if (!DayOfWeek.HasValue) return "?";

            switch (DayOfWeek.Value)
            {
                case 0:
                    return "Sunday";
                case 1:
                    return "Monday";
                case 2:
                    return "Tuesday";
                case 3:
                    return "Wednesday";
                case 4:
                    return "Thursday";
                case 5:
                    return "Friday";
                case 6:
                    return "Saturday";
                default:
                    return "Invalid day";
            }
        }
    }
}
