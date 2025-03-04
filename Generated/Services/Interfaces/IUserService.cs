using System.Collections.Generic;
using AdminDomain.Models;

namespace AdminDomain.Services
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        User GetUserById(int userId);
        User GetUserByUsername(string username);
        int CreateUser(User user, string password);
        bool UpdateUser(User user);
        bool UpdateUserPassword(int userId, string newPassword);
        bool DeleteUser(int userId);
        bool DeactivateUser(int userId);
        bool ValidateUser(string username, string password);
        List<Role> GetAllRoles();
        Role GetRoleById(int roleId);
        List<Permission> GetPermissionsByRoleId(int roleId);
        bool UpdateRolePermissions(int roleId, List<int> permissionIds);
        List<Permission> GetAllPermissions();
    }
}