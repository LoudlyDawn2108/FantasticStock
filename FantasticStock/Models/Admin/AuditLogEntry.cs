using System;

namespace FantasticStock.Models
{
    public class AuditLogEntry
    {
        public int AuditID { get; set; }
        public int? UserID { get; set; }
        public string Username { get; set; }
        public string EventType { get; set; }
        public string TableName { get; set; }
        public string RecordID { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
        public string IPAddress { get; set; }
        public int SeverityLevel { get; set; }
        public DateTime Timestamp { get; set; }

        public string SeverityText
        {
            get
            {
                switch (SeverityLevel)
                {
                    case 1:
                        return "Information";
                    case 2:
                        return "Warning";
                    case 3:
                        return "Security";
                    default:
                        return "Unknown";
                }
            }
        }

        public string FormattedTimestamp => Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
    }
}