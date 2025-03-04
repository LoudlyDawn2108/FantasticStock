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
                return SeverityLevel switch
                {
                    1 => "Information",
                    2 => "Warning",
                    3 => "Security",
                    _ => "Unknown"
                };
            }
        }
        
        public string FormattedTimestamp => Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
    }
}