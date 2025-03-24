using System.Collections.Generic;
using FantasticStock.Models;

namespace FantasticStock.Services.Admin
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
        bool ActivateUser(int userId);
        bool ValidateUser(string username, string password);
        List<Role> GetAllRoles();
        Role GetRoleById(int roleId);
    }
}