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
                switch (SeverityLevel)
                {
                    case 1:
                        return "Low";
                    case 2:
                        return "Medium";
                    case 3:
                        return "High";
                    case 4:
                        return "Critical";
                    default:
                        return "Unknown";
                }
            }
        }
    }
}