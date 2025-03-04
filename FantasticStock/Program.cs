using System;
using System.Windows.Forms;
using FantasticStock.Common;
using FantasticStock.Services;
using FantasticStock.Views;

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
            using (var loginForm = new LoginForm())
            {
                if (loginForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Log application start
                        var auditService = ServiceLocator.GetService<IAuditService>();
                        auditService.LogEvent(
                            CurrentUser.UserID,
                            "AppStart",
                            "Application",
                            "Main",
                            null,
                            $"Application started by {CurrentUser.Username} at {DateTime.Parse("2025-03-02 16:24:02").ToString("yyyy-MM-dd HH:mm:ss")}"
                        );
                        
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
                }
            }
        }
        
        private static void ConfigureServices()
        {
            // Register services with the service locator
            ServiceLocator.Register<IDatabaseService>(new SqlDatabaseService());
            ServiceLocator.Register<IUserService>(new UserService());
            ServiceLocator.Register<IConfigService>(new ConfigService());
            ServiceLocator.Register<IBackupService>(new BackupService());
            ServiceLocator.Register<IAuditService>(new AuditService());
            ServiceLocator.Register<IMonitoringService>(new MonitoringService());
        }
    }
}
