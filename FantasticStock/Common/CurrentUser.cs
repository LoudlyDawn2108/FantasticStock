using System;

namespace FantasticStock.Common
{
    public static class CurrentUser
    {
        public static int UserID { get; set; }
        public static string Username { get; set; }
        public static string DisplayName { get; set; }
        public static int RoleID { get; set; }
        public static string RoleName { get; set; }
        
        public static void Initialize(int userId, string username, string displayName, int roleId, string roleName)
        {
            UserID = userId;
            Username = username;
            DisplayName = displayName;
            RoleID = roleId;
            RoleName = roleName;
        }
    }
}