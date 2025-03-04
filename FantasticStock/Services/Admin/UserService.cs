using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using FantasticStock.Models;

namespace FantasticStock.Services
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
                users.Add(MapUser(row));
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
                
            return MapUser(dataTable.Rows[0]);
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
                
            return MapUser(dataTable.Rows[0]);
        }

        public int CreateUser(User user, string password)
        {
            // Generate salt and hash password
            string salt = GenerateSalt();
            string passwordHash = HashPassword(password, salt);

            string query = @"
                INSERT INTO Users (Username, PasswordHash, Salt, DisplayName, Email, Phone, RoleID, 
                                   Status, TwoFactorEnabled, AccountExpiry)
                VALUES (@Username, @PasswordHash, @Salt, @DisplayName, @Email, @Phone, @RoleID,
                        @Status, @TwoFactorEnabled, @AccountExpiry);
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
                new SqlParameter("@AccountExpiry", user.AccountExpiry ?? (object)DBNull.Value)
            };

            var userId = Convert.ToInt32(_databaseService.ExecuteScalar(query, parameters));
            
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
                    AccountExpiry = @AccountExpiry,
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
                new SqlParameter("@AccountExpiry", user.AccountExpiry ?? (object)DBNull.Value)
            };

            int rowsAffected = _databaseService.ExecuteNonQuery(query, parameters);
            
            if (rowsAffected > 0)
            {
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
            // Get existing user data for auditing
            User existingUser = GetUserById(userId);
            if (existingUser == null)
                return false;

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
            // Get existing user data for auditing
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
                // Update last login time
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
                Permissions = GetPermissionsByRoleId(roleId)
            };
            
            return role;
        }

        public List<Permission> GetPermissionsByRoleId(int roleId)
        {
            var permissions = new List<Permission>();
            
            string query = @"
                SELECT p.*, CASE WHEN rp.PermissionID IS NOT NULL THEN 1 ELSE 0 END AS IsAssigned
                FROM Permissions p
                LEFT JOIN RolePermissions rp ON p.PermissionID = rp.PermissionID AND rp.RoleID = @RoleID
                ORDER BY p.Category, p.PermissionName";
            
            var parameter = new SqlParameter("@RoleID", roleId);
            DataTable dataTable = _databaseService.ExecuteQuery(query, parameter);
            
            foreach (DataRow row in dataTable.Rows)
            {
                permissions.Add(new Permission
                {
                    PermissionID = Convert.ToInt32(row["PermissionID"]),
                    PermissionName = row["PermissionName"].ToString(),
                    Category = row["Category"].ToString(),
                    Description = row["Description"].ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    IsAssigned = Convert.ToBoolean(row["IsAssigned"])
                });
            }
            
            return permissions;
        }

        public bool UpdateRolePermissions(int roleId, List<int> permissionIds)
        {
            try
            {
                _databaseService.ExecuteInTransaction((connection, transaction) =>
                {
                    // Delete existing permissions
                    string deleteQuery = "DELETE FROM RolePermissions WHERE RoleID = @RoleID";
                    var deleteCommand = new SqlCommand(deleteQuery, connection, transaction);
                    deleteCommand.Parameters.AddWithValue("@RoleID", roleId);
                    deleteCommand.ExecuteNonQuery();

                    // Insert new permissions
                    if (permissionIds != null && permissionIds.Count > 0)
                    {
                        string insertQuery = "INSERT INTO RolePermissions (RoleID, PermissionID) VALUES (@RoleID, @PermissionID)";
                        foreach (int permissionId in permissionIds)
                        {
                            var insertCommand = new SqlCommand(insertQuery, connection, transaction);
                            insertCommand.Parameters.AddWithValue("@RoleID", roleId);
                            insertCommand.Parameters.AddWithValue("@PermissionID", permissionId);
                            insertCommand.ExecuteNonQuery();
                        }
                    }
                });

                // Log the role permissions update
                _auditService.LogEvent(
                    CurrentUser.UserID,
                    "RolePermissionsUpdate",
                    "RolePermissions",
                    roleId.ToString(),
                    null,
                    Newtonsoft.Json.JsonConvert.SerializeObject(permissionIds)
                );

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Permission> GetAllPermissions()
        {
            var permissions = new List<Permission>();
            
            string query = "SELECT * FROM Permissions ORDER BY Category, PermissionName";
            DataTable dataTable = _databaseService.ExecuteQuery(query);
            
            foreach (DataRow row in dataTable.Rows)
            {
                permissions.Add(new Permission
                {
                    PermissionID = Convert.ToInt32(row["PermissionID"]),
                    PermissionName = row["PermissionName"].ToString(),
                    Category = row["Category"].ToString(),
                    Description = row["Description"].ToString(),
                    CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                    IsAssigned = false
                });
            }
            
            return permissions;
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
                AccountExpiry = row["AccountExpiry"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["AccountExpiry"]) : null,
                LastLogin = row["LastLogin"] != DBNull.Value ? (DateTime?)Convert.ToDateTime(row["LastLogin"]) : null,
                CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                ModifiedDate = Convert.ToDateTime(row["ModifiedDate"])
            };
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