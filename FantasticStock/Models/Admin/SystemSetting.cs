using System;

namespace FantasticStock.Models
{
    public class SystemSetting
    {
        public int SettingID { get; set; }
        public string SettingCategory { get; set; }
        public string SettingName { get; set; }
        public string SettingValue { get; set; }
        public string DataType { get; set; }
        public string Description { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
    }
}