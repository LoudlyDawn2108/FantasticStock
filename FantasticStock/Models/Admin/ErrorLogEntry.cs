using System;

namespace FantasticStock.Models
{
    public class ErrorLogEntry
    {
        public int ErrorID { get; set; }
        public string ErrorModule { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public int SeverityLevel { get; set; }
        public int? UserID { get; set; }
        public string Username { get; set; }
        public string IPAddress { get; set; }
        public DateTime Timestamp { get; set; }
        
        public string SeverityText
        {
            get
            {
                return SeverityLevel switch
                {
                    1 => "Low",
                    2 => "Medium",
                    3 => "High",
                    4 => "Critical",
                    _ => "Unknown"
                };
            }
        }
    }
}