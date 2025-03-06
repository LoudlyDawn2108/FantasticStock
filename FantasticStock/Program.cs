using System;
using System.Windows.Forms;
using FantasticStock.Common;
using FantasticStock.Models;
using FantasticStock.Services;
using FantasticStock.Views;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace FantasticStock
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Initialize service locator
            ConfigureServices();
            
            // Show login form
            //using (var loginForm = new LoginForm())
            //{
            //    if (loginForm.ShowDialog() == DialogResult.OK)
            //    {
                    try
                    {
                // Log application start
                //var auditService = ServiceLocator.GetService<IAuditService>();
                //auditService.LogEvent(
                //    CurrentUser.UserID,
                //    "AppStart",
                //    "Application",
                //    "Main",
                //    null,
                //    $"Application started by {CurrentUser.Username} at {DateTime.Parse("2025-03-02 16:24:02").ToString("yyyy-MM-dd HH:mm:ss")}"
                //);
                var username = "admin";
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

                // Start the main application
                Application.Run(new MainForm());
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}", "Application Error",
                                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                                       
                        // Try to log the error
                        try
                        {
                            var monitoringService = ServiceLocator.GetService<IMonitoringService>();
                            monitoringService.LogError("Application", $"Application error: {ex.Message}", ex.StackTrace, 4);
                        }
                        catch
                        {
                            // Ignore logging errors on startup
                        }
                    }
                //}
            //}
        }
        
        private static void ConfigureServices()
        {
            // Register services with the service locator
            ServiceLocator.Register<IDatabaseService>(new SqlDatabaseService());
            ServiceLocator.Register<IAuditService>(new AuditService());
            ServiceLocator.Register<IUserService>(new UserService());
            ServiceLocator.Register<IConfigService>(new ConfigService());
            ServiceLocator.Register<IBackupService>(new BackupService());
            ServiceLocator.Register<IMonitoringService>(new MonitoringService());
        }
    }
}
