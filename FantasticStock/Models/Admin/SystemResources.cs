using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FantasticStock.Models.Admin
{
    public class SystemResources
    {
        public int CpuUsage { get; set; }
        public int MemoryUsage { get; set; }
        public int DiskUsage { get; set; }
        public string DatabaseSize { get; set; }
        public int ActiveUsers { get; set; }
        public DateTime? LastBackupDate { get; set; }
        public string Uptime { get; set; }

        public string LastBackupFormatted
        {
            get
            {
                if (LastBackupDate.HasValue)
                {
                    return LastBackupDate.Value.ToString("yyyy-MM-dd HH:mm:ss");
                }
                return "Never";
            }
        }
    }
}
