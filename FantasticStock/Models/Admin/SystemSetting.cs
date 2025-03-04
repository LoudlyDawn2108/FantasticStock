using System;

namespace FantasticStock.Models
{
    public class SystemSetting
    {
        public int SettingID { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string DataType { get; set; }
        public bool IsEditable { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}