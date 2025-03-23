using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using FantasticStock.Common;
using FantasticStock.Models;
using FantasticStock.ViewModels;

namespace FantasticStock.Services.Admin
{
    public class UserService : IUserService
    {
        private readonly IDatabaseService _databaseService;
        private readonly IAuditService _auditService;

        public UserService()
        {
            _databaseService = ServiceLocator.GetService<IDatabaseService>();
            _auditService = ServiceLocator.GetService<IAuditService>();
        }

        public List<User> GetAllUsers()
        {
            var users = new List<User>();

            string query = @"
                SELECT u.*, r.RoleName
                FROM Users u
                INNER JOIN Roles r ON u.RoleID = r.RoleID
                ORDER BY u.Username";

            DataTable dataTable = _databaseService.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                User user = MapUser(row);
                LoadUserAllowedDays(user);
                users.Add(user);
            }

            return users;
        }

        public User GetUserById(int userId)
        {
            string query = @"
                SELECT u.*, r.RoleName
                FROM Users u
                INNER JOIN Roles r ON u.RoleID = r.RoleID
                WHERE u.UserID = @UserID";

            var parameter = new SqlParameter("@UserID", userId);
            DataTable dataTable = _databaseService.ExecuteQuery(query, parameter);

            if (dataTable.Rows.Count == 0)
                return null;

            User user = MapUser(dataTable.Rows[0]);
            LoadUserAllowedDays(user);
            return user;
        }

        public User GetUserByUsername(string username)
        {
            string query = @"
                SELECT u.*, r.RoleName
                FROM Users u
                INNER JOIN Roles r ON u.RoleID = r.RoleID
                WHERE u.Username = @Username";

            var parameter = new SqlParameter("@Username", username);
            DataTable dataTable = _databaseService.ExecuteQuery(query, parameter);

            if (dataTable.Rows.Count == 0)
                return null;

            User user = MapUser(dataTable.Rows[0]);
            LoadUserAllowedDays(user);
            return user;
        }

        public int CreateUser(User user, string password)
        {
            // Generate salt and hash password
            string salt = GenerateSalt();
            string passwordHash = HashPassword(password, salt);

            string query = @"
                INSERT INTO Users (Username, PasswordHash, Salt, DisplayName, Email, Phone, RoleID, 
                                   Status, TwoFactorEnabled)
                VALUES (@Username, @PasswordHash, @Salt, @DisplayName, @Email, @Phone, @RoleID,
                        @Status, @TwoFactorEnabled);
                SELECT SCOPE_IDENTITY();";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Username", user.Username),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@Salt", salt),
                new SqlParameter("@DisplayName", user.DisplayName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Phone", user.Phone ?? (object)DBNull.Value),
                new SqlParameter("@RoleID", user.RoleID),
                new SqlParameter("@Status", user.Status),
                new SqlParameter("@TwoFactorEnabled", user.TwoFactorEnabled),
            };

            int userId = Convert.ToInt32(_databaseService.ExecuteScalar(query, parameters));

            // Save user allowed days if provided
            if (user.AllowedDays != null)
            {
                SaveUserAllowedDays(userId, user.AllowedDays);
            }

            // Log the user creation
            _auditService.LogEvent(
                CurrentUser.UserID,
                "UserCreate",
                "Users",
                userId.ToString(),
                null,
                Newtonsoft.Json.JsonConvert.SerializeObject(user)
            );

            return userId;
        }

        public bool UpdateUser(User user)
        {
            // Get existing user data for auditing
            User existingUser = GetUserById(user.UserID);
            if (existingUser == null)
                return false;

            string query = @"
                UPDATE Users
                SET DisplayName = @DisplayName,
                    Email = @Email,
                    Phone = @Phone,
                    RoleID = @RoleID,
                    Status = @Status,
                    TwoFactorEnabled = @TwoFactorEnabled,
                    ModifiedDate = GETDATE()
                WHERE UserID = @UserID";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", user.UserID),
                new SqlParameter("@DisplayName", user.DisplayName),
                new SqlParameter("@Email", user.Email),
                new SqlParameter("@Phone", user.Phone ?? (object)DBNull.Value),
                new SqlParameter("@RoleID", user.RoleID),
                new SqlParameter("@Status", user.Status),
                new SqlParameter("@TwoFactorEnabled", user.TwoFactorEnabled),
            };

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);

            if (rowsAffected > 0)
            {
                // Update user allowed days if provided
                if (user.AllowedDays != null)
                {
                    SaveUserAllowedDays(user.UserID, user.AllowedDays);
                }

                // Log the user update
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "UserUpdate",
                    "Users",
                    user.UserID.ToString(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(existingUser),
                    Newtonsoft.Json.JsonConvert.SerializeObject(user)
                );
            }

            return rowsAffected > 0;
        }

        public bool UpdateUserPassword(int userId, string newPassword)
        {
            // Generate new salt and hash password
            string salt = GenerateSalt();
            string passwordHash = HashPassword(newPassword, salt);

            string query = @"
                UPDATE Users
                SET PasswordHash = @PasswordHash,
                    Salt = @Salt,
                    ModifiedDate = GETDATE()
                WHERE UserID = @UserID";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@UserID", userId),
                new SqlParameter("@PasswordHash", passwordHash),
                new SqlParameter("@Salt", salt)
            };

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);

            if (rowsAffected > 0)
            {
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "PasswordChange",
                    "Users",
                    userId.ToString(),
                    null,
                    null
                );
            }

            return rowsAffected > 0;
        }

        public bool DeleteUser(int userId)
        {
            User existingUser = GetUserById(userId);
            if (existingUser == null)
                return false;

            string deleteRestrictionsQuery = "DELETE FROM UserScheduleRestrictions WHERE UserID = @UserID";
            _databaseService.ExecuteNonQuery(deleteRestrictionsQuery, new SqlParameter("@UserID", userId));

            string query = "DELETE FROM Users WHERE UserID = @UserID";
            var parameter = new SqlParameter("@UserID", userId);

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameter);

            if (rowsAffected > 0)
            {
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "UserDelete",
                    "Users",
                    userId.ToString(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(existingUser),
                    null
                );
            }

            return rowsAffected > 0;
        }

        public bool DeactivateUser(int userId)
        {
            User existingUser = GetUserById(userId);
            if (existingUser == null)
                return false;

            string query = @"
                UPDATE Users
                SET Status = 'Inactive',
                    ModifiedDate = GETDATE()
                WHERE UserID = @UserID";

            var parameter = new SqlParameter("@UserID", userId);

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameter);

            if (rowsAffected > 0)
            {
                User updatedUser = existingUser.Clone();
                updatedUser.Status = "Inactive";

                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "UserDeactivate",
                    "Users",
                    userId.ToString(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(existingUser),
                    Newtonsoft.Json.JsonConvert.SerializeObject(updatedUser)
                );
            }

            return rowsAffected > 0;
        }

        public bool ActivateUser(int userId)
        {
            User existingUser = GetUserById(userId);
            if (existingUser == null)
                return false;

            string query = @"
                UPDATE Users
                SET Status = 'Active',
                    ModifiedDate = GETDATE()
                WHERE UserID = @UserID";

            var parameter = new SqlParameter("@UserID", userId);

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameter);

            if (rowsAffected > 0)
            {
                User updatedUser = existingUser.Clone();
                updatedUser.Status = "Active";

                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "UserActivate",
                    "Users",
                    userId.ToString(),
                    Newtonsoft.Json.JsonConvert.SerializeObject(existingUser),
                    Newtonsoft.Json.JsonConvert.SerializeObject(updatedUser)
                );
            }

            return rowsAffected > 0;
        }

        public bool ValidateUser(string username, string password)
        {
            string query = "SELECT PasswordHash, Salt FROM Users WHERE Username = @Username AND Status = 'Active'";
            var parameter = new SqlParameter("@Username", username);

            DataTable dataTable = _databaseService.ExecuteQuery(query, parameter);

            if (dataTable.Rows.Count == 0)
                return false;

            string storedHash = dataTable.Rows[0]["PasswordHash"].ToString();
            string salt = dataTable.Rows[0]["Salt"].ToString();

            string calculatedHash = HashPassword(password, salt);

            if (storedHash == calculatedHash)
            {
                query = "UPDATE Users SET LastLogin = GETDATE() WHERE Username = @Username";
                _databaseService.ExecuteNonQuery(query, parameter);

                return true;
            }

            return false;
        }

        public List<Role> GetAllRoles()
        {
            var roles = new List<Role>();

            string query = "SELECT * FROM Roles ORDER BY RoleName";
            DataTable dataTable = _databaseService.ExecuteQuery(query);

            foreach (DataRow row in dataTable.Rows)
            {
                roles.Add(new Role
                {
                    RoleID = Convert.ToInt32(row["RoleID"]),
                    RoleName = row["RoleName"].ToString(),
                    Description = row["Description"].ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    ModifiedDate = Convert.ToDateTime(row["ModifiedDate"])
                });
            }

            return roles;
        }

        public Role GetRoleById(int roleId)
        {
            string query = "SELECT * FROM Roles WHERE RoleID = @RoleID";
            var parameter = new SqlParameter("@RoleID", roleId);

            DataTable dataTable = _databaseService.ExecuteQuery(query, parameter);

            if (dataTable.Rows.Count == 0)
                return null;

            DataRow row = dataTable.Rows[0];

            var role = new Role
            {
                RoleID = Convert.ToInt32(row["RoleID"]),
                RoleName = row["RoleName"].ToString(),
                Description = row["Description"].ToString(),
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]),
            };

            return role;
        }

        private User MapUser(DataRow row)
        {
            return new User
            {
                UserID = Convert.ToInt32(row["UserID"]),
                Username = row["Username"].ToString(),
                DisplayName = row["DisplayName"].ToString(),
                Email = row["Email"].ToString(),
                Phone = row["Phone"] != DBNull.Value ? row["Phone"].ToString() : null,
                RoleID = Convert.ToInt32(row["RoleID"]),
                RoleName = row["RoleName"].ToString(),
                Status = row["Status"].ToString(),
                TwoFactorEnabled = Convert.ToBoolean(row["TwoFactorEnabled"]),
                LastLogin = row["LastLogin"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["LastLogin"]) : null,
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                ModifiedDate = Convert.ToDateTime(row["ModifiedDate"]),
                AllowedDays = new bool[7] { true, true, true, true, true, true, true }
            };
        }

        private void LoadUserAllowedDays(User user)
        {
            if (user == null)
                return;

            user.AllowedDays = new bool[7] { true, true, true, true, true, true, true };

            string query = @"
                SELECT DISTINCT DayOfWeek 
                FROM UserScheduleRestrictions 
                WHERE UserID = @UserID";

            var parameter = new SqlParameter("@UserID", user.UserID);
            DataTable dataTable = _databaseService.ExecuteQuery(query, parameter);

            if (dataTable.Rows.Count == 0)
                return;

            foreach (DataRow row in dataTable.Rows)
            {
                int dayOfWeek = Convert.ToInt32(row["DayOfWeek"]);
                int index = dayOfWeek - 1;
                if (index >= 0 && index < 7)
                {
                    user.AllowedDays[index] = false;
                }
            }
        }

        private void SaveUserAllowedDays(int userId, bool[] allowedDays)
        {
            if (allowedDays == null || allowedDays.Length != 7)
                return;

            string deleteQuery = "DELETE FROM UserScheduleRestrictions WHERE UserID = @UserID";
            _databaseService.ExecuteNonQuery(deleteQuery, new SqlParameter("@UserID", userId));

            for (int i = 0; i < allowedDays.Length; i++)
            {
                if (!allowedDays[i])
                {
                    int dayOfWeek = i + 1;

                    string insertQuery = @"
                        INSERT INTO UserScheduleRestrictions (UserID, DayOfWeek, StartTime, EndTime)
                        VALUES (@UserID, @DayOfWeek, '00:00:00', '23:59:59')";

                    var parameters = new SqlParameter[]
                    {
                        new SqlParameter("@UserID", userId),
                        new SqlParameter("@DayOfWeek", dayOfWeek)
                    };

                    _databaseService.ExecuteNonQuery(insertQuery, parameters);
                }
            }
        }

        private string GenerateSalt()
        {
            byte[] saltBytes = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(saltBytes);
            }
            return Convert.ToBase64String(saltBytes);
        }

        private string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                string saltedPassword = password + salt;
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
                return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
            }
        }
    }
}
