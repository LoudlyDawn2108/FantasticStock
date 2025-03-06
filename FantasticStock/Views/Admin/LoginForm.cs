using FantasticStock.Common;
using FantasticStock.Models;
using FantasticStock.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views
{
    public partial class LoginForm : Form
    {
        private readonly IUserService _userService;

        public LoginForm()
        {
            InitializeComponent();
            _userService = ServiceLocator.GetService<IUserService>();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Login Error",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                //bool isValid = _userService.ValidateUser(username, password);
                bool isValid = false;

                if ((username == "user" || username == "admin") && password == "123")
                {
                    isValid = true;
                }

                if (isValid)
                {
                    // Get user details for current session
                    //var user = _userService.GetUserByUsername(username);
                    var user = new User
                    {
                        UserID = 1,
                        Username = username,
                        DisplayName = username,
                        RoleID = 1,
                        RoleName = username,
                    };

                    // Set current user information
                    CurrentUser.Initialize(
                        user.UserID,
                        user.Username,
                        user.DisplayName,
                        user.RoleID,
                        user.RoleName);

                    // Log the login event
                    //var auditService = ServiceLocator.GetService<IAuditService>();
                    //auditService.LogEvent(
                    //    user.UserID,
                    //    "UserLogin",
                    //    "Users",
                    //    user.UserID.ToString(),
                    //    null,
                    //    $"User '{username}' logged in at {DateTime.Parse("2025-03-02 16:16:14").ToString("yyyy-MM-dd HH:mm:ss")}"
                    //);

                    DialogResult = DialogResult.OK;
                    Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password.\nPlease try again.", "Login Failed",
                                   MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txtPassword.Clear();
                    txtPassword.Focus();

                    // Log failed login attempt
                    //var monitoringService = ServiceLocator.GetService<IMonitoringService>();
                    //monitoringService.LogError(
                    //    "Security",
                    //    $"Failed login attempt for user '{username}'",
                    //    null,
                    //    2);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred during login: {ex.Message}", "Error",
                               MessageBoxButtons.OK, MessageBoxIcon.Error);

                // Log the error
                var monitoringService = ServiceLocator.GetService<IMonitoringService>();
                monitoringService.LogError("Security", $"Login error: {ex.Message}", ex.StackTrace, 3);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
