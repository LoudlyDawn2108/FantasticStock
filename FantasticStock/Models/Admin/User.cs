using System;

namespace AdminDomain.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Username { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string Status { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public DateTime? AccountExpiry { get; set; }
        public DateTime? LastLogin { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}