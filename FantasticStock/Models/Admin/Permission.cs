using System;

namespace FantasticStock.Models
{
    public class Permission
    {
        //public int PermissionID { get; set; }
        //public string PermissionName { get; set; }
        //public string Description { get; set; }
        //public string Module { get; set; }
        //public bool IsAssigned { get; set; }
        public int PermissionID { get; set; }
        public string PermissionName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsAssigned { get; set; }
    }
}