using FantasticStock.Common;
using FantasticStock.Services;
using FantasticStock.Views.Financial;
using FantasticStock.Views.Inventory;
using FantasticStock.Views.Sales;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FantasticStock.Views
{
    public partial class MainForm : Form
    {
        private readonly IDatabaseService _databaseService;
        private readonly IAuditService _auditService;

        public MainForm()
        {
            InitializeComponent();

            _databaseService = ServiceLocator.GetService<IDatabaseService>();
            _auditService = ServiceLocator.GetService<IAuditService>();

            

            flowLayoutPanel2.Height = 35;
            flowLayoutPanel3.Height = 35;
            flowLayoutPanel4.Height = 35;
            flowLayoutPanel5.Height = 35;

            InitializeMeunuButton();

            // Update status bar with current user info
            UpdateStatusBar();
        }

        private void InitializeMeunuButton()
        {
            button2.Tag = ModuleType.Products;

            button7.Tag = ModuleType.SalesOrders;

            button17.Tag = ModuleType.UserManagement;
            button18.Tag = ModuleType.SystemConfiguration;
            button19.Tag = ModuleType.BackupRestore;
            button20.Tag = ModuleType.SystemMonitoring;
        }

        public enum ModuleType
        {
            // Inventory module types
            Products,
            Categories,
            Suppliers,
            StockLevels,

            // Sales module types
            SalesOrders,
            Customers,
            Invoices,
            SalesReports,

            // Financial module types
            FinancialDashboard,
            Payments,
            Expenses,
            FinancialReports,

            // Admin module types
            UserManagement,
            SystemConfiguration,
            BackupRestore,
            SystemMonitoring,
        }

        private void menuButton_Click(object sender, EventArgs e)
        {
            var thisButton = sender as Button;
            if (thisButton.Tag != null)
            {
                LoadModule((ModuleType)thisButton.Tag);
            }
            else
            {
                LoadModule(ModuleType.FinancialReports);
            }
        }

        private void headerMenuButton_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;

            if (button != null)
            {
                // Get the parent panel of the button
                Panel parentPanel = button.Parent as Panel;

                if (parentPanel != null)
                {
                    // Resize the height of the parent panel
                    parentPanel.Height = parentPanel.Height == 175 ? 35 : 175; // Set the desired height
                }
            }
        }

        private void LoadModule(ModuleType moduleType)
        {
            // Clear current content
            moduleContentPanel.Controls.Clear();

            // Create and load appropriate module
            Control moduleControl = null;

            switch (moduleType)
            {
                case ModuleType.UserManagement:
                    moduleControl = new UserManagementView();
                    break;

                case ModuleType.SystemConfiguration: 
                    moduleControl = new SystemConfigurationView(); 
                    break;

                case ModuleType.BackupRestore:
                    moduleControl = new SystemConfigurationView();
                    break;

                case ModuleType.SystemMonitoring:
                    moduleControl = new MonitoringView();
                    break;

                // Inventory modules
                case ModuleType.Products:
                    moduleControl = new ProductManagementView();
                    break;

                // Sales modules
                case ModuleType.SalesOrders:
                    moduleControl = new SalesOrderView();
                    break;

                // Financial modules
                case ModuleType.FinancialDashboard:
                    moduleControl = new FinancialDashboardView();
                    break;

                // Future modules - stubs for now
                case ModuleType.Categories:
                case ModuleType.Suppliers:
                case ModuleType.StockLevels:
                case ModuleType.Customers:
                case ModuleType.Invoices:
                case ModuleType.SalesReports:
                case ModuleType.Payments:
                case ModuleType.Expenses:
                case ModuleType.FinancialReports:
                    moduleControl = CreatePlaceholderModule(moduleType.ToString());
                    break;
            }

            if (moduleControl != null)
            {
                moduleControl.Dock = DockStyle.Fill;
                moduleContentPanel.Controls.Add(moduleControl);

                // Log access to the module
                //ServiceLocator.GetService<IAuditService>().LogEvent(
                //    CurrentUser.UserID,
                //    "Access",
                //    "Module",
                //    moduleType.ToString(),
                //    null,
                //    $"User {CurrentUser.Username} accessed {moduleType} module at {DateTime.Parse("2025-03-04 02:19:11").ToString("yyyy-MM-dd HH:mm:ss")}"
                //);
            }
        }

        private UserControl CreatePlaceholderModule(string moduleName)
        {
            Panel panel = new Panel { Dock = DockStyle.Fill };
            Label label = new Label
            {
                Text = $"{moduleName} module is under construction",
                Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
                TextAlign = System.Drawing.ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };
            panel.Controls.Add(label);
            return new UserControl { Controls = { panel } };
        }

        private void UpdateStatusBar()
        {
            // Display user info in status bar
            //lblUserStatus.Text = $"Logged in as: {CurrentUser.DisplayName} ({CurrentUser.RoleName})";
            lblDateTime.Text = DateTime.Parse("2025-03-02 16:16:14").ToString("yyyy-MM-dd HH:mm:ss");
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Log the logout event
            try
            {
                Console.WriteLine("Exited");
                //_auditService.LogEvent(
                //    CurrentUser.UserID,
                //    "UserLogout",
                //    "Users",
                //    CurrentUser.UserID.ToString(),
                //    null,
                //    $"User '{CurrentUser.Username}' logged out at {DateTime.Parse("2025-03-02 16:16:14"):yyyy-MM-dd HH:mm:ss}"
                //);
            }
            catch
            {
                // Ignore errors during shutdown
            }
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(
                "Admin Domain Management System\nVersion 1.0\n\n© 2025 Your Company",
                "About",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var changePasswordForm = new ChangePasswordForm())
            {
                changePasswordForm.ShowDialog(this);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Update the date/time display
            lblDateTime.Text = DateTime.Parse("2025-03-02 16:19:04").ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
