using System;
using System.Collections.Generic;

namespace AdminDomain.Models
{
    public class Role
    {
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        public List<Permission> Permissions { get; set; } = new List<Permission>();
    }
}